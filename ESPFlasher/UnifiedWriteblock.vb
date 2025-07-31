Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Enum BlockType
    Sensor
    Display
    Template
    BinarySensor
    TextSensor
    Switch
End Enum

Module UnifiedWriteBlock

    ' ========================================
    ' ERWEITERTE WRITEBLOCK MIT SENSOR-TYP-UNTERSCHEIDUNG
    ' ========================================
    Public Sub WriteUniversalBlockWithSensorType(sb As StringBuilder, sensorClass As String, platform As String,
                                                 paramString As String, filterOrLambdaContent As String, Optional lambdaCellContent As String = "")



        ' Bestimme BlockType basierend auf sensorClass
        Dim blockType As BlockType
        Select Case sensorClass.ToLower()
            Case "binary_sensor"
                blockType = BlockType.BinarySensor
            Case "text_sensor"
                blockType = BlockType.TextSensor
            Case "switch"
                blockType = BlockType.Switch
            Case "template"
                blockType = BlockType.Template
            Case Else
                blockType = BlockType.Sensor ' Standard
        End Select



        ' Verwende die normale WriteUniversalBlock
        WriteUniversalBlock(sb, blockType, platform, paramString, filterOrLambdaContent, lambdaCellContent)
    End Sub

    ' ========================================
    ' HAUPTFUNKTION - UNIVERSELLE WRITEBLOCK
    ' ========================================
    Public Sub WriteUniversalBlock(sb As StringBuilder, blockType As BlockType, platform As String,
                               paramString As String, filterContent As String, Optional lambdaCellContent As String = "")



        sb.AppendLine($"  - platform: {platform}")

        ' ROBUSTES Parameter-Parsing (einheitlich für alle)
        Dim flatDict = ParseParameterString(paramString)

        ' Block-spezifische Verarbeitung
        Select Case blockType
            Case BlockType.Sensor, BlockType.BinarySensor

                WriteSensorSpecificFields(sb, flatDict, filterContent)

            Case BlockType.Display

                ' Für Displays: Lambda aus separater Spalte verwenden
                Dim displayLambda = If(String.IsNullOrEmpty(lambdaCellContent), "", lambdaCellContent)
                WriteDisplaySpecificFields(sb, flatDict, displayLambda)

            Case BlockType.Template, BlockType.TextSensor, BlockType.Switch

                Dim templateLambda As String = ""

                ' Priorität: lambdaCellContent (aus Lambda-Spalte) vor filterContent
                If Not String.IsNullOrEmpty(lambdaCellContent) Then
                    templateLambda = lambdaCellContent

                ElseIf Not String.IsNullOrEmpty(filterContent) Then
                    templateLambda = filterContent

                Else

                End If


                WriteTemplateSpecificFields(sb, flatDict, templateLambda)

            Case Else
                MsgBox($"UNBEKANNTER BlockType: {blockType}")
        End Select

        sb.AppendLine()
    End Sub

    ' ========================================
    ' PARAMETER-PARSING (ROBUST)
    ' ========================================
    Private Function ParseParameterString(paramString As String) As Dictionary(Of String, String)
        Dim flatDict As New Dictionary(Of String, String)

        If String.IsNullOrWhiteSpace(paramString) Then Return flatDict

        ' Robustes manuelles Parsing für komplexe Parameter
        Dim i As Integer = 0
        While i < paramString.Length
            ' Finde nächsten Key
            Dim keyStart As Integer = i
            While i < paramString.Length AndAlso paramString(i) <> "="c
                i += 1
            End While

            If i >= paramString.Length Then Exit While

            Dim key As String = paramString.Substring(keyStart, i - keyStart).Trim()
            i += 1 ' Skip "="

            ' Finde Wert (bis zum nächsten key=value oder Ende)
            Dim valueStart As Integer = i
            Dim foundNextKey As Boolean = False

            While i < paramString.Length AndAlso Not foundNextKey
                If paramString(i) = ","c Then
                    ' Prüfe ob nach dem Komma ein neuer Key kommt
                    Dim j As Integer = i + 1
                    Dim nextKeyCandidate As String = ""
                    While j < paramString.Length AndAlso paramString(j) <> "="c AndAlso paramString(j) <> ","c
                        nextKeyCandidate += paramString(j)
                        j += 1
                    End While

                    If j < paramString.Length AndAlso paramString(j) = "="c Then
                        foundNextKey = True
                    Else
                        i += 1
                    End If
                Else
                    i += 1
                End If
            End While

            Dim value As String = paramString.Substring(valueStart, i - valueStart).Trim()
            If value.EndsWith(",") Then value = value.Substring(0, value.Length - 1).Trim()

            flatDict(key) = value

            If i < paramString.Length AndAlso paramString(i) = ","c Then i += 1
        End While

        Return flatDict
    End Function

    ' ========================================
    ' SENSOR-SPEZIFISCHE VERARBEITUNG
    ' ========================================
    Private Sub WriteSensorSpecificFields(sb As StringBuilder, flatDict As Dictionary(Of String, String),
                                         filterContent As String)

        Dim nestedDict = ProcessNestedFields(flatDict, GetSensorFlatKeys())

        ' Sensor-spezifische Behandlungen
        ProcessSensorSpecialFields(sb, flatDict, nestedDict)

        ' Standard-Felder schreiben
        WriteStandardFields(sb, flatDict, GetSensorUnitMappings())

        ' Nested-Felder schreiben
        WriteNestedFields(sb, nestedDict)

        ' Filter schreiben
        If Not String.IsNullOrWhiteSpace(filterContent) Then
            WriteFilterBlock(sb, filterContent)
        End If
    End Sub

    Private Sub ProcessSensorSpecialFields(sb As StringBuilder, flatDict As Dictionary(Of String, String),
                                          nestedDict As Dictionary(Of String, Dictionary(Of String, String)))

        ' Count Mode behandeln
        If nestedDict.ContainsKey("count_mode_rising") OrElse nestedDict.ContainsKey("count_mode_falling") Then
            sb.AppendLine("    count_mode:")
            If nestedDict.ContainsKey("count_mode_rising") Then
                For Each inner In nestedDict("count_mode_rising")
                    sb.AppendLine($"      rising_edge: {inner.Value}")
                Next
                nestedDict.Remove("count_mode_rising")
            End If
            If nestedDict.ContainsKey("count_mode_falling") Then
                For Each inner In nestedDict("count_mode_falling")
                    sb.AppendLine($"      falling_edge: {inner.Value}")
                Next
                nestedDict.Remove("count_mode_falling")
            End If
        End If
    End Sub

    ' ========================================
    ' DISPLAY-SPEZIFISCHE VERARBEITUNG
    ' ========================================
    Private Sub WriteDisplaySpecificFields(sb As StringBuilder, flatDict As Dictionary(Of String, String),
                                          lambdaContent As String)

        Dim nestedDict = ProcessNestedFields(flatDict, GetDisplayFlatKeys())

        ' Display-spezifische Behandlungen
        ProcessDisplaySpecialFields(sb, flatDict, nestedDict)

        ' Standard-Felder schreiben
        WriteStandardFields(sb, flatDict, GetDisplayUnitMappings())

        ' Nested-Felder schreiben
        WriteNestedFields(sb, nestedDict)

        ' Lambda schreiben (aus separater Spalte)
        If Not String.IsNullOrWhiteSpace(lambdaContent) Then
            WriteLambdaBlock(sb, lambdaContent)
        End If
    End Sub

    Private Sub ProcessDisplaySpecialFields(sb As StringBuilder, flatDict As Dictionary(Of String, String),
                                           nestedDict As Dictionary(Of String, Dictionary(Of String, String)))

        ' Dimensions behandeln
        If flatDict.ContainsKey("dimensions") Then
            Dim dimValue = flatDict("dimensions")
            If dimValue.Contains("x") Then
                Dim parts = dimValue.Split("x"c)
                If parts.Length = 2 Then
                    sb.AppendLine($"    dimensions: {parts(0).Trim()}x{parts(1).Trim()}")
                Else
                    sb.AppendLine($"    dimensions: {dimValue}")
                End If
            Else
                sb.AppendLine($"    dimensions: {dimValue}")
            End If
            flatDict.Remove("dimensions")
        End If

        ' Color Correct
        If flatDict.ContainsKey("color_correct") Then
            sb.AppendLine($"    color_correct: [{flatDict("color_correct")}]")
            flatDict.Remove("color_correct")
        End If

        ' Rotation (ohne °)
        If flatDict.ContainsKey("rotation") Then
            Dim rotValue = flatDict("rotation").Replace("°", "")
            sb.AppendLine($"    rotation: {rotValue}")
            flatDict.Remove("rotation")
        End If
    End Sub

    ' ========================================
    ' TEMPLATE-SPEZIFISCHE VERARBEITUNG
    ' ========================================
    ' ========================================
    ' TEMPLATE-SPEZIFISCHE VERARBEITUNG (GEFIXT)
    ' ========================================
    Private Sub WriteTemplateSpecificFields(sb As StringBuilder, flatDict As Dictionary(Of String, String),
                                       lambdaContent As String)



        Dim templateFields = GetTemplateSpecificFields()

        For Each kvp In flatDict.ToList()
            Dim key = kvp.Key.ToLower()
            Dim val = kvp.Value

            If templateFields.Contains(key) Then
                Select Case key
                    Case "name"
                        sb.AppendLine($"    name: ""{val}""")
                    Case "unit_of_measurement"
                        If Not String.IsNullOrEmpty(val) Then
                            val = FixUnitOfMeasurement(val)
                            sb.AppendLine($"    unit_of_measurement: ""{val}""")
                        End If
                    Case "device_class", "state_class"
                        If Not String.IsNullOrEmpty(val) Then
                            sb.AppendLine($"    {key}: {val}")
                        End If
                    Case "accuracy_decimals"
                        If IsNumeric(val) Then
                            sb.AppendLine($"    accuracy_decimals: {val}")
                        End If
                    Case "icon"
                        If Not String.IsNullOrEmpty(val) Then
                            sb.AppendLine($"    icon: ""{val}""")
                        End If
                    Case "update_interval"
                        If Not String.IsNullOrEmpty(val) Then
                            If IsNumeric(val) Then
                                sb.AppendLine($"    update_interval: {val}s")
                            Else
                                sb.AppendLine($"    update_interval: {val}")
                            End If
                        End If
                    Case "optimistic", "assumed_state", "disabled_by_default"
                        If val.ToLower() = "true" Then
                            sb.AppendLine($"    {key}: true")
                        ElseIf val.ToLower() = "false" Then
                            sb.AppendLine($"    {key}: false")
                        End If
                    Case "turn_on_action", "turn_off_action"
                        WriteActionField(sb, key, val)
                End Select

                flatDict.Remove(kvp.Key)
            End If
        Next

        If Not String.IsNullOrWhiteSpace(lambdaContent) Then
            WriteLambdaBlock(sb, lambdaContent)
        End If



    End Sub

    ' ========================================
    ' HILFSFUNKTIONEN - KONFIGURATION
    ' ========================================
    Private Function GetSensorFlatKeys() As HashSet(Of String)
        Return New HashSet(Of String) From {
            "update_interval", "timeout", "accuracy_decimals", "echo_pin", "trigger_pin", "pulse_time",
            "shunt_resistance", "max_voltage", "max_current", "bus_voltage", "shunt_voltage",
            "cs_pin", "internal_filter", "unit_of_measurement", "device_class", "pin", "name", "state_class"
        }
    End Function

    Private Function GetDisplayFlatKeys() As HashSet(Of String)
        Return New HashSet(Of String) From {
            "address", "reset_pin", "cs_pin", "dc_pin", "busy_pin", "led_pin",
            "model", "rotation", "contrast", "intensity", "measurement_duration",
            "clk_pin", "dio_pin", "pin", "chipset", "num_leds", "rgb_order",
            "max_refresh_rate", "color_correct", "dimensions"
        }
    End Function

    Private Function GetTemplateSpecificFields() As HashSet(Of String)
        Return New HashSet(Of String) From {
            "name", "unit_of_measurement", "device_class", "state_class", "accuracy_decimals",
            "icon", "update_interval", "optimistic", "assumed_state", "disabled_by_default",
            "turn_on_action", "turn_off_action"
        }
    End Function

    Private Function GetSensorUnitMappings() As Dictionary(Of String, String)
        Return New Dictionary(Of String, String) From {
            {"update_interval", "s"}, {"timeout", "s"}, {"pulse_time", "ms"},
            {"max_voltage", "V"}, {"max_current", "A"}, {"shunt_resistance", " ohm"},
            {"internal_filter", "us"}
        }
    End Function

    Private Function GetDisplayUnitMappings() As Dictionary(Of String, String)
        Return New Dictionary(Of String, String) From {
            {"contrast", "%"}, {"intensity", "%"}, {"measurement_duration", "ms"}
        }
    End Function

    ' ========================================
    ' HILFSFUNKTIONEN - VERARBEITUNG
    ' ========================================
    Private Function ProcessNestedFields(flatDict As Dictionary(Of String, String), flatKeys As HashSet(Of String)) As Dictionary(Of String, Dictionary(Of String, String))
        Dim nestedDict As New Dictionary(Of String, Dictionary(Of String, String))

        ' Accuracy-Fixups
        Dim accuracyFixups = New Dictionary(Of String, Tuple(Of String, String)) From {
            {"temperature_accuracy_decimals", Tuple.Create("temperature", "accuracy_decimals")},
            {"humidity_accuracy_decimals", Tuple.Create("humidity", "accuracy_decimals")},
            {"pressure_accuracy_decimals", Tuple.Create("pressure", "accuracy_decimals")}
        }

        For Each kvp In flatDict.ToList()
            Dim keyLower = kvp.Key.ToLower()

            ' Accuracy Fixups behandeln
            If accuracyFixups.ContainsKey(keyLower) Then
                Dim target = accuracyFixups(keyLower)
                If Not nestedDict.ContainsKey(target.Item1) Then nestedDict(target.Item1) = New Dictionary(Of String, String)
                nestedDict(target.Item1)(target.Item2) = kvp.Value
                flatDict.Remove(kvp.Key)
                Continue For
            End If

            ' Nested vs. Flat entscheiden
            If kvp.Key.Contains("_") AndAlso Not flatKeys.Contains(keyLower) Then
                Dim parts = kvp.Key.Split("_"c)
                If parts.Length >= 2 Then
                    Dim group = String.Join("_", parts.Take(parts.Length - 1)).Trim()
                    Dim subkey = parts.Last().Trim()
                    If Not nestedDict.ContainsKey(group) Then nestedDict(group) = New Dictionary(Of String, String)
                    nestedDict(group)(subkey) = kvp.Value
                    flatDict.Remove(kvp.Key)
                End If
            End If
        Next

        Return nestedDict
    End Function

    Private Sub WriteStandardFields(sb As StringBuilder, flatDict As Dictionary(Of String, String), unitMappings As Dictionary(Of String, String))
        For Each kvp In flatDict.ToList()
            Dim key = kvp.Key
            Dim val = kvp.Value

            ' Unit-Mapping anwenden
            If unitMappings.ContainsKey(key.ToLower()) Then
                sb.AppendLine($"    {key}: {val}{unitMappings(key.ToLower())}")
            ElseIf key.ToLower() = "model" OrElse key.ToLower() = "chipset" OrElse key.ToLower() = "rgb_order" Then
                sb.AppendLine($"    {key}: ""{val}""")
            Else
                sb.AppendLine($"    {key}: {val}")
            End If
        Next
    End Sub

    Private Sub WriteNestedFields(sb As StringBuilder, nestedDict As Dictionary(Of String, Dictionary(Of String, String)))
        For Each outer In nestedDict
            sb.AppendLine($"    {outer.Key}:")
            For Each inner In outer.Value
                sb.AppendLine($"      {inner.Key}: {inner.Value}")
            Next
        Next
    End Sub

    Private Sub WriteFilterBlock(sb As StringBuilder, filterContent As String)
        Try
            Dim filterData As JObject = JObject.Parse(filterContent)

            If filterData.ContainsKey("global") Then
                sb.AppendLine("    filters:")
                For Each f In CType(filterData("global"), JObject).Properties()
                    Dim val = f.Value.ToString().Trim()
                    If val.Contains(Environment.NewLine) Then
                        sb.AppendLine($"      - {f.Name}:")
                        For Each line In val.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                            sb.AppendLine($"          {line.Trim()}")
                        Next
                    Else
                        sb.AppendLine($"      - {f.Name}: {val}")
                    End If
                Next
            End If
        Catch
            ' Fallback: Als einfachen Text behandeln
            If Not String.IsNullOrWhiteSpace(filterContent) Then
                sb.AppendLine("    filters:")
                sb.AppendLine($"      - {filterContent}")
            End If
        End Try
    End Sub

    ' ========================================
    ' LAMBDA-VALIDIERUNG UND -SCHREIBUNG
    ' ========================================
    Private Sub WriteLambdaBlock(sb As StringBuilder, lambdaContent As String)

        ' Validate lambda syntax before writing
        Dim validationResult = ValidateLambdaSyntax(lambdaContent)



        If validationResult.Errors.Any() Then

            sb.AppendLine("    # LAMBDA ERRORS - WILL NOT COMPILE:")
            For Each errorMsg In validationResult.Errors
                sb.AppendLine($"    # ERROR: {errorMsg}")
            Next
            sb.AppendLine("    # lambda: |- # DISABLED DUE TO ERRORS")
            Return
        End If

        sb.AppendLine("    lambda: |-")
        For Each line In lambdaContent.Split({vbCrLf, vbLf, Environment.NewLine}, StringSplitOptions.None)
            If String.IsNullOrWhiteSpace(line) Then
                sb.AppendLine()
            Else
                sb.AppendLine($"      {line.TrimEnd()}")
            End If
        Next

    End Sub

    Private Structure ValidationResult
        Public Errors As List(Of String)
        Public Warnings As List(Of String)

        Public Sub New(errors As List(Of String), warnings As List(Of String))
            Me.Errors = errors
            Me.Warnings = warnings
        End Sub
    End Structure

    Private Function ValidateLambdaSyntax(content As String) As ValidationResult
        Dim errors As New List(Of String)
        Dim warnings As New List(Of String)
        Dim lines = content.Split({vbCrLf, vbLf, Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

        Dim bracketStack As New Stack(Of Char)
        Dim inMultiLineComment As Boolean = False
        Dim declaredVariables As New HashSet(Of String)

        For lineNum As Integer = 0 To lines.Length - 1
            Dim line = lines(lineNum)
            Dim trimmedLine = line.Trim()

            ' Skip empty lines
            If String.IsNullOrWhiteSpace(trimmedLine) Then Continue For

            ' Check multi-line comments
            If trimmedLine.Contains("/*") Then inMultiLineComment = True
            If trimmedLine.Contains("*/") Then inMultiLineComment = False
            If inMultiLineComment Then Continue For

            ' Skip single line comments
            If trimmedLine.StartsWith("//") Then Continue For

            ' Remove inline comments for analysis
            Dim codeOnly = trimmedLine
            If trimmedLine.Contains("//") Then
                codeOnly = trimmedLine.Substring(0, trimmedLine.IndexOf("//")).Trim()
            End If

            ' 1. BRACKET AND PARENTHESES VALIDATION
            ValidateBrackets(codeOnly, lineNum + 1, bracketStack, errors)

            ' 2. SEMICOLON VALIDATION
            ValidateSemicolons(codeOnly, lineNum + 1, errors, warnings)

            ' 3. VARIABLE DECLARATIONS
            TrackVariableDeclarations(codeOnly, declaredVariables)

            ' 4. ESPHome SPECIFIC VALIDATIONS
            ValidateESPHomeSpecific(codeOnly, lineNum + 1, errors, warnings, declaredVariables)

            ' 5. C++ SYNTAX VALIDATIONS
            ValidateCppSyntax(codeOnly, lineNum + 1, errors, warnings)

            ' 6. COMMON MISTAKES
            ValidateCommonMistakes(codeOnly, lineNum + 1, warnings)
        Next

        ' Final bracket balance check
        If bracketStack.Count > 0 Then
            errors.Add($"Unmatched opening brackets: {String.Join("", bracketStack.Reverse())}")
        End If

        Return New ValidationResult(errors, warnings)
    End Function

    Private Sub ValidateBrackets(line As String, lineNum As Integer, bracketStack As Stack(Of Char), errors As List(Of String))
        For Each c As Char In line
            Select Case c
                Case "("c, "{"c, "["c
                    bracketStack.Push(c)
                Case ")"c
                    If bracketStack.Count = 0 Then
                        errors.Add($"Line {lineNum}: Unmatched closing parenthesis")
                    ElseIf bracketStack.Pop() <> "("c Then
                        errors.Add($"Line {lineNum}: Mismatched brackets")
                    End If
                Case "}"c
                    If bracketStack.Count = 0 Then
                        errors.Add($"Line {lineNum}: Unmatched closing brace")
                    ElseIf bracketStack.Pop() <> "{"c Then
                        errors.Add($"Line {lineNum}: Mismatched brackets")
                    End If
                Case "]"c
                    If bracketStack.Count = 0 Then
                        errors.Add($"Line {lineNum}: Unmatched closing bracket")
                    ElseIf bracketStack.Pop() <> "["c Then
                        errors.Add($"Line {lineNum}: Mismatched brackets")
                    End If
            End Select
        Next
    End Sub

    Private Sub ValidateSemicolons(line As String, lineNum As Integer, errors As List(Of String), warnings As List(Of String))
        ' Lines that should end with semicolon
        Dim shouldHaveSemicolon = {
            "return", "break", "continue", "++", "--", "=", "+=", "-=", "*=", "/="
        }

        ' Lines that shouldn't end with semicolon
        Dim shouldNotHaveSemicolon = {
            "if", "else", "for", "while", "switch", "case", "default", "{"
        }

        Dim endsWithSemicolon = line.TrimEnd().EndsWith(";")

        ' Check if line should have semicolon
        For Each pattern In shouldHaveSemicolon
            If line.Contains(pattern) AndAlso Not line.Contains("==") AndAlso Not line.Contains("!=") Then
                If Not endsWithSemicolon AndAlso Not line.TrimEnd().EndsWith("{") Then
                    errors.Add($"Line {lineNum}: Missing semicolon")
                    Exit For
                End If
            End If
        Next

        ' Check if line shouldn't have semicolon
        For Each pattern In shouldNotHaveSemicolon
            If line.TrimStart().StartsWith(pattern) AndAlso endsWithSemicolon Then
                warnings.Add($"Line {lineNum}: Unnecessary semicolon after '{pattern}'")
                Exit For
            End If
        Next
    End Sub

    Private Sub TrackVariableDeclarations(line As String, declaredVariables As HashSet(Of String))
        ' Track variable declarations (auto, int, float, bool, string, etc.)
        Dim variablePatterns = {
            "auto\s+(\w+)", "int\s+(\w+)", "float\s+(\w+)", "bool\s+(\w+)",
            "String\s+(\w+)", "std::string\s+(\w+)", "double\s+(\w+)"
        }

        For Each pattern In variablePatterns
            Dim matches = System.Text.RegularExpressions.Regex.Matches(line, pattern)
            For Each match As System.Text.RegularExpressions.Match In matches
                If match.Groups.Count > 1 Then
                    declaredVariables.Add(match.Groups(1).Value)
                End If
            Next
        Next
    End Sub

    Private Sub ValidateESPHomeSpecific(line As String, lineNum As Integer, errors As List(Of String), warnings As List(Of String), declaredVariables As HashSet(Of String))
        ' 1. ID() function validation
        Dim idMatches = System.Text.RegularExpressions.Regex.Matches(line, "id\(([^)]+)\)")
        For Each match As System.Text.RegularExpressions.Match In idMatches
            Dim idName = match.Groups(1).Value.Trim().Trim(""""c)
            ' Here you could check against your component list
            If idName.Contains(" ") Then
                errors.Add($"Line {lineNum}: ID names cannot contain spaces: '{idName}'")
            End If
            If Not System.Text.RegularExpressions.Regex.IsMatch(idName, "^[a-zA-Z_][a-zA-Z0-9_]*$") Then
                errors.Add($"Line {lineNum}: Invalid ID format: '{idName}'")
            End If
        Next

        ' 2. Common ESPHome functions
        Dim espHomeFunctions = {
            "ESP_LOGD", "ESP_LOGI", "ESP_LOGW", "ESP_LOGE",
            "publish_state", "set_level", "turn_on", "turn_off"
        }

        For Each func In espHomeFunctions
            If line.Contains($"{func}(") AndAlso Not line.Contains($"{func}();") Then
                ' Check if function call has proper parameters
                If line.Split("("c).Length <> line.Split(")"c).Length Then
                    errors.Add($"Line {lineNum}: Unmatched parentheses in {func}() call")
                End If
            End If
        Next

        ' 3. Variable usage validation - VEREINFACHT und ROBUSTER
        ' ✅ Einfacher Ansatz: Skip komplette String-Validierung wenn Anführungszeichen vorhanden
        If line.Contains("""") Then
            ' Line enthält Strings - skip variable validation für diese Zeile
            Return
        End If

        ' ESPHome-spezifische Variablen und Funktionen
        Dim espHomeBuiltins = {
            "it", "x", "y", "id", "state", "value", "print", "printf", "println",
            "has_state", "set_level", "turn_on", "turn_off", "publish_state",
            "millis", "delay", "digitalRead", "digitalWrite", "analogRead", "analogWrite",
            "HIGH", "LOW", "INPUT", "OUTPUT", "INPUT_PULLUP",
            "min", "max", "abs", "round", "floor", "ceil", "sqrt", "pow",
            "String", "toString", "c_str", "length", "substring", "indexOf",
            "ESP_LOGD", "ESP_LOGI", "ESP_LOGW", "ESP_LOGE"
        }

        ' Keywords
        Dim keywords = {"if", "else", "for", "while", "return", "true", "false", "auto", "int", "float", "double", "bool", "String", "std"}

        Dim variableUsage = System.Text.RegularExpressions.Regex.Matches(line, "\b([a-zA-Z_][a-zA-Z0-9_]*)\b")
        For Each match As System.Text.RegularExpressions.Match In variableUsage
            Dim varName = match.Value

            ' Skip if it's a known keyword, builtin, or declared variable
            If Not keywords.Contains(varName) AndAlso
               Not espHomeBuiltins.Contains(varName) AndAlso
               Not declaredVariables.Contains(varName) AndAlso
               Not line.Contains($"auto {varName}") AndAlso
               Not line.Contains($"int {varName}") AndAlso
               Not line.Contains($"{varName}(") Then  ' Skip function calls

                ' Additional check: Skip if it's part of a method call (like it.print)
                Dim wordIndex = line.IndexOf(varName)
                If wordIndex > 0 AndAlso line(wordIndex - 1) = "."c Then
                    Continue For ' Skip method names after dot
                End If

                ' Could be undefined variable
                warnings.Add($"Line {lineNum}: Possible undefined variable: '{varName}'")
            End If
        Next
    End Sub

    Private Sub ValidateCppSyntax(line As String, lineNum As Integer, errors As List(Of String), warnings As List(Of String))
        ' 1. Assignment vs comparison
        If System.Text.RegularExpressions.Regex.IsMatch(line, "if\s*\([^=]*=[^=]") Then
            warnings.Add($"Line {lineNum}: Possible assignment in if condition (use == for comparison)")
        End If

        ' 2. Missing comparison operators
        If line.Contains("if") AndAlso line.Contains("=") AndAlso Not line.Contains("==") AndAlso Not line.Contains("!=") Then
            warnings.Add($"Line {lineNum}: Single '=' in if statement - did you mean '=='?")
        End If

        ' 3. String concatenation
        If line.Contains("+") AndAlso line.Contains("""") Then
            warnings.Add($"Line {lineNum}: Use String() constructor for number to string conversion")
        End If

        ' 4. Array access without bounds check
        If line.Contains("[") AndAlso line.Contains("]") Then
            warnings.Add($"Line {lineNum}: Ensure array bounds are checked")
        End If
    End Sub

    Private Sub ValidateCommonMistakes(line As String, lineNum As Integer, warnings As List(Of String))
        ' 1. Delay in lambda (not recommended)
        If line.ToLower().Contains("delay(") Then
            warnings.Add($"Line {lineNum}: delay() blocks the main loop - consider using timers instead")
        End If

        ' 2. Serial.print in ESPHome
        If line.Contains("Serial.print") Then
            warnings.Add($"Line {lineNum}: Use ESP_LOG* macros instead of Serial.print")
        End If

        ' 3. Hardcoded pin numbers
        If System.Text.RegularExpressions.Regex.IsMatch(line, "\b\d+\b") AndAlso line.ToLower().Contains("pin") Then
            warnings.Add($"Line {lineNum}: Consider using named constants for pin numbers")
        End If

        ' 4. Missing const for read-only variables
        If line.Contains("=") AndAlso Not line.Contains("const") AndAlso Not line.Contains("auto") Then
            If Not line.Contains("++") AndAlso Not line.Contains("--") AndAlso Not line.Contains("+=") Then
                warnings.Add($"Line {lineNum}: Consider using 'const' for read-only variables")
            End If
        End If

        ' 5. Magic numbers
        Dim numberMatches = System.Text.RegularExpressions.Regex.Matches(line, "\b\d{2,}\b")
        For Each match As System.Text.RegularExpressions.Match In numberMatches
            If Integer.Parse(match.Value) > 100 Then
                warnings.Add($"Line {lineNum}: Magic number '{match.Value}' - consider using a named constant")
            End If
        Next
    End Sub

    Private Sub WriteActionField(sb As StringBuilder, key As String, val As String)
        sb.AppendLine($"    {key}:")
        If val.Contains(vbCrLf) OrElse val.Contains(vbLf) Then
            For Each line In val.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                Dim trimmedLine = line.Trim()
                If trimmedLine.StartsWith("-") Then
                    sb.AppendLine($"      {trimmedLine}")
                Else
                    sb.AppendLine($"      - {trimmedLine}")
                End If
            Next
        Else
            If val.StartsWith("-") Then
                sb.AppendLine($"      {val}")
            Else
                sb.AppendLine($"      - {val}")
            End If
        End If
    End Sub

    Private Function FixUnitOfMeasurement(value As String) As String
        value = value.Replace("â€", "°")
        value = value.Replace("°", "°")
        value = value.Trim()
        If value = "C" Then value = "°C"
        If value = "F" Then value = "°F"
        Return value
    End Function

End Module
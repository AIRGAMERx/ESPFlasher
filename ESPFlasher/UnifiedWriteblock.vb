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
                                                 paramString As String, filterOrLambdaContent As String)

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
        WriteUniversalBlock(sb, blockType, platform, paramString, filterOrLambdaContent)
    End Sub

    ' ========================================
    ' HAUPTFUNKTION - UNIVERSELLE WRITEBLOCK
    ' ========================================
    Public Sub WriteUniversalBlock(sb As StringBuilder, blockType As BlockType, platform As String,
                                   paramString As String, filterOrLambdaContent As String)

        sb.AppendLine($"  - platform: {platform}")

        ' ROBUSTES Parameter-Parsing (einheitlich für alle)
        Dim flatDict = ParseParameterString(paramString)

        ' Auto-Korrektur für Code-Inhalte anwenden
        Dim correctedContent = ApplyCodeCorrection(filterOrLambdaContent, blockType)

        ' Block-spezifische Verarbeitung
        Select Case blockType
            Case BlockType.Sensor, BlockType.BinarySensor
                WriteSensorSpecificFields(sb, flatDict, correctedContent)

            Case BlockType.Display
                WriteDisplaySpecificFields(sb, flatDict, correctedContent)

            Case BlockType.Template, BlockType.TextSensor, BlockType.Switch
                WriteTemplateSpecificFields(sb, flatDict, correctedContent)
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
    ' CODE-KORREKTUR
    ' ========================================
    Private Function ApplyCodeCorrection(content As String, blockType As BlockType) As String
        If String.IsNullOrWhiteSpace(content) Then Return content

        Select Case blockType
            Case BlockType.Display
                Return ProcessCodeCorrection(content, "lambda")
            Case BlockType.Template, BlockType.TextSensor, BlockType.Switch
                Return ProcessCodeCorrection(content, "template")
            Case BlockType.Sensor, BlockType.BinarySensor
                Return ProcessCodeCorrection(content, "sensor")
            Case Else
                Return ProcessCodeCorrection(content, "general")
        End Select
    End Function

    Private Function ProcessCodeCorrection(content As String, codeType As String) As String
        If String.IsNullOrWhiteSpace(content) Then Return content

        Dim corrected As String = content

        ' 1. Entferne problematische °-Zeichen
        corrected = corrected.Replace("°C", "C")
        corrected = corrected.Replace("°F", "F")
        corrected = corrected.Replace("°", "")

        ' 2. Korrigiere häufige Tippfehler bei Funktionen
        corrected = Regex.Replace(corrected, "\bif\.print\b", "it.print", RegexOptions.IgnoreCase)
        corrected = Regex.Replace(corrected, "\bif\.printf\b", "it.printf", RegexOptions.IgnoreCase)
        corrected = Regex.Replace(corrected, "\bit\.hast_state\b", "it.has_state", RegexOptions.IgnoreCase)
        corrected = Regex.Replace(corrected, "\bhast_state\b", "has_state", RegexOptions.IgnoreCase)

        ' 3. Korrigiere printf Format-Strings
        corrected = Regex.Replace(corrected, "%(\d+)f", "%.${1}f") ' %1f -> %.1f
        corrected = Regex.Replace(corrected, "%f\b", "%.1f") ' %f -> %.1f

        ' 4. Füge fehlende Anführungszeichen hinzu
        corrected = Regex.Replace(corrected, "it\.print\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*([^""][^,)]+)\s*\)",
                                 "it.print($1, $2, ""$3"")")

        ' 5. Korrigiere fehlende Semikolons
        Dim lines = corrected.Split({Environment.NewLine, vbCrLf, vbLf}, StringSplitOptions.None)
        For i = 0 To lines.Length - 1
            Dim line = lines(i).Trim()
            If Not String.IsNullOrEmpty(line) AndAlso
               Not line.EndsWith(";") AndAlso
               Not line.EndsWith("{") AndAlso
               Not line.EndsWith("}") AndAlso
               Not line.StartsWith("//") AndAlso
               Not line.StartsWith("/*") AndAlso
               Not line.Contains("if (") AndAlso
               Not line.Contains("} else {") Then
                lines(i) = lines(i) + ";"
            End If
        Next
        corrected = String.Join(Environment.NewLine, lines)

        ' 6. Automatische Einrückung
        corrected = FormatCppIndentation(corrected)

        Return corrected
    End Function

    Private Function FormatCppIndentation(code As String) As String
        Dim lines = code.Split({Environment.NewLine, vbCrLf, vbLf}, StringSplitOptions.None)
        Dim result As New List(Of String)
        Dim indentLevel As Integer = 0
        Dim indentSize As Integer = 2

        For Each line In lines
            Dim trimmedLine = line.Trim()

            If String.IsNullOrWhiteSpace(trimmedLine) OrElse
               trimmedLine.StartsWith("//") OrElse
               trimmedLine.StartsWith("/*") Then
                result.Add(trimmedLine)
                Continue For
            End If

            If trimmedLine.StartsWith("}") OrElse trimmedLine.Contains("} else") Then
                indentLevel = Math.Max(0, indentLevel - 1)
            End If

            Dim indent As String = New String(" "c, indentLevel * indentSize)
            result.Add(indent + trimmedLine)

            If trimmedLine.EndsWith("{") OrElse
               trimmedLine.Contains("if (") OrElse
               trimmedLine.Contains("} else {") Then
                indentLevel += 1
            End If
        Next

        Return String.Join(Environment.NewLine, result)
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

        ' Lambda schreiben
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

        ' Lambda schreiben
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
            "cs_pin", "internal_filter", "unit_of_measurement", "device_class", "pin", "name"
        }
    End Function

    Private Function GetDisplayFlatKeys() As HashSet(Of String)
        Return New HashSet(Of String) From {
            "address", "reset_pin", "cs_pin", "dc_pin", "busy_pin", "led_pin",
            "model", "rotation", "contrast", "intensity", "measurement_duration",
            "clk_pin", "dio_pin", "pin", "chipset", "num_leds", "rgb_order",
            "max_refresh_rate", "color_correct", "lambda", "dimensions"
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

    Private Sub WriteLambdaBlock(sb As StringBuilder, lambdaContent As String)
        sb.AppendLine("    lambda: |-")
        For Each line In lambdaContent.Split({vbCrLf, vbLf, Environment.NewLine}, StringSplitOptions.None)
            If String.IsNullOrWhiteSpace(line) Then
                sb.AppendLine()
            Else
                sb.AppendLine($"      {line.TrimEnd()}")
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

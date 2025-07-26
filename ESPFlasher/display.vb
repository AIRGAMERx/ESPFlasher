Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq
Module display
    Public displayData As JObject
    Dim GBLocation As New Point(480, 17)
    Function LoadDisplayData() As Task
        Dim filePath As String = Path.Combine(Application.StartupPath, "displays.json")
        If Not File.Exists(filePath) Then
            MessageBox.Show("Die Display.json wurde nicht gefunden.")
            Return Task.CompletedTask
            Exit Function
        End If

        Dim json = File.ReadAllText(filePath)
        displayData = JObject.Parse(json)

        Form1.CBB_DisplayGroup.Items.Clear()
        For Each group In displayData.Properties()
            Form1.CBB_DisplayGroup.Items.Add(group.Name)
        Next
        Return Task.CompletedTask



    End Function

    Public Function LoadDisplyFromJson(path As String) As List(Of DisplayCategory)
        Dim json = File.ReadAllText(path)
        Return System.Text.Json.JsonSerializer.Deserialize(Of List(Of DisplayCategory))(json)
    End Function

    Public Sub GenerateDisplayConfigFields(displayInfo As JObject, targetPanel As Panel)
        targetPanel.Controls.Clear()
        Dim yOffset As Integer = 10

        Dim uiFields As JObject = Nothing
        If displayInfo("ui_fields") IsNot Nothing Then
            uiFields = CType(displayInfo("ui_fields"), JObject)
        End If


        If displayInfo("required") IsNot Nothing Then
            For Each r In displayInfo("required")
                Dim fieldName = r.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddDisplayField(fieldName, True, targetPanel, yOffset, fieldConfig)
            Next
        End If


        If displayInfo("optional") IsNot Nothing Then
            For Each o In displayInfo("optional")
                Dim fieldName = o.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddDisplayField(fieldName, False, targetPanel, yOffset, fieldConfig)
            Next
        End If

        If displayInfo("nested_fields") IsNot Nothing Then
            Dim nestedFields As JObject = CType(displayInfo("nested_fields"), JObject)

            For Each section In nestedFields
                Dim prefix As String = section.Key
                Dim fieldList As JArray = CType(section.Value, JArray)

                For Each field In fieldList
                    Dim fullFieldName As String = $"{prefix}_{field.ToString()}"
                    Dim fieldConfig As JObject = Nothing

                    If uiFields IsNot Nothing AndAlso uiFields(fullFieldName) IsNot Nothing Then
                        fieldConfig = CType(uiFields(fullFieldName), JObject)
                    End If

                    AddDisplayField(fullFieldName, False, targetPanel, yOffset, fieldConfig)
                Next
            Next
        End If


        If displayInfo("info") IsNot Nothing Then
            For Each o In displayInfo("info")
                AddInfoField(o.ToString(), targetPanel, yOffset)
            Next
        End If




    End Sub
    Public Sub AddDisplayField(fieldName As String, isRequired As Boolean, targetPanel As Panel, ByRef yOffset As Integer, Optional fieldConfig As JObject = Nothing)
        Dim label As New Label With {
        .Text = If(isRequired, "* ", "") & fieldName,
        .Location = New Point(10, yOffset),
        .AutoSize = True
    }
        targetPanel.Controls.Add(label)

        Dim control As Control = Nothing


        Dim ctrlType As String = If(fieldConfig IsNot Nothing AndAlso fieldConfig("type") IsNot Nothing,
                                fieldConfig("type").ToString().ToLower(),
                                "textbox")

        Select Case ctrlType
            Case "textbox"
                control = New TextBox With {
                .Name = "TXT_" & fieldName,
                .Location = New Point(230, yOffset),
                .Width = 150
            }
                If fieldConfig?("multiline")?.ToObject(Of Boolean) = True Then
                    CType(control, TextBox).Multiline = True
                    control.Height = 60
                End If
                AddHandler CType(control, TextBox).TextChanged, AddressOf ConfigControl_Changed

            Case "combobox"
                control = New ComboBox With {
                .Name = "CMB_" & fieldName,
                .Location = New Point(230, yOffset),
                .Width = 150,
                .DropDownStyle = ComboBoxStyle.DropDownList
            }
                If fieldConfig?("values") IsNot Nothing Then
                    Dim items = fieldConfig("values").ToObject(Of List(Of String))
                    CType(control, ComboBox).Items.AddRange(items.ToArray())
                End If
                AddHandler CType(control, ComboBox).SelectedIndexChanged, AddressOf ConfigControl_Changed

            Case "numericupdown"
                control = New NumericUpDown With {
                .Name = "NUM_" & fieldName,
                .Location = New Point(230, yOffset),
                .Width = 100,
                .DecimalPlaces = 1
            }
                With CType(control, NumericUpDown)
                    .Minimum = If(fieldConfig?("min") IsNot Nothing, fieldConfig("min").ToObject(Of Decimal), 0)
                    .Maximum = If(fieldConfig?("max") IsNot Nothing, fieldConfig("max").ToObject(Of Decimal), 100)
                    .Increment = If(fieldConfig?("Step") IsNot Nothing, fieldConfig("Step").ToObject(Of Decimal), 1)
                End With
                AddHandler CType(control, NumericUpDown).TextChanged, AddressOf ConfigControl_Changed

            Case Else
                ' Fallback: TextBox
                control = New TextBox With {
                .Name = "TXT_" & fieldName,
                .Location = New Point(150, yOffset),
                .Width = 150
            }
                AddHandler CType(control, TextBox).TextChanged, AddressOf ConfigControl_Changed
        End Select

        If control IsNot Nothing Then
            targetPanel.Controls.Add(control)
            yOffset += control.Height + 10
        End If
        yOffset += 10
    End Sub


    Private Sub AddInfoField(fieldName As String, panel As Panel, ByRef yOffset As Integer)
        Dim lbl As New Label With {
        .Text = fieldName,
        .Left = 0,
        .Top = yOffset,
        .Width = 265,
        .ForeColor = Color.Red
        }
        panel.Controls.Add(lbl)
    End Sub

    Private Function CollectCurrentDisplayData() As (Platform As String, Parameters As String, Filters As String)
        Try
            Dim platform As String = ""
            Dim parameterList As New List(Of String)


            If Form1.CBB_DisplayGroup.SelectedItem IsNot Nothing AndAlso Form1.CBB_DisplayType.SelectedItem IsNot Nothing Then
                Dim selectedGroup = Form1.CBB_DisplayGroup.SelectedItem.ToString()
                Dim selectedDisplay = Form1.CBB_DisplayType.SelectedItem.ToString()

                Dim jsonText = File.ReadAllText(Application.StartupPath & "\displays.json")
                Dim jsonData = JObject.Parse(jsonText)

                If jsonData.ContainsKey(selectedGroup) AndAlso jsonData(selectedGroup)(selectedDisplay) IsNot Nothing Then
                    Dim displayinfo = jsonData(selectedGroup)(selectedDisplay)
                    platform = displayinfo("platform")?.ToString()
                End If
            End If


            For Each ctrl As Control In Form1.pnl_DisplayConfig.Controls
                Dim value As String = ""
                Dim fieldName As String = ""

                If TypeOf ctrl Is TextBox Then
                    Dim txt = CType(ctrl, TextBox)
                    If txt.Name.StartsWith("TXT_") AndAlso Not String.IsNullOrWhiteSpace(txt.Text) Then
                        fieldName = txt.Name.Substring(4) ' "TXT_" entfernen
                        value = txt.Text
                    End If

                ElseIf TypeOf ctrl Is ComboBox Then
                    Dim cmb = CType(ctrl, ComboBox)
                    If cmb.Name.StartsWith("CMB_") AndAlso cmb.SelectedItem IsNot Nothing Then
                        fieldName = cmb.Name.Substring(4) ' "CMB_" entfernen  
                        value = cmb.SelectedItem.ToString()
                    End If

                ElseIf TypeOf ctrl Is NumericUpDown Then
                    Dim num = CType(ctrl, NumericUpDown)
                    If num.Name.StartsWith("NUM_") Then
                        fieldName = num.Name.Substring(4) ' "NUM_" entfernen
                        value = num.Value.ToString()
                    End If
                End If

                If Not String.IsNullOrEmpty(fieldName) AndAlso Not String.IsNullOrEmpty(value) Then
                    parameterList.Add($"{fieldName}={value}")
                End If
            Next


            Dim parameters = String.Join(",", parameterList)


            Dim filterString As String = ""

            Return (platform, parameters, filterString)

        Catch ex As Exception
            Return ("", "", "# Fehler: " & ex.Message)
        End Try
    End Function
    Private Sub UpdateYAMLPreview()
        Try
            Dim displayData = CollectCurrentDisplayData()

            If String.IsNullOrEmpty(displayData.Platform) Then
                Form1.RTB_yamlPreviewDisplay.Text = "# Display auswählen für Live Preview"
                Return
            End If

            Dim sb As New StringBuilder()
            sb.AppendLine("# Live Preview - Nur dieses Display:")
            sb.AppendLine("display:")


            WriteDisplayBlock(sb, displayData.Platform, displayData.Parameters, displayData.Filters)

            Form1.RTB_yamlPreviewDisplay.Text = sb.ToString()

        Catch ex As Exception
            Form1.RTB_yamlPreviewDisplay.Text = "# Fehler in der Konfiguration:" & vbCrLf & ex.Message
        End Try
    End Sub
    Private Sub ConfigControl_Changed(sender As Object, e As EventArgs)
        UpdateYAMLPreview()
    End Sub
    Private Sub WriteDisplayBlock(sb As StringBuilder, platform As String, paramString As String, filterCellContent As String)
        sb.AppendLine($"  - platform: {platform}")

        ' Key-Value-Paare aus paramString
        Dim flatDict As New Dictionary(Of String, String)
        For Each part In paramString.Split(","c)
            Dim keyVal = part.Trim().Split("="c, 2)
            If keyVal.Length = 2 Then
                flatDict(keyVal(0).Trim()) = keyVal(1).Trim()
            End If
        Next

        ' Vorbereitung verschachtelte Felder
        Dim nestedDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim simpleKeys As New Dictionary(Of String, String)

        ' Display-spezifische flache Keys
        Dim flatKeys As New HashSet(Of String) From {
       "address", "reset_pin", "cs_pin", "dc_pin", "busy_pin", "led_pin",
       "model", "rotation", "contrast", "intensity", "measurement_duration",
       "clk_pin", "dio_pin", "pin", "chipset", "num_leds", "rgb_order",
       "max_refresh_rate", "color_correct"
   }

        ' Display-spezifische accuracy_decimals Fixups
        Dim accuracyFixups = New Dictionary(Of String, Tuple(Of String, String)) From {
       {"temperature_accuracy_decimals", Tuple.Create("temperature", "accuracy_decimals")},
       {"humidity_accuracy_decimals", Tuple.Create("humidity", "accuracy_decimals")},
       {"pressure_accuracy_decimals", Tuple.Create("pressure", "accuracy_decimals")}
   }

        For Each kvp In flatDict
            Dim keyLower = kvp.Key.ToLower()

            If accuracyFixups.ContainsKey(keyLower) Then
                Dim target = accuracyFixups(keyLower)
                If Not nestedDict.ContainsKey(target.Item1) Then nestedDict(target.Item1) = New Dictionary(Of String, String)
                nestedDict(target.Item1)(target.Item2) = kvp.Value
                Continue For
            End If

            If kvp.Key.Contains("_") AndAlso Not flatKeys.Contains(keyLower) Then
                Dim parts = kvp.Key.Split("_"c)
                If parts.Length >= 2 Then
                    Dim group = String.Join("_", parts.Take(parts.Length - 1)).Trim()
                    Dim subkey = parts.Last().Trim()
                    If Not nestedDict.ContainsKey(group) Then nestedDict(group) = New Dictionary(Of String, String)
                    nestedDict(group)(subkey) = kvp.Value
                Else
                    simpleKeys(kvp.Key) = kvp.Value
                End If
            Else
                simpleKeys(kvp.Key) = kvp.Value
            End If
        Next

        ' Spezielle Display-Behandlungen

        ' Dimensions für LCD Displays
        If nestedDict.ContainsKey("dimensions") Then
            sb.AppendLine("    dimensions:")
            For Each inner In nestedDict("dimensions")
                If inner.Key = "columns" OrElse inner.Key = "rows" Then
                    sb.AppendLine($"      {inner.Key}: {inner.Value}")
                End If
            Next
            nestedDict.Remove("dimensions")
        End If

        ' Color Correct für LEDs (Array-Format)
        If simpleKeys.ContainsKey("color_correct") Then
            sb.AppendLine($"    color_correct: [{simpleKeys("color_correct")}]")
            simpleKeys.Remove("color_correct")
        End If

        ' Rotation (ohne °-Zeichen)
        If simpleKeys.ContainsKey("rotation") Then
            Dim rotValue = simpleKeys("rotation").Replace("°", "")
            sb.AppendLine($"    rotation: {rotValue}")
            simpleKeys.Remove("rotation")
        End If

        ' Einheiten für Display-Parameter
        Dim unit_percent = New HashSet(Of String) From {"contrast", "intensity"}
        Dim unit_ms = New HashSet(Of String) From {"measurement_duration"}

        ' Flache Felder schreiben
        For Each kvp In simpleKeys
            Dim key = kvp.Key
            Dim val = kvp.Value

            ' Anführungszeichen für String-Werte
            If key = "model" OrElse key = "chipset" OrElse key = "rgb_order" Then
                sb.AppendLine($"    {key}: ""{val}""")
            ElseIf unit_percent.Contains(key) Then
                sb.AppendLine($"    {key}: {val}%")
            ElseIf unit_ms.Contains(key) Then
                sb.AppendLine($"    {key}: {val}ms")
            Else
                sb.AppendLine($"    {key}: {val}")
            End If
        Next

        ' Filter als JSON auswerten
        Dim filterData As JObject = Nothing
        If Not String.IsNullOrWhiteSpace(filterCellContent) Then
            Try
                filterData = JObject.Parse(filterCellContent)
            Catch ex As Exception
                sb.AppendLine("    # Fehler beim Parsen der Filterkonfiguration")
            End Try
        End If

        ' Nested Felder schreiben (für komplexere Displays)
        For Each outer In nestedDict
            sb.AppendLine($"    {outer.Key}:")
            For Each inner In outer.Value
                sb.AppendLine($"      {inner.Key}: {inner.Value}")
            Next

            ' Filter für diesen Block?
            If filterData IsNot Nothing AndAlso filterData.ContainsKey(outer.Key) Then
                sb.AppendLine("      filters:")
                For Each f In CType(filterData(outer.Key), JObject).Properties()
                    Dim val = f.Value.ToString().Trim()
                    If val.Contains(Environment.NewLine) Then
                        sb.AppendLine($"        - {f.Name}:")
                        For Each line In val.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                            sb.AppendLine($"            {line.Trim()}")
                        Next
                    Else
                        sb.AppendLine($"        - {f.Name}: {val}")
                    End If
                Next
            End If
        Next

        ' Lambda-Filter für Displays (meist global)
        If filterData IsNot Nothing AndAlso filterData.ContainsKey("global") Then
            Dim globalFilters = CType(filterData("global"), JObject)

            ' Lambda separat behandeln (mehrzeilig)
            If globalFilters.ContainsKey("lambda") Then
                sb.AppendLine("    lambda: |-")
                Dim lambdaContent = globalFilters("lambda").ToString().Trim()

                ' Split auf verschiedene Zeilenendezeichen
                For Each line In lambdaContent.Split({vbCrLf, vbLf, Environment.NewLine}, StringSplitOptions.None)
                    If String.IsNullOrWhiteSpace(line) Then
                        sb.AppendLine()  ' Leere Zeile beibehalten
                    Else
                        ' Einrückung von 6 Spaces für Lambda-Code
                        sb.AppendLine($"      {line.TrimEnd()}")
                    End If
                Next
            End If

            ' Andere globale Filter
            For Each f In globalFilters.Properties()
                If f.Name <> "lambda" Then
                    Dim val = f.Value.ToString().Trim()
                    If val.Contains(vbCrLf) OrElse val.Contains(vbLf) OrElse val.Contains(Environment.NewLine) Then
                        sb.AppendLine($"    {f.Name}: |-")
                        For Each line In val.Split({vbCrLf, vbLf, Environment.NewLine}, StringSplitOptions.None)
                            If String.IsNullOrWhiteSpace(line) Then
                                sb.AppendLine()
                            Else
                                sb.AppendLine($"      {line.TrimEnd()}")
                            End If
                        Next
                    Else
                        sb.AppendLine($"    {f.Name}: {val}")
                    End If
                End If
            Next
        End If

        sb.AppendLine()
    End Sub

    Public Sub ExportDisplayToYaml(sb As StringBuilder, dgv As DataGridView)
        If dgv.Rows.Count = 0 Then Exit Sub

        Dim displayExist As Boolean = False

        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For

            Dim platform = row.Cells("Platform").Value?.ToString()?.Trim()
            Dim paramString = row.Cells("Parameter").Value?.ToString()?.Trim()
            Dim filterString = row.Cells("Filter").Value?.ToString?.Trim()

            If String.IsNullOrEmpty(platform) Then Continue For


            If Not displayExist Then
                sb.AppendLine("display:")
                displayExist = True
            End If


            WriteDisplayBlock(sb, platform, paramString, filterString)
        Next
    End Sub
    Public Sub AddDisplayToGrid(displayGroup As String, displayType As String, displayInfo As JObject, paneldisplay As Panel, dgv As DataGridView)




        ' 1. Pflichtfelder prüfen
        If displayInfo("required") IsNot Nothing Then
            For Each reqField In displayInfo("required")
                Dim fieldName = reqField.ToString()
                Dim ctrl = paneldisplay.Controls.Cast(Of Control).FirstOrDefault(Function(c) c.Name.ToLower().EndsWith("_" & fieldName.ToLower()))

                If ctrl Is Nothing Then
                    MessageBox.Show($"Das Pflichtfeld '{fieldName}' fehlt im UI.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim isEmpty As Boolean = False
                Select Case True
                    Case TypeOf ctrl Is TextBox
                        isEmpty = String.IsNullOrWhiteSpace(CType(ctrl, TextBox).Text)
                    Case TypeOf ctrl Is ComboBox
                        isEmpty = CType(ctrl, ComboBox).SelectedIndex = -1
                    Case TypeOf ctrl Is NumericUpDown
                        ' Optional: 0 als leer definieren
                        isEmpty = False ' Hier bewusst erlaubt
                End Select

                If isEmpty Then
                    MessageBox.Show($"Das Pflichtfeld '{fieldName}' muss ausgefüllt werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                Dim pinFields = New HashSet(Of String) From {"pin", "trigger_pin", "echo_pin", "cs_pin"}
                If pinFields.Contains(fieldName.ToLower()) Then
                    Dim pinValue As String = ""

                    If TypeOf ctrl Is TextBox Then
                        pinValue = CType(ctrl, TextBox).Text.Trim()
                    ElseIf TypeOf ctrl Is ComboBox Then
                        pinValue = CType(ctrl, ComboBox).Text.Trim()
                    End If

                    If Form1.clickedRow = -1 Then
                        If Not String.IsNullOrEmpty(pinValue) AndAlso helper.pinInUse(pinValue) Then
                            MessageBox.Show($"Der Pin '{pinValue}' wird bereits von einem anderen Sensor verwendet.", "Pin-Konflikt", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If
                    End If
                End If
            Next
        End If

        ' 2. Parameterliste erzeugen
        Dim parameter As New List(Of String)

        For Each ctrl As Control In paneldisplay.Controls
            If Not ctrl.Name.Contains("_"c) Then Continue For

            Dim parts = ctrl.Name.Split("_"c, 2)
            If parts.Length <> 2 Then Continue For

            Dim paramName As String = parts(1)
            Dim paramValue As String = ""

            Select Case True
                Case TypeOf ctrl Is TextBox
                    Dim tb = CType(ctrl, TextBox)
                    If Not String.IsNullOrWhiteSpace(tb.Text) Then paramValue = tb.Text

                Case TypeOf ctrl Is ComboBox
                    Dim cb = CType(ctrl, ComboBox)
                    If cb.SelectedIndex >= 0 Then paramValue = cb.SelectedItem.ToString()

                Case TypeOf ctrl Is NumericUpDown
                    Dim num = CType(ctrl, NumericUpDown)
                    paramValue = num.Value.ToString()
            End Select

            If Not String.IsNullOrWhiteSpace(paramValue) Then
                parameter.Add($"{paramName}={paramValue}")
            End If
        Next

        ' 3. Plattform ermitteln
        Dim platform As String = ""
        If displayInfo("platform") IsNot Nothing Then
            platform = displayInfo("platform").ToString()
        End If

        ' 4. Zeile zum DataGridView hinzufügen
        If Form1.clickedRow > -1 Then
            Try

                Dim row As DataGridViewRow = dgv.Rows(Form1.clickedRow)
                row.Cells(0).Value = displayGroup
                row.Cells(1).Value = displayType
                row.Cells(2).Value = platform
                row.Cells(3).Value = ""
                row.Cells(4).Value = String.Join(",", parameter)
                MsgBox("Sensor wurde gespeichert")
                Form1.clickedRow = -1

            Catch ex As Exception

            End Try
        Else
            dgv.Rows.Add(displayGroup, displayType, platform, "", String.Join(", ", parameter))

        End If

        Form1.clickedRow = -1
    End Sub


End Module
Public Class DisplayCategory
    Public Property category As String
    Public Property displays As List(Of DisplayExportModel)
End Class
Public Class DisplayExportModel
    Public Property Platform As String
    Public Property GlobalPins As Dictionary(Of String, String)
    Public Property DisplayEntries As List(Of Dictionary(Of String, String))
End Class
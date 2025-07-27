Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq

Module templates
    Sub New()
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
    End Sub
    Public templateData As JObject

    Function LoadTemplateData() As Task
        Dim filePath = Path.Combine(Application.StartupPath, "template.json")
        If Not File.Exists(filePath) Then
            MessageBox.Show("Die template.json wurde nicht gefunden")
            Return Task.CompletedTask
        End If

        Dim json = File.ReadAllText(filePath, Encoding.UTF8)
        templateData = JObject.Parse(json)

        ' Template-Gruppen in ComboBox laden
        Main.CBB_TemplateGroup.Items.Clear()
        For Each group In templateData.Properties()
            Main.CBB_TemplateGroup.Items.Add(group.Name)
        Next
        Return Task.CompletedTask
    End Function

    Public Sub LoadTemplateTypes(selectedGroup As String)
        If templateData Is Nothing OrElse Not templateData.ContainsKey(selectedGroup) Then
            Return
        End If

        Main.CBB_TemplateType.Items.Clear()
        Dim groupData = CType(templateData(selectedGroup), JObject)

        For Each templateType In groupData.Properties()
            Main.CBB_TemplateType.Items.Add(templateType.Name)
        Next
    End Sub

    Public Sub GenerateTemplateConfigFields(templateInfo As JObject, targetPanel As Panel)
        targetPanel.Controls.Clear()
        Dim yOffset As Integer = 10

        Dim uiFields As JObject = Nothing
        If templateInfo("ui_fields") IsNot Nothing Then
            uiFields = CType(templateInfo("ui_fields"), JObject)
        End If

        ' Required Felder
        If templateInfo("required") IsNot Nothing Then
            For Each r In templateInfo("required")
                Dim fieldName = r.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddTemplateField(fieldName, True, targetPanel, yOffset, fieldConfig)
            Next
        End If

        ' Optional Felder
        If templateInfo("optional") IsNot Nothing Then
            For Each o In templateInfo("optional")
                Dim fieldName = o.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddTemplateField(fieldName, False, targetPanel, yOffset, fieldConfig)
            Next
        End If

        ' Verschachtelte Felder (nested_fields)
        If templateInfo("nested_fields") IsNot Nothing Then
            Dim nestedFields As JObject = CType(templateInfo("nested_fields"), JObject)

            For Each section In nestedFields
                Dim prefix As String = section.Key
                Dim fieldList As JArray = CType(section.Value, JArray)

                For Each field In fieldList
                    Dim fullFieldName As String = $"{prefix}_{field.ToString()}"
                    Dim fieldConfig As JObject = Nothing

                    If uiFields IsNot Nothing AndAlso uiFields(fullFieldName) IsNot Nothing Then
                        fieldConfig = CType(uiFields(fullFieldName), JObject)
                    End If

                    AddTemplateField(fullFieldName, False, targetPanel, yOffset, fieldConfig)
                Next
            Next
        End If

        ' Lambda-Beispiele anzeigen (falls vorhanden)
        If templateInfo("lambda_examples") IsNot Nothing Then
            AddLambdaExamples(templateInfo("lambda_examples"), targetPanel, yOffset)
        End If

        ' Info-Felder
        If templateInfo("info") IsNot Nothing Then
            For Each o In templateInfo("info")
                AddInfoField(o.ToString(), targetPanel, yOffset)
            Next
        End If
    End Sub

    Public Sub AddTemplateField(fieldName As String, isRequired As Boolean, targetPanel As Panel, ByRef yOffset As Integer, Optional fieldConfig As JObject = Nothing)
        Dim label As New Label With {
            .Text = If(isRequired, "* ", "") & fieldName,
            .Location = New Point(10, yOffset),
            .AutoSize = True,
            .Font = New Font("Arial", 9, If(isRequired, FontStyle.Bold, FontStyle.Regular))
        }
        targetPanel.Controls.Add(label)

        Dim control As Control = Nothing

        ' Standard: TextBox
        Dim ctrlType As String = If(fieldConfig IsNot Nothing AndAlso fieldConfig("type") IsNot Nothing,
                                    fieldConfig("type").ToString().ToLower(),
                                    "textbox")

        Select Case ctrlType
            Case "textbox"
                control = New TextBox With {
                    .Name = "TXT_" & fieldName,
                    .Location = New Point(230, yOffset),
                    .Width = 200
                }
                If fieldConfig?("multiline")?.ToObject(Of Boolean) = True Then
                    CType(control, TextBox).Multiline = True
                    control.Height = 80
                    CType(control, TextBox).ScrollBars = ScrollBars.Vertical
                End If
                If fieldConfig?("placeholder") IsNot Nothing Then
                    CType(control, TextBox).PlaceholderText = fieldConfig("placeholder").ToString()
                End If
                AddHandler CType(control, TextBox).TextChanged, AddressOf ConfigControl_Changed

            Case "combobox"
                control = New ComboBox With {
                    .Name = "CMB_" & fieldName,
                    .Location = New Point(230, yOffset),
                    .Width = 200,
                    .DropDownStyle = ComboBoxStyle.DropDownList
                }
                If fieldConfig?("values") IsNot Nothing Then
                    Dim items = fieldConfig("values").ToObject(Of List(Of String))
                    CType(control, ComboBox).Items.AddRange(items.ToArray())
                End If
                AddHandler CType(control, ComboBox).SelectedIndexChanged, AddressOf ConfigControl_Changed

            Case "checkbox"
                control = New CheckBox With {
                    .Name = "CHK_" & fieldName,
                    .Location = New Point(230, yOffset),
                    .Width = 200,
                    .Text = ""
                }
                AddHandler CType(control, CheckBox).CheckedChanged, AddressOf ConfigControl_Changed

            Case "numericupdown"
                control = New NumericUpDown With {
                    .Name = "NUM_" & fieldName,
                    .Location = New Point(230, yOffset),
                    .Width = 120,
                    .DecimalPlaces = If(fieldConfig?("decimals") IsNot Nothing, fieldConfig("decimals").ToObject(Of Integer), 1)
                }
                With CType(control, NumericUpDown)
                    .Minimum = If(fieldConfig?("min") IsNot Nothing, fieldConfig("min").ToObject(Of Decimal), 0)
                    .Maximum = If(fieldConfig?("max") IsNot Nothing, fieldConfig("max").ToObject(Of Decimal), 100)
                    .Increment = If(fieldConfig?("step") IsNot Nothing, fieldConfig("step").ToObject(Of Decimal), 1)
                End With
                AddHandler CType(control, NumericUpDown).ValueChanged, AddressOf ConfigControl_Changed

            Case Else
                ' Fallback: TextBox
                control = New TextBox With {
                    .Name = "TXT_" & fieldName,
                    .Location = New Point(230, yOffset),
                    .Width = 200
                }
                AddHandler CType(control, TextBox).TextChanged, AddressOf ConfigControl_Changed
        End Select

        If control IsNot Nothing Then
            targetPanel.Controls.Add(control)
            yOffset += Math.Max(control.Height + 10, 25)
        End If
    End Sub

    Private Sub AddLambdaExamples(examples As JToken, panel As Panel, ByRef yOffset As Integer)
        Dim exampleLabel As New Label With {
            .Text = "Lambda Beispiele:",
            .Location = New Point(10, yOffset),
            .Font = New Font("Arial", 9, FontStyle.Bold),
            .ForeColor = Color.DarkGreen,
            .AutoSize = True
        }
        panel.Controls.Add(exampleLabel)
        yOffset += 25

        For Each example In examples
            Dim lbl As New Label With {
                .Text = example.ToString(),
                .Location = New Point(20, yOffset),
                .Width = 420,
                .Height = 20,
                .Font = New Font("Consolas", 8),
                .ForeColor = Color.Gray,
                .AutoSize = False
            }
            panel.Controls.Add(lbl)
            yOffset += 22
        Next

        yOffset += 10 ' Extra Abstand nach Beispielen
    End Sub

    Private Function CollectCurrentTemplateData() As (Platform As String, Parameters As String, Lambda As String, SensorClass As String)
        Try
            Dim platform As String = ""
            Dim sensorClass As String = ""
            Dim parameterList As New List(Of String)
            Dim lambdaContent As String = ""

            If Main.CBB_TemplateGroup.SelectedItem IsNot Nothing AndAlso Main.CBB_TemplateType.SelectedItem IsNot Nothing Then
                Dim selectedGroup = Main.CBB_TemplateGroup.SelectedItem.ToString()
                Dim selectedTemplate = Main.CBB_TemplateType.SelectedItem.ToString()
                If templateData IsNot Nothing AndAlso templateData.ContainsKey(selectedGroup) AndAlso templateData(selectedGroup)(selectedTemplate) IsNot Nothing Then
                    Dim templateInfo = templateData(selectedGroup)(selectedTemplate)
                    platform = templateInfo("platform")?.ToString()
                    sensorClass = templateInfo("sensor_class")?.ToString()
                End If
            End If

            For Each ctrl As Control In Main.pnl_TemplateConfig.Controls
                Dim value As String = ""
                Dim fieldName As String = ""

                If TypeOf ctrl Is TextBox Then
                    Dim txt = CType(ctrl, TextBox)
                    If txt.Name.StartsWith("TXT_") AndAlso Not String.IsNullOrWhiteSpace(txt.Text) Then
                        fieldName = txt.Name.Substring(4) ' "TXT_" entfernen
                        value = txt.Text
                        ' Lambda separat behandeln
                        If fieldName.ToLower() = "lambda" Then
                            lambdaContent = value
                            Continue For
                        End If
                        ' Multiline-Felder (Turn On/Off Actions)
                        If fieldName.ToLower().Contains("action") AndAlso txt.Multiline Then
                            ' Multiline-Aktionen beibehalten
                            value = txt.Text
                        End If
                    End If

                ElseIf TypeOf ctrl Is ComboBox Then
                    Dim cmb = CType(ctrl, ComboBox)
                    If cmb.Name.StartsWith("CMB_") AndAlso cmb.SelectedItem IsNot Nothing Then
                        fieldName = cmb.Name.Substring(4) ' "CMB_" entfernen  
                        value = cmb.SelectedItem.ToString()

                        ' ✅ ENCODING-FIX für ComboBox-Werte
                        If fieldName.ToLower() = "unit_of_measurement" Then
                            ' Kaputte UTF-8 Zeichen reparieren
                            value = value.Replace("â€", "°")    ' UTF-8 → Windows-1252 Fehler
                            value = value.Replace("°", "°")     ' Andere kaputte Grad-Symbole
                            value = value.Replace("â„ƒ", "°C")  ' Weitere mögliche Varianten
                            value = value.Replace("â„‰", "°F")
                            value = value.Trim()

                            ' Standard-Korrekturen
                            If value = "C" Then value = "°C"
                            If value = "F" Then value = "°F"
                            If value = " C" Then value = "°C"
                            If value = " F" Then value = "°F"
                        End If
                    End If

                ElseIf TypeOf ctrl Is CheckBox Then
                    Dim chk = CType(ctrl, CheckBox)
                    If chk.Name.StartsWith("CHK_") Then
                        fieldName = chk.Name.Substring(4) ' "CHK_" entfernen
                        value = If(chk.Checked, "true", "false")
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
            Return (platform, parameters, lambdaContent, sensorClass)

        Catch ex As Exception
            Return ("", "", "# Fehler: " & ex.Message, "")
        End Try
    End Function

    Private Sub UpdateYAMLPreview()
        Try
            Dim templateData = CollectCurrentTemplateData()
            Dim sb As New StringBuilder()

            sb.AppendLine("# Live Preview - Nur dieses Template:")

            ' KORRIGIERT: Verwende WriteUniversalBlockWithSensorType direkt
            WriteUniversalBlockWithSensorType(sb, templateData.SensorClass, templateData.Platform, templateData.Parameters, templateData.Lambda)

            Main.RTB_yamlPreviewTemplate.Text = sb.ToString()

        Catch ex As Exception
            Main.RTB_yamlPreviewTemplate.Text = "# Fehler in der Konfiguration:" & vbCrLf & ex.Message
        End Try
    End Sub

    Private Sub ConfigControl_Changed(sender As Object, e As EventArgs)
        UpdateYAMLPreview()
    End Sub

    Private Sub AddInfoField(fieldName As String, panel As Panel, ByRef yOffset As Integer)
        Dim lbl As New Label With {
            .Text = "ℹ️ " & fieldName,
            .Left = 10,
            .Top = yOffset,
            .Width = 420,
            .ForeColor = Color.Blue,
            .Font = New Font("Arial", 8, FontStyle.Italic),
            .AutoSize = False
        }
        panel.Controls.Add(lbl)
        yOffset += 20
    End Sub

    Public Sub AddTemplateToGrid(templateGroup As String, templateType As String, templateInfo As JObject, panelTemplate As Panel, dgv As DataGridView)
        ' 1. Pflichtfelder prüfen
        If templateInfo("required") IsNot Nothing Then
            For Each reqField In templateInfo("required")
                Dim fieldName = reqField.ToString()
                Dim ctrl = panelTemplate.Controls.Cast(Of Control).FirstOrDefault(Function(c) c.Name.ToLower().EndsWith("_" & fieldName.ToLower()))

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
                        isEmpty = False ' NumericUpDown hat immer einen Wert
                    Case TypeOf ctrl Is CheckBox
                        isEmpty = False ' CheckBox hat immer einen Zustand
                End Select

                If isEmpty Then
                    MessageBox.Show($"Das Pflichtfeld '{fieldName}' muss ausgefüllt werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Next
        End If

        Dim parameter As New List(Of String)

        For Each ctrl As Control In panelTemplate.Controls
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

        ' 2. Template-Daten sammeln
        Dim templateData = CollectCurrentTemplateData()

        ' 3. Plattform und SensorClass ermitteln
        Dim platform As String = templateData.Platform
        Dim sensorClass As String = templateData.SensorClass

        If String.IsNullOrEmpty(platform) Then
            platform = "template" ' Fallback
        End If

        If String.IsNullOrEmpty(sensorClass) Then
            sensorClass = "sensor" ' Fallback
        End If

        ' 4. Zeile zum DataGridView hinzufügen
        If Main.clickedRow > -1 Then
            Try
                Dim row As DataGridViewRow = dgv.Rows(Main.clickedRow)
                row.Cells("Gruppe").Value = templateGroup
                row.Cells("Typ").Value = templateType
                row.Cells("Platform").Value = platform
                row.Cells("Class").Value = sensorClass
                row.Cells("Parameter").Value = templateData.Parameters
                row.Cells("Filter").Value = templateData.Lambda ' Lambda in Filter-Spalte
                MessageBox.Show("Template wurde aktualisiert", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Main.clickedRow = -1

            Catch ex As Exception
                MessageBox.Show($"Fehler beim Aktualisieren: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ' Neue Zeile hinzufügen
            dgv.Rows.Add(templateGroup, templateType, platform, "", templateData.Parameters, templateData.Lambda, sensorClass)
            MessageBox.Show("Template wurde hinzugefügt", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Main.clickedRow = -1

        ' Panel leeren nach erfolgreichem Hinzufügen
        panelTemplate.Controls.Clear()
    End Sub

    Public Sub LoadTemplateForEditing(row As DataGridViewRow, templatePanel As Panel)
        Try
            Dim templateGroup = row.Cells("Gruppe").Value?.ToString()
            Dim templateType = row.Cells("Typ").Value?.ToString()
            Dim parameters = row.Cells("Parameter").Value?.ToString()
            Dim lambdaCode = row.Cells("Filter").Value?.ToString()

            If String.IsNullOrEmpty(templateGroup) OrElse String.IsNullOrEmpty(templateType) Then
                Return
            End If

            ' Template-Info aus JSON laden
            If templateData IsNot Nothing AndAlso templateData.ContainsKey(templateGroup) AndAlso
               templateData(templateGroup)(templateType) IsNot Nothing Then

                Dim templateInfo = templateData(templateGroup)(templateType)

                ' UI-Felder generieren
                GenerateTemplateConfigFields(templateInfo, templatePanel)

                ' Werte aus DataGridView in Controls laden
                LoadParametersIntoControls(parameters, lambdaCode, templatePanel)
            End If

        Catch ex As Exception
            MessageBox.Show($"Fehler beim Laden des Templates: {ex.Message}", "Fehler")
        End Try
    End Sub

    Private Sub LoadParametersIntoControls(parameters As String, lambdaCode As String, panel As Panel)
        ' Parameter-Dictionary erstellen
        Dim paramDict As New Dictionary(Of String, String)

        If Not String.IsNullOrEmpty(parameters) Then
            For Each param In parameters.Split(","c)
                Dim keyVal = param.Trim().Split("="c, 2)
                If keyVal.Length = 2 Then
                    paramDict(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next
        End If

        ' Werte in Controls setzen
        For Each ctrl As Control In panel.Controls
            If Not ctrl.Name.Contains("_"c) Then Continue For

            Dim parts = ctrl.Name.Split("_"c, 2)
            If parts.Length <> 2 Then Continue For

            Dim fieldName = parts(1).ToLower()

            ' Lambda separat behandeln
            If fieldName = "lambda" AndAlso TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = lambdaCode
                Continue For
            End If

            ' Normale Parameter
            If paramDict.ContainsKey(fieldName) Then
                Dim value = paramDict(fieldName)

                Select Case True
                    Case TypeOf ctrl Is TextBox
                        CType(ctrl, TextBox).Text = value
                    Case TypeOf ctrl Is ComboBox
                        Dim cmb = CType(ctrl, ComboBox)
                        Dim index = cmb.Items.IndexOf(value)
                        If index >= 0 Then cmb.SelectedIndex = index
                    Case TypeOf ctrl Is CheckBox
                        CType(ctrl, CheckBox).Checked = (value.ToLower() = "true")
                    Case TypeOf ctrl Is NumericUpDown
                        Dim numValue As Decimal
                        If Decimal.TryParse(value, numValue) Then
                            CType(ctrl, NumericUpDown).Value = numValue
                        End If
                End Select
            End If
        Next
    End Sub

    Public Sub ExportTemplateToYaml(sb As StringBuilder, dgv As DataGridView)
        If dgv.Rows.Count = 0 Then Exit Sub

        ' Gruppiere Templates nach sensor_class
        Dim templateGroups As New Dictionary(Of String, List(Of DataGridViewRow))

        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For

            Dim sensorClass = row.Cells("Class").Value?.ToString()?.Trim().ToLower()
            If String.IsNullOrEmpty(sensorClass) Then sensorClass = "sensor"

            If Not templateGroups.ContainsKey(sensorClass) Then
                templateGroups(sensorClass) = New List(Of DataGridViewRow)
            End If
            templateGroups(sensorClass).Add(row)
        Next

        ' Schreibe Templates gruppiert nach Typ
        For Each group In templateGroups
            Dim sensorType = group.Key
            Dim rows = group.Value

            ' Header für Template-Typ
            Select Case sensorType
                Case "sensor"
                    sb.AppendLine("sensor:")
                Case "binary_sensor"
                    sb.AppendLine("binary_sensor:")
                Case "text_sensor"
                    sb.AppendLine("text_sensor:")
                Case "switch"
                    sb.AppendLine("switch:")
                Case Else
                    sb.AppendLine("sensor:")
            End Select

            ' Alle Templates dieses Typs
            For Each row In rows
                Dim platform = row.Cells("Platform").Value?.ToString()?.Trim()
                Dim paramString = row.Cells("Parameter").Value?.ToString()?.Trim()
                Dim lambdaContent = row.Cells("Filter").Value?.ToString()?.Trim()

                If String.IsNullOrEmpty(platform) Then Continue For

                WriteTemplateSingleBlock(sb, platform, paramString, lambdaContent)
            Next

            sb.AppendLine() ' Leerzeile zwischen Template-Typen
        Next
    End Sub

    Private Sub WriteTemplateSingleBlock(sb As StringBuilder, platform As String, paramString As String, lambdaContent As String)

        WriteUniversalBlock(sb, BlockType.Template, platform, paramString, lambdaContent)
    End Sub


    Private Sub WriteTemplateBlock(sb As StringBuilder, sensorClass As String, platform As String, paramString As String, lambdaContent As String)

        WriteUniversalBlockWithSensorType(sb, sensorClass, platform, paramString, lambdaContent)
    End Sub



End Module
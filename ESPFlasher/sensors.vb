Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.Json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module sensors
    Public sensorData As JObject


    Function LoadSensorData() As Task
        Dim filePath = Path.Combine(Application.StartupPath, "sensors.json")
        If Not File.Exists(filePath) Then
            MessageBox.Show("Die sensors.json wurde nicht gefunden.")
            Return Task.CompletedTask
            Exit Function
        End If

        Dim json = File.ReadAllText(filePath)
        sensorData = JObject.Parse(json)

        ' Hauptgruppen in ComboBox1 laden
        Main.CBB_SensoreGroup.Items.Clear()
        For Each group In sensorData.Properties()
            Main.CBB_SensoreGroup.Items.Add(group.Name)
        Next
        Return Task.CompletedTask
    End Function




    Public Sub GenerateSensorConfigFields(sensorInfo As JObject, targetPanel As Panel)
        targetPanel.Controls.Clear()
        Dim yOffset As Integer = 10

        Dim uiFields As JObject = Nothing
        If sensorInfo("ui_fields") IsNot Nothing Then
            uiFields = CType(sensorInfo("ui_fields"), JObject)
        End If

        ' Required Felder
        If sensorInfo("required") IsNot Nothing Then
            For Each r In sensorInfo("required")
                Dim fieldName = r.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddSensorField(fieldName, True, targetPanel, yOffset, fieldConfig)
            Next
        End If

        ' Optional Felder
        If sensorInfo("optional") IsNot Nothing Then
            For Each o In sensorInfo("optional")
                Dim fieldName = o.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddSensorField(fieldName, False, targetPanel, yOffset, fieldConfig)
            Next
        End If

        ' Verschachtelte Felder (nested_fields)
        If sensorInfo("nested_fields") IsNot Nothing Then
            Dim nestedFields As JObject = CType(sensorInfo("nested_fields"), JObject)

            For Each section In nestedFields
                Dim prefix As String = section.Key
                Dim fieldList As JArray = CType(section.Value, JArray)

                For Each field In fieldList
                    Dim fullFieldName As String = $"{prefix}_{field.ToString()}"
                    Dim fieldConfig As JObject = Nothing

                    If uiFields IsNot Nothing AndAlso uiFields(fullFieldName) IsNot Nothing Then
                        fieldConfig = CType(uiFields(fullFieldName), JObject)
                    End If

                    AddSensorField(fullFieldName, False, targetPanel, yOffset, fieldConfig)
                Next
            Next
        End If

        ' Info-Felder
        If sensorInfo("info") IsNot Nothing Then
            For Each o In sensorInfo("info")
                AddInfoField(o.ToString(), targetPanel, yOffset)
            Next
        End If



    End Sub



    Public Sub AddSensorField(fieldName As String, isRequired As Boolean, targetPanel As Panel, ByRef yOffset As Integer, Optional fieldConfig As JObject = Nothing)
        Dim label As New Label With {
        .Text = If(isRequired, "* ", "") & fieldName,
        .Location = New Point(10, yOffset),
        .AutoSize = True
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
    Private Function CollectCurrentSensorData() As (Platform As String, Parameters As String, Filters As String, SensorClass As String)
        Try
            Dim platform As String = ""
            Dim sensorClass As String = "sensor"
            Dim parameterList As New List(Of String)


            If Main.CBB_SensoreGroup.SelectedItem IsNot Nothing AndAlso Main.CBB_SensorType.SelectedItem IsNot Nothing Then
                Dim selectedGroup = Main.CBB_SensoreGroup.SelectedItem.ToString()
                Dim selectedSensor = Main.CBB_SensorType.SelectedItem.ToString()

                Dim jsonText = File.ReadAllText(Application.StartupPath & "\sensors.json")
                Dim jsonData = JObject.Parse(jsonText)

                If jsonData.ContainsKey(selectedGroup) AndAlso jsonData(selectedGroup)(selectedSensor) IsNot Nothing Then
                    Dim sensorInfo = jsonData(selectedGroup)(selectedSensor)
                    platform = sensorInfo("platform")?.ToString()
                    sensorClass = If(sensorInfo("sensor_class")?.ToString(), "sensor")
                End If
            End If


            For Each ctrl As Control In Main.pnl_SensorConfig.Controls
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

            Return (platform, parameters, filterString, sensorClass)

        Catch ex As Exception
            Return ("", "", "# Fehler: " & ex.Message, "")
        End Try
    End Function
    Private Sub UpdateYAMLPreview()
        Try
            Dim sensorData = CollectCurrentSensorData()
            Dim sb As New StringBuilder()

            sb.AppendLine("# Live Preview - Nur dieser Sensor:")

            Select Case sensorData.SensorClass.ToLower()
                Case "binary_sensor"
                    sb.AppendLine("binary_sensor:")
                Case "sensor"
                    sb.AppendLine("sensor:")
                Case Else
                    sb.AppendLine("sensor:")
            End Select

            ' KORRIGIERT: Richtige Parameter-Reihenfolge
            WriteSensorBlock(sb, sensorData.Platform, sensorData.Parameters, sensorData.Filters)

            Main.RTB_yamlPreviewSensor.Text = sb.ToString()

        Catch ex As Exception
            Main.RTB_yamlPreviewSensor.Text = "# Fehler in der Konfiguration:" & vbCrLf & ex.Message
        End Try
    End Sub
    Private Sub ConfigControl_Changed(sender As Object, e As EventArgs)
        UpdateYAMLPreview()
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


    Public Sub AddSensorToGrid(sensorGroup As String, sensorType As String, sensorInfo As JObject, panelSensor As Panel, dgv As DataGridView)




        ' 1. Pflichtfelder prüfen
        If sensorInfo("required") IsNot Nothing Then
            For Each reqField In sensorInfo("required")
                Dim fieldName = reqField.ToString()
                Dim ctrl = panelSensor.Controls.Cast(Of Control).FirstOrDefault(Function(c) c.Name.ToLower().EndsWith("_" & fieldName.ToLower()))

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

                    If Main.clickedRow = -1 Then
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

        For Each ctrl As Control In panelSensor.Controls
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
        Dim sensorClass As String = "sensor"
        If sensorInfo("platform") IsNot Nothing Then
            platform = sensorInfo("platform").ToString()
        End If
        If sensorInfo("sensor_class") IsNot Nothing Then
            sensorClass = sensorInfo("sensor_class").ToString()
        End If


        ' 4. Zeile zum DataGridView hinzufügen
        If Main.clickedRow > -1 Then
            Try
                Dim row As DataGridViewRow = dgv.Rows(Main.clickedRow)
                row.Cells("Gruppe").Value = sensorGroup
                row.Cells("Typ").Value = sensorType
                row.Cells("Platform").Value = platform
                row.Cells("Class").Value = sensorClass
                row.Cells("Parameter").Value = String.Join(",", parameter)
                MsgBox("Sensor wurde gespeichert")
                Main.clickedRow = -1

            Catch ex As Exception

            End Try
        Else
            dgv.Rows.Add(sensorGroup, sensorType, platform, "", String.Join(", ", parameter), "", sensorClass)

        End If

        Main.clickedRow = -1
    End Sub

    Public Sub ExportSensorsToYaml(sb As StringBuilder, dgv As DataGridView)
        If dgv.Rows.Count = 0 Then Exit Sub

        Dim sensorGroups As New Dictionary(Of String, List(Of DataGridViewRow))

        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For

            Dim sensorClass = row.Cells("Class").Value?.ToString()?.Trim().ToLower()
            If String.IsNullOrEmpty(sensorClass) Then sensorClass = "sensor"

            If Not sensorGroups.ContainsKey(sensorClass) Then
                sensorGroups(sensorClass) = New List(Of DataGridViewRow)
            End If
            sensorGroups(sensorClass).Add(row)
        Next

        For Each group In sensorGroups
            Dim sensorType = group.Key
            Dim rows = group.Value

            Select Case sensorType
                Case "sensor"
                    sb.AppendLine("sensor:")
                Case "binary_sensor"
                    sb.AppendLine("binary_sensor:")
                Case Else
                    sb.AppendLine("sensor:")
            End Select

            For Each row In rows
                Dim platform = row.Cells("Platform").Value?.ToString()?.Trim()
                Dim paramString = row.Cells("Parameter").Value?.ToString()?.Trim()
                Dim filterString = row.Cells("Filter").Value?.ToString()?.Trim()
                Dim sensorClass = row.Cells("Class").Value?.ToString()?.Trim()

                If String.IsNullOrEmpty(platform) Then Continue For

                ' KORRIGIERT: Richtige Parameter-Reihenfolge
                WriteUniversalBlockWithSensorType(sb, sensorClass, platform, paramString, filterString)
            Next

            sb.AppendLine()
        Next
    End Sub


    Private Sub WriteSensorBlock(sb As StringBuilder, platform As String, paramString As String, filterCellContent As String)
        ' Verwende das universelle System
        WriteUniversalBlockWithSensorType(sb, "sensor", platform, paramString, filterCellContent)
    End Sub


End Module

Public Class SensorModel
    Public Property name As String
    Public Property platform As String
    Public Property description As String
    Public Property bus As String
End Class





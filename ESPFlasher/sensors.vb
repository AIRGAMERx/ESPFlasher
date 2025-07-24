Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.Json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module sensors
    Public sensorData As JObject
    Dim GBLocation As New Point(480, 17)

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
        Form1.CBB_SensoreGroup.Items.Clear()
        For Each group In sensorData.Properties()
            Form1.CBB_SensoreGroup.Items.Add(group.Name)
        Next
        Return Task.CompletedTask
    End Function
    Public Function LoadSensorsFromJson(path As String) As List(Of SensorCategory)
        Dim json = File.ReadAllText(path)
        Return System.Text.Json.JsonSerializer.Deserialize(Of List(Of SensorCategory))(json)
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



        If sensorInfo("bus") IsNot Nothing Then

            For Each o In sensorInfo("bus")

                Dim busValue As String = o.ToString().ToLower()


                Select Case busValue
                    Case "onewire"
                        Form1.GB_OneWire.Visible = True
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = False
                        Form1.GB_OneWire.Location = GBLocation

                    Case "i2c"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = True
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = False
                        Form1.GB_I2C.Location = GBLocation

                    Case "spi"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = True
                        Form1.GB_Uart.Visible = False
                        Form1.GB_SPI.Location = GBLocation

                    Case "uart"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = True
                        Form1.GB_Uart.Location = GBLocation

                    Case "na"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = False
                End Select


            Next
        Else
            Form1.GB_OneWire.Visible = False
            Form1.GB_I2C.Visible = False
            Form1.GB_SPI.Visible = False


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

            Case Else
                ' Fallback: TextBox
                control = New TextBox With {
                .Name = "TXT_" & fieldName,
                .Location = New Point(150, yOffset),
                .Width = 150
            }
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

    Private Function IsPinInUse(pin As String, dgv As DataGridView) As Boolean
        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For
            Dim paramString As String = row.Cells("Parameter").Value?.ToString()
            If String.IsNullOrWhiteSpace(paramString) Then Continue For

            If paramString.Contains($"pin={pin}") OrElse
           paramString.Contains($"trigger_pin={pin}") OrElse
           paramString.Contains($"echo_pin={pin}") OrElse
           paramString.Contains($"cs_pin={pin}") Then
                Return True
            End If
        Next
        Return False
    End Function
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
                    MsgBox(Form1.clickedRow.ToString)
                    If Form1.clickedRow = -1 Then
                        If Not String.IsNullOrEmpty(pinValue) AndAlso IsPinInUse(pinValue, dgv) Then
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
        If sensorInfo("plattform") IsNot Nothing Then
            platform = sensorInfo("plattform").ToString()
        End If

        ' 4. Zeile zum DataGridView hinzufügen
        If Form1.clickedRow > -1 Then
            Try

                Dim row As DataGridViewRow = dgv.Rows(Form1.clickedRow)
                row.Cells(0).Value = sensorGroup
                row.Cells(1).Value = sensorType
                row.Cells(2).Value = platform
                row.Cells(3).Value = ""
                row.Cells(4).Value = String.Join(",", parameter)
                MsgBox("Sensor wurde gespeichert")

            Catch ex As Exception

            End Try
        Else
            dgv.Rows.Add(sensorGroup, sensorType, platform, "", String.Join(", ", parameter))

        End If

        Form1.clickedRow = -1
    End Sub


    Public Function ParseSensorsFromGrid(dgv As DataGridView) As List(Of SensorExportModel)
        Dim grouped As New Dictionary(Of String, SensorExportModel)

        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For

            Dim plattform = row.Cells("Plattform").Value?.ToString()?.Trim()
            If String.IsNullOrEmpty(plattform) Then Continue For

            If Not grouped.ContainsKey(plattform) Then
                grouped(plattform) = New SensorExportModel With {
                .Plattform = plattform,
                .GlobalPins = New Dictionary(Of String, String),
                .SensorEntries = New List(Of Dictionary(Of String, String))
            }
            End If

            ' Pins (auch globale)
            Dim pinsDict As New Dictionary(Of String, String)
            Dim pinsRaw = row.Cells("Pins").Value?.ToString()
            If Not String.IsNullOrEmpty(pinsRaw) Then
                For Each p In pinsRaw.Split(","c)
                    Dim kv = p.Split("="c)
                    If kv.Length = 2 Then
                        Dim key = kv(0).Trim()
                        Dim val = kv(1).Trim()
                        pinsDict(key) = val

                        ' Prüfe auf global bekannte Pins
                        If key.Contains("sda") Or key.Contains("scl") Or key.Contains("dallas_pin") Or key.Contains("spi_") Then
                            If Not grouped(plattform).GlobalPins.ContainsKey(key) Then
                                grouped(plattform).GlobalPins.Add(key, val)
                            End If
                        End If
                    End If
                Next
            End If

            ' Parameter (optional)
            Dim paramDict As New Dictionary(Of String, String)(pinsDict)
            Dim paramRaw = row.Cells("Parameter").Value?.ToString()
            If Not String.IsNullOrEmpty(paramRaw) Then
                For Each p In paramRaw.Split(","c)
                    Dim kv = p.Split("="c)
                    If kv.Length = 2 Then
                        paramDict(kv(0).Trim()) = kv(1).Trim()
                    End If
                Next
            End If

            grouped(plattform).SensorEntries.Add(paramDict)
        Next

        Return grouped.Values.ToList()
    End Function
    Public Sub ExportSensorsToYaml(sb As StringBuilder, dgv As DataGridView)
        If dgv.Rows.Count = 0 Then Exit Sub

        Dim sensorsExist As Boolean = False

        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For

            Dim platform = row.Cells("Plattform").Value?.ToString()?.Trim()
            Dim paramString = row.Cells("Parameter").Value?.ToString()?.Trim()
            Dim filterString = row.Cells("Filter").Value?.ToString?.Trim()

            If String.IsNullOrEmpty(platform) Then Continue For

            ' Sensor-Header nur einmal schreiben
            If Not sensorsExist Then
                sb.AppendLine("sensor:")
                sensorsExist = True
            End If


            WriteSensorBlock(sb, platform, paramString, filterString)
        Next
    End Sub


    Public Function KeyValuePairsFun(rowIndex As Integer, dgv As DataGridView) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)
        Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
        If String.IsNullOrWhiteSpace(completeParams) Then Return dict
        For Each part In completeParams.Split(","c)
            Dim keyVal = part.Trim().Split("="c, 2)
            If keyVal.Length = 2 Then dict(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
        Next
        Return dict
    End Function

    Private Function BuildParamTree(paramString As String) As Dictionary(Of String, Object)
        Dim root As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

        If String.IsNullOrWhiteSpace(paramString) Then Return root

        For Each part In paramString.Split(","c)
            Dim kv = part.Split("="c, 2)
            If kv.Length <> 2 Then Continue For

            Dim rawKey = kv(0).Trim()
            Dim rawVal = kv(1).Trim()

            Dim path = rawKey.Split("."c)
            Dim node = root

            For i = 0 To path.Length - 1
                Dim key = path(i).Trim()
                Dim isLeaf = (i = path.Length - 1)

                If isLeaf Then
                    node(key) = rawVal
                Else
                    If Not node.ContainsKey(key) OrElse Not TypeOf node(key) Is Dictionary(Of String, Object) Then
                        node(key) = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
                    End If
                    node = CType(node(key), Dictionary(Of String, Object))
                End If
            Next
        Next

        Return root
    End Function
    Private Sub WriteYamlNode(sb As StringBuilder, level As Integer, key As String, value As Object)
        Dim indent = New String(" "c, level * 2)

        If TypeOf value Is String Then
            sb.AppendLine($"{indent}{key}: {value}")
        ElseIf TypeOf value Is Dictionary(Of String, Object) Then
            sb.AppendLine($"{indent}{key}:")
            For Each kvp In CType(value, Dictionary(Of String, Object))
                WriteYamlNode(sb, level + 1, kvp.Key, kvp.Value)
            Next
        End If
    End Sub
    Private Sub WriteSensorBlock(sb As StringBuilder, platform As String, paramString As String, filterCellContent As String)
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

        Dim flatKeys As New HashSet(Of String) From {
        "update_interval", "timeout", "accuracy_decimals", "echo_pin", "trigger_pin", "pulse_time",
        "shunt_resistance", "max_voltage", "max_current", "bus_voltage", "shunt_voltage",
        "cs_pin", "internal_filter", "unit_of_measurement", "accuracy_decimals"
    }

        Dim accuracyFixups = New Dictionary(Of String, Tuple(Of String, String)) From {
        {"temperature_accuracy_decimals", Tuple.Create("temperature", "accuracy_decimals")},
        {"humidity_accuracy_decimals", Tuple.Create("humidity", "accuracy_decimals")}
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

        ' Count Mode
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

        ' Einheiten
        Dim unit_s = New HashSet(Of String) From {"update_interval", "timeout"}
        Dim unit_ms = New HashSet(Of String) From {"pulse_time"}
        Dim unit_v = New HashSet(Of String) From {"max_voltage"}
        Dim unit_a = New HashSet(Of String) From {"max_current"}
        Dim unit_ohm = New HashSet(Of String) From {"shunt_resistance"}
        Dim unit_us = New HashSet(Of String) From {"internal_filter"}

        ' Flache Felder
        For Each kvp In simpleKeys
            Dim key = kvp.Key
            Dim val = kvp.Value

            If unit_s.Contains(key) Then
                sb.AppendLine($"    {key}: {val}s")
            ElseIf unit_ms.Contains(key) Then
                sb.AppendLine($"    {key}: {val}ms")
            ElseIf unit_v.Contains(key) Then
                sb.AppendLine($"    {key}: {val}V")
            ElseIf unit_a.Contains(key) Then
                sb.AppendLine($"    {key}: {val}A")
            ElseIf unit_ohm.Contains(key) Then
                sb.AppendLine($"    {key}: {val} ohm")
            ElseIf unit_us.Contains(key) Then
                sb.AppendLine($"    {key}: {val}us")
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

        ' Nested Felder schreiben (inkl. Filter)
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

        ' Falls keine Nested vorhanden → globaler Filterblock
        If nestedDict.Count = 0 AndAlso filterData IsNot Nothing AndAlso filterData.ContainsKey("global") Then
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

        sb.AppendLine()
    End Sub






End Module

Public Class SensorModel
    Public Property name As String
    Public Property platform As String
    Public Property description As String
    Public Property bus As String
End Class

Public Class SensorCategory
    Public Property category As String
    Public Property sensors As List(Of SensorModel)
End Class

Public Class SensorExportModel
    Public Property Plattform As String
    Public Property GlobalPins As Dictionary(Of String, String)
    Public Property SensorEntries As List(Of Dictionary(Of String, String))
End Class
#Region "GPIO"
Public Class DS18B20Model
    Public Property name As String
    Public Property platform As String
    Public Property adress As String
    Public Property onewireid As String
    Public Property update_interval

End Class


Public Class DHT
    Public Property platform As String
    Public Property pin As String
    Public Property tempname As String
    Public Property humname As String
    Public Property update_interval As String
    Public Property model As String
End Class

Public Class HCSR04
    Public Property platform As String
    Public Property trigger As String
    Public Property echo As String
    Public Property name As String
    Public Property timeout As String
    Public Property update_interval As String
    Public Property pulse_time As String
    Public Property id As String

End Class
#End Region
#Region "I2C"


Public Class BMP280
    Public Property platform As String
    Public Property tempname As String
    Public Property tempoversampling As String
    Public Property pressurename As String
    Public Property pressuroversampling As String
    Public Property adress As String
    Public Property update_interval As String


End Class

Public Class BME280
    Public Property platform As String
    Public Property tempname As String
    Public Property pressurename As String
    Public Property humidityname As String
    Public Property adress As String
    Public Property update_interval As String
End Class

Public Class BH1750
    Public Property platform As String
    Public Property name As String
    Public Property adress As String
    Public Property update_interval As String

End Class

Public Class SHT3XD
    Public Property platform As String
    Public Property tempname As String
    Public Property humidityname As String
    Public Property adress As String
    Public Property update_interval As String

End Class

Public Class INA219

    Public Property platform As String
    Public Property adress As String
    Public Property shunt_resistance As String
    Public Property currentname As String
    Public Property powername As String
    Public Property bus_voltagename As String
    Public Property shunt_voltagename As String
    Public Property max_voltage As String
    Public Property max_current As String
    Public Property update_interval As String

End Class
#End Region
#Region "SPI"
Public Class MAX6675
    Public Property platform As String
    Public Property cs As String
    Public Property name As String
    Public Property update_interval As String

End Class

Public Class MAX31855
    Public Property platform As String
    Public Property cs As String
    Public Property name As String
    Public Property update_interval As String

End Class

Public Class InternalSensor
    Public Property platform As String
    Public Property name As String
    Public Property update_interval As String
End Class
#End Region


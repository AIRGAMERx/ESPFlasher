Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.Json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module sensors
    Public sensorData As JObject


    Public Sub LoadSensorData()
        Dim filePath = Path.Combine(Application.StartupPath, "sensors.json")
        If Not File.Exists(filePath) Then
            MessageBox.Show("Die sensors.json wurde nicht gefunden.")
            Exit Sub
        End If

        Dim json = File.ReadAllText(filePath)
        sensorData = JObject.Parse(json)

        ' Hauptgruppen in ComboBox1 laden
        Form1.CBB_SensoreGroup.Items.Clear()
        For Each group In sensorData.Properties()
            Form1.CBB_SensoreGroup.Items.Add(group.Name)
        Next
    End Sub
    Public Function LoadSensorsFromJson(path As String) As List(Of SensorCategory)
        Dim json = File.ReadAllText(path)
        Return System.Text.Json.JsonSerializer.Deserialize(Of List(Of SensorCategory))(json)
    End Function



    Public Sub GenerateSensorConfigFields(sensorInfo As JObject, targetPanel As Panel)
        ' Panel leeren
        targetPanel.Controls.Clear()

        Dim yOffset As Integer = 10

        ' Required
        If sensorInfo("required") IsNot Nothing Then
            For Each r In sensorInfo("required")
                AddSensorField(r.ToString(), True, targetPanel, yOffset)
            Next
        End If

        ' Optional
        If sensorInfo("optional") IsNot Nothing Then
            For Each o In sensorInfo("optional")
                AddSensorField(o.ToString(), False, targetPanel, yOffset)
            Next
        End If

        ' Info
        If sensorInfo("info") IsNot Nothing Then
            For Each o In sensorInfo("info")
                AddInfoField(o.ToString, targetPanel, yOffset)
            Next
        End If
    End Sub

    Private Sub AddSensorField(fieldName As String, isRequired As Boolean, panel As Panel, ByRef yOffset As Integer)
        Dim lbl As New Label With {
        .Text = If(isRequired, $"{fieldName} (erforderlich)", fieldName),
        .Left = 0,
        .Top = yOffset,
        .Width = 100
    }
        panel.Controls.Add(lbl)

        Dim txt As New TextBox With {
        .Name = $"txt_{fieldName}",
        .Left = 115,
        .Top = yOffset,
        .Width = 150
    }
        If Not isRequired Then txt.BackColor = Color.LightYellow

        panel.Controls.Add(txt)

        yOffset += 30
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





    ' Public Sub ShowGlobalBusFields(sensorTyp As String, parentPanel As Panel)
    '    parentPanel.Controls.Clear()
    'Dim yOffset As Integer = 10




    'Case "bme280", "bmp280", "sht3xd", "max44009", "ina219", "bh1750"
    '           AddGlobalField("SDA", "i2c_sda", parentPanel, yOffset)
    '          AddGlobalField("SCL", "i2c_scl", parentPanel, yOffset)

    'Case "max6675", "max31855"
    '           AddGlobalField("CLK", "spi_clk", parentPanel, yOffset)
    '          AddGlobalField("MISO", "spi_miso", parentPanel, yOffset)

    'Case Else
    ' Kein globaler Bus nötig
    'End Select
    'End Sub

    Public Sub AddSensorToGrid(sensorGroup As String, sensorType As String, sensorInfo As JObject, panelSensor As Panel, dgv As DataGridView)

        ' 1. Pflichtfeldprüfung
        If sensorInfo("required") IsNot Nothing Then
            For Each reqField In sensorInfo("required")
                Dim fieldName = reqField.ToString()
                Dim ctrl = panelSensor.Controls.Find($"txt_{fieldName}", True).FirstOrDefault()
                If ctrl Is Nothing OrElse String.IsNullOrWhiteSpace(ctrl.Text) Then
                    MessageBox.Show($"Das Pflichtfeld '{fieldName}' muss ausgefüllt werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Next
        End If

        ' 2. Sensor-Pins + Parameter zusammensetzen
        Dim pins As New List(Of String)
        Dim parameter As New List(Of String)

        ' Sensor-spezifische Felder
        For Each ctrl In panelSensor.Controls.OfType(Of TextBox)
            If Not String.IsNullOrWhiteSpace(ctrl.Text) Then
                pins.Add($"{ctrl.Name.Replace("txt_", "")}={ctrl.Text}")
            End If
        Next





        ' 3. Plattform ermitteln
        Dim platform As String = ""
        If sensorInfo("plattform") IsNot Nothing Then
            platform = sensorInfo("plattform").ToString()
        End If

        ' 4. Zeile hinzufügen
        dgv.Rows.Add(sensorGroup, sensorType, platform, String.Join(", ", pins), String.Join(", ", parameter))

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
        Dim alreadysensors As Boolean = False

        If Form1.OneWire = True Then
            sb.AppendLine("one_wire:")
            sb.AppendLine($"  - platform: gpio")
            sb.AppendLine($"    pin: GPIO{Form1.OneWirePIN}")
            sb.AppendLine($"    id: {Form1.OneWireID}")
            sb.AppendLine()
            sb.AppendLine()

        End If

        If Form1.i2c = True Then
            Dim scan As String = "false"
            If Form1.i2cScan = True Then scan = "true" Else scan = "false"

            sb.AppendLine("i2c:")
            sb.AppendLine($"  - id: i2c_bus")
            sb.AppendLine($"    sda: {Form1.i2cSda}")
            sb.AppendLine($"    scl: {Form1.i2cScl}")
            sb.AppendLine($"    scan: {scan}")
            sb.AppendLine()
            sb.AppendLine()
        End If

        If alreadysensors = False Then sb.AppendLine("sensor:")
            alreadysensors = True



        Dim ds18b20list As New List(Of DS18B20Model)
        Dim dhtlist As New List(Of DHT)
        Dim hcsr04list As New List(Of HCSR04)
        Dim bmp280list As New List(Of BMP280)
        Dim bme280list As New List(Of BME280)
        Dim bh1750list As New List(Of BH1750)
        Dim sht3xdlist As New List(Of SHT3XD)




        For i = 0 To dgv.Rows.Count - 1
                Dim row = dgv.Rows(i)


            If row.IsNewRow OrElse row.Cells(1).Value Is Nothing Then Continue For


            Dim rowcontent As String = row.Cells(1).Value.ToString


            Select Case True
                Case rowcontent = "DS18B20"
                    Dim sensor = ds18b20Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        ds18b20list.Add(sensor)
                    End If
                Case rowcontent = "DHT11/DHT22"
                    Dim sensor = dhtHelper(i, dgv)
                    If sensor IsNot Nothing Then
                        dhtlist.Add(sensor)
                    End If
                Case rowcontent = "HC-SR04"
                    Dim sensor = hcsr04Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        hcsr04list.Add(sensor)
                    End If
                Case rowcontent = "BMP280"
                    Dim sensor = bmp280Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        bmp280list.Add(sensor)
                    End If
                Case rowcontent = "BME280"
                    Dim sensor = bme280Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        bme280list.Add(sensor)
                    End If
                Case rowcontent = "BH1750"
                    Dim sensor = bh1750Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        bh1750list.Add(sensor)
                    End If
                Case rowcontent = "SHT3X-D"
                    Dim sensor = sht3xdHelper(i, dgv)
                    If sensor IsNot Nothing Then
                        sht3xdlist.Add(sensor)
                    End If
            End Select





        Next


        For Each sensor As DS18B20Model In ds18b20list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.adress) AndAlso Not sensor.adress = "0" Then sb.AppendLine($"    address: {sensor.adress}")
            If Not String.IsNullOrEmpty(sensor.name) Then sb.AppendLine($"    name: {sensor.name}")
            If Not String.IsNullOrEmpty(sensor.onewireid) Then sb.AppendLine($"    one_wire_id: {sensor.onewireid}")
            If Not String.IsNullOrEmpty(sensor.updateinterval) Then sb.AppendLine($"    update_interval: {sensor.updateinterval}s")
            sb.AppendLine()
        Next


        For Each sensor As DHT In dhtlist
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.pin) Then sb.AppendLine($"    pin: {sensor.pin}")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"    temperature:")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"      name: {sensor.tempname}")
            If Not String.IsNullOrEmpty(sensor.humname) Then sb.AppendLine($"    humidity:")
            If Not String.IsNullOrEmpty(sensor.humname) Then sb.AppendLine($"      name: {sensor.humname}")
            If Not String.IsNullOrEmpty(sensor.updateinterval) Then sb.AppendLine($"    update_interval: {sensor.updateinterval}s")
            If Not String.IsNullOrEmpty(sensor.model) Then sb.AppendLine($"    model: {sensor.model}")
            sb.AppendLine()
        Next


        For Each sensor As HCSR04 In hcsr04list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.trigger) Then sb.AppendLine($"    trigger_pin: {sensor.trigger}")
            If Not String.IsNullOrEmpty(sensor.echo) Then sb.AppendLine($"    echo_pin: {sensor.echo}")
            If Not String.IsNullOrEmpty(sensor.name) Then sb.AppendLine($"    name: {sensor.name}")
            If Not String.IsNullOrEmpty(sensor.timeout) Then sb.AppendLine($"    timeout: {sensor.timeout}m")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            If Not String.IsNullOrEmpty(sensor.pulse_time) Then sb.AppendLine($"    pulse_time: {sensor.pulse_time}ms")
            If Not String.IsNullOrEmpty(sensor.id) Then sb.AppendLine($"    id: {sensor.id}")
            sb.AppendLine()
        Next

        For Each sensor As BMP280 In bmp280list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"    temperature:")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"      name: {sensor.tempname}")
            If Not String.IsNullOrEmpty(sensor.tempoversampling) Then sb.AppendLine($"      oversampling: {sensor.tempoversampling}")
            If Not String.IsNullOrEmpty(sensor.pressurename) Then sb.AppendLine($"    pressure:")
            If Not String.IsNullOrEmpty(sensor.pressurename) Then sb.AppendLine($"      name: {sensor.pressurename}")
            If Not String.IsNullOrEmpty(sensor.pressuroversampling) Then sb.AppendLine($"      oversampling: {sensor.pressuroversampling}")
            If Not String.IsNullOrEmpty(sensor.adress) Then sb.AppendLine($"    address: {sensor.adress}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next

        For Each sensor As BME280 In bme280list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"    temperature:")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"      name: {sensor.tempname}")
            If Not String.IsNullOrEmpty(sensor.pressurename) Then sb.AppendLine($"    pressure:")
            If Not String.IsNullOrEmpty(sensor.pressurename) Then sb.AppendLine($"      name: {sensor.pressurename}")
            If Not String.IsNullOrEmpty(sensor.humidityname) Then sb.AppendLine($"    humidity:")
            If Not String.IsNullOrEmpty(sensor.humidityname) Then sb.AppendLine($"      name: {sensor.humidityname}")
            If Not String.IsNullOrEmpty(sensor.adress) Then sb.AppendLine($"    address: {sensor.adress}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next

        For Each sensor As BH1750 In bh1750list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.name) Then sb.AppendLine($"    name: {sensor.name}")
            If Not String.IsNullOrEmpty(sensor.adress) Then sb.AppendLine($"    address: {sensor.adress}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next

        For Each sensor As SHT3XD In sht3xdlist
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"    temperature:")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"      name: {sensor.tempname}")
            If Not String.IsNullOrEmpty(sensor.humidityname) Then sb.AppendLine($"    humidity:")
            If Not String.IsNullOrEmpty(sensor.humidityname) Then sb.AppendLine($"      name: {sensor.humidityname}")
            If Not String.IsNullOrEmpty(sensor.adress) Then sb.AppendLine($"    address: {sensor.adress}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next





    End Sub

    Private Function ds18b20Helper(rowIndex As Integer, dgv As DataGridView) As DS18B20Model
        Try
            Dim sensor As New DS18B20Model

            sensor.platform = dgv.Rows(rowIndex).Cells(2).Value?.ToString()
            sensor.onewireid = Form1.OneWireID

            Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
            If String.IsNullOrWhiteSpace(completeParams) Then Return sensor

            Dim keyValuePairs As New Dictionary(Of String, String)
            Dim parts = completeParams.Split(","c)

            For Each part In parts
                Dim trimmed = part.Trim()
                Dim keyVal = trimmed.Split("="c, 2)
                If keyVal.Length = 2 Then
                    keyValuePairs(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next

            If keyValuePairs.ContainsKey("name") Then
                sensor.name = keyValuePairs("name")
            End If

            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address") ' 
            End If

            If keyValuePairs.ContainsKey("update_interval") Then
                sensor.updateinterval = keyValuePairs("update_interval") ' 
            End If

            Return sensor

        Catch ex As Exception
            MsgBox($"Sensor aus der Zeile {rowIndex} konnte nicht gelesen werden: {ex.Message}")
            Return Nothing
        End Try
    End Function
    Private Function dhtHelper(rowIndex As Integer, dgv As DataGridView) As DHT
        Try
            Dim sensor As New DHT

            sensor.platform = "dht"


            Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
            If String.IsNullOrWhiteSpace(completeParams) Then Return sensor

            Dim keyValuePairs As New Dictionary(Of String, String)
            Dim parts = completeParams.Split(","c)


            For Each part In parts
                Dim trimmed = part.Trim()
                Dim keyVal = trimmed.Split("="c, 2)
                If keyVal.Length = 2 Then
                    keyValuePairs(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next

            If keyValuePairs.ContainsKey("pin") Then
                sensor.pin = keyValuePairs("pin")
            End If

            If keyValuePairs.ContainsKey("tempname") Then
                sensor.tempname = keyValuePairs("tempname")
            End If

            If keyValuePairs.ContainsKey("humidityName") Then
                sensor.humname = keyValuePairs("humidityName")
            End If

            If keyValuePairs.ContainsKey("update_interval") Then
                sensor.updateinterval = keyValuePairs("update_interval") ' 
            End If

            If keyValuePairs.ContainsKey("model") Then
                sensor.model = keyValuePairs("model") ' 
            End If


            Return sensor

        Catch ex As Exception
            MsgBox($"Sensor aus der Zeile {rowIndex} konnte nicht gelesen werden: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Private Function hcsr04Helper(rowIndex As Integer, dgv As DataGridView) As HCSR04
        Try
            Dim sensor As New HCSR04

            sensor.platform = "ultrasonic"


            Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
            If String.IsNullOrWhiteSpace(completeParams) Then Return sensor

            Dim keyValuePairs As New Dictionary(Of String, String)
            Dim parts = completeParams.Split(","c)


            For Each part In parts
                Dim trimmed = part.Trim()
                Dim keyVal = trimmed.Split("="c, 2)
                If keyVal.Length = 2 Then
                    keyValuePairs(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next

            If keyValuePairs.ContainsKey("trigger_pin") Then
                sensor.trigger = keyValuePairs("trigger_pin")
            End If

            If keyValuePairs.ContainsKey("echo_pin") Then
                sensor.echo = keyValuePairs("echo_pin")
            End If

            If keyValuePairs.ContainsKey("name") Then
                sensor.name = keyValuePairs("name")
            End If

            If keyValuePairs.ContainsKey("update_interval") Then
                sensor.update_interval = keyValuePairs("update_interval") ' 
            End If

            If keyValuePairs.ContainsKey("timeout") Then
                sensor.timeout = keyValuePairs("timeout") ' 
            End If

            If keyValuePairs.ContainsKey("pulse_time") Then
                sensor.pulse_time = keyValuePairs("pulse_time") ' 
            End If

            If keyValuePairs.ContainsKey("id") Then
                sensor.id = keyValuePairs("id") ' 
            End If


            Return sensor

        Catch ex As Exception
            MsgBox($"Sensor aus der Zeile {rowIndex} konnte nicht gelesen werden: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Private Function bmp280Helper(rowIndex As Integer, dgv As DataGridView) As BMP280
        Try
            Dim sensor As New BMP280

            sensor.platform = "bmp280_i2c"


            Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
            If String.IsNullOrWhiteSpace(completeParams) Then Return sensor

            Dim keyValuePairs As New Dictionary(Of String, String)
            Dim parts = completeParams.Split(","c)


            For Each part In parts
                Dim trimmed = part.Trim()
                Dim keyVal = trimmed.Split("="c, 2)
                If keyVal.Length = 2 Then
                    keyValuePairs(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next

            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address")
            End If

            If keyValuePairs.ContainsKey("tempname") Then
                sensor.tempname = keyValuePairs("tempname")
            End If

            If keyValuePairs.ContainsKey("tempoversampling") Then
                sensor.tempoversampling = keyValuePairs("tempoversampling")
            End If

            If keyValuePairs.ContainsKey("pressurename") Then
                sensor.pressurename = keyValuePairs("pressurename")

            End If

            If keyValuePairs.ContainsKey("pressureoversampling") Then
                sensor.pressuroversampling = keyValuePairs("pressureoversampling") ' 
            End If

            If keyValuePairs.ContainsKey("update_interval") Then
                sensor.update_interval = keyValuePairs("update_interval") ' 
            End If

            Return sensor

        Catch ex As Exception
            MsgBox($"Sensor aus der Zeile {rowIndex} konnte nicht gelesen werden: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Private Function bme280Helper(rowIndex As Integer, dgv As DataGridView) As BME280
        Try
            Dim sensor As New BME280

            sensor.platform = "bme280_i2c"


            Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
            If String.IsNullOrWhiteSpace(completeParams) Then Return sensor

            Dim keyValuePairs As New Dictionary(Of String, String)
            Dim parts = completeParams.Split(","c)


            For Each part In parts
                Dim trimmed = part.Trim()
                Dim keyVal = trimmed.Split("="c, 2)
                If keyVal.Length = 2 Then
                    keyValuePairs(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next

            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address")
            End If

            If keyValuePairs.ContainsKey("tempname") Then
                sensor.tempname = keyValuePairs("tempname")
            End If

            If keyValuePairs.ContainsKey("pressurename") Then
                sensor.pressurename = keyValuePairs("pressurename")
            End If

            If keyValuePairs.ContainsKey("humidityname") Then
                sensor.humidityname = keyValuePairs("humidityname")
            End If

            If keyValuePairs.ContainsKey("update_interval") Then
                sensor.update_interval = keyValuePairs("update_interval") ' 
            End If

            Return sensor

        Catch ex As Exception
            MsgBox($"Sensor aus der Zeile {rowIndex} konnte nicht gelesen werden: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Private Function bh1750Helper(rowIndex As Integer, dgv As DataGridView) As BH1750
        Try
            Dim sensor As New BH1750

            sensor.platform = "bh1750"


            Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
            If String.IsNullOrWhiteSpace(completeParams) Then Return sensor

            Dim keyValuePairs As New Dictionary(Of String, String)
            Dim parts = completeParams.Split(","c)


            For Each part In parts
                Dim trimmed = part.Trim()
                Dim keyVal = trimmed.Split("="c, 2)
                If keyVal.Length = 2 Then
                    keyValuePairs(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next

            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address")
            End If

            If keyValuePairs.ContainsKey("name") Then
                sensor.name = keyValuePairs("name")
            End If


            If keyValuePairs.ContainsKey("update_interval") Then
                sensor.update_interval = keyValuePairs("update_interval") ' 
            End If

            Return sensor

        Catch ex As Exception
            MsgBox($"Sensor aus der Zeile {rowIndex} konnte nicht gelesen werden: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Private Function sht3xdHelper(rowIndex As Integer, dgv As DataGridView) As SHT3XD
        Try
            Dim sensor As New SHT3XD

            sensor.platform = "sht3xd"


            Dim completeParams As String = dgv.Rows(rowIndex).Cells(3).Value?.ToString()
            If String.IsNullOrWhiteSpace(completeParams) Then Return sensor

            Dim keyValuePairs As New Dictionary(Of String, String)
            Dim parts = completeParams.Split(","c)


            For Each part In parts
                Dim trimmed = part.Trim()
                Dim keyVal = trimmed.Split("="c, 2)
                If keyVal.Length = 2 Then
                    keyValuePairs(keyVal(0).Trim().ToLower()) = keyVal(1).Trim()
                End If
            Next

            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address")
            End If

            If keyValuePairs.ContainsKey("tempname") Then
                sensor.tempname = keyValuePairs("tempname")
            End If

            If keyValuePairs.ContainsKey("humitidy") Then
                sensor.humidityname = keyValuePairs("humitidy")
            End If

            If keyValuePairs.ContainsKey("update_interval") Then
                sensor.update_interval = keyValuePairs("update_interval") ' 
            End If

            Return sensor

        Catch ex As Exception
            MsgBox($"Sensor aus der Zeile {rowIndex} konnte nicht gelesen werden: {ex.Message}")
            Return Nothing
        End Try
    End Function
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

Public Class DS18B20Model
    Public Property name As String
    Public Property platform As String
    Public Property adress As String
    Public Property onewireid As String
    Public Property updateinterval

End Class


Public Class DHT
    Public Property platform As String
    Public Property pin As String
    Public Property tempname As String
    Public Property humname As String
    Public Property updateinterval As String
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



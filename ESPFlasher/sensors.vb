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

        ' Info Felder
        If sensorInfo("info") IsNot Nothing Then
            For Each o In sensorInfo("info")
                AddInfoField(o.ToString, targetPanel, yOffset)
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
                .Location = New Point(150, yOffset),
                .Width = 150
            }
                If fieldConfig?("multiline")?.ToObject(Of Boolean) = True Then
                    CType(control, TextBox).Multiline = True
                    control.Height = 60
                End If

            Case "combobox"
                control = New ComboBox With {
                .Name = "CMB_" & fieldName,
                .Location = New Point(150, yOffset),
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
                .Location = New Point(150, yOffset),
                .Width = 100
            }
                With CType(control, NumericUpDown)
                    .Minimum = If(fieldConfig?("min") IsNot Nothing, fieldConfig("min").ToObject(Of Decimal), 0)
                    .Maximum = If(fieldConfig?("max") IsNot Nothing, fieldConfig("max").ToObject(Of Decimal), 100)
                    .Increment = If(fieldConfig?("step") IsNot Nothing, fieldConfig("step").ToObject(Of Decimal), 1)
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
        dgv.Rows.Add(sensorGroup, sensorType, platform, "", String.Join(", ", parameter))

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

        If Form1.spi = True Then
            sb.AppendLine("spi:")
            sb.AppendLine($"  clk_pin: {Form1.spiclk}")
            sb.AppendLine($"  mosi_pin: {Form1.spimosi}")
            sb.AppendLine($"  miso_pin: {Form1.spimiso}")
            sb.AppendLine()
            sb.AppendLine()
        End If

        If alreadysensors = False Then sb.AppendLine("sensor:")
            alreadysensors = True


        'GPIO
        Dim ds18b20list As New List(Of DS18B20Model)
        Dim dhtlist As New List(Of DHT)
        Dim hcsr04list As New List(Of HCSR04)
        'I2C
        Dim bmp280list As New List(Of BMP280)
        Dim bme280list As New List(Of BME280)
        Dim bh1750list As New List(Of BH1750)
        Dim sht3xdlist As New List(Of SHT3XD)
        Dim ina219list As New List(Of INA219)
        'SPI
        Dim max6675list As New List(Of MAX6675)
        Dim max31855list As New List(Of MAX31855)

        'Internal 
        Dim internallist As New List(Of InternalSensor)






        For i = 0 To dgv.Rows.Count - 1
                Dim row = dgv.Rows(i)


            If row.IsNewRow OrElse row.Cells(1).Value Is Nothing Then Continue For


            Dim typecontent As String = row.Cells(1).Value.ToString
            Dim groupcontent As String = row.Cells(0).Value.ToString

            Select Case True
                Case typecontent = "DS18B20"
                    Dim sensor = ds18b20Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        ds18b20list.Add(sensor)
                    End If
                Case typecontent = "DHT11/DHT22"
                    Dim sensor = dhtHelper(i, dgv)
                    If sensor IsNot Nothing Then
                        dhtlist.Add(sensor)
                    End If
                Case typecontent = "HC-SR04"
                    Dim sensor = hcsr04Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        hcsr04list.Add(sensor)
                    End If
                Case typecontent = "BMP280"
                    Dim sensor = bmp280Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        bmp280list.Add(sensor)
                    End If
                Case typecontent = "BME280"
                    Dim sensor = bme280Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        bme280list.Add(sensor)
                    End If
                Case typecontent = "BH1750"
                    Dim sensor = bh1750Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        bh1750list.Add(sensor)
                    End If
                Case typecontent = "SHT3X-D"
                    Dim sensor = sht3xdHelper(i, dgv)
                    If sensor IsNot Nothing Then
                        sht3xdlist.Add(sensor)
                    End If
                Case typecontent = "INA219"
                    Dim sensor = ina219Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        ina219list.Add(sensor)
                    End If
                Case typecontent = "MAX6675"
                    Dim sensor = max6675Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        max6675list.Add(sensor)
                    End If
                Case typecontent = "MAX31855"
                    Dim sensor = max31855Helper(i, dgv)
                    If sensor IsNot Nothing Then
                        max31855list.Add(sensor)
                    End If
                Case groupcontent = "Interne Sensoren"
                    Dim sensor = internalHelper(i, dgv)
                    If sensor IsNot Nothing Then
                        internallist.Add(sensor)
                    End If

            End Select





        Next


        For Each sensor As DS18B20Model In ds18b20list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.adress) AndAlso Not sensor.adress = "0" Then sb.AppendLine($"    address: {sensor.adress}")
            If Not String.IsNullOrEmpty(sensor.name) Then sb.AppendLine($"    name: {sensor.name}")
            If Not String.IsNullOrEmpty(sensor.onewireid) Then sb.AppendLine($"    one_wire_id: {sensor.onewireid}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next


        For Each sensor As DHT In dhtlist
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.pin) Then sb.AppendLine($"    pin: {sensor.pin}")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"    temperature:")
            If Not String.IsNullOrEmpty(sensor.tempname) Then sb.AppendLine($"      name: {sensor.tempname}")
            If Not String.IsNullOrEmpty(sensor.humname) Then sb.AppendLine($"    humidity:")
            If Not String.IsNullOrEmpty(sensor.humname) Then sb.AppendLine($"      name: {sensor.humname}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
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

        For Each sensor As INA219 In ina219list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.adress) Then sb.AppendLine($"    address: {sensor.adress}")
            If Not String.IsNullOrEmpty(sensor.shunt_resistance) Then sb.AppendLine($"    shunt_resistance: {sensor.shunt_resistance} ohm")
            If Not String.IsNullOrEmpty(sensor.currentname) Then sb.AppendLine($"    current:")
            If Not String.IsNullOrEmpty(sensor.currentname) Then sb.AppendLine($"      name: {sensor.currentname}")
            If Not String.IsNullOrEmpty(sensor.powername) Then sb.AppendLine($"    power:")
            If Not String.IsNullOrEmpty(sensor.powername) Then sb.AppendLine($"      name: {sensor.powername}")
            If Not String.IsNullOrEmpty(sensor.bus_voltagename) Then sb.AppendLine($"    bus_voltage:")
            If Not String.IsNullOrEmpty(sensor.bus_voltagename) Then sb.AppendLine($"      name: {sensor.bus_voltagename}")
            If Not String.IsNullOrEmpty(sensor.shunt_voltagename) Then sb.AppendLine($"    shunt_voltage:")
            If Not String.IsNullOrEmpty(sensor.shunt_voltagename) Then sb.AppendLine($"      name: {sensor.shunt_voltagename}")
            If Not String.IsNullOrEmpty(sensor.max_voltage) Then sb.AppendLine($"    max_voltage: {sensor.max_voltage}V")
            If Not String.IsNullOrEmpty(sensor.max_current) Then sb.AppendLine($"    max_current: {sensor.max_current}A")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next


        For Each sensor As MAX6675 In max6675list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.name) Then sb.AppendLine($"    name: {sensor.name}")
            If Not String.IsNullOrEmpty(sensor.cs) Then sb.AppendLine($"    cs_pin: {sensor.cs}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next
        For Each sensor As MAX31855 In max31855list
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.name) Then sb.AppendLine($"    name: {sensor.name}")
            If Not String.IsNullOrEmpty(sensor.cs) Then sb.AppendLine($"    cs_pin: {sensor.cs}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
        Next

        For Each sensor As InternalSensor In internallist
            sb.AppendLine($"  - platform: {sensor.platform}")
            If Not String.IsNullOrEmpty(sensor.name) Then sb.AppendLine($"    name: {sensor.name}")
            If Not String.IsNullOrEmpty(sensor.update_interval) Then sb.AppendLine($"    update_interval: {sensor.update_interval}s")
            sb.AppendLine()
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
#Region "GPIO-Helper"
    Private Function ds18b20Helper(rowIndex As Integer, dgv As DataGridView) As DS18B20Model
        Try
            Dim sensor As New DS18B20Model

            sensor.platform = dgv.Rows(rowIndex).Cells(2).Value?.ToString()
            sensor.onewireid = Form1.OneWireID

            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)

            If keyValuePairs.ContainsKey("name") Then
                sensor.name = keyValuePairs("name")
            End If

            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address") ' 
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
    Private Function dhtHelper(rowIndex As Integer, dgv As DataGridView) As DHT
        Try
            Dim sensor As New DHT

            sensor.platform = "dht"

            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)

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
                sensor.update_interval = keyValuePairs("update_interval") ' 
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


            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)

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

#End Region
#Region "I2C-Helper"


    Private Function bmp280Helper(rowIndex As Integer, dgv As DataGridView) As BMP280
        Try
            Dim sensor As New BMP280

            sensor.platform = "bmp280_i2c"


            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)

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


            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)

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


            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)

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

            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)


            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address")
            End If

            If keyValuePairs.ContainsKey("tempname") Then
                sensor.tempname = keyValuePairs("tempname")
            End If

            If keyValuePairs.ContainsKey("humitidyname") Then
                sensor.humidityname = keyValuePairs("humitidyname")
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

    Private Function ina219Helper(rowIndex As Integer, dgv As DataGridView) As INA219
        Try
            Dim sensor As New INA219

            sensor.platform = "ina219"

            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)


            If keyValuePairs.ContainsKey("address") Then
                sensor.adress = keyValuePairs("address")
            End If

            If keyValuePairs.ContainsKey("shunt_resistance") Then
                sensor.shunt_resistance = keyValuePairs("shunt_resistance")
            End If

            If keyValuePairs.ContainsKey("currentname") Then
                sensor.currentname = keyValuePairs("currentname")
            End If

            If keyValuePairs.ContainsKey("powername") Then
                sensor.powername = keyValuePairs("powername") ' 
            End If

            If keyValuePairs.ContainsKey("bus_voltage_name") Then
                sensor.bus_voltagename = keyValuePairs("bus_voltage_name")
            End If

            If keyValuePairs.ContainsKey("shunt_voltage_name") Then
                sensor.shunt_voltagename = keyValuePairs("shunt_voltage_name")
            End If

            If keyValuePairs.ContainsKey("max_voltage") Then
                sensor.max_voltage = keyValuePairs("max_voltage") ' 
            End If

            If keyValuePairs.ContainsKey("max_current") Then
                sensor.max_current = keyValuePairs("max_current") ' 
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
#End Region
#Region "SPI-HELPER"

    Private Function max6675Helper(rowIndex As Integer, dgv As DataGridView) As MAX6675
        Try
            Dim sensor As New MAX6675

            sensor.platform = "max6675"

            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)


            If keyValuePairs.ContainsKey("name") Then
                sensor.name = keyValuePairs("name")
            End If

            If keyValuePairs.ContainsKey("cs_pin") Then
                sensor.cs = keyValuePairs("cs_pin")
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

    Private Function max31855Helper(rowIndex As Integer, dgv As DataGridView) As MAX31855
        Try
            Dim sensor As New MAX31855

            sensor.platform = "max31855"

            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)


            If keyValuePairs.ContainsKey("name") Then
                sensor.name = keyValuePairs("name")
            End If

            If keyValuePairs.ContainsKey("cs_pin") Then
                sensor.cs = keyValuePairs("cs_pin")
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
#End Region
#Region "Internal Sensor"
    Private Function internalHelper(rowIndex As Integer, dgv As DataGridView) As InternalSensor
        Try
            Dim sensor As New InternalSensor

            sensor.platform = dgv.Rows(rowIndex).Cells(2).Value.ToString

            Dim keyValuePairs As New Dictionary(Of String, String)
            keyValuePairs = KeyValuePairsFun(rowIndex, dgv)


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
#End Region
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


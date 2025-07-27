Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Module yaml
    Dim rawName As String = Main.Txt_ESPName.Text.Trim()
    Dim name As String = rawName.ToLower().Replace(" ", "-")
    Public yamlPath As String = $"build\{name}\{name}.yaml"
    Public Function GenerateYaml(createbin As Boolean, Optional ota As Boolean = False, Optional freshyamlota As Boolean = False) As Boolean


        Dim writtenPlatforms As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)


        If File.Exists(yamlPath) Then
            If MessageBox.Show($"Soll die Datei {name} überschrieben werden?", "Datei bereits vorhanden", MessageBoxButtons.YesNo) = DialogResult.No Then
                Return False
                Exit Function
            End If
        End If

        Dim sb As New StringBuilder()

        ' Basis
        sb.AppendLine("esphome:")
        sb.AppendLine($"  name: {name}")
        sb.AppendLine($"  friendly_name: {rawName}")
        sb.AppendLine()

        ' Chiptyp
        Select Case Main.CBB_Chipset.SelectedItem?.ToString().ToLower()
            Case "esp32"
                sb.AppendLine("esp32:")
                sb.AppendLine("  board: esp32dev")
            Case "esp8266"
                sb.AppendLine("esp8266:")
                sb.AppendLine("  board: nodemcuv2")
            Case Else
                MessageBox.Show("❌ Kein gültiger Chiptyp ausgewählt!", "Fehler")
                Return False
                Exit Function
        End Select
        sb.AppendLine()
        sb.AppendLine("logger:")
        sb.AppendLine()

        ' WLAN
        sb.AppendLine("wifi:")
        sb.AppendLine($"  ssid: ""{Main.Txt_WIFISSID.Text}""")
        sb.AppendLine($"  password: ""{Main.Txt_WIFIPassword.Text}""")
        If Main.CB_ActivateFallback.Checked Then
            sb.AppendLine("  ap:")
            sb.AppendLine($"    ssid: ""{Main.Txt_FallbackSSID.Text}""")
            sb.AppendLine($"    password: ""{Main.Txt_FallbackPassword.Text}""")
        End If
        sb.AppendLine()

        ' OTA
        If Main.CB_OTA.Checked Then
            sb.AppendLine("ota:")
            sb.AppendLine("  platform: esphome")
            If Not String.IsNullOrWhiteSpace(Main.Txt_OTAPassword.Text) Then
                sb.AppendLine($"  password: ""{Main.Txt_OTAPassword.Text}""")
            End If
            sb.AppendLine()
        End If

        ' API
        If Main.CB_API.Checked Then
            sb.AppendLine("api:")
            sb.AppendLine($"  password: ""{Main.Txt_APIPassword.Text}""")
            sb.AppendLine()
        End If

        ' Webserver
        If Main.CB_Webserver.Checked Then
            sb.AppendLine("web_server:")
            If Not String.IsNullOrWhiteSpace(Main.Txt_WebServerPort.Text) Then
                sb.AppendLine($"  port: {Main.Txt_WebServerPort.Text}")
            End If
            If Main.CB_WebServerAuth.Checked Then
                sb.AppendLine("  auth:")
                sb.AppendLine($"    username: ""{Main.Txt_WebserverUsername.Text}""")
                sb.AppendLine($"    password: ""{Main.Txt_WebServerPassword.Text}""")
            End If
            sb.AppendLine()
        End If
        If Main.OneWire Then
            sb.AppendLine("one_wire:")
            sb.AppendLine("  - platform: gpio")
            If Not String.IsNullOrWhiteSpace(Main.OneWirePIN) Then sb.AppendLine($"    pin: GPIO{Main.OneWirePIN}")
            If Not String.IsNullOrWhiteSpace(Main.OneWireID) Then sb.AppendLine($"    id: {Main.OneWireID}")
            sb.AppendLine()
        End If

        ' I2C
        If Main.i2c Then
            Dim scan As String = If(Main.i2cScan, "true", "false")
            sb.AppendLine("i2c:")
            sb.AppendLine("  - id: i2c_bus")
            If Not String.IsNullOrWhiteSpace(Main.i2cSda) Then sb.AppendLine($"    sda: {Main.i2cSda}")
            If Not String.IsNullOrWhiteSpace(Main.i2cScl) Then sb.AppendLine($"    scl: {Main.i2cScl}")
            sb.AppendLine($"    scan: {scan}")
            sb.AppendLine()
        End If

        ' SPI
        If Main.spi Then
            sb.AppendLine("spi:")
            If Not String.IsNullOrWhiteSpace(Main.spiclk) Then sb.AppendLine($"  clk_pin: {Main.spiclk}")
            If Not String.IsNullOrWhiteSpace(Main.spimosi) Then sb.AppendLine($"  mosi_pin: {Main.spimosi}")
            If Not String.IsNullOrWhiteSpace(Main.spimiso) Then sb.AppendLine($"  miso_pin: {Main.spimiso}")
            sb.AppendLine()
        End If

        'UART
        If Main.uart Then
            sb.AppendLine("uart:")
            If Not String.IsNullOrWhiteSpace(Main.uartTx) Then sb.AppendLine($"  tx_pin: {Main.uartTx}")
            If Not String.IsNullOrWhiteSpace(Main.uartRx) Then sb.AppendLine($"  rx_pin: {Main.uartRx}")
            If Not String.IsNullOrWhiteSpace(Main.uartBaudrate) Then sb.AppendLine($"  baud_rate: {Main.uartBaudrate}")
            sb.AppendLine()

        End If



        ' Oder einfacher - immer hinzufügen wenn Displays vorhanden:
        If Main.DGV_Display.Rows.Count > 0 Then
            sb.AppendLine("font:")
            sb.AppendLine("  - file: ""gfonts://Roboto""")
            sb.AppendLine("    id: roboto")
            sb.AppendLine("    size: 12")
            sb.AppendLine()
        End If


        ExportSensorsToYaml(sb, Main.DGV_Sensors)
        ExportDisplayToYaml(sb, Main.DGV_Display)
        ExportTemplateToYaml(sb, Main.DGV_Templates)



        ' Ordner anlegen und Datei schreiben
        Directory.CreateDirectory($"build\{name}")
        File.WriteAllText(yamlPath, sb.ToString(), Encoding.UTF8)
        GenerateJsonFromDGV(Main.DGV_Sensors)
        GenerateJsonFromDisplayDGV(Main.DGV_Display)
        GenerateJsonFromTemplatesDGV(Main.DGV_Templates)
        GenerateJsonFromGlobalBus()
        If ota Then
            Dim otaForm As New OTA
            otaForm.Yamlpath = yamlPath
            If Not String.IsNullOrEmpty(Main.Txt_OTAPassword.Text) Then
                otaForm.otaPassword = Main.Txt_OTAPassword.Text
            End If
            otaForm.ShowDialog()
            Return True
            Exit Function
        End If
        If freshyamlota = True Then
            Return True
        End If

        If createbin And freshyamlota = False Then
            Main.generateBin(yamlPath)
            Return True
        ElseIf createbin = False And freshyamlota = False Then
            EditYaml.Show()
            Return True
        End If

        Return True
    End Function
    Public Sub GenerateJsonFromDGV(dgv As DataGridView)
        Dim entries As New List(Of DGVEntrie)()
        For i = 0 To dgv.Rows.Count - 1
            If Not dgv.Rows(i).IsNewRow Then


                Dim newEntry As New DGVEntrie() With {
                .group = If(dgv.Rows(i).Cells(0).Value?.ToString(), ""),
                .type = If(dgv.Rows(i).Cells(1).Value?.ToString(), ""),
                .platform = If(dgv.Rows(i).Cells(2).Value?.ToString(), ""),
                .pins = If(dgv.Rows(i).Cells(3).Value?.ToString(), ""),
                .parameters = If(dgv.Rows(i).Cells(4).Value?.ToString(), ""),
                .filter = If(dgv.Rows(i).Cells(5).Value?.ToString(), ""),
                .sensorclass = If(dgv.Rows(i).Cells(6).Value?.ToString(), "")
            }
                entries.Add(newEntry)
            End If


        Next


        Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(entries, Newtonsoft.Json.Formatting.Indented)
        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\sensors.json"
        Directory.CreateDirectory(Path.GetDirectoryName(jsonPath))
        File.WriteAllText(jsonPath, json, Encoding.UTF8)

    End Sub
    Public Sub GenerateJsonFromDisplayDGV(dgv As DataGridView)
        Dim entries As New List(Of DGVEntrie)()
        For i = 0 To dgv.Rows.Count - 1
            If Not dgv.Rows(i).IsNewRow Then


                Dim newEntry As New DGVEntrie() With {
                .group = If(dgv.Rows(i).Cells(0).Value?.ToString(), ""),
                .type = If(dgv.Rows(i).Cells(1).Value?.ToString(), ""),
                .platform = If(dgv.Rows(i).Cells(2).Value?.ToString(), ""),
                .pins = If(dgv.Rows(i).Cells(3).Value?.ToString(), ""),
                .parameters = If(dgv.Rows(i).Cells(4).Value?.ToString(), ""),
                .filter = If(dgv.Rows(i).Cells(5).Value?.ToString(), "")
            }
                entries.Add(newEntry)
            End If


        Next


        Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(entries, Newtonsoft.Json.Formatting.Indented)
        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\displays.json"
        Directory.CreateDirectory(Path.GetDirectoryName(jsonPath))
        File.WriteAllText(jsonPath, json, Encoding.UTF8)
    End Sub

    Public Sub GenerateJsonFromTemplatesDGV(dgv As DataGridView)
        Dim entries As New List(Of DGVEntrie)()
        For i = 0 To dgv.Rows.Count - 1
            If Not dgv.Rows(i).IsNewRow Then


                Dim newEntry As New DGVEntrie() With {
                .group = If(dgv.Rows(i).Cells(0).Value?.ToString(), ""),
                .type = If(dgv.Rows(i).Cells(1).Value?.ToString(), ""),
                .platform = If(dgv.Rows(i).Cells(2).Value?.ToString(), ""),
                .pins = If(dgv.Rows(i).Cells(3).Value?.ToString(), ""),
                .parameters = If(dgv.Rows(i).Cells(4).Value?.ToString(), ""),
                .filter = If(dgv.Rows(i).Cells(5).Value?.ToString(), ""),
                .sensorclass = If(dgv.Rows(i).Cells(6).Value?.ToString(), "")
            }
                entries.Add(newEntry)
            End If


        Next


        Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(entries, Newtonsoft.Json.Formatting.Indented)
        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\template.json"
        Directory.CreateDirectory(Path.GetDirectoryName(jsonPath))
        File.WriteAllText(jsonPath, json, Encoding.UTF8)
    End Sub

    Public Sub GenerateJsonFromGlobalBus()
        Dim globalBus As New GlobalBus() With {
            .gpiopin = Main.OneWirePIN,
            .gpioid = Main.OneWireID,
            .onewire = Main.OneWire,
            .i2cSda = Main.i2cSda,
            .i2cScl = Main.i2cScl,
            .i2cScan = Main.i2cScan,
            .i2c = Main.i2c,
            .spi = Main.spi,
            .spiclk = Main.spiclk,
            .spimosi = Main.spimosi,
            .spimiso = Main.spimiso
        }
        Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(globalBus, Newtonsoft.Json.Formatting.Indented)
        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\globalbus.json"
        Directory.CreateDirectory(Path.GetDirectoryName(jsonPath))
        File.WriteAllText(jsonPath, json, Encoding.UTF8)
    End Sub
    Function LoadGlobalBusFromJson() As Task
        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\globalbus.json"
        If Not File.Exists(jsonPath) Then
            MsgBox("Globale Bus Daten nicht gefunden")
            Return Task.CompletedTask
            Exit Function
        End If
        Dim json As String = File.ReadAllText(jsonPath, Encoding.UTF8)
        Dim globalBus As GlobalBus = Newtonsoft.Json.JsonConvert.DeserializeObject(Of GlobalBus)(json)
        Main.OneWirePIN = globalBus.gpiopin
        Main.OneWireID = globalBus.gpioid
        Main.OneWire = globalBus.onewire
        Main.i2cSda = globalBus.i2cSda
        Main.i2cScl = globalBus.i2cScl
        Main.i2cScan = globalBus.i2cScan
        Main.i2c = globalBus.i2c
        Main.spi = globalBus.spi
        Main.spiclk = globalBus.spiclk
        Main.spimosi = globalBus.spimosi
        Main.spimiso = globalBus.spimiso
        Main.uart = globalBus.uart
        Main.uartRx = globalBus.uartrx
        Main.uartTx = globalBus.uarttx
        Main.uartBaudrate = globalBus.uartbaudrate

        Bussettings.Txt_OneWireBusID.Text = Main.OneWireID
        Bussettings.Txt_OneWireGPIOPin.Text = Main.OneWirePIN
        Bussettings.txt_i2csda.Text = Main.i2cSda
        Bussettings.txt_i2cscl.Text = Main.i2cScl
        Bussettings.CB_i2cScan.Checked = Main.i2cScan
        Bussettings.txt_spiclk.Text = Main.spiclk
        Bussettings.txt_spimosi.Text = Main.spimosi
        Bussettings.txt_spimiso.Text = Main.spimiso
        Bussettings.txt_UartRx.Text = Main.uartRx
        Bussettings.txt_UartTx.Text = Main.uartTx

        Return Task.CompletedTask
    End Function



    Function LoadDGVFromJson(dgv As DataGridView) As Task

        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\sensors.json"
        If Not File.Exists(jsonPath) Then
            Return Task.CompletedTask
            Exit Function
        End If
        Dim json As String = File.ReadAllText(jsonPath, Encoding.UTF8)
        Dim entries As List(Of DGVEntrie) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of DGVEntrie))(json)
        dgv.Rows.Clear()
        For Each entry In entries
            dgv.Rows.Add(entry.group, entry.type, entry.platform, entry.pins, entry.parameters, entry.filter, entry.sensorclass)
        Next
        Return Task.CompletedTask
    End Function
    Function LoadDGVDisplayFromJson(dgv As DataGridView) As Task

        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\displays.json"
        If Not File.Exists(jsonPath) Then
            Return Task.CompletedTask
            Exit Function
        End If
        Dim json As String = File.ReadAllText(jsonPath, Encoding.UTF8)
        Dim entries As List(Of DGVEntrie) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of DGVEntrie))(json)
        dgv.Rows.Clear()
        For Each entry In entries
            dgv.Rows.Add(entry.group, entry.type, entry.platform, entry.pins, entry.parameters, entry.filter)
        Next
        Return Task.CompletedTask
    End Function

    Function LoadDGVTemplatesFromJson(dgv As DataGridView) As Task

        Dim jsonPath As String = $"build\{Main.Txt_ESPName.Text.Trim().ToLower().Replace(" ", "-")}\templates.json"
        If Not File.Exists(jsonPath) Then
            Return Task.CompletedTask
            Exit Function
        End If
        Dim json As String = File.ReadAllText(jsonPath, Encoding.UTF8)
        Dim entries As List(Of DGVEntrie) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of DGVEntrie))(json)
        dgv.Rows.Clear()
        For Each entry In entries
            dgv.Rows.Add(entry.group, entry.type, entry.platform, entry.pins, entry.parameters, entry.filter, entry.sensorclass)
        Next
        Return Task.CompletedTask
    End Function






    Function LoadYaml(yamlPath As String) As Task
        If Not File.Exists(yamlPath) Then
            MessageBox.Show("YAML-Datei wurde nicht gefunden!")
            Return Task.CompletedTask
            Exit Function
        End If

        For Each ctrl As Control In Main.TabControl1.Controls
            If TypeOf ctrl Is System.Windows.Forms.TextBox Then
                ctrl.Text = String.Empty
            End If

        Next

        Dim lines = File.ReadAllLines(yamlPath)

        ' Block-Statusflags
        Dim inOtaBlock As Boolean = False
        Dim inApiBlock As Boolean = False
        Dim inWifiApBlock As Boolean = False
        Dim inWebBlock As Boolean = False
        Dim inWebAuthBlock As Boolean = False

        Dim chipTyp As String = ""
        Dim boardTyp As String = ""




        For Each line In lines
            Dim trimmed = line.Trim()


            ' --- Block-Erkennung ---
            If trimmed.StartsWith("esp32:") Then
                chipTyp = "ESP32"
                Continue For
            ElseIf trimmed.StartsWith("esp8266:") Then
                chipTyp = "ESP8266"
                Continue For
            ElseIf trimmed.StartsWith("board:") Then
                boardTyp = trimmed.Substring(6).Trim()
            ElseIf trimmed.StartsWith("ota:") Then
                Main.CB_OTA.Checked = True
                inOtaBlock = True
                inApiBlock = False
                inWifiApBlock = False
                inWebBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf trimmed.StartsWith("api:") Then
                Main.CB_API.Checked = True
                inApiBlock = True
                inOtaBlock = False
                inWifiApBlock = False
                inWebBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf trimmed.StartsWith("ap:") Then
                Main.CB_ActivateFallback.Checked = True
                inWifiApBlock = True
                inOtaBlock = False
                inApiBlock = False
                inWebBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf trimmed.StartsWith("web_server:") Then
                Main.CB_Webserver.Checked = True
                inWebBlock = True
                inOtaBlock = False
                inApiBlock = False
                inWifiApBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf inWebBlock AndAlso trimmed.StartsWith("auth:") Then
                Main.CB_WebServerAuth.Checked = True
                inWebAuthBlock = True
                Continue For
            ElseIf Not line.StartsWith(" ") AndAlso Not line.StartsWith("-") Then
                ' Block-Ende bei neuer Top-Level-Direktive
                inOtaBlock = False
                inApiBlock = False
                inWifiApBlock = False
                inWebBlock = False
                inWebAuthBlock = False
            End If

            ' --- Werte lesen ---
            If trimmed.StartsWith("name:") AndAlso Not trimmed.Contains("friendly_name") AndAlso String.IsNullOrEmpty(Main.Txt_ESPName.Text) Then
                Main.Txt_ESPName.Text = trimmed.Substring(5).Trim()
            ElseIf inWebAuthBlock AndAlso trimmed.StartsWith("username:") Then
                Main.Txt_WebserverUsername.Text = ExtractQuotedValue(trimmed)
            ElseIf inWebAuthBlock AndAlso trimmed.StartsWith("password:") Then
                Main.Txt_WebServerPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inOtaBlock AndAlso trimmed.StartsWith("password:") Then
                Main.Txt_OTAPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inApiBlock AndAlso trimmed.StartsWith("password:") Then
                Main.Txt_APIPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inWifiApBlock AndAlso trimmed.StartsWith("ssid:") Then
                Main.Txt_FallbackSSID.Text = ExtractQuotedValue(trimmed)
            ElseIf inWifiApBlock AndAlso trimmed.StartsWith("password:") Then
                Main.Txt_FallbackPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inWebBlock AndAlso trimmed.StartsWith("port:") Then
                Main.Txt_WebServerPort.Text = trimmed.Substring(5).Trim()
            ElseIf trimmed.StartsWith("ssid:") Then
                Main.Txt_WIFISSID.Text = ExtractQuotedValue(trimmed)
            ElseIf trimmed.StartsWith("password:") Then
                Main.Txt_WIFIPassword.Text = ExtractQuotedValue(trimmed)
            End If
        Next

        ' Chiptyp setzen
        If chipTyp <> "" AndAlso Main.CBB_Chipset.Items.Contains(chipTyp) Then
            Main.CBB_Chipset.SelectedItem = chipTyp
        End If
        Return Task.CompletedTask
    End Function



    Private Function ExtractQuotedValue(line As String) As String
        Dim parts = line.Split(""""c)
        If parts.Length >= 2 Then
            Return parts(1)
        Else
            Return ""
        End If
    End Function
End Module

Public Class DGVEntrie
    Public Property group As String
    Public Property type As String
    Public Property platform As String
    Public Property pins As String
    Public Property parameters As String
    Public Property filter As String
    Public Property sensorclass As String

End Class

Public Class OneWireBus
    Public Property pin As String
    Public Property id As String

End Class


Public Class GlobalBus
    Public Property gpiopin As String
    Public Property gpioid As String
    Public Property onewire As Boolean
    Public Property i2cSda As String
    Public Property i2cScl As String
    Public Property i2cScan As Boolean
    Public Property i2c As Boolean
    Public Property spi As Boolean
    Public Property spiclk As String
    Public Property spimosi As String
    Public Property spimiso As String
    Public Property uartbaudrate As String
    Public Property uartrx As String
    Public Property uarttx As String
    Public Property uart As Boolean

End Class

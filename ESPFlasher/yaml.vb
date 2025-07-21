Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Module yaml
    Public Sub generateYaml(createbin As Boolean)
        Dim rawName As String = Form1.Txt_ESPName.Text.Trim()
        Dim name As String = rawName.ToLower().Replace(" ", "-")
        Dim yamlPath As String = $"build\{name}\{name}.yaml"
        Dim writtenPlatforms As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)


        If File.Exists(yamlPath) Then
            If MessageBox.Show($"Soll die Datei {name} überschrieben werden?", "Datei bereits vorhanden", MessageBoxButtons.YesNo) = DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim sb As New StringBuilder()

        ' Basis
        sb.AppendLine("esphome:")
        sb.AppendLine($"  name: {name}")
        sb.AppendLine($"  friendly_name: {rawName}")
        sb.AppendLine()

        ' Chiptyp
        Select Case Form1.CBB_Chipset.SelectedItem?.ToString().ToLower()
            Case "esp32"
                sb.AppendLine("esp32:")
                sb.AppendLine("  board: esp32dev")
            Case "esp8266"
                sb.AppendLine("esp8266:")
                sb.AppendLine("  board: nodemcuv2")
            Case Else
                MessageBox.Show("❌ Kein gültiger Chiptyp ausgewählt!", "Fehler")
                Exit Sub
        End Select
        sb.AppendLine()
        sb.AppendLine("logger:")
        sb.AppendLine()

        ' WLAN
        sb.AppendLine("wifi:")
        sb.AppendLine($"  ssid: ""{Form1.Txt_WIFISSID.Text}""")
        sb.AppendLine($"  password: ""{Form1.Txt_WIFIPassword.Text}""")
        If Form1.CB_ActivateFallback.Checked Then
            sb.AppendLine("  ap:")
            sb.AppendLine($"    ssid: ""{Form1.Txt_FallbackSSID.Text}""")
            sb.AppendLine($"    password: ""{Form1.Txt_FallbackPassword.Text}""")
        End If
        sb.AppendLine()

        ' OTA
        If Form1.CB_OTA.Checked Then
            sb.AppendLine("ota:")
            sb.AppendLine("  platform: esphome")
            If Not String.IsNullOrWhiteSpace(Form1.Txt_OTAPassword.Text) Then
                sb.AppendLine($"  password: ""{Form1.Txt_OTAPassword.Text}""")
            End If
            sb.AppendLine()
        End If

        ' API
        If Form1.CB_API.Checked Then
            sb.AppendLine("api:")
            sb.AppendLine($"  password: ""{Form1.Txt_APIPassword.Text}""")
            sb.AppendLine()
        End If

        ' Webserver
        If Form1.CB_Webserver.Checked Then
            sb.AppendLine("web_server:")
            If Not String.IsNullOrWhiteSpace(Form1.Txt_WebServerPort.Text) Then
                sb.AppendLine($"  port: {Form1.Txt_WebServerPort.Text}")
            End If
            If Form1.CB_WebServerAuth.Checked Then
                sb.AppendLine("  auth:")
                sb.AppendLine($"    username: ""{Form1.Txt_WebserverUsername.Text}""")
                sb.AppendLine($"    password: ""{Form1.Txt_WebServerPassword.Text}""")
            End If
            sb.AppendLine()
        End If

        ExportSensorsToYaml(sb, Form1.DGV_Sensors)


        ' Ordner anlegen und Datei schreiben
        Directory.CreateDirectory($"build\{name}")
        File.WriteAllText(yamlPath, sb.ToString(), Encoding.UTF8)

        ' Entweder kompilieren oder öffnen
        If createbin Then
            Form1.generateBin(yamlPath)
        Else
            Process.Start("notepad.exe", yamlPath)
        End If
    End Sub



    Public Sub LoadYaml(yamlPath As String)
        If Not File.Exists(yamlPath) Then
            MessageBox.Show("YAML-Datei wurde nicht gefunden!")
            Exit Sub
        End If

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
                Form1.CB_OTA.Checked = True
                inOtaBlock = True
                inApiBlock = False
                inWifiApBlock = False
                inWebBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf trimmed.StartsWith("api:") Then
                Form1.CB_API.Checked = True
                inApiBlock = True
                inOtaBlock = False
                inWifiApBlock = False
                inWebBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf trimmed.StartsWith("ap:") Then
                Form1.CB_ActivateFallback.Checked = True
                inWifiApBlock = True
                inOtaBlock = False
                inApiBlock = False
                inWebBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf trimmed.StartsWith("web_server:") Then
                Form1.CB_Webserver.Checked = True
                inWebBlock = True
                inOtaBlock = False
                inApiBlock = False
                inWifiApBlock = False
                inWebAuthBlock = False
                Continue For
            ElseIf inWebBlock AndAlso trimmed.StartsWith("auth:") Then
                Form1.CB_WebServerAuth.Checked = True
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
            If trimmed.StartsWith("name:") AndAlso Not trimmed.Contains("friendly_name") AndAlso String.IsNullOrEmpty(Form1.Txt_ESPName.Text) Then
                Form1.Txt_ESPName.Text = trimmed.Substring(5).Trim()
            ElseIf inWebAuthBlock AndAlso trimmed.StartsWith("username:") Then
                Form1.Txt_WebserverUsername.Text = ExtractQuotedValue(trimmed)
            ElseIf inWebAuthBlock AndAlso trimmed.StartsWith("password:") Then
                Form1.Txt_WebServerPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inOtaBlock AndAlso trimmed.StartsWith("password:") Then
                Form1.Txt_OTAPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inApiBlock AndAlso trimmed.StartsWith("password:") Then
                Form1.Txt_APIPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inWifiApBlock AndAlso trimmed.StartsWith("ssid:") Then
                Form1.Txt_FallbackSSID.Text = ExtractQuotedValue(trimmed)
            ElseIf inWifiApBlock AndAlso trimmed.StartsWith("password:") Then
                Form1.Txt_FallbackPassword.Text = ExtractQuotedValue(trimmed)
            ElseIf inWebBlock AndAlso trimmed.StartsWith("port:") Then
                Form1.Txt_WebServerPort.Text = trimmed.Substring(5).Trim()
            ElseIf trimmed.StartsWith("ssid:") Then
                Form1.Txt_WIFISSID.Text = ExtractQuotedValue(trimmed)
            ElseIf trimmed.StartsWith("password:") Then
                Form1.Txt_WIFIPassword.Text = ExtractQuotedValue(trimmed)
            End If
        Next

        ' Chiptyp setzen
        If chipTyp <> "" AndAlso Form1.CBB_Chipset.Items.Contains(chipTyp) Then
            Form1.CBB_Chipset.SelectedItem = chipTyp
        End If
    End Sub



    Private Function ExtractQuotedValue(line As String) As String
        Dim parts = line.Split(""""c)
        If parts.Length >= 2 Then
            Return parts(1)
        Else
            Return ""
        End If
    End Function






End Module

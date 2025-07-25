Imports System.Net
Imports System.Net.Http
Imports System.Net.Sockets

Public Class OTA
    Public Yamlpath As String
    Public otaPassword As String
    Dim foundDevices As Integer = 0
    Private Structure ESPDevice
        Public Name As String
        Public IP As String



        Public Overrides Function ToString() As String
            Return $"{Name} ({IP})"
        End Function
    End Structure

    Private Async Sub BTN_Scan_Click(sender As Object, e As EventArgs) Handles BTN_Scan.Click
        BTN_Scan.Enabled = False
        BTN_Scan.Text = "Scanne Netzwerk..."
        LB_Devices.Items.Clear()

        LB_Devices.Items.Add("🔍 Scanne 192.168.178.x Netzwerk...")
        LB_Devices.Items.Add("(Das kann 1-2 Minuten dauern)")



        ' Paralleles Scannen aller IPs
        Dim tasks = New List(Of Task)()
        For i = 1 To 254
            tasks.Add(ScanIP($"192.168.178.{i}"))
            tasks.Add(ScanIP($"192.168.178.{i}:88"))  ' Auch Port 88 testen
        Next

        Await Task.WhenAll(tasks)

        LB_Devices.Items.Add($"✅ Scan beendet. {foundDevices} ESP-Geräte gefunden.")
        BTN_Scan.Enabled = True
        BTN_Scan.Text = "Nach Geräten suchen"
    End Sub

    Private Async Function ScanIP(ip As String) As Task
        Try
            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(3)

                Dim response = Await client.GetAsync($"http://{ip}")

                ' Jeder HTTP-Response ist interessant (auch Auth-Fehler)
                If response.StatusCode = HttpStatusCode.OK OrElse
               response.StatusCode = HttpStatusCode.Unauthorized OrElse
               response.StatusCode = HttpStatusCode.Forbidden Then

                    ' Thread-safe UI Update
                    If LB_Devices.InvokeRequired Then
                        LB_Devices.Invoke(Sub()
                                              Dim status = If(response.StatusCode = HttpStatusCode.OK, "", " (Auth)")
                                              LB_Devices.Items.Add($"🎯 ESP GEFUNDEN: {ip}{status}")
                                          End Sub)
                    Else
                        Dim status = If(response.StatusCode = HttpStatusCode.OK, "", " (Auth)")
                        LB_Devices.Items.Add($"🎯 ESP GEFUNDEN: {ip}{status}")
                    End If
                End If
            End Using


        Catch
            ' Timeout/Fehler - IP nicht erreichbar, ignorieren
        End Try
    End Function

    Private Sub LB_Devices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_Devices.SelectedIndexChanged
        If LB_Devices.SelectedItem IsNot Nothing Then
            Dim selectedText = LB_Devices.SelectedItem.ToString()

            ' IP aus dem Text extrahieren
            If selectedText.Contains("ESP GEFUNDEN:") Then
                Dim ip = selectedText.Split(":"c)(1).Split("("c)(0).Trim()
                TB_SelectedIP.Text = ip
            End If
        End If
    End Sub

    Private Async Function ScanForESPDevices() As Task
        ' Lokales Netzwerk bestimmen
        Dim localIP = GetLocalIPAddress()
        Dim networkBase = localIP.Substring(0, localIP.LastIndexOf(".") + 1)

        ' Parallel scan von IP 1-254
        Dim tasks = New List(Of Task)()
        For i = 1 To 254
            Dim ip = networkBase & i
            tasks.Add(CheckESPDevice(ip))
        Next

        Await Task.WhenAll(tasks)
    End Function

    Private Async Function CheckESPDevice(ip As String) As Task
        Try
            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(5)

                ' Vollständige URL mit http://
                Dim url = $"http://{ip}"
                Console.WriteLine($"Teste: {url}")

                Dim response = Await client.GetAsync(url)

                If response.IsSuccessStatusCode Then
                    Console.WriteLine($"Erfolg! Status: {response.StatusCode}")

                    Dim content = Await response.Content.ReadAsStringAsync()
                    Console.WriteLine($"Content Länge: {content.Length}")

                    Dim deviceName = ExtractDeviceName(content, ip)

                    LB_Devices.Items.Add(New ESPDevice With {
                    .Name = deviceName,
                    .IP = ip
                })

                    Console.WriteLine($"Device hinzugefügt: {deviceName}")
                    foundDevices += 1
                End If
            End Using
        Catch ex As Exception
            Console.WriteLine($"Fehler bei {ip}: {ex.Message}")
        End Try
    End Function

    Private Function ExtractDeviceName(content As String, fallbackIP As String) As String
        Try
            ' ESPHome Web-Interface parsen für Gerätenamen
            Dim titleStart = content.IndexOf("<title>")
            If titleStart >= 0 Then
                titleStart += 7
                Dim titleEnd = content.IndexOf("</title>", titleStart)
                If titleEnd >= 0 Then
                    Return content.Substring(titleStart, titleEnd - titleStart).Trim()
                End If
            End If
        Catch
        End Try
        Return $"ESP Device ({fallbackIP})"
    End Function

    Private Function GetLocalIPAddress() As String
        Try
            Dim host = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = AddressFamily.InterNetwork AndAlso Not IPAddress.IsLoopback(ip) Then
                    Return ip.ToString()
                End If
            Next
        Catch
        End Try
        Return "192.168.1.100" ' Fallback
    End Function

    Private Async Sub BTN_FlashOTA_Click(sender As Object, e As EventArgs) Handles BTN_FlashOTA.Click
        If String.IsNullOrEmpty(TB_SelectedIP.Text) Then
            MessageBox.Show("Bitte erst ein ESP-Gerät auswählen!")
            Return
        End If

        If String.IsNullOrEmpty(TB_OtaPassword.Text) Then
            MessageBox.Show("Bitte gib das OTA-Passwort ein!")
            Return
        End If

        ' YAML-Pfad von der Hauptform holen
        Dim yamlPath = yaml.yamlPath ' Oder wie auch immer du den Pfad bekommst

        If Not IO.File.Exists(yamlPath) Then
            MessageBox.Show("Keine YAML-Datei gefunden! Bitte erst Konfiguration generieren.")
            Return
        End If

        BTN_FlashOTA.Enabled = False
        BTN_FlashOTA.Text = "Flashe via OTA..."

        ' Log-TextBox für Ausgabe (falls du eine hast)
        Await FlashViaOTA(yamlPath, TB_SelectedIP.Text)

        BTN_FlashOTA.Enabled = True
        BTN_FlashOTA.Text = "Flash via OTA"
    End Sub

    Private Async Function FlashViaOTA(yamlPath As String, espIP As String) As Task
        Try
            ' ESPHome OTA Command
            Dim psi As New ProcessStartInfo()
            psi.FileName = "cmd.exe"
            psi.Arguments = $"/c esphome upload ""{yamlPath}"" --device {espIP} --password {TB_OtaPassword.Text}"
            psi.UseShellExecute = False
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True
            psi.CreateNoWindow = True

            LB_Devices.Items.Add($"🚀 Starte OTA Flash auf {espIP}...")

            Dim proc As New Process()
            proc.StartInfo = psi

            ' Output live anzeigen
            AddHandler proc.OutputDataReceived, Sub(sender, e)
                                                    If Not String.IsNullOrEmpty(e.Data) Then
                                                        If LB_Devices.InvokeRequired Then
                                                            LB_Devices.Invoke(Sub() LB_Devices.Items.Add(e.Data))
                                                        Else
                                                            LB_Devices.Items.Add(e.Data)
                                                        End If
                                                    End If
                                                End Sub

            AddHandler proc.ErrorDataReceived, Sub(sender, e)
                                                   If Not String.IsNullOrEmpty(e.Data) Then
                                                       If LB_Devices.InvokeRequired Then
                                                           LB_Devices.Invoke(Sub() LB_Devices.Items.Add($"❌ {e.Data}"))
                                                       Else
                                                           LB_Devices.Items.Add($"❌ {e.Data}")
                                                       End If
                                                   End If
                                               End Sub

            proc.Start()
            proc.BeginOutputReadLine()
            proc.BeginErrorReadLine()

            ' Warten bis fertig
            Await Task.Run(Sub() proc.WaitForExit())

            If proc.ExitCode = 0 Then
                LB_Devices.Items.Add("✅ OTA Flash erfolgreich!")
                MessageBox.Show("OTA Update erfolgreich!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                LB_Devices.Items.Add("❌ OTA Flash fehlgeschlagen!")
                MessageBox.Show("OTA Update fehlgeschlagen. Prüfe die Logs.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            LB_Devices.Items.Add($"❌ Fehler: {ex.Message}")
            MessageBox.Show($"Fehler beim OTA Flash: {ex.Message}", "Fehler")
        End Try
    End Function

    Private Sub OTA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not String.IsNullOrEmpty(otaPassword) Then
            TB_OtaPassword.Text = otaPassword
        End If
    End Sub
End Class
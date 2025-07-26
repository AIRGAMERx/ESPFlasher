Imports System.Net
Imports System.Net.Http
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class OTA
    Public Yamlpath As String
    Public otaPassword As String
    Dim foundDevices As Integer = 0
    Public portstoscan As List(Of Integer)
    Private Structure ESPDevice
        Public Name As String
        Public IP As String



        Public Overrides Function ToString() As String
            Return $"{Name} ({IP})"
        End Function
    End Structure

    Private Async Sub BTN_Scan_Click(sender As Object, e As EventArgs) Handles BTN_Scan.Click
        foundDevices = 0
        ' Port-Dialog anzeigen
        If CB_PortScan.Checked Then
            Using portDialog As New portscan()
                If portDialog.ShowDialog() <> DialogResult.OK Then
                    Return
                End If


                Dim portsToScan = portDialog.PortList



                ' Scan starten
                Await StartNetworkScan(portsToScan)
            End Using
        Else
            Await StartNetworkScan() ' Standard-Port 80
        End If
    End Sub
    Private Async Function StartNetworkScan(Optional portsToScan As List(Of Integer) = Nothing) As Task
        ' Standard-Ports falls nichts übergeben wurde
        If portsToScan Is Nothing Then
            portsToScan = New List(Of Integer) From {80}
        End If



        BTN_Scan.Enabled = False
        BTN_Scan.Text = "Scanne Netzwerk..."
        LB_Devices.Items.Clear()
        LB_Devices.Items.Add("🔍 Scanne 192.168.178.x Netzwerk...")

        If portsToScan.Count = 1 AndAlso portsToScan(0) = 80 Then
            LB_Devices.Items.Add("(Nur Standard-Port 80)")
        Else
            LB_Devices.Items.Add($"(Scanne {portsToScan.Count} Ports pro IP: {String.Join(",", portsToScan)})")
        End If

        LB_Devices.Items.Add("📍 Bereit zum Scannen...")

        ' Rest deines Scan-Codes...
        Dim totalScans As Integer = 254 * portsToScan.Count

        ' ProgressBar setup
        If PB_Scan IsNot Nothing Then
            PB_Scan.Minimum = 0
            PB_Scan.Maximum = totalScans
            PB_Scan.Value = 0
            PB_Scan.Visible = True
        End If

        Dim statusIndex As Integer
        ' Begrenze gleichzeitige Scans
        Using semaphore As New SemaphoreSlim(50, 50)
            Dim tasks = New List(Of Task)()
            Dim completedCount As Integer = 0
            statusIndex = LB_Devices.Items.Count - 1

            For i = 1 To 254
                For Each port In portsToScan
                    tasks.Add(ScanIPWithProgressLimited($"192.168.178.{i}:{port}", statusIndex, completedCount, semaphore, totalScans))
                Next
            Next

            Await Task.WhenAll(tasks)
        End Using

        ' Abschluss
        LB_Devices.Items(statusIndex) = "✅ Alle IPs und Ports gescannt!"
        LB_Devices.Items.Add($"✅ Scan beendet. {foundDevices} ESP-Geräte gefunden.")

        If PB_Scan IsNot Nothing Then
            PB_Scan.Visible = False
        End If

        BTN_Scan.Enabled = True
        BTN_Scan.Text = "Nach Geräten suchen"
    End Function

    Private Async Function ScanIPWithProgressLimited(target As String, statusIndex As Integer, completedCount As Integer, semaphore As SemaphoreSlim, totalScans As Integer) As Task
        Await semaphore.WaitAsync()
        Try
            ' Status-Update nur alle 25 Scans
            If completedCount Mod 25 = 0 Then
                Me.Invoke(Sub()
                              LB_Devices.Items(statusIndex) = $"📍 Scanne {target}... ({completedCount}/{totalScans})"
                              LB_Devices.TopIndex = Math.Max(0, LB_Devices.Items.Count - 1)
                          End Sub)
            End If

            ' Dein ScanIP Code
            Await ScanIP(target)

            ' Progress-Update
            Interlocked.Increment(completedCount)
            Me.Invoke(Sub()
                          If PB_Scan IsNot Nothing Then
                              PB_Scan.Value = completedCount
                          End If
                      End Sub)

        Finally
            semaphore.Release()
        End Try
    End Function

    Private Async Function ScanIP(ip As String) As Task
        Try
            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(3)

                Dim response = Await client.GetAsync($"http://{ip}")

                ' Debug-Output
                Console.WriteLine($"Response von {ip}: {response.StatusCode}")

                ' Jeder HTTP-Response ist interessant (auch Auth-Fehler)
                If response.StatusCode = HttpStatusCode.OK OrElse
               response.StatusCode = HttpStatusCode.Unauthorized OrElse
               response.StatusCode = HttpStatusCode.Forbidden Then

                    ' Thread-safe increment
                    Interlocked.Increment(foundDevices)

                    ' Status-Text bestimmen
                    Dim status = ""
                    Select Case response.StatusCode
                        Case HttpStatusCode.Unauthorized
                            status = " (Auth Required)"
                        Case HttpStatusCode.Forbidden
                            status = " (Forbidden)"
                    End Select

                    ' Thread-safe UI Update
                    Me.Invoke(Sub()
                                  LB_Devices.Items.Add($"🎯 ESP GEFUNDEN: {ip}{status}")
                                  LB_Devices.TopIndex = LB_Devices.Items.Count - 1 ' Auto-scroll
                              End Sub)

                    Console.WriteLine($"ESP gefunden: {ip} - Total: {foundDevices}")
                End If
            End Using

        Catch ex As Exception
            ' Debug für deine IP
            If ip.Contains("192.168.178.125") Then
                Console.WriteLine($"Fehler bei {ip}: {ex.Message}")
            End If
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
        foundDevices = 0
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

            rtb_compilelog.AppendText($"🚀 Starte OTA Flash auf {espIP}...")

            Dim proc As New Process()
            proc.StartInfo = psi

            ' Output live anzeigen
            AddHandler proc.OutputDataReceived, Sub(sender, e)
                                                    If Not String.IsNullOrEmpty(e.Data) Then
                                                        If rtb_compilelog.InvokeRequired Then
                                                            rtb_compilelog.Invoke(Sub() rtb_compilelog.AppendText(e.Data))
                                                        Else
                                                            rtb_compilelog.AppendText(e.Data)
                                                        End If
                                                    End If
                                                End Sub

            AddHandler proc.ErrorDataReceived, Sub(sender, e)
                                                   If Not String.IsNullOrEmpty(e.Data) Then
                                                       If rtb_compilelog.InvokeRequired Then
                                                           rtb_compilelog.Invoke(Sub() rtb_compilelog.AppendText($"❌ {e.Data}"))
                                                       Else
                                                           rtb_compilelog.AppendText($"❌ {e.Data}")
                                                       End If
                                                   End If
                                               End Sub

            proc.Start()
            proc.BeginOutputReadLine()
            proc.BeginErrorReadLine()

            ' Warten bis fertig
            Await Task.Run(Sub() proc.WaitForExit())

            If proc.ExitCode = 0 Then
                rtb_compilelog.AppendText("✅ OTA Flash erfolgreich!")
                If Form1.CB_Webserver.Checked Then
                    Dim result As DialogResult = MessageBox.Show("OTA Update erfolgreich!" & vbCrLf & "Webserver im Browser aufrufen?", "Erfolg", MessageBoxButtons.YesNo)

                    If result = DialogResult.Yes Then
                        Process.Start(New ProcessStartInfo($"http://{TB_SelectedIP.Text}:{Form1.Txt_WebServerPort.Text}") With {
                        .UseShellExecute = True
                         })
                    End If

                End If
                MessageBox.Show("OTA Update erfolgreich!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                rtb_compilelog.AppendText("❌ OTA Flash fehlgeschlagen!")
                MessageBox.Show("OTA Update fehlgeschlagen. Prüfe die Logs.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            rtb_compilelog.AppendText($"❌ Fehler: {ex.Message}")
            MessageBox.Show($"Fehler beim OTA Flash: {ex.Message}", "Fehler")
        End Try
    End Function

    Private Sub OTA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not String.IsNullOrEmpty(otaPassword) Then
            TB_OtaPassword.Text = otaPassword
        End If
    End Sub

    Private Async Sub BTN_PingAdress_Click(sender As Object, e As EventArgs) Handles BTN_PingAdress.Click
        BTN_PingAdress.Enabled = False
        BTN_PingAdress.Text = "Pinge Adresse..."
        Try
            Await PingAdress()
        Catch ex As Exception
            rtb_compilelog.AppendText($"❌ Fehler beim Pingen: {ex.Message}")
        Finally
            BTN_PingAdress.Enabled = True
            BTN_PingAdress.Text = "IP Adresse anpingen"

        End Try
    End Sub


    Async Function PingAdress() As Task
        rtb_compilelog.Clear()
        rtb_compilelog.AppendText($"{TB_SelectedIP.Text} wird angepingt")
        rtb_compilelog.AppendText(vbCrLf)

        Dim psi As New ProcessStartInfo()
        psi.FileName = "cmd.exe"
        psi.Arguments = $"/c ping -n 4 {TB_SelectedIP.Text}"
        psi.UseShellExecute = False
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.CreateNoWindow = True

        Dim proc As New Process()
        proc.StartInfo = psi

        AddHandler proc.OutputDataReceived, Sub(sender, e)
                                                If Not String.IsNullOrEmpty(e.Data) Then
                                                    If rtb_compilelog.InvokeRequired Then
                                                        rtb_compilelog.Invoke(Sub() rtb_compilelog.AppendText(e.Data & vbCrLf))
                                                    Else
                                                        rtb_compilelog.AppendText(e.Data & vbCrLf)
                                                    End If
                                                End If
                                            End Sub

        AddHandler proc.ErrorDataReceived, Sub(sender, e)
                                               If Not String.IsNullOrEmpty(e.Data) Then
                                                   If rtb_compilelog.InvokeRequired Then
                                                       rtb_compilelog.Invoke(Sub() rtb_compilelog.AppendText($"❌ {e.Data}" & vbCrLf))
                                                   Else
                                                       rtb_compilelog.AppendText($"❌ {e.Data}" & vbCrLf)
                                                   End If
                                               End If
                                           End Sub

        proc.Start()
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()

        ' Warten bis fertig
        Await Task.Run(Sub() proc.WaitForExit())

    End Function

    Private Sub CB_PortScan_CheckedChanged(sender As Object, e As EventArgs) Handles CB_PortScan.CheckedChanged

    End Sub
End Class
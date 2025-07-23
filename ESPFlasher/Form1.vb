Imports System.IO
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json.Linq

Public Class Form1
    ' Allgemeine Einstellungen
    Dim GBLocation As New Point(345, 6)

    'COM Port
    Public ComPort As String = ""

    'OneWire
    Public OneWireID As String = ""
    Public OneWirePIN As String = ""
    Public OneWire As Boolean = False
    'i2c
    Public i2cSda As String = ""
    Public i2cScl As String = ""
    Public i2cScan As Boolean = False
    Public i2c As Boolean = False

    'spi
    Public spiclk As String = ""
    Public spimosi As String = ""
    Public spimiso As String = ""
    Public spi As Boolean = False

    Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await Init()


    End Sub


    Async Function Init() As Task
        ' Schritt 1: Wartefenster vorbereiten
        Dim waitForm As New Form With {
        .Text = "Initialisierung",
        .Size = New Size(350, 100),
        .FormBorderStyle = FormBorderStyle.FixedDialog,
        .StartPosition = FormStartPosition.WindowsDefaultLocation,
        .ControlBox = False,
        .ShowInTaskbar = False,
        .TopMost = True
    }

        Dim lbl As New Label With {
        .Text = "Bitte warten, das Programm wird initialisiert...",
        .AutoSize = False,
        .TextAlign = ContentAlignment.MiddleCenter,
        .Dock = DockStyle.Fill,
        .Font = New Font("Segoe UI", 11, FontStyle.Regular)
    }

        waitForm.Controls.Add(lbl)

        ' Fenster in einem eigenen Thread anzeigen
        Dim showTask As Task = Task.Run(Sub()
                                            waitForm.ShowDialog()
                                        End Sub)

        ' Warten auf Initialisierung
        Await CheckEnviroment()
        Await ListProjects()
        Await LoadSensorData()
        Await SetDGVSensors()

        ' Fenster schließen (UI-Thread erforderlich)
        If waitForm.InvokeRequired Then
            waitForm.Invoke(Sub() waitForm.Close())
        Else
            waitForm.Close()
        End If

        ' Warten bis das Fenster endgültig geschlossen ist
        Await showTask
    End Function







    Private Sub DownloadPython_Click(sender As Object, e As EventArgs) Handles DownloadPython.Click
        Process.Start(New ProcessStartInfo With {
    .FileName = "https://www.python.org/downloads/",
    .UseShellExecute = True
        })

    End Sub

    Private Sub PIPESPHome_Click(sender As Object, e As EventArgs) Handles PIPESPHome.Click
        InstallESPHome()
    End Sub
#Region "ESPSettings"
    Private Sub CB_ActivateFallback_CheckedChanged(sender As Object, e As EventArgs) Handles CB_ActivateFallback.CheckedChanged
        If CB_ActivateFallback.Checked Then
            Txt_FallbackSSID.ReadOnly = False
            Txt_FallbackPassword.ReadOnly = False
        Else
            Txt_FallbackSSID.ReadOnly = True
            Txt_FallbackPassword.ReadOnly = True
        End If
    End Sub

    Private Sub CB_OTA_CheckedChanged(sender As Object, e As EventArgs) Handles CB_OTA.CheckedChanged
        If CB_OTA.Checked Then
            Txt_OTAPassword.ReadOnly = False
        Else
            Txt_OTAPassword.ReadOnly = True
        End If
    End Sub

    Private Sub CB_API_CheckedChanged(sender As Object, e As EventArgs) Handles CB_API.CheckedChanged
        If CB_API.Checked Then
            Txt_APIPassword.ReadOnly = False
        Else
            Txt_APIPassword.ReadOnly = True
        End If
    End Sub

    Private Sub CB_Webserver_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Webserver.CheckedChanged
        If CB_Webserver.Checked Then
            Txt_WebServerPort.ReadOnly = False
        Else
            Txt_WebServerPort.ReadOnly = True
        End If
    End Sub
    Private Sub CB_WebServerAuth_CheckedChanged(sender As Object, e As EventArgs) Handles CB_WebServerAuth.CheckedChanged
        If CB_WebServerAuth.Checked Then
            Txt_WebserverUsername.ReadOnly = False
            Txt_WebServerPassword.ReadOnly = False
        Else
            Txt_WebserverUsername.ReadOnly = True
            Txt_WebServerPassword.ReadOnly = True
        End If
    End Sub

    Private Sub CreateBinFile_Click(sender As Object, e As EventArgs) Handles CreateBinFile.Click
        TabControl1.SelectedIndex = 0


        generateYaml(True)
    End Sub
    Private Sub CompileTest_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub CreateYamlandOpen_Click(sender As Object, e As EventArgs) Handles CreateYamlandOpen.Click
        generateYaml(False)
    End Sub



#End Region


#Region "Flashen"
    Public Sub generateBin(yamlPath As String)

        If String.IsNullOrEmpty(ComPort) Then
            MsgBox("ESP konnte nicht gefunden werden")
            ' Exit Sub
        End If



        Dim psi As New ProcessStartInfo()
        psi.FileName = "cmd.exe"
        psi.Arguments = $"/c esphome compile ""{yamlPath}"""
        psi.UseShellExecute = False
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.CreateNoWindow = True

        Dim proc As New Process()
        proc.StartInfo = psi

        If RichTextBox1.IsHandleCreated Then
            RichTextBox1.Invoke(Sub() RichTextBox1.Clear())
        End If

        ' Loggingfunktion
        Dim WriteToLog As Action(Of String, Color) = Sub(text, farbe)
                                                         If RichTextBox1.IsHandleCreated Then
                                                             If RichTextBox1.InvokeRequired Then
                                                                 RichTextBox1.BeginInvoke(Sub()
                                                                                              RichTextBox1.SelectionColor = farbe
                                                                                              RichTextBox1.AppendText(text & vbCrLf)
                                                                                              RichTextBox1.ScrollToCaret()
                                                                                          End Sub)
                                                             Else
                                                                 RichTextBox1.SelectionColor = farbe
                                                                 RichTextBox1.AppendText(text & vbCrLf)
                                                                 RichTextBox1.ScrollToCaret()
                                                             End If
                                                         End If
                                                     End Sub

        AddHandler proc.OutputDataReceived, Sub(sender, e)
                                                If Not String.IsNullOrEmpty(e.Data) Then
                                                    WriteToLog(e.Data, Color.Black)
                                                End If
                                            End Sub

        AddHandler proc.ErrorDataReceived, Sub(sender, e)
                                               If Not String.IsNullOrEmpty(e.Data) Then
                                                   WriteToLog(e.Data, Color.Red)
                                               End If
                                           End Sub

        proc.Start()
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()

        Task.Run(Sub()
                     proc.WaitForExit()
                     If proc.ExitCode = 0 Then
                         WriteToLog(vbCrLf & "✅ Kompilierung erfolgreich!", Color.Green)

                         ' === AUTOMATISCH FLASHEN ===
                         FlashFirmware(yamlPath, ComPort, WriteToLog)

                     Else
                         WriteToLog(vbCrLf & "❌ Kompilierung fehlgeschlagen!", Color.Red)
                     End If
                 End Sub)
    End Sub
    Private Sub FlashFirmware(yamlPath As String, comPort As String, logAction As Action(Of String, Color))
        Dim psi As New ProcessStartInfo()
        psi.FileName = "cmd.exe"
        psi.Arguments = $"/c esphome upload ""{yamlPath}"" --device {comPort}"
        psi.UseShellExecute = False
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.CreateNoWindow = True

        Dim proc As New Process()
        proc.StartInfo = psi

        AddHandler proc.OutputDataReceived, Sub(sender, e)
                                                If Not String.IsNullOrEmpty(e.Data) Then
                                                    logAction(e.Data, Color.Black)
                                                End If
                                            End Sub

        AddHandler proc.ErrorDataReceived, Sub(sender, e)
                                               If Not String.IsNullOrEmpty(e.Data) Then
                                                   logAction(e.Data, Color.Red)
                                               End If
                                           End Sub

        proc.Start()
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()

        Task.Run(Sub()
                     proc.WaitForExit()
                     If proc.ExitCode = 0 Then
                         logAction(vbCrLf & "✅ Flash erfolgreich!", Color.Green)
                     Else
                         logAction(vbCrLf & "❌ Flash fehlgeschlagen!", Color.Red)
                     End If
                 End Sub)
    End Sub


#End Region



#Region "LoadProjects"
    Function ListProjects() As Task
        Dim buildPath As String = Path.Combine(Application.StartupPath, "build")

        OpenProjects.DropDownItems.Clear()


        If Directory.Exists(buildPath) Then
            For Each dirPath In Directory.GetDirectories(buildPath)
                Dim projektName As String = Path.GetFileName(dirPath)
                Dim yamlPath As String = Path.Combine(dirPath, projektName & ".yaml")

                If File.Exists(yamlPath) Then

                    Dim item As New ToolStripMenuItem(projektName)

                    AddHandler item.Click, Async Sub(sender, e)

                                               Try
                                                   Await LoadYaml(yamlPath)
                                                   Await LoadDGVFromJson(DGV_Sensors)
                                                   Await LoadGlobalBusFromJson()
                                               Catch ex As Exception
                                                   MsgBox("Projekt konnte nicht geladen werden")
                                               End Try
                                           End Sub


                    OpenProjects.DropDownItems.Add(item)
                End If
            Next
        End If


        If OpenProjects.DropDownItems.Count = 0 Then
            OpenProjects.DropDownItems.Add("⚠️ Keine Projekte gefunden").Enabled = False
        End If
        Return Task.CompletedTask

    End Function

    Private Sub TestESPConnection_Click(sender As Object, e As EventArgs) Handles TestESPConnection.Click
        Dim espPort As String = FindeESPPort()
        If espPort <> "" Then
            ComPort = espPort
            MessageBox.Show("✅ ESP gefunden auf " & espPort)
            Exit Sub
        Else
            MessageBox.Show("❌ Kein ESP gefunden.")
        End If
    End Sub




#End Region



#Region "Sensors"
    Private Sub CBB_SensoreGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBB_SensoreGroup.SelectedIndexChanged
        Dim selectedGroup = CBB_SensoreGroup.SelectedItem.ToString()

        CBB_SensorType.Items.Clear()
        CBB_SensorType.Text = ""
        pnl_SensorConfig.Controls.Clear()


        If sensorData IsNot Nothing AndAlso sensorData.ContainsKey(selectedGroup) Then
            For Each sensor In sensorData(selectedGroup)
                CBB_SensorType.Items.Add(CType(sensor, JProperty).Name)
            Next
        End If
    End Sub

    Private Sub CBB_SensorType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBB_SensorType.SelectedIndexChanged



        Dim selectedGroup = CStr(CBB_SensoreGroup.SelectedItem)
        Dim selectedSensor = CStr(CBB_SensorType.SelectedItem)

        Dim jsonText = File.ReadAllText(Application.StartupPath & "\sensors.json")
        Dim jsonData = JObject.Parse(jsonText)

        If jsonData.ContainsKey(selectedGroup) AndAlso jsonData(selectedGroup)(selectedSensor) IsNot Nothing Then
            Dim sensorInfo = jsonData(selectedGroup)(selectedSensor)
            GenerateSensorConfigFields(sensorInfo, pnl_SensorConfig)


        End If



        If CBB_SensoreGroup.SelectedItem?.ToString().Trim().ToLowerInvariant() = "digitale gpio sensoren" Then
            GB_OneWire.Visible = True
            GB_OneWire.Location = GBLocation

            For Each c As Control In pnl_SensorConfig.Controls
                If TypeOf c Is TextBox AndAlso c.Name.ToLower().Contains("pin") Then
                    Dim txt = CType(c, TextBox)
                    txt.Text = OneWirePIN
                    txt.ReadOnly = True
                End If
            Next
        Else
            GB_OneWire.Visible = False
        End If

        If CBB_SensoreGroup.SelectedItem?.ToString().Trim().ToLowerInvariant() = "i2c sensoren" Then
            GB_I2C.Visible = True
            GB_I2C.Location = GBLocation

        Else
            GB_I2C.Visible = False
        End If

        If CBB_SensoreGroup.SelectedItem?.ToString().Trim().ToLowerInvariant() = "spi sensoren" Then
            GB_SPI.Visible = True
            GB_SPI.Location = GBLocation
        Else
            GB_SPI.Visible = False
        End If





    End Sub

    Async Function SetDGVSensors() As Task
        With DGV_Sensors.Columns
            .Add("Gruppe", "Gruppe")
            .Add("Typ", "Typ")
            .Add("Plattform", "Plattform")
            .Add("Pins", "Pins")
            .Add("Parameter", "Parameter")
        End With
    End Function

    Private Sub BTN_AddSensor_Click(sender As Object, e As EventArgs) Handles BTN_AddSensor.Click
        Dim group As String = CBB_SensoreGroup.SelectedItem?.ToString()
        Dim sensor As String = CBB_SensorType.SelectedItem?.ToString()

        If String.IsNullOrEmpty(group) OrElse String.IsNullOrEmpty(sensor) Then
            MessageBox.Show("Bitte Sensorgruppe und Sensortyp auswählen.")
            Exit Sub
        End If

        If sensor = "DS18B20" And String.IsNullOrEmpty(OneWireID) And String.IsNullOrEmpty(OneWirePIN) Then
            MsgBox("Bitte die Globalen OneWire Einstellungen ausfüllen und speichern")
            Exit Sub
        End If

        Dim sensorInfo As JObject = sensorData(group)(sensor)
        AddSensorToGrid(group, sensor, sensorInfo, pnl_SensorConfig, DGV_Sensors)

    End Sub

    Private Sub BTN_DeleteSelectedSensor_Click(sender As Object, e As EventArgs) Handles BTN_DeleteSelectedSensor.Click
        For Each RowToDelete In DGV_Sensors.SelectedRows.Cast(Of DataGridViewRow).ToArray
            DGV_Sensors.Rows.Remove(RowToDelete)
        Next
    End Sub

    Private Sub BTN_OneWireSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_OneWireSettingsSave.Click
        If Txt_OneWireBusID.Text.Length > 0 AndAlso Txt_OneWireGPIOPin.Text.Length > 0 Then
            OneWireID = Txt_OneWireBusID.Text
            OneWirePIN = Txt_OneWireGPIOPin.Text
            OneWire = True
            lbl_onewiresavestate.Text = "Gespeichert"
            lbl_onewiresavestate.ForeColor = Color.Green

        Else
            MsgBox("Bitte ID und PIN ausfüllen")
        End If
    End Sub

    Private Sub BTN_OneWireSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_OneWireSettingsDelete.Click
        OneWireID = ""
        OneWirePIN = ""
        OneWire = False
        lbl_onewiresavestate.Text = "Nicht Gespeichert"
        lbl_onewiresavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_i2cSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_i2cSettingsSave.Click
        If txt_i2cscl.Text.Length > 0 AndAlso txt_i2csda.Text.Length > 0 Then
            i2cScl = txt_i2cscl.Text
            i2cSda = txt_i2csda.Text
            i2cScan = CB_i2cScan.Checked
            i2c = True
            lbl_i2csavestate.Text = "Gespeichert"
            lbl_i2csavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte scl und sda ausfüllen")
        End If
    End Sub

    Private Sub BTN_i2cSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_i2cSettingsDelete.Click
        i2cScl = ""
        i2cSda = ""
        i2cScan = False
        i2c = False
        lbl_i2csavestate.Text = "Nicht Gespeichert"
        lbl_i2csavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_spiSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_spiSettingsSave.Click
        If txt_spiclk.Text.Length > 0 AndAlso txt_spimosi.Text.Length > 0 AndAlso txt_spimiso.Text.Length > 0 Then
            spiclk = txt_spiclk.Text
            spimosi = txt_spimosi.Text
            spimiso = txt_spimiso.Text
            spi = True
            lbl_spisavestate.Text = "Gespeichert"
            lbl_spisavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte clk, mosi und miso ausfüllen")
        End If


    End Sub

    Private Sub BTN_spiSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_spiSettingsDelete.Click
        spiclk = ""
        spimosi = ""
        spimiso = ""
        spi = False
        lbl_spisavestate.Text = "Nicht Gespeichert"
        lbl_spisavestate.ForeColor = Color.Red
    End Sub

    Private Sub Edit_Click(sender As Object, e As EventArgs) Handles Edit.Click
        Dim dgv As DataGridView = DGV_Sensors

        Try
            Dim selectedRow As DataGridViewRow = dgv.CurrentRow
            If selectedRow IsNot Nothing Then
                Dim group = dgv.CurrentRow().Cells(0).Value.ToString
                Dim type = dgv.CurrentRow().Cells(1).Value.ToString
                Dim platform = dgv.CurrentRow().Cells(2).Value.ToString
                Dim param = dgv.CurrentRow().Cells(3).Value.ToString

                CBB_SensoreGroup.SelectedItem = group
                CBB_SensorType.SelectedItem = type

                Dim dict As New Dictionary(Of String, String)
                Dim parts() = param.Split(","c)
                For Each part In parts

                    Dim keyVal = part.Trim().Split("="c, 2)
                    If keyVal.Length = 2 Then dict(keyVal(0).Trim) = keyVal(1).Trim
                Next
                For Each ctrl As Control In pnl_SensorConfig.Controls
                    If TypeOf ctrl Is TextBox Then
                        Dim tb As TextBox = CType(ctrl, TextBox)
                        Dim tb_part As String() = tb.Name.Split("_"c, 2)

                        If tb_part.Length = 2 Then
                            For Each entry As KeyValuePair(Of String, String) In dict
                                ' Debug-Ausgabe:


                                If tb_part(1) = entry.Key Then
                                    tb.Text = entry.Value
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next



                For Each entry As KeyValuePair(Of String, String) In dict

                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try





    End Sub

#End Region

End Class

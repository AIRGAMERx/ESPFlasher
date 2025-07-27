Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json.Linq

Public Class Main
    ' Allgemeine Einstellungen

    Public clickedRow As Integer = -1

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

    'uart
    Public uart As Boolean = False
    Public uartBaudrate As String = ""
    Public uartRx As String = ""
    Public uartTx As String = ""


    Async Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance)
        Await Init()


    End Sub


    Async Function Init() As Task
        ' Schritt 1: Wartefenster vorbereiten
        Dim waitForm As New Form With {
        .Text = "Initialisierung",
        .Size = New Size(350, 120),  ' ← Etwas höher für beide Labels
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
        .Location = New Point(10, 20),
        .Size = New Size(330, 30),
        .Font = New Font("Segoe UI", 11, FontStyle.Regular)
    }

        Dim status As New Label With {
        .Text = "",
        .AutoSize = False,
        .TextAlign = ContentAlignment.MiddleCenter,
        .Location = New Point(10, 50),
        .Size = New Size(330, 30),
        .Font = New Font("Segoe UI", 9, FontStyle.Bold),
        .ForeColor = Color.Blue
    }
        waitForm.Controls.Add(lbl)
        waitForm.Controls.Add(status)

        ' Fenster in einem eigenen Thread anzeigen
        Dim showTask As Task = Task.Run(Sub()
                                            waitForm.ShowDialog()
                                        End Sub)

        ' Kurz warten bis Form bereit ist
        Await Task.Delay(100)
        Try
            ' Thread-safe Updates mit Invoke-Check
            UpdateStatus(status, "Prüfe Umgebung...")
            Await CheckEnviroment()

            UpdateStatus(status, "Lade Projekte...")
            Await ListProjects()

            UpdateStatus(status, "Lade Sensor-Daten...")
            Await LoadSensorData()

            UpdateStatus(status, "Lade Display-Daten...")
            Await LoadDisplayData()

            UpdateStatus(status, "Lade Template-Daten...")
            Await LoadTemplateData()

            UpdateStatus(status, "Setze DataGridView...")
            Await SetDGV()

            UpdateStatus(status, "Suche ESP...")
            Await AutoDetectESP()

            UpdateStatus(status, "Initialisierung abgeschlossen!")

        Catch ex As Exception
            UpdateStatus(status, "Fehler aufgetreten!", Color.Red)
            MessageBox.Show("Fehler: " & ex.Message)
        Finally
            ' Sicherstellen dass waitForm geschlossen wird
            Try
                If waitForm IsNot Nothing AndAlso Not waitForm.IsDisposed Then
                    If waitForm.InvokeRequired Then
                        waitForm.Invoke(Sub()
                                            If Not waitForm.IsDisposed Then
                                                waitForm.Close()
                                            End If
                                        End Sub)
                    Else
                        waitForm.Close()
                    End If
                End If
            Catch ex As ObjectDisposedException
                ' Form bereits disposed - ignorieren
            End Try
        End Try

        ' Warten bis das Fenster endgültig geschlossen ist
        Try
            Await showTask
        Catch ex As Exception
            ' ShowDialog Task-Fehler ignorieren
        End Try
    End Function

    Private Sub UpdateStatus(statusLabel As Label, text As String, Optional color As Color = Nothing)
        Try
            If statusLabel.InvokeRequired Then
                statusLabel.BeginInvoke(Sub()
                                            statusLabel.Text = text
                                            If color <> Nothing Then statusLabel.ForeColor = color
                                        End Sub)
            Else
                statusLabel.Text = text
                If color <> Nothing Then statusLabel.ForeColor = color
            End If
        Catch
            ' Ignore threading errors
        End Try
    End Sub

    Private Sub PythonWebseiteÖffnenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PythonWebseiteÖffnenToolStripMenuItem1.Click
        Process.Start(New ProcessStartInfo With {
.FileName = "https://www.python.org/downloads/",
.UseShellExecute = True
    })
    End Sub

    Private Sub ESPHomePerPIPInstallierenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ESPHomePerPIPInstallierenToolStripMenuItem.Click
        InstallESPHome()
    End Sub
    Private Sub BusEinstellungenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BusEinstellungenToolStripMenuItem.Click
        Bussettings.ShowDialog()
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

    Private Sub VerbindungPrüfenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerbindungPrüfenToolStripMenuItem.Click
        AutoDetectESP()
    End Sub

    Private Sub BinErstellenUndFlashenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BinErstellenUndFlashenToolStripMenuItem.Click
        TabControl1.SelectedIndex = 0


        GenerateYaml(True, False)
    End Sub
    Private Sub YamlErstellenUndÖffnenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YamlErstellenUndÖffnenToolStripMenuItem.Click
        GenerateYaml(False, False)
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
                                                   Await LoadDGVDisplayFromJson(DGV_Display)
                                                   Await LoadDGVTemplatesFromJson(DGV_Templates)
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

    Private Sub TestESPConnection_Click(sender As Object, e As EventArgs)
        AutoDetectESP()
    End Sub

    Function AutoDetectESP() As Task
        Dim espPort As String = FindeESPPort()
        If espPort <> "" Then
            ComPort = espPort
            ' Status in der UI anzeigen (Label oder StatusBar)
            Label_ESPStatus.Text = "✅ ESP gefunden: " & espPort
            Label_ESPStatus.ForeColor = Color.Green
        Else
            Label_ESPStatus.Text = "❌ Kein ESP erkannt"
            Label_ESPStatus.ForeColor = Color.Red
        End If
        Return Task.CompletedTask
    End Function
    Private Sub OTAUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OTAUpdateToolStripMenuItem.Click
        GenerateYaml(False, True)
    End Sub

#End Region



#Region "Sensors & Displays"
    Private Sub CBB_SensoreGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBB_SensoreGroup.SelectedIndexChanged
        Dim selectedGroup = If(CBB_SensoreGroup.SelectedItem IsNot Nothing, CBB_SensoreGroup.SelectedItem.ToString(), Nothing)

        CBB_SensorType.Items.Clear()
        CBB_SensorType.Text = ""
        pnl_SensorConfig.Controls.Clear()
        RTB_yamlPreviewSensor.Clear()

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
        RTB_yamlPreviewSensor.Clear()

        If jsonData.ContainsKey(selectedGroup) AndAlso jsonData(selectedGroup)(selectedSensor) IsNot Nothing Then
            Dim sensorInfo = jsonData(selectedGroup)(selectedSensor)
            GenerateSensorConfigFields(sensorInfo, pnl_SensorConfig)


        End If

    End Sub

    Function SetDGV() As Task
        With DGV_Sensors.Columns
            .Add("Gruppe", "Gruppe")
            .Add("Typ", "Typ")
            .Add("Platform", "Plattform")
            .Add("Pins", "Pins")
            .Add("Parameter", "Parameter")
            .Add("Filter", "Filter")
            .Add("Class", "Class")
        End With


        With DGV_Display.Columns
            .Add("Gruppe", "Gruppe")
            .Add("Typ", "Typ")
            .Add("Platform", "Plattform")
            .Add("Pins", "Pins")
            .Add("Parameter", "Parameter")
            .Add("Filter", "Filter")
            .Add("Class", "Class")
        End With

        With DGV_Templates.Columns
            .Add("Gruppe", "Gruppe")
            .Add("Typ", "Typ")
            .Add("Platform", "Plattform")
            .Add("Pins", "Pins")
            .Add("Parameter", "Parameter")
            .Add("Filter", "Filter")
            .Add("Class", "Class")
        End With
        Return Task.CompletedTask
    End Function

    Private Sub BTN_AddSensor_Click(sender As Object, e As EventArgs) Handles BTN_AddSensor.Click
        Dim group = CBB_SensoreGroup.SelectedItem?.ToString
        Dim sensor = CBB_SensorType.SelectedItem?.ToString

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
        ' Wenn ganze Zeilen markiert sind:
        If DGV_Sensors.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In DGV_Sensors.SelectedRows
                If Not row.IsNewRow Then
                    DGV_Sensors.Rows.Remove(row)
                End If
            Next
        ElseIf DGV_Sensors.SelectedCells.Count > 0 Then
            ' Falls nur eine Zelle markiert ist:
            Dim rowIndex = DGV_Sensors.SelectedCells(0).RowIndex
            If Not DGV_Sensors.Rows(rowIndex).IsNewRow Then
                DGV_Sensors.Rows.RemoveAt(rowIndex)
            End If
        End If
    End Sub
    Private Sub Edit_Click(sender As Object, e As EventArgs) Handles Edit.Click
        Dim dgv = DGV_Sensors
        BTN_StopEditingSensor.Visible = True
        clickedRow = If(dgv.SelectedCells.Count > 0, dgv.SelectedCells(0).RowIndex, -1)
        Dim selectedRow = dgv.CurrentRow
        If selectedRow Is Nothing Then Exit Sub
        Try

            ' Hole Werte aus der DataGridView
            Dim group = If(selectedRow.Cells(0).Value?.ToString(), "")
            Dim type = If(selectedRow.Cells(1).Value?.ToString(), "")
            Dim platform = If(selectedRow.Cells(2).Value?.ToString(), "")
            Dim pins = If(selectedRow.Cells(3).Value?.ToString(), "")
            Dim param = If(selectedRow.Cells(4).Value?.ToString(), "")
            Dim filter = If(selectedRow.Cells(5).Value?.ToString(), "")

            ' Setze die ComboBox-Auswahl
            CBB_SensoreGroup.SelectedItem = group
            CBB_SensorType.SelectedItem = type

            ' Parse das Parameter-String in ein Dictionary
            Dim dict As New Dictionary(Of String, String)
            Dim parts() = param.Split(","c)
            For Each part In parts
                Dim keyVal = part.Trim.Split("="c, 2)
                If keyVal.Length = 2 Then
                    dict(keyVal(0).Trim) = keyVal(1).Trim
                End If
            Next

            ' Gehe durch alle Controls im Panel und setze die passenden Werte
            For Each ctrl As Control In pnl_SensorConfig.Controls
                Dim fieldName = ctrl.Name.ToLower

                For Each entry In dict
                    Dim key = entry.Key.ToLower
                    Dim value = entry.Value

                    ' Prüfe auf Übereinstimmung mit dem Ende des Control-Namens
                    If fieldName.EndsWith("_" & key) Then
                        If TypeOf ctrl Is TextBox Then
                            CType(ctrl, TextBox).Text = value
                        ElseIf TypeOf ctrl Is ComboBox Then
                            Dim cb = CType(ctrl, ComboBox)
                            If cb.Items.Contains(value) Then
                                cb.SelectedItem = value
                            Else
                                cb.Text = value ' falls dynamisch
                            End If
                        ElseIf TypeOf ctrl Is NumericUpDown Then
                            Dim num As Decimal
                            If Decimal.TryParse(value.Replace("s", "").Replace("ms", "").Replace("us", "").Replace("V", "").Replace("A", "").Replace("ohm", ""), num) Then
                                Dim nud = CType(ctrl, NumericUpDown)
                                If num >= nud.Minimum AndAlso num <= nud.Maximum Then
                                    nud.Value = num
                                End If
                            End If
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            MessageBox.Show("Fehler beim Bearbeiten: " & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            clickedRow = -1
        End Try
    End Sub

    Private Sub BTN_StopEditing_Click(sender As Object, e As EventArgs) Handles BTN_StopEditingSensor.Click
        clickedRow = -1
        CBB_SensorType.Items.Clear()
        CBB_SensorType.Text = ""
        pnl_SensorConfig.Controls.Clear()
        BTN_StopEditingSensor.Visible = False
    End Sub

    Private Sub AdvancedConfiguration_Click(sender As Object, e As EventArgs) Handles AdvancedConfiguration.Click
        If DGV_Sensors.CurrentRow Is Nothing Then Exit Sub
        Dim jsonText = File.ReadAllText(Application.StartupPath & "\sensors.json")
        Dim AllSensorsJson = JObject.Parse(jsonText)
        Dim sensorGroup = DGV_Sensors.CurrentRow.Cells(0).Value.ToString
        Dim sensorType = DGV_Sensors.CurrentRow.Cells(1).Value.ToString
        Dim currentRow = DGV_Sensors.CurrentRow

        ' Wichtig: AllSensorsJson ist das JObject mit allen Sensor-Definitionen
        Dim configForm As New baseconfig(sensorGroup, sensorType, currentRow, AllSensorsJson)
        configForm.ShowDialog()
    End Sub

    Private Sub CBB_DisplayGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBB_DisplayGroup.SelectedIndexChanged
        Dim selectedGroup = If(CBB_DisplayGroup.SelectedItem IsNot Nothing, CBB_DisplayGroup.SelectedItem.ToString, Nothing)

        CBB_DisplayType.Items.Clear()
        CBB_DisplayType.Text = ""
        pnl_DisplayConfig.Controls.Clear()

        If displayData IsNot Nothing AndAlso displayData.ContainsKey(selectedGroup) Then
            For Each display In displayData(selectedGroup)
                CBB_DisplayType.Items.Add(CType(display, JProperty).Name)
            Next
        End If
    End Sub

    Private Sub CBB_DisplayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBB_DisplayType.SelectedIndexChanged
        Dim selectedGroup = CStr(CBB_DisplayGroup.SelectedItem)
        Dim selectedDisplay = CStr(CBB_DisplayType.SelectedItem)

        Dim jsonText = File.ReadAllText(Application.StartupPath & "\displays.json")
        Dim jsonData = JObject.Parse(jsonText)

        If jsonData.ContainsKey(selectedGroup) AndAlso jsonData(selectedGroup)(selectedDisplay) IsNot Nothing Then
            Dim displayInfo = jsonData(selectedGroup)(selectedDisplay)
            GenerateDisplayConfigFields(displayInfo, pnl_DisplayConfig)


        End If

    End Sub

    Private Sub BTN_AddDisplay_Click(sender As Object, e As EventArgs) Handles BTN_AddDisplay.Click
        Dim group = CBB_DisplayGroup.SelectedItem?.ToString
        Dim display = CBB_DisplayType.SelectedItem?.ToString

        If String.IsNullOrEmpty(group) OrElse String.IsNullOrEmpty(display) Then
            MessageBox.Show("Bitte Displaygruppe und Displaytyp auswählen.")
            Exit Sub
        End If



        Dim displayInfo As JObject = displayData(group)(display)
        AddDisplayToGrid(group, display, displayInfo, pnl_DisplayConfig, DGV_Display)
    End Sub

    Private Sub AdvancedConfigurationDisplay_Click(sender As Object, e As EventArgs) Handles AdvancedConfigurationDisplay.Click

        If DGV_Display.CurrentRow Is Nothing Then Exit Sub
        Dim jsonText = File.ReadAllText(Application.StartupPath & "\displays.json")
        Dim AllDisplayJson = JObject.Parse(jsonText)


        Dim displayGroup = DGV_Display.CurrentRow.Cells(0).Value.ToString
        Dim displayType = DGV_Display.CurrentRow.Cells(1).Value.ToString
        Dim currentRow = DGV_Display.CurrentRow

        Dim configForm As New baseconfig(displayGroup, displayType, currentRow, AllDisplayJson)
        configForm.ShowDialog()

    End Sub

    Private Sub BTN_DeleteDisplay_Click(sender As Object, e As EventArgs) Handles BTN_DeleteDisplay.Click

        ' Wenn ganze Zeilen markiert sind:
        If DGV_Display.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In DGV_Display.SelectedRows
                If Not row.IsNewRow Then
                    DGV_Display.Rows.Remove(row)
                End If
            Next
        ElseIf DGV_Display.SelectedCells.Count > 0 Then
            ' Falls nur eine Zelle markiert ist:
            Dim rowIndex = DGV_Display.SelectedCells(0).RowIndex
            If Not DGV_Display.Rows(rowIndex).IsNewRow Then
                DGV_Display.Rows.RemoveAt(rowIndex)
            End If
        End If
    End Sub


    Private Sub CBB_TemplateGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBB_TemplateGroup.SelectedIndexChanged
        Dim selectedGroup = If(CBB_TemplateGroup.SelectedItem IsNot Nothing, CBB_TemplateGroup.SelectedItem.ToString, Nothing)
        CBB_TemplateType.Items.Clear()
        CBB_TemplateType.Text = ""
        pnl_TemplateConfig.Controls.Clear()


        If templates.templateData IsNot Nothing AndAlso templates.templateData.ContainsKey(selectedGroup) Then
            For Each template In templates.templateData(selectedGroup)
                CBB_TemplateType.Items.Add(CType(template, JProperty).Name)
            Next
        End If
    End Sub

    Private Sub CBB_TemplateType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBB_TemplateType.SelectedIndexChanged

        Dim selectedGroup = CStr(CBB_TemplateGroup.SelectedItem)
        Dim selectedTemplate = CStr(CBB_TemplateType.SelectedItem)

        If selectedGroup IsNot Nothing And selectedTemplate IsNot Nothing Then

            Dim jsonText = File.ReadAllText(Application.StartupPath & "\template.json")
            Dim jsonData = JObject.Parse(jsonText)
            RTB_yamlPreviewSensor.Clear()

            If jsonData.ContainsKey(selectedGroup) AndAlso jsonData(selectedGroup)(selectedTemplate) IsNot Nothing Then
                Dim templateInfo = jsonData(selectedGroup)(selectedTemplate)
                GenerateTemplateConfigFields(templateInfo, pnl_TemplateConfig)
            End If

        End If
    End Sub

    Private Sub BTN_AddTemplate_Click(sender As Object, e As EventArgs) Handles BTN_AddTemplate.Click
        Dim group = CBB_TemplateGroup.SelectedItem?.ToString
        Dim template = CBB_TemplateType.SelectedItem?.ToString

        If String.IsNullOrEmpty(group) OrElse String.IsNullOrEmpty(template) Then
            MessageBox.Show("Bitte Templategruppe und Templatetype auswählen.")
            Exit Sub
        End If



        Dim templateInfo As JObject = templateData(group)(template)
        AddTemplateToGrid(group, template, templateInfo, pnl_TemplateConfig, DGV_Templates)

    End Sub

    Private Sub BTN_DeleteTemplate_Click(sender As Object, e As EventArgs) Handles BTN_DeleteTemplate.Click
        If DGV_Templates.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In DGV_Sensors.SelectedRows
                If Not row.IsNewRow Then
                    DGV_Templates.Rows.Remove(row)
                End If
            Next
        ElseIf DGV_Templates.SelectedCells.Count > 0 Then
            ' Falls nur eine Zelle markiert ist:
            Dim rowIndex = DGV_Templates.SelectedCells(0).RowIndex
            If Not DGV_Templates.Rows(rowIndex).IsNewRow Then
                DGV_Templates.Rows.RemoveAt(rowIndex)
            End If
        End If
    End Sub

    Private Sub EditDisplay_Click(sender As Object, e As EventArgs) Handles EditDisplay.Click

        Dim dgv = DGV_Display
        BTN_StopEditingDisplay.Visible = True
        clickedRow = If(dgv.SelectedCells.Count > 0, dgv.SelectedCells(0).RowIndex, -1)
        Dim selectedRow = dgv.CurrentRow
        If selectedRow Is Nothing Then Exit Sub

        Try
            ' Hole Werte aus der DataGridView
            Dim group = If(selectedRow.Cells(0).Value?.ToString(), "")
            Dim type = If(selectedRow.Cells(1).Value?.ToString(), "")
            Dim platform = If(selectedRow.Cells(2).Value?.ToString(), "")
            Dim pins = If(selectedRow.Cells(3).Value?.ToString(), "")
            Dim param = If(selectedRow.Cells(4).Value?.ToString(), "")
            Dim filter = If(selectedRow.Cells(5).Value?.ToString(), "")

            ' Setze die ComboBox-Auswahl
            CBB_DisplayGroup.SelectedItem = group
            CBB_DisplayType.SelectedItem = type

            ' VERBESSERTES Parameter-Parsing (wie in WriteDisplayBlock)
            Dim dict As New Dictionary(Of String, String)

            If Not String.IsNullOrEmpty(param) Then
                ' Robustes manuelles Parsing
                Dim i As Integer = 0
                While i < param.Length
                    ' Finde nächsten Key
                    Dim keyStart As Integer = i
                    While i < param.Length AndAlso param(i) <> "="c
                        i += 1
                    End While

                    If i >= param.Length Then Exit While

                    Dim key As String = param.Substring(keyStart, i - keyStart).Trim()
                    i += 1 ' Skip "="

                    ' Finde Wert (bis zum nächsten key=value oder Ende)
                    Dim valueStart As Integer = i
                    Dim foundNextKey As Boolean = False

                    While i < param.Length AndAlso Not foundNextKey
                        If param(i) = ","c Then
                            ' Prüfe ob nach dem Komma ein neuer Key kommt
                            Dim j As Integer = i + 1
                            Dim nextKeyCandidate As String = ""
                            While j < param.Length AndAlso param(j) <> "="c AndAlso param(j) <> ","c
                                nextKeyCandidate += param(j)
                                j += 1
                            End While

                            If j < param.Length AndAlso param(j) = "="c Then
                                ' Gefunden: Nächster Key beginnt
                                foundNextKey = True
                            Else
                                ' Komma ist Teil des Wertes
                                i += 1
                            End If
                        Else
                            i += 1
                        End If
                    End While

                    Dim value As String = param.Substring(valueStart, i - valueStart).Trim()
                    If value.EndsWith(",") Then value = value.Substring(0, value.Length - 1).Trim()

                    dict(key) = value
                    Console.WriteLine($"DGV Parsed: '{key}' = '{value}'")

                    ' Skip Komma für nächste Iteration
                    If i < param.Length AndAlso param(i) = ","c Then i += 1
                End While
            End If

            ' ✅ RICHTIG: Verwende das Display-Panel!
            For Each ctrl As Control In pnl_DisplayConfig.Controls
                Dim fieldName = ctrl.Name.ToLower
                For Each entry In dict
                    Dim key = entry.Key.ToLower
                    Dim value = entry.Value


                    ' Prüfe auf Übereinstimmung mit dem Ende des Control-Namens
                    If fieldName.EndsWith("_" & key) Then
                        Console.WriteLine($"Match found! Setting {fieldName} = {value}")

                        If TypeOf ctrl Is TextBox Then
                            CType(ctrl, TextBox).Text = value
                        ElseIf TypeOf ctrl Is ComboBox Then
                            Dim cb = CType(ctrl, ComboBox)
                            If cb.Items.Contains(value) Then
                                cb.SelectedItem = value
                            Else
                                cb.Text = value
                            End If
                        ElseIf TypeOf ctrl Is NumericUpDown Then
                            Dim num As Decimal
                            If Decimal.TryParse(value.Replace("s", "").Replace("ms", "").Replace("us", "").Replace("V", "").Replace("A", "").Replace("ohm", ""), num) Then
                                Dim nud = CType(ctrl, NumericUpDown)
                                If num >= nud.Minimum AndAlso num <= nud.Maximum Then
                                    nud.Value = num
                                End If
                            End If
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            MessageBox.Show("Fehler beim Bearbeiten: " & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            clickedRow = -1
        End Try
    End Sub

    Private Sub BuyMeACoffeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuyMeACoffeeToolStripMenuItem.Click
        Try
            Dim url As String = "https://coff.ee/airgamer"
            Dim psi As New ProcessStartInfo(url) With {
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            ' Fallback: URL in Zwischenablage kopieren
            Clipboard.SetText("https://coff.ee/airgamer")
            MessageBox.Show("Could not open browser. URL copied to clipboard!",
                       "Donation Link",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information)
        End Try
    End Sub






#End Region


End Class

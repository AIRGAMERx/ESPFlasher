<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        StatusStrip1 = New StatusStrip()
        TSSL_Python = New ToolStripStatusLabel()
        TSSL_ESPHome = New ToolStripStatusLabel()
        MenuStrip1 = New MenuStrip()
        DateiToolStripMenuItem1 = New ToolStripMenuItem()
        OpenProjects = New ToolStripMenuItem()
        DateiToolStripMenuItem = New ToolStripMenuItem()
        TestESPConnection = New ToolStripMenuItem()
        CreateBinFile = New ToolStripMenuItem()
        ExtrasToolStripMenuItem = New ToolStripMenuItem()
        PythonToolStripMenuItem = New ToolStripMenuItem()
        DownloadPython = New ToolStripMenuItem()
        ESPHomeToolStripMenuItem = New ToolStripMenuItem()
        PIPESPHome = New ToolStripMenuItem()
        CreateYamlandOpen = New ToolStripMenuItem()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        Webserver = New GroupBox()
        Txt_WebServerPassword = New TextBox()
        Label12 = New Label()
        CB_WebServerAuth = New CheckBox()
        CB_Webserver = New CheckBox()
        Txt_WebserverUsername = New TextBox()
        Txt_WebServerPort = New TextBox()
        Label10 = New Label()
        Label11 = New Label()
        Label9 = New Label()
        RichTextBox1 = New RichTextBox()
        Txt_APIPassword = New TextBox()
        Label8 = New Label()
        CB_API = New CheckBox()
        Txt_OTAPassword = New TextBox()
        Label7 = New Label()
        CB_OTA = New CheckBox()
        CB_ActivateFallback = New CheckBox()
        Label6 = New Label()
        CBB_Chipset = New ComboBox()
        Txt_FallbackPassword = New TextBox()
        Label5 = New Label()
        Txt_FallbackSSID = New TextBox()
        Label4 = New Label()
        Txt_WIFIPassword = New TextBox()
        Label3 = New Label()
        Txt_WIFISSID = New TextBox()
        Label2 = New Label()
        Txt_ESPName = New TextBox()
        Label1 = New Label()
        TabPage2 = New TabPage()
        GB_SPI = New GroupBox()
        txt_spimiso = New TextBox()
        Label23 = New Label()
        lbl_spisavestate = New Label()
        BTN_spiSettingsDelete = New Button()
        BTN_spiSettingsSave = New Button()
        txt_spimosi = New TextBox()
        Label21 = New Label()
        txt_spiclk = New TextBox()
        Label22 = New Label()
        GB_I2C = New GroupBox()
        CB_i2cScan = New CheckBox()
        lbl_i2csavestate = New Label()
        BTN_i2cSettingsDelete = New Button()
        BTN_i2cSettingsSave = New Button()
        txt_i2cscl = New TextBox()
        Label19 = New Label()
        txt_i2csda = New TextBox()
        Label20 = New Label()
        GB_OneWire = New GroupBox()
        lbl_onewiresavestate = New Label()
        BTN_OneWireSettingsDelete = New Button()
        BTN_OneWireSettingsSave = New Button()
        Txt_OneWireBusID = New TextBox()
        Label17 = New Label()
        Txt_OneWireGPIOPin = New TextBox()
        Label16 = New Label()
        BTN_DeleteSelectedSensor = New Button()
        BTN_AddSensor = New Button()
        pnl_SensorConfig = New Panel()
        Label15 = New Label()
        CBB_SensorType = New ComboBox()
        Label14 = New Label()
        CBB_SensoreGroup = New ComboBox()
        Label13 = New Label()
        DGV_Sensors = New DataGridView()
        CM_EditSensor = New ContextMenuStrip(components)
        Edit = New ToolStripMenuItem()
        StatusStrip1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        Webserver.SuspendLayout()
        TabPage2.SuspendLayout()
        GB_SPI.SuspendLayout()
        GB_I2C.SuspendLayout()
        GB_OneWire.SuspendLayout()
        CType(DGV_Sensors, ComponentModel.ISupportInitialize).BeginInit()
        CM_EditSensor.SuspendLayout()
        SuspendLayout()
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {TSSL_Python, TSSL_ESPHome})
        StatusStrip1.Location = New Point(0, 794)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(709, 22)
        StatusStrip1.TabIndex = 0
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' TSSL_Python
        ' 
        TSSL_Python.Name = "TSSL_Python"
        TSSL_Python.Size = New Size(75, 17)
        TSSL_Python.Text = "TSSL_Python"
        ' 
        ' TSSL_ESPHome
        ' 
        TSSL_ESPHome.Name = "TSSL_ESPHome"
        TSSL_ESPHome.Size = New Size(89, 17)
        TSSL_ESPHome.Text = "TSSL_ESPHome"
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {DateiToolStripMenuItem1, DateiToolStripMenuItem, ExtrasToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(709, 24)
        MenuStrip1.TabIndex = 1
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' DateiToolStripMenuItem1
        ' 
        DateiToolStripMenuItem1.DropDownItems.AddRange(New ToolStripItem() {OpenProjects})
        DateiToolStripMenuItem1.Name = "DateiToolStripMenuItem1"
        DateiToolStripMenuItem1.Size = New Size(46, 20)
        DateiToolStripMenuItem1.Text = "Datei"
        ' 
        ' OpenProjects
        ' 
        OpenProjects.Name = "OpenProjects"
        OpenProjects.Size = New Size(149, 22)
        OpenProjects.Text = "Projekt öffnen"
        ' 
        ' DateiToolStripMenuItem
        ' 
        DateiToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {TestESPConnection, CreateBinFile})
        DateiToolStripMenuItem.Name = "DateiToolStripMenuItem"
        DateiToolStripMenuItem.Size = New Size(74, 20)
        DateiToolStripMenuItem.Text = "Ausführen"
        ' 
        ' TestESPConnection
        ' 
        TestESPConnection.Name = "TestESPConnection"
        TestESPConnection.Size = New Size(238, 22)
        TestESPConnection.Text = "Verbindung Testen"
        ' 
        ' CreateBinFile
        ' 
        CreateBinFile.Name = "CreateBinFile"
        CreateBinFile.Size = New Size(238, 22)
        CreateBinFile.Text = ".Bin Datei erstellen und Flashen"
        ' 
        ' ExtrasToolStripMenuItem
        ' 
        ExtrasToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {PythonToolStripMenuItem, ESPHomeToolStripMenuItem, CreateYamlandOpen})
        ExtrasToolStripMenuItem.Name = "ExtrasToolStripMenuItem"
        ExtrasToolStripMenuItem.Size = New Size(49, 20)
        ExtrasToolStripMenuItem.Text = "Extras"
        ' 
        ' PythonToolStripMenuItem
        ' 
        PythonToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {DownloadPython})
        PythonToolStripMenuItem.Name = "PythonToolStripMenuItem"
        PythonToolStripMenuItem.Size = New Size(209, 22)
        PythonToolStripMenuItem.Text = "Python"
        PythonToolStripMenuItem.ToolTipText = "Python muss in den PATH (Umgebunbsvariablen) hinzugefügt sein"
        ' 
        ' DownloadPython
        ' 
        DownloadPython.Name = "DownloadPython"
        DownloadPython.Size = New Size(160, 22)
        DownloadPython.Text = "Webseite öffnen"
        ' 
        ' ESPHomeToolStripMenuItem
        ' 
        ESPHomeToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {PIPESPHome})
        ESPHomeToolStripMenuItem.Name = "ESPHomeToolStripMenuItem"
        ESPHomeToolStripMenuItem.Size = New Size(209, 22)
        ESPHomeToolStripMenuItem.Text = "ESPHome"
        ESPHomeToolStripMenuItem.ToolTipText = "ESPHome muss in den PATH (Umgebunbsvariablen) hinzugefügt sein"
        ' 
        ' PIPESPHome
        ' 
        PIPESPHome.Name = "PIPESPHome"
        PIPESPHome.Size = New Size(171, 22)
        PIPESPHome.Text = "Per PIP Installieren"
        PIPESPHome.ToolTipText = "Geht nur wenn Python installiert und erkannt wurde"
        ' 
        ' CreateYamlandOpen
        ' 
        CreateYamlandOpen.Name = "CreateYamlandOpen"
        CreateYamlandOpen.Size = New Size(209, 22)
        CreateYamlandOpen.Text = "Yaml erstellen und öffnen"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(12, 27)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(692, 764)
        TabControl1.TabIndex = 2
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(Webserver)
        TabPage1.Controls.Add(Label9)
        TabPage1.Controls.Add(RichTextBox1)
        TabPage1.Controls.Add(Txt_APIPassword)
        TabPage1.Controls.Add(Label8)
        TabPage1.Controls.Add(CB_API)
        TabPage1.Controls.Add(Txt_OTAPassword)
        TabPage1.Controls.Add(Label7)
        TabPage1.Controls.Add(CB_OTA)
        TabPage1.Controls.Add(CB_ActivateFallback)
        TabPage1.Controls.Add(Label6)
        TabPage1.Controls.Add(CBB_Chipset)
        TabPage1.Controls.Add(Txt_FallbackPassword)
        TabPage1.Controls.Add(Label5)
        TabPage1.Controls.Add(Txt_FallbackSSID)
        TabPage1.Controls.Add(Label4)
        TabPage1.Controls.Add(Txt_WIFIPassword)
        TabPage1.Controls.Add(Label3)
        TabPage1.Controls.Add(Txt_WIFISSID)
        TabPage1.Controls.Add(Label2)
        TabPage1.Controls.Add(Txt_ESPName)
        TabPage1.Controls.Add(Label1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(684, 736)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Allgemein"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' Webserver
        ' 
        Webserver.Controls.Add(Txt_WebServerPassword)
        Webserver.Controls.Add(Label12)
        Webserver.Controls.Add(CB_WebServerAuth)
        Webserver.Controls.Add(CB_Webserver)
        Webserver.Controls.Add(Txt_WebserverUsername)
        Webserver.Controls.Add(Txt_WebServerPort)
        Webserver.Controls.Add(Label10)
        Webserver.Controls.Add(Label11)
        Webserver.Location = New Point(332, 17)
        Webserver.Name = "Webserver"
        Webserver.Size = New Size(327, 426)
        Webserver.TabIndex = 21
        Webserver.TabStop = False
        Webserver.Text = "Webserver"
        ' 
        ' Txt_WebServerPassword
        ' 
        Txt_WebServerPassword.Location = New Point(6, 178)
        Txt_WebServerPassword.Name = "Txt_WebServerPassword"
        Txt_WebServerPassword.ReadOnly = True
        Txt_WebServerPassword.Size = New Size(279, 23)
        Txt_WebServerPassword.TabIndex = 28
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(6, 160)
        Label12.Name = "Label12"
        Label12.Size = New Size(54, 15)
        Label12.TabIndex = 27
        Label12.Text = "Passwort"
        ' 
        ' CB_WebServerAuth
        ' 
        CB_WebServerAuth.AutoSize = True
        CB_WebServerAuth.Location = New Point(6, 91)
        CB_WebServerAuth.Name = "CB_WebServerAuth"
        CB_WebServerAuth.Size = New Size(106, 19)
        CB_WebServerAuth.TabIndex = 26
        CB_WebServerAuth.Text = "Auth aktivieren"
        CB_WebServerAuth.UseVisualStyleBackColor = True
        ' 
        ' CB_Webserver
        ' 
        CB_Webserver.AutoSize = True
        CB_Webserver.Location = New Point(6, 22)
        CB_Webserver.Name = "CB_Webserver"
        CB_Webserver.Size = New Size(84, 19)
        CB_Webserver.TabIndex = 22
        CB_Webserver.Text = "Webserver "
        CB_Webserver.UseVisualStyleBackColor = True
        ' 
        ' Txt_WebserverUsername
        ' 
        Txt_WebserverUsername.Location = New Point(6, 134)
        Txt_WebserverUsername.Name = "Txt_WebserverUsername"
        Txt_WebserverUsername.ReadOnly = True
        Txt_WebserverUsername.Size = New Size(279, 23)
        Txt_WebserverUsername.TabIndex = 25
        ' 
        ' Txt_WebServerPort
        ' 
        Txt_WebServerPort.Location = New Point(6, 62)
        Txt_WebServerPort.Name = "Txt_WebServerPort"
        Txt_WebServerPort.ReadOnly = True
        Txt_WebServerPort.Size = New Size(279, 23)
        Txt_WebServerPort.TabIndex = 23
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(6, 116)
        Label10.Name = "Label10"
        Label10.Size = New Size(63, 15)
        Label10.TabIndex = 24
        Label10.Text = "Username:"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(6, 44)
        Label11.Name = "Label11"
        Label11.Size = New Size(32, 15)
        Label11.TabIndex = 22
        Label11.Text = "Port:"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(19, 474)
        Label9.Name = "Label9"
        Label9.Size = New Size(75, 15)
        Label9.TabIndex = 20
        Label9.Text = "Compile Log"
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.Location = New Point(15, 492)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(663, 238)
        RichTextBox1.TabIndex = 19
        RichTextBox1.Text = ""
        ' 
        ' Txt_APIPassword
        ' 
        Txt_APIPassword.Location = New Point(15, 420)
        Txt_APIPassword.Name = "Txt_APIPassword"
        Txt_APIPassword.ReadOnly = True
        Txt_APIPassword.Size = New Size(279, 23)
        Txt_APIPassword.TabIndex = 18
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(15, 402)
        Label8.Name = "Label8"
        Label8.Size = New Size(75, 15)
        Label8.TabIndex = 17
        Label8.Text = "API Passwort"
        ' 
        ' CB_API
        ' 
        CB_API.AutoSize = True
        CB_API.Location = New Point(15, 380)
        CB_API.Name = "CB_API"
        CB_API.Size = New Size(256, 19)
        CB_API.TabIndex = 16
        CB_API.Text = "API (Home Assistant Integration) aktivieren "
        CB_API.UseVisualStyleBackColor = True
        ' 
        ' Txt_OTAPassword
        ' 
        Txt_OTAPassword.Location = New Point(15, 351)
        Txt_OTAPassword.Name = "Txt_OTAPassword"
        Txt_OTAPassword.ReadOnly = True
        Txt_OTAPassword.Size = New Size(279, 23)
        Txt_OTAPassword.TabIndex = 15
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(15, 333)
        Label7.Name = "Label7"
        Label7.Size = New Size(79, 15)
        Label7.TabIndex = 14
        Label7.Text = "OTA Passwort"
        ' 
        ' CB_OTA
        ' 
        CB_OTA.AutoSize = True
        CB_OTA.Location = New Point(15, 311)
        CB_OTA.Name = "CB_OTA"
        CB_OTA.Size = New Size(105, 19)
        CB_OTA.TabIndex = 13
        CB_OTA.Text = "OTA aktivieren "
        CB_OTA.UseVisualStyleBackColor = True
        ' 
        ' CB_ActivateFallback
        ' 
        CB_ActivateFallback.AutoSize = True
        CB_ActivateFallback.Location = New Point(15, 198)
        CB_ActivateFallback.Name = "CB_ActivateFallback"
        CB_ActivateFallback.Size = New Size(123, 19)
        CB_ActivateFallback.TabIndex = 12
        CB_ActivateFallback.Text = "Fallback aktivieren"
        CB_ActivateFallback.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(15, 61)
        Label6.Name = "Label6"
        Label6.Size = New Size(108, 15)
        Label6.TabIndex = 11
        Label6.Text = "Chiptyp auswählen"
        ' 
        ' CBB_Chipset
        ' 
        CBB_Chipset.FormattingEnabled = True
        CBB_Chipset.Items.AddRange(New Object() {"ESP32", "ESP8266"})
        CBB_Chipset.Location = New Point(15, 79)
        CBB_Chipset.Name = "CBB_Chipset"
        CBB_Chipset.Size = New Size(279, 23)
        CBB_Chipset.TabIndex = 10
        ' 
        ' Txt_FallbackPassword
        ' 
        Txt_FallbackPassword.Location = New Point(15, 282)
        Txt_FallbackPassword.Name = "Txt_FallbackPassword"
        Txt_FallbackPassword.ReadOnly = True
        Txt_FallbackPassword.Size = New Size(279, 23)
        Txt_FallbackPassword.TabIndex = 9
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(15, 264)
        Label5.Name = "Label5"
        Label5.Size = New Size(100, 15)
        Label5.TabIndex = 8
        Label5.Text = "Fallback Passwort"
        ' 
        ' Txt_FallbackSSID
        ' 
        Txt_FallbackSSID.Location = New Point(15, 238)
        Txt_FallbackSSID.Name = "Txt_FallbackSSID"
        Txt_FallbackSSID.ReadOnly = True
        Txt_FallbackSSID.Size = New Size(279, 23)
        Txt_FallbackSSID.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(15, 220)
        Label4.Name = "Label4"
        Label4.Size = New Size(76, 15)
        Label4.TabIndex = 6
        Label4.Text = "Fallback SSID"
        ' 
        ' Txt_WIFIPassword
        ' 
        Txt_WIFIPassword.Location = New Point(15, 169)
        Txt_WIFIPassword.Name = "Txt_WIFIPassword"
        Txt_WIFIPassword.Size = New Size(279, 23)
        Txt_WIFIPassword.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(15, 151)
        Label3.Name = "Label3"
        Label3.Size = New Size(54, 15)
        Label3.TabIndex = 4
        Label3.Text = "Passwort"
        ' 
        ' Txt_WIFISSID
        ' 
        Txt_WIFISSID.Location = New Point(15, 125)
        Txt_WIFISSID.Name = "Txt_WIFISSID"
        Txt_WIFISSID.Size = New Size(279, 23)
        Txt_WIFISSID.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(15, 107)
        Label2.Name = "Label2"
        Label2.Size = New Size(30, 15)
        Label2.TabIndex = 2
        Label2.Text = "SSID"
        ' 
        ' Txt_ESPName
        ' 
        Txt_ESPName.Location = New Point(15, 35)
        Txt_ESPName.Name = "Txt_ESPName"
        Txt_ESPName.Size = New Size(279, 23)
        Txt_ESPName.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(15, 17)
        Label1.Name = "Label1"
        Label1.Size = New Size(39, 15)
        Label1.TabIndex = 0
        Label1.Text = "Name"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(GB_SPI)
        TabPage2.Controls.Add(GB_I2C)
        TabPage2.Controls.Add(GB_OneWire)
        TabPage2.Controls.Add(BTN_DeleteSelectedSensor)
        TabPage2.Controls.Add(BTN_AddSensor)
        TabPage2.Controls.Add(pnl_SensorConfig)
        TabPage2.Controls.Add(Label15)
        TabPage2.Controls.Add(CBB_SensorType)
        TabPage2.Controls.Add(Label14)
        TabPage2.Controls.Add(CBB_SensoreGroup)
        TabPage2.Controls.Add(Label13)
        TabPage2.Controls.Add(DGV_Sensors)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(684, 736)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Sensoren"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' GB_SPI
        ' 
        GB_SPI.Controls.Add(txt_spimiso)
        GB_SPI.Controls.Add(Label23)
        GB_SPI.Controls.Add(lbl_spisavestate)
        GB_SPI.Controls.Add(BTN_spiSettingsDelete)
        GB_SPI.Controls.Add(BTN_spiSettingsSave)
        GB_SPI.Controls.Add(txt_spimosi)
        GB_SPI.Controls.Add(Label21)
        GB_SPI.Controls.Add(txt_spiclk)
        GB_SPI.Controls.Add(Label22)
        GB_SPI.Location = New Point(345, 224)
        GB_SPI.Name = "GB_SPI"
        GB_SPI.Size = New Size(333, 148)
        GB_SPI.TabIndex = 12
        GB_SPI.TabStop = False
        GB_SPI.Text = "SPI"
        GB_SPI.Visible = False
        ' 
        ' txt_spimiso
        ' 
        txt_spimiso.Location = New Point(6, 83)
        txt_spimiso.Name = "txt_spimiso"
        txt_spimiso.Size = New Size(155, 23)
        txt_spimiso.TabIndex = 8
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(6, 65)
        Label23.Name = "Label23"
        Label23.Size = New Size(55, 15)
        Label23.TabIndex = 7
        Label23.Text = "miso PIN"
        ' 
        ' lbl_spisavestate
        ' 
        lbl_spisavestate.AutoSize = True
        lbl_spisavestate.Location = New Point(6, 118)
        lbl_spisavestate.Name = "lbl_spisavestate"
        lbl_spisavestate.Size = New Size(0, 15)
        lbl_spisavestate.TabIndex = 6
        ' 
        ' BTN_spiSettingsDelete
        ' 
        BTN_spiSettingsDelete.ForeColor = Color.Red
        BTN_spiSettingsDelete.Location = New Point(248, 110)
        BTN_spiSettingsDelete.Name = "BTN_spiSettingsDelete"
        BTN_spiSettingsDelete.Size = New Size(75, 23)
        BTN_spiSettingsDelete.TabIndex = 5
        BTN_spiSettingsDelete.Text = "Löschen"
        BTN_spiSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_spiSettingsSave
        ' 
        BTN_spiSettingsSave.Location = New Point(167, 110)
        BTN_spiSettingsSave.Name = "BTN_spiSettingsSave"
        BTN_spiSettingsSave.Size = New Size(75, 23)
        BTN_spiSettingsSave.TabIndex = 4
        BTN_spiSettingsSave.Text = "Speichern"
        BTN_spiSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' txt_spimosi
        ' 
        txt_spimosi.Location = New Point(167, 39)
        txt_spimosi.Name = "txt_spimosi"
        txt_spimosi.Size = New Size(155, 23)
        txt_spimosi.TabIndex = 3
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(167, 18)
        Label21.Name = "Label21"
        Label21.Size = New Size(55, 15)
        Label21.TabIndex = 2
        Label21.Text = "mosi PIN"
        ' 
        ' txt_spiclk
        ' 
        txt_spiclk.Location = New Point(6, 39)
        txt_spiclk.Name = "txt_spiclk"
        txt_spiclk.Size = New Size(155, 23)
        txt_spiclk.TabIndex = 1
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(6, 21)
        Label22.Name = "Label22"
        Label22.Size = New Size(44, 15)
        Label22.TabIndex = 0
        Label22.Text = "clk PIN"
        ' 
        ' GB_I2C
        ' 
        GB_I2C.Controls.Add(CB_i2cScan)
        GB_I2C.Controls.Add(lbl_i2csavestate)
        GB_I2C.Controls.Add(BTN_i2cSettingsDelete)
        GB_I2C.Controls.Add(BTN_i2cSettingsSave)
        GB_I2C.Controls.Add(txt_i2cscl)
        GB_I2C.Controls.Add(Label19)
        GB_I2C.Controls.Add(txt_i2csda)
        GB_I2C.Controls.Add(Label20)
        GB_I2C.Location = New Point(345, 115)
        GB_I2C.Name = "GB_I2C"
        GB_I2C.Size = New Size(333, 103)
        GB_I2C.TabIndex = 11
        GB_I2C.TabStop = False
        GB_I2C.Text = "I2C"
        GB_I2C.Visible = False
        ' 
        ' CB_i2cScan
        ' 
        CB_i2cScan.AutoSize = True
        CB_i2cScan.Location = New Point(6, 72)
        CB_i2cScan.Name = "CB_i2cScan"
        CB_i2cScan.Size = New Size(51, 19)
        CB_i2cScan.TabIndex = 7
        CB_i2cScan.Text = "Scan"
        CB_i2cScan.UseVisualStyleBackColor = True
        ' 
        ' lbl_i2csavestate
        ' 
        lbl_i2csavestate.AutoSize = True
        lbl_i2csavestate.Location = New Point(72, 74)
        lbl_i2csavestate.Name = "lbl_i2csavestate"
        lbl_i2csavestate.Size = New Size(0, 15)
        lbl_i2csavestate.TabIndex = 6
        ' 
        ' BTN_i2cSettingsDelete
        ' 
        BTN_i2cSettingsDelete.ForeColor = Color.Red
        BTN_i2cSettingsDelete.Location = New Point(247, 68)
        BTN_i2cSettingsDelete.Name = "BTN_i2cSettingsDelete"
        BTN_i2cSettingsDelete.Size = New Size(75, 23)
        BTN_i2cSettingsDelete.TabIndex = 5
        BTN_i2cSettingsDelete.Text = "Löschen"
        BTN_i2cSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_i2cSettingsSave
        ' 
        BTN_i2cSettingsSave.Location = New Point(167, 68)
        BTN_i2cSettingsSave.Name = "BTN_i2cSettingsSave"
        BTN_i2cSettingsSave.Size = New Size(75, 23)
        BTN_i2cSettingsSave.TabIndex = 4
        BTN_i2cSettingsSave.Text = "Speichern"
        BTN_i2cSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' txt_i2cscl
        ' 
        txt_i2cscl.Location = New Point(167, 39)
        txt_i2cscl.Name = "txt_i2cscl"
        txt_i2cscl.Size = New Size(155, 23)
        txt_i2cscl.TabIndex = 3
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(167, 18)
        Label19.Name = "Label19"
        Label19.Size = New Size(43, 15)
        Label19.TabIndex = 2
        Label19.Text = "scl PIN"
        ' 
        ' txt_i2csda
        ' 
        txt_i2csda.Location = New Point(6, 39)
        txt_i2csda.Name = "txt_i2csda"
        txt_i2csda.Size = New Size(155, 23)
        txt_i2csda.TabIndex = 1
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(6, 21)
        Label20.Name = "Label20"
        Label20.Size = New Size(47, 15)
        Label20.TabIndex = 0
        Label20.Text = "sda PIN"
        ' 
        ' GB_OneWire
        ' 
        GB_OneWire.Controls.Add(lbl_onewiresavestate)
        GB_OneWire.Controls.Add(BTN_OneWireSettingsDelete)
        GB_OneWire.Controls.Add(BTN_OneWireSettingsSave)
        GB_OneWire.Controls.Add(Txt_OneWireBusID)
        GB_OneWire.Controls.Add(Label17)
        GB_OneWire.Controls.Add(Txt_OneWireGPIOPin)
        GB_OneWire.Controls.Add(Label16)
        GB_OneWire.Location = New Point(345, 6)
        GB_OneWire.Name = "GB_OneWire"
        GB_OneWire.Size = New Size(333, 103)
        GB_OneWire.TabIndex = 10
        GB_OneWire.TabStop = False
        GB_OneWire.Text = "1-Wire Bus"
        GB_OneWire.Visible = False
        ' 
        ' lbl_onewiresavestate
        ' 
        lbl_onewiresavestate.AutoSize = True
        lbl_onewiresavestate.Location = New Point(6, 76)
        lbl_onewiresavestate.Name = "lbl_onewiresavestate"
        lbl_onewiresavestate.Size = New Size(0, 15)
        lbl_onewiresavestate.TabIndex = 6
        ' 
        ' BTN_OneWireSettingsDelete
        ' 
        BTN_OneWireSettingsDelete.ForeColor = Color.Red
        BTN_OneWireSettingsDelete.Location = New Point(247, 68)
        BTN_OneWireSettingsDelete.Name = "BTN_OneWireSettingsDelete"
        BTN_OneWireSettingsDelete.Size = New Size(75, 23)
        BTN_OneWireSettingsDelete.TabIndex = 5
        BTN_OneWireSettingsDelete.Text = "Löschen"
        BTN_OneWireSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_OneWireSettingsSave
        ' 
        BTN_OneWireSettingsSave.Location = New Point(167, 68)
        BTN_OneWireSettingsSave.Name = "BTN_OneWireSettingsSave"
        BTN_OneWireSettingsSave.Size = New Size(75, 23)
        BTN_OneWireSettingsSave.TabIndex = 4
        BTN_OneWireSettingsSave.Text = "Speichern"
        BTN_OneWireSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' Txt_OneWireBusID
        ' 
        Txt_OneWireBusID.Location = New Point(167, 39)
        Txt_OneWireBusID.Name = "Txt_OneWireBusID"
        Txt_OneWireBusID.Size = New Size(155, 23)
        Txt_OneWireBusID.TabIndex = 3
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(167, 18)
        Label17.Name = "Label17"
        Label17.Size = New Size(40, 15)
        Label17.TabIndex = 2
        Label17.Text = "Bus ID"
        ' 
        ' Txt_OneWireGPIOPin
        ' 
        Txt_OneWireGPIOPin.Location = New Point(6, 39)
        Txt_OneWireGPIOPin.Name = "Txt_OneWireGPIOPin"
        Txt_OneWireGPIOPin.Size = New Size(155, 23)
        Txt_OneWireGPIOPin.TabIndex = 1
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(6, 21)
        Label16.Name = "Label16"
        Label16.Size = New Size(56, 15)
        Label16.TabIndex = 0
        Label16.Text = "GPIO PIN"
        ' 
        ' BTN_DeleteSelectedSensor
        ' 
        BTN_DeleteSelectedSensor.Location = New Point(6, 701)
        BTN_DeleteSelectedSensor.Name = "BTN_DeleteSelectedSensor"
        BTN_DeleteSelectedSensor.Size = New Size(675, 34)
        BTN_DeleteSelectedSensor.TabIndex = 9
        BTN_DeleteSelectedSensor.Text = "Ausgewählten Sensor Löschen"
        BTN_DeleteSelectedSensor.UseVisualStyleBackColor = True
        ' 
        ' BTN_AddSensor
        ' 
        BTN_AddSensor.Location = New Point(15, 472)
        BTN_AddSensor.Name = "BTN_AddSensor"
        BTN_AddSensor.Size = New Size(663, 34)
        BTN_AddSensor.TabIndex = 8
        BTN_AddSensor.Text = "Sensor Hinzufügen"
        BTN_AddSensor.UseVisualStyleBackColor = True
        ' 
        ' pnl_SensorConfig
        ' 
        pnl_SensorConfig.AutoScroll = True
        pnl_SensorConfig.AutoSize = True
        pnl_SensorConfig.Location = New Point(16, 102)
        pnl_SensorConfig.Name = "pnl_SensorConfig"
        pnl_SensorConfig.Size = New Size(268, 364)
        pnl_SensorConfig.TabIndex = 6
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(16, 55)
        Label15.Name = "Label15"
        Label15.Size = New Size(123, 15)
        Label15.TabIndex = 5
        Label15.Text = "Sensor Typ auswählen"
        ' 
        ' CBB_SensorType
        ' 
        CBB_SensorType.FormattingEnabled = True
        CBB_SensorType.Location = New Point(16, 73)
        CBB_SensorType.Name = "CBB_SensorType"
        CBB_SensorType.Size = New Size(264, 23)
        CBB_SensorType.TabIndex = 4
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(15, 6)
        Label14.Name = "Label14"
        Label14.Size = New Size(143, 15)
        Label14.TabIndex = 3
        Label14.Text = "Sensor Gruppe auswählen"
        ' 
        ' CBB_SensoreGroup
        ' 
        CBB_SensoreGroup.FormattingEnabled = True
        CBB_SensoreGroup.Location = New Point(15, 24)
        CBB_SensoreGroup.Name = "CBB_SensoreGroup"
        CBB_SensoreGroup.Size = New Size(265, 23)
        CBB_SensoreGroup.TabIndex = 2
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(6, 509)
        Label13.Name = "Label13"
        Label13.Size = New Size(130, 15)
        Label13.TabIndex = 1
        Label13.Text = "Hinzugefügte Sensoren"
        ' 
        ' DGV_Sensors
        ' 
        DGV_Sensors.BackgroundColor = SystemColors.ControlLight
        DGV_Sensors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV_Sensors.ContextMenuStrip = CM_EditSensor
        DGV_Sensors.Location = New Point(6, 527)
        DGV_Sensors.Name = "DGV_Sensors"
        DGV_Sensors.Size = New Size(675, 170)
        DGV_Sensors.TabIndex = 0
        ' 
        ' CM_EditSensor
        ' 
        CM_EditSensor.Items.AddRange(New ToolStripItem() {Edit})
        CM_EditSensor.Name = "CM_EditSensor"
        CM_EditSensor.Size = New Size(181, 48)
        ' 
        ' Edit
        ' 
        Edit.Name = "Edit"
        Edit.Size = New Size(180, 22)
        Edit.Text = "Bearbeiten"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(709, 816)
        Controls.Add(TabControl1)
        Controls.Add(StatusStrip1)
        Controls.Add(MenuStrip1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MainMenuStrip = MenuStrip1
        Name = "Form1"
        Text = "ESPFlasher"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        Webserver.ResumeLayout(False)
        Webserver.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        GB_SPI.ResumeLayout(False)
        GB_SPI.PerformLayout()
        GB_I2C.ResumeLayout(False)
        GB_I2C.PerformLayout()
        GB_OneWire.ResumeLayout(False)
        GB_OneWire.PerformLayout()
        CType(DGV_Sensors, ComponentModel.ISupportInitialize).EndInit()
        CM_EditSensor.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TSSL_Python As ToolStripStatusLabel
    Friend WithEvents TSSL_ESPHome As ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ExtrasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PythonToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DownloadPython As ToolStripMenuItem
    Friend WithEvents ESPHomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PIPESPHome As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents CB_ActivateFallback As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CBB_Chipset As ComboBox
    Friend WithEvents Txt_FallbackPassword As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_FallbackSSID As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_WIFIPassword As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_WIFISSID As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_ESPName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_APIPassword As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CB_API As CheckBox
    Friend WithEvents Txt_OTAPassword As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents CB_OTA As CheckBox
    Friend WithEvents DateiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateBinFile As ToolStripMenuItem
    Friend WithEvents TestESPConnection As ToolStripMenuItem
    Friend WithEvents Label9 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents DateiToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents OpenProjects As ToolStripMenuItem
    Friend WithEvents Webserver As GroupBox
    Friend WithEvents Txt_WebServerPassword As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents CB_WebServerAuth As CheckBox
    Friend WithEvents CB_Webserver As CheckBox
    Friend WithEvents Txt_WebserverUsername As TextBox
    Friend WithEvents Txt_WebServerPort As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents DGV_Sensors As DataGridView
    Friend WithEvents Label14 As Label
    Friend WithEvents CBB_SensoreGroup As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents CBB_SensorType As ComboBox
    Friend WithEvents pnl_SensorConfig As Panel
    Friend WithEvents BTN_AddSensor As Button
    Friend WithEvents BTN_DeleteSelectedSensor As Button
    Friend WithEvents CreateYamlandOpen As ToolStripMenuItem
    Friend WithEvents GB_OneWire As GroupBox
    Friend WithEvents BTN_OneWireSettingsSave As Button
    Friend WithEvents Txt_OneWireBusID As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Txt_OneWireGPIOPin As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents BTN_OneWireSettingsDelete As Button
    Friend WithEvents lbl_onewiresavestate As Label
    Friend WithEvents GB_I2C As GroupBox
    Friend WithEvents lbl_i2csavestate As Label
    Friend WithEvents BTN_i2cSettingsDelete As Button
    Friend WithEvents BTN_i2cSettingsSave As Button
    Friend WithEvents txt_i2cscl As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txt_i2csda As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents CB_i2cScan As CheckBox
    Friend WithEvents GB_SPI As GroupBox
    Friend WithEvents txt_spimiso As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents lbl_spisavestate As Label
    Friend WithEvents BTN_spiSettingsDelete As Button
    Friend WithEvents BTN_spiSettingsSave As Button
    Friend WithEvents txt_spimosi As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents txt_spiclk As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents CM_EditSensor As ContextMenuStrip
    Friend WithEvents Edit As ToolStripMenuItem

End Class

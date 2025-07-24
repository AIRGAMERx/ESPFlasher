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
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        Label_ESPStatus = New ToolStripStatusLabel()
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
        BTN_AddSensor = New Button()
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
        BTN_StopEditingSensor = New Button()
        GB_Uart = New GroupBox()
        txt_UartTx = New TextBox()
        Label18 = New Label()
        CBB_UartBaudrate = New ComboBox()
        lbl_uartsavestate = New Label()
        BTN_uartSettingsDelete = New Button()
        BTN_uartSettingsSave = New Button()
        txt_UartRx = New TextBox()
        Label25 = New Label()
        Label26 = New Label()
        BTN_DeleteSelectedSensor = New Button()
        GB_OneWire = New GroupBox()
        lbl_onewiresavestate = New Label()
        BTN_OneWireSettingsDelete = New Button()
        BTN_OneWireSettingsSave = New Button()
        Txt_OneWireBusID = New TextBox()
        Label17 = New Label()
        Txt_OneWireGPIOPin = New TextBox()
        Label16 = New Label()
        pnl_SensorConfig = New Panel()
        GB_I2C = New GroupBox()
        CB_i2cScan = New CheckBox()
        lbl_i2csavestate = New Label()
        BTN_i2cSettingsDelete = New Button()
        BTN_i2cSettingsSave = New Button()
        txt_i2cscl = New TextBox()
        Label19 = New Label()
        txt_i2csda = New TextBox()
        Label20 = New Label()
        Label15 = New Label()
        CBB_SensorType = New ComboBox()
        Label14 = New Label()
        CBB_SensoreGroup = New ComboBox()
        Label13 = New Label()
        DGV_Sensors = New DataGridView()
        CM_EditSensor = New ContextMenuStrip(components)
        Edit = New ToolStripMenuItem()
        AdvancedConfiguration = New ToolStripMenuItem()
        TabPage3 = New TabPage()
        BTN_AddDisplay = New Button()
        GroupBox1 = New GroupBox()
        TextBox1 = New TextBox()
        Label24 = New Label()
        Label27 = New Label()
        Button2 = New Button()
        Button3 = New Button()
        TextBox2 = New TextBox()
        Label28 = New Label()
        TextBox3 = New TextBox()
        Label29 = New Label()
        BTN_StopEditingDisplay = New Button()
        GroupBox2 = New GroupBox()
        TextBox4 = New TextBox()
        Label30 = New Label()
        ComboBox1 = New ComboBox()
        Label31 = New Label()
        Button5 = New Button()
        Button6 = New Button()
        TextBox5 = New TextBox()
        Label32 = New Label()
        Label33 = New Label()
        BTN_DeleteDisplay = New Button()
        GroupBox3 = New GroupBox()
        Label34 = New Label()
        Button8 = New Button()
        Button9 = New Button()
        TextBox6 = New TextBox()
        Label35 = New Label()
        TextBox7 = New TextBox()
        Label36 = New Label()
        pnl_DisplayConfig = New Panel()
        GroupBox4 = New GroupBox()
        CheckBox1 = New CheckBox()
        Label37 = New Label()
        Button10 = New Button()
        Button11 = New Button()
        TextBox8 = New TextBox()
        Label38 = New Label()
        TextBox9 = New TextBox()
        Label39 = New Label()
        Label40 = New Label()
        CBB_DisplayType = New ComboBox()
        Label41 = New Label()
        CBB_DisplayGroup = New ComboBox()
        Label42 = New Label()
        DGV_Display = New DataGridView()
        CM_EditDisplay = New ContextMenuStrip(components)
        EditDisplay = New ToolStripMenuItem()
        AdvancedConfigurationDisplay = New ToolStripMenuItem()
        StatusStrip1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        Webserver.SuspendLayout()
        TabPage2.SuspendLayout()
        GB_SPI.SuspendLayout()
        GB_Uart.SuspendLayout()
        GB_OneWire.SuspendLayout()
        GB_I2C.SuspendLayout()
        CType(DGV_Sensors, ComponentModel.ISupportInitialize).BeginInit()
        CM_EditSensor.SuspendLayout()
        TabPage3.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox4.SuspendLayout()
        CType(DGV_Display, ComponentModel.ISupportInitialize).BeginInit()
        CM_EditDisplay.SuspendLayout()
        SuspendLayout()
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {TSSL_Python, TSSL_ESPHome, ToolStripStatusLabel1, Label_ESPStatus})
        StatusStrip1.Location = New Point(0, 902)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(845, 22)
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
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(0, 17)
        ' 
        ' Label_ESPStatus
        ' 
        Label_ESPStatus.Name = "Label_ESPStatus"
        Label_ESPStatus.Size = New Size(0, 17)
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {DateiToolStripMenuItem1, DateiToolStripMenuItem, ExtrasToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(845, 24)
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
        TestESPConnection.Text = "ESP Suchen"
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
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Location = New Point(12, 27)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(827, 872)
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
        TabPage1.Size = New Size(819, 844)
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
        Webserver.Location = New Point(486, 17)
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
        RichTextBox1.Size = New Size(796, 346)
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
        TabPage2.Controls.Add(BTN_AddSensor)
        TabPage2.Controls.Add(GB_SPI)
        TabPage2.Controls.Add(BTN_StopEditingSensor)
        TabPage2.Controls.Add(GB_Uart)
        TabPage2.Controls.Add(BTN_DeleteSelectedSensor)
        TabPage2.Controls.Add(GB_OneWire)
        TabPage2.Controls.Add(pnl_SensorConfig)
        TabPage2.Controls.Add(GB_I2C)
        TabPage2.Controls.Add(Label15)
        TabPage2.Controls.Add(CBB_SensorType)
        TabPage2.Controls.Add(Label14)
        TabPage2.Controls.Add(CBB_SensoreGroup)
        TabPage2.Controls.Add(Label13)
        TabPage2.Controls.Add(DGV_Sensors)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(819, 844)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Sensoren"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' BTN_AddSensor
        ' 
        BTN_AddSensor.Location = New Point(480, 573)
        BTN_AddSensor.Name = "BTN_AddSensor"
        BTN_AddSensor.Size = New Size(323, 34)
        BTN_AddSensor.TabIndex = 8
        BTN_AddSensor.Text = "Sensor Hinzufügen"
        BTN_AddSensor.UseVisualStyleBackColor = True
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
        GB_SPI.Location = New Point(480, 389)
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
        ' BTN_StopEditingSensor
        ' 
        BTN_StopEditingSensor.ForeColor = Color.Red
        BTN_StopEditingSensor.Location = New Point(303, 62)
        BTN_StopEditingSensor.Name = "BTN_StopEditingSensor"
        BTN_StopEditingSensor.Size = New Size(171, 34)
        BTN_StopEditingSensor.TabIndex = 13
        BTN_StopEditingSensor.Text = "Bearbeiten beenden"
        BTN_StopEditingSensor.UseVisualStyleBackColor = True
        BTN_StopEditingSensor.Visible = False
        ' 
        ' GB_Uart
        ' 
        GB_Uart.Controls.Add(txt_UartTx)
        GB_Uart.Controls.Add(Label18)
        GB_Uart.Controls.Add(CBB_UartBaudrate)
        GB_Uart.Controls.Add(lbl_uartsavestate)
        GB_Uart.Controls.Add(BTN_uartSettingsDelete)
        GB_Uart.Controls.Add(BTN_uartSettingsSave)
        GB_Uart.Controls.Add(txt_UartRx)
        GB_Uart.Controls.Add(Label25)
        GB_Uart.Controls.Add(Label26)
        GB_Uart.Location = New Point(480, 235)
        GB_Uart.Name = "GB_Uart"
        GB_Uart.Size = New Size(333, 148)
        GB_Uart.TabIndex = 13
        GB_Uart.TabStop = False
        GB_Uart.Text = "Uart"
        GB_Uart.Visible = False
        ' 
        ' txt_UartTx
        ' 
        txt_UartTx.Location = New Point(6, 39)
        txt_UartTx.Name = "txt_UartTx"
        txt_UartTx.Size = New Size(155, 23)
        txt_UartTx.TabIndex = 9
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(6, 65)
        Label18.Name = "Label18"
        Label18.Size = New Size(54, 15)
        Label18.TabIndex = 8
        Label18.Text = "Baudrate"
        ' 
        ' CBB_UartBaudrate
        ' 
        CBB_UartBaudrate.FormattingEnabled = True
        CBB_UartBaudrate.Items.AddRange(New Object() {"9600", "115200", "38400", "19200"})
        CBB_UartBaudrate.Location = New Point(6, 85)
        CBB_UartBaudrate.Name = "CBB_UartBaudrate"
        CBB_UartBaudrate.Size = New Size(155, 23)
        CBB_UartBaudrate.TabIndex = 7
        ' 
        ' lbl_uartsavestate
        ' 
        lbl_uartsavestate.AutoSize = True
        lbl_uartsavestate.Location = New Point(6, 118)
        lbl_uartsavestate.Name = "lbl_uartsavestate"
        lbl_uartsavestate.Size = New Size(0, 15)
        lbl_uartsavestate.TabIndex = 6
        ' 
        ' BTN_uartSettingsDelete
        ' 
        BTN_uartSettingsDelete.ForeColor = Color.Red
        BTN_uartSettingsDelete.Location = New Point(248, 110)
        BTN_uartSettingsDelete.Name = "BTN_uartSettingsDelete"
        BTN_uartSettingsDelete.Size = New Size(75, 23)
        BTN_uartSettingsDelete.TabIndex = 5
        BTN_uartSettingsDelete.Text = "Löschen"
        BTN_uartSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_uartSettingsSave
        ' 
        BTN_uartSettingsSave.Location = New Point(167, 110)
        BTN_uartSettingsSave.Name = "BTN_uartSettingsSave"
        BTN_uartSettingsSave.Size = New Size(75, 23)
        BTN_uartSettingsSave.TabIndex = 4
        BTN_uartSettingsSave.Text = "Speichern"
        BTN_uartSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' txt_UartRx
        ' 
        txt_UartRx.Location = New Point(167, 39)
        txt_UartRx.Name = "txt_UartRx"
        txt_UartRx.Size = New Size(155, 23)
        txt_UartRx.TabIndex = 3
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(167, 18)
        Label25.Name = "Label25"
        Label25.Size = New Size(38, 15)
        Label25.TabIndex = 2
        Label25.Text = "rx PIN"
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(6, 21)
        Label26.Name = "Label26"
        Label26.Size = New Size(38, 15)
        Label26.TabIndex = 0
        Label26.Text = "tx PIN"
        ' 
        ' BTN_DeleteSelectedSensor
        ' 
        BTN_DeleteSelectedSensor.Location = New Point(3, 804)
        BTN_DeleteSelectedSensor.Name = "BTN_DeleteSelectedSensor"
        BTN_DeleteSelectedSensor.Size = New Size(808, 34)
        BTN_DeleteSelectedSensor.TabIndex = 9
        BTN_DeleteSelectedSensor.Text = "Ausgewählten Sensor Löschen"
        BTN_DeleteSelectedSensor.UseVisualStyleBackColor = True
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
        GB_OneWire.Location = New Point(480, 126)
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
        ' pnl_SensorConfig
        ' 
        pnl_SensorConfig.AutoScroll = True
        pnl_SensorConfig.Location = New Point(16, 102)
        pnl_SensorConfig.Name = "pnl_SensorConfig"
        pnl_SensorConfig.Size = New Size(458, 505)
        pnl_SensorConfig.TabIndex = 6
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
        GB_I2C.Location = New Point(480, 17)
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
        Label13.Location = New Point(16, 610)
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
        DGV_Sensors.Location = New Point(0, 628)
        DGV_Sensors.Name = "DGV_Sensors"
        DGV_Sensors.Size = New Size(811, 170)
        DGV_Sensors.TabIndex = 0
        ' 
        ' CM_EditSensor
        ' 
        CM_EditSensor.Items.AddRange(New ToolStripItem() {Edit, AdvancedConfiguration})
        CM_EditSensor.Name = "CM_EditSensor"
        CM_EditSensor.Size = New Size(203, 48)
        ' 
        ' Edit
        ' 
        Edit.Name = "Edit"
        Edit.Size = New Size(202, 22)
        Edit.Text = "Bearbeiten"
        ' 
        ' AdvancedConfiguration
        ' 
        AdvancedConfiguration.Name = "AdvancedConfiguration"
        AdvancedConfiguration.Size = New Size(202, 22)
        AdvancedConfiguration.Text = "Erweiterte Konfiguration"
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(BTN_AddDisplay)
        TabPage3.Controls.Add(GroupBox1)
        TabPage3.Controls.Add(BTN_StopEditingDisplay)
        TabPage3.Controls.Add(GroupBox2)
        TabPage3.Controls.Add(BTN_DeleteDisplay)
        TabPage3.Controls.Add(GroupBox3)
        TabPage3.Controls.Add(pnl_DisplayConfig)
        TabPage3.Controls.Add(GroupBox4)
        TabPage3.Controls.Add(Label40)
        TabPage3.Controls.Add(CBB_DisplayType)
        TabPage3.Controls.Add(Label41)
        TabPage3.Controls.Add(CBB_DisplayGroup)
        TabPage3.Controls.Add(Label42)
        TabPage3.Controls.Add(DGV_Display)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(819, 844)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Display"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' BTN_AddDisplay
        ' 
        BTN_AddDisplay.Location = New Point(483, 573)
        BTN_AddDisplay.Name = "BTN_AddDisplay"
        BTN_AddDisplay.Size = New Size(323, 34)
        BTN_AddDisplay.TabIndex = 21
        BTN_AddDisplay.Text = "Display Hinzufügen"
        BTN_AddDisplay.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(Label24)
        GroupBox1.Controls.Add(Label27)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label28)
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(Label29)
        GroupBox1.Location = New Point(483, 389)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(333, 148)
        GroupBox1.TabIndex = 25
        GroupBox1.TabStop = False
        GroupBox1.Text = "SPI"
        GroupBox1.Visible = False
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(6, 83)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(155, 23)
        TextBox1.TabIndex = 8
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(6, 65)
        Label24.Name = "Label24"
        Label24.Size = New Size(55, 15)
        Label24.TabIndex = 7
        Label24.Text = "miso PIN"
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(6, 118)
        Label27.Name = "Label27"
        Label27.Size = New Size(0, 15)
        Label27.TabIndex = 6
        ' 
        ' Button2
        ' 
        Button2.ForeColor = Color.Red
        Button2.Location = New Point(248, 110)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 5
        Button2.Text = "Löschen"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(167, 110)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 23)
        Button3.TabIndex = 4
        Button3.Text = "Speichern"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(167, 39)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(155, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.Location = New Point(167, 18)
        Label28.Name = "Label28"
        Label28.Size = New Size(55, 15)
        Label28.TabIndex = 2
        Label28.Text = "mosi PIN"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(6, 39)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(155, 23)
        TextBox3.TabIndex = 1
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(6, 21)
        Label29.Name = "Label29"
        Label29.Size = New Size(44, 15)
        Label29.TabIndex = 0
        Label29.Text = "clk PIN"
        ' 
        ' BTN_StopEditingDisplay
        ' 
        BTN_StopEditingDisplay.ForeColor = Color.Red
        BTN_StopEditingDisplay.Location = New Point(306, 62)
        BTN_StopEditingDisplay.Name = "BTN_StopEditingDisplay"
        BTN_StopEditingDisplay.Size = New Size(171, 34)
        BTN_StopEditingDisplay.TabIndex = 26
        BTN_StopEditingDisplay.Text = "Bearbeiten beenden"
        BTN_StopEditingDisplay.UseVisualStyleBackColor = True
        BTN_StopEditingDisplay.Visible = False
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(TextBox4)
        GroupBox2.Controls.Add(Label30)
        GroupBox2.Controls.Add(ComboBox1)
        GroupBox2.Controls.Add(Label31)
        GroupBox2.Controls.Add(Button5)
        GroupBox2.Controls.Add(Button6)
        GroupBox2.Controls.Add(TextBox5)
        GroupBox2.Controls.Add(Label32)
        GroupBox2.Controls.Add(Label33)
        GroupBox2.Location = New Point(483, 235)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(333, 148)
        GroupBox2.TabIndex = 27
        GroupBox2.TabStop = False
        GroupBox2.Text = "Uart"
        GroupBox2.Visible = False
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(6, 39)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(155, 23)
        TextBox4.TabIndex = 9
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(6, 65)
        Label30.Name = "Label30"
        Label30.Size = New Size(54, 15)
        Label30.TabIndex = 8
        Label30.Text = "Baudrate"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"9600", "115200", "38400", "19200"})
        ComboBox1.Location = New Point(6, 85)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(155, 23)
        ComboBox1.TabIndex = 7
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(6, 118)
        Label31.Name = "Label31"
        Label31.Size = New Size(0, 15)
        Label31.TabIndex = 6
        ' 
        ' Button5
        ' 
        Button5.ForeColor = Color.Red
        Button5.Location = New Point(248, 110)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 23)
        Button5.TabIndex = 5
        Button5.Text = "Löschen"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(167, 110)
        Button6.Name = "Button6"
        Button6.Size = New Size(75, 23)
        Button6.TabIndex = 4
        Button6.Text = "Speichern"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(167, 39)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(155, 23)
        TextBox5.TabIndex = 3
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Location = New Point(167, 18)
        Label32.Name = "Label32"
        Label32.Size = New Size(38, 15)
        Label32.TabIndex = 2
        Label32.Text = "rx PIN"
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Location = New Point(6, 21)
        Label33.Name = "Label33"
        Label33.Size = New Size(38, 15)
        Label33.TabIndex = 0
        Label33.Text = "tx PIN"
        ' 
        ' BTN_DeleteDisplay
        ' 
        BTN_DeleteDisplay.Location = New Point(6, 804)
        BTN_DeleteDisplay.Name = "BTN_DeleteDisplay"
        BTN_DeleteDisplay.Size = New Size(808, 34)
        BTN_DeleteDisplay.TabIndex = 22
        BTN_DeleteDisplay.Text = "Ausgewählte Display Löschen"
        BTN_DeleteDisplay.UseVisualStyleBackColor = True
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(Label34)
        GroupBox3.Controls.Add(Button8)
        GroupBox3.Controls.Add(Button9)
        GroupBox3.Controls.Add(TextBox6)
        GroupBox3.Controls.Add(Label35)
        GroupBox3.Controls.Add(TextBox7)
        GroupBox3.Controls.Add(Label36)
        GroupBox3.Location = New Point(483, 126)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(333, 103)
        GroupBox3.TabIndex = 23
        GroupBox3.TabStop = False
        GroupBox3.Text = "1-Wire Bus"
        GroupBox3.Visible = False
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Location = New Point(6, 76)
        Label34.Name = "Label34"
        Label34.Size = New Size(0, 15)
        Label34.TabIndex = 6
        ' 
        ' Button8
        ' 
        Button8.ForeColor = Color.Red
        Button8.Location = New Point(247, 68)
        Button8.Name = "Button8"
        Button8.Size = New Size(75, 23)
        Button8.TabIndex = 5
        Button8.Text = "Löschen"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' Button9
        ' 
        Button9.Location = New Point(167, 68)
        Button9.Name = "Button9"
        Button9.Size = New Size(75, 23)
        Button9.TabIndex = 4
        Button9.Text = "Speichern"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(167, 39)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(155, 23)
        TextBox6.TabIndex = 3
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(167, 18)
        Label35.Name = "Label35"
        Label35.Size = New Size(40, 15)
        Label35.TabIndex = 2
        Label35.Text = "Bus ID"
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(6, 39)
        TextBox7.Name = "TextBox7"
        TextBox7.Size = New Size(155, 23)
        TextBox7.TabIndex = 1
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(6, 21)
        Label36.Name = "Label36"
        Label36.Size = New Size(56, 15)
        Label36.TabIndex = 0
        Label36.Text = "GPIO PIN"
        ' 
        ' pnl_DisplayConfig
        ' 
        pnl_DisplayConfig.AutoScroll = True
        pnl_DisplayConfig.Location = New Point(19, 102)
        pnl_DisplayConfig.Name = "pnl_DisplayConfig"
        pnl_DisplayConfig.Size = New Size(458, 505)
        pnl_DisplayConfig.TabIndex = 20
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(CheckBox1)
        GroupBox4.Controls.Add(Label37)
        GroupBox4.Controls.Add(Button10)
        GroupBox4.Controls.Add(Button11)
        GroupBox4.Controls.Add(TextBox8)
        GroupBox4.Controls.Add(Label38)
        GroupBox4.Controls.Add(TextBox9)
        GroupBox4.Controls.Add(Label39)
        GroupBox4.Location = New Point(483, 17)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(333, 103)
        GroupBox4.TabIndex = 24
        GroupBox4.TabStop = False
        GroupBox4.Text = "I2C"
        GroupBox4.Visible = False
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(6, 72)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(51, 19)
        CheckBox1.TabIndex = 7
        CheckBox1.Text = "Scan"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Location = New Point(72, 74)
        Label37.Name = "Label37"
        Label37.Size = New Size(0, 15)
        Label37.TabIndex = 6
        ' 
        ' Button10
        ' 
        Button10.ForeColor = Color.Red
        Button10.Location = New Point(247, 68)
        Button10.Name = "Button10"
        Button10.Size = New Size(75, 23)
        Button10.TabIndex = 5
        Button10.Text = "Löschen"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' Button11
        ' 
        Button11.Location = New Point(167, 68)
        Button11.Name = "Button11"
        Button11.Size = New Size(75, 23)
        Button11.TabIndex = 4
        Button11.Text = "Speichern"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' TextBox8
        ' 
        TextBox8.Location = New Point(167, 39)
        TextBox8.Name = "TextBox8"
        TextBox8.Size = New Size(155, 23)
        TextBox8.TabIndex = 3
        ' 
        ' Label38
        ' 
        Label38.AutoSize = True
        Label38.Location = New Point(167, 18)
        Label38.Name = "Label38"
        Label38.Size = New Size(43, 15)
        Label38.TabIndex = 2
        Label38.Text = "scl PIN"
        ' 
        ' TextBox9
        ' 
        TextBox9.Location = New Point(6, 39)
        TextBox9.Name = "TextBox9"
        TextBox9.Size = New Size(155, 23)
        TextBox9.TabIndex = 1
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Location = New Point(6, 21)
        Label39.Name = "Label39"
        Label39.Size = New Size(47, 15)
        Label39.TabIndex = 0
        Label39.Text = "sda PIN"
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Location = New Point(19, 55)
        Label40.Name = "Label40"
        Label40.Size = New Size(126, 15)
        Label40.TabIndex = 19
        Label40.Text = "Display Typ auswählen"
        ' 
        ' CBB_DisplayType
        ' 
        CBB_DisplayType.FormattingEnabled = True
        CBB_DisplayType.Location = New Point(19, 73)
        CBB_DisplayType.Name = "CBB_DisplayType"
        CBB_DisplayType.Size = New Size(264, 23)
        CBB_DisplayType.TabIndex = 18
        ' 
        ' Label41
        ' 
        Label41.AutoSize = True
        Label41.Location = New Point(18, 6)
        Label41.Name = "Label41"
        Label41.Size = New Size(146, 15)
        Label41.TabIndex = 17
        Label41.Text = "Display Gruppe auswählen"
        ' 
        ' CBB_DisplayGroup
        ' 
        CBB_DisplayGroup.FormattingEnabled = True
        CBB_DisplayGroup.Location = New Point(18, 24)
        CBB_DisplayGroup.Name = "CBB_DisplayGroup"
        CBB_DisplayGroup.Size = New Size(265, 23)
        CBB_DisplayGroup.TabIndex = 16
        ' 
        ' Label42
        ' 
        Label42.AutoSize = True
        Label42.Location = New Point(19, 610)
        Label42.Name = "Label42"
        Label42.Size = New Size(125, 15)
        Label42.TabIndex = 15
        Label42.Text = "Hinzugefügte Displays"
        ' 
        ' DGV_Display
        ' 
        DGV_Display.BackgroundColor = SystemColors.ControlLight
        DGV_Display.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV_Display.ContextMenuStrip = CM_EditSensor
        DGV_Display.Location = New Point(3, 628)
        DGV_Display.Name = "DGV_Display"
        DGV_Display.Size = New Size(811, 170)
        DGV_Display.TabIndex = 14
        ' 
        ' CM_EditDisplay
        ' 
        CM_EditDisplay.Items.AddRange(New ToolStripItem() {EditDisplay, AdvancedConfigurationDisplay})
        CM_EditDisplay.Name = "CM_EditSensor"
        CM_EditDisplay.Size = New Size(203, 70)
        ' 
        ' EditDisplay
        ' 
        EditDisplay.Name = "EditDisplay"
        EditDisplay.Size = New Size(202, 22)
        EditDisplay.Text = "Bearbeiten"
        ' 
        ' AdvancedConfigurationDisplay
        ' 
        AdvancedConfigurationDisplay.Name = "AdvancedConfigurationDisplay"
        AdvancedConfigurationDisplay.Size = New Size(202, 22)
        AdvancedConfigurationDisplay.Text = "Erweiterte Konfiguration"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(845, 924)
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
        GB_Uart.ResumeLayout(False)
        GB_Uart.PerformLayout()
        GB_OneWire.ResumeLayout(False)
        GB_OneWire.PerformLayout()
        GB_I2C.ResumeLayout(False)
        GB_I2C.PerformLayout()
        CType(DGV_Sensors, ComponentModel.ISupportInitialize).EndInit()
        CM_EditSensor.ResumeLayout(False)
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        CType(DGV_Display, ComponentModel.ISupportInitialize).EndInit()
        CM_EditDisplay.ResumeLayout(False)
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
    Friend WithEvents BTN_StopEditingSensor As Button
    Friend WithEvents AdvancedConfiguration As ToolStripMenuItem
    Friend WithEvents GB_Uart As GroupBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents lbl_uartsavestate As Label
    Friend WithEvents BTN_uartSettingsDelete As Button
    Friend WithEvents BTN_uartSettingsSave As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents txt_UartRx As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents CBB_UartBaudrate As ComboBox
    Friend WithEvents txt_UartTx As TextBox
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents Label_ESPStatus As ToolStripStatusLabel
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents BTN_AddDisplay As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label28 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents BTN_StopEditingDisplay As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents BTN_DeleteDisplay As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label34 As Label
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents pnl_DisplayConfig As Panel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label37 As Label
    Friend WithEvents Button10 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents Label38 As Label
    Friend WithEvents TextBox9 As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents CBB_DisplayType As ComboBox
    Friend WithEvents Label41 As Label
    Friend WithEvents CBB_DisplayGroup As ComboBox
    Friend WithEvents Label42 As Label
    Friend WithEvents DGV_Display As DataGridView
    Friend WithEvents CM_EditDisplay As ContextMenuStrip
    Friend WithEvents EditDisplay As ToolStripMenuItem
    Friend WithEvents AdvancedConfigurationDisplay As ToolStripMenuItem

End Class

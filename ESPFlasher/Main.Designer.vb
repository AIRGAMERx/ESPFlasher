<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
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
        Label16 = New Label()
        pnl_LiveCodingSensor = New Panel()
        RTB_yamlPreviewSensor = New RichTextBox()
        BTN_AddSensor = New Button()
        BTN_StopEditingSensor = New Button()
        BTN_DeleteSelectedSensor = New Button()
        pnl_SensorConfig = New Panel()
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
        Label17 = New Label()
        pnl_LiveCodingDisplay = New Panel()
        RTB_yamlPreviewDisplay = New RichTextBox()
        BTN_AddDisplay = New Button()
        BTN_StopEditingDisplay = New Button()
        BTN_DeleteDisplay = New Button()
        pnl_DisplayConfig = New Panel()
        Label40 = New Label()
        CBB_DisplayType = New ComboBox()
        Label41 = New Label()
        CBB_DisplayGroup = New ComboBox()
        Label42 = New Label()
        DGV_Display = New DataGridView()
        CM_EditDisplay = New ContextMenuStrip(components)
        EditDisplay = New ToolStripMenuItem()
        AdvancedConfigurationDisplay = New ToolStripMenuItem()
        TabPage4 = New TabPage()
        Label18 = New Label()
        pnl_LiveCodingTemplate = New Panel()
        RTB_yamlPreviewTemplate = New RichTextBox()
        BTN_AddTemplate = New Button()
        BTN_StopEdingTemplate = New Button()
        BTN_DeleteTemplate = New Button()
        pnl_TemplateConfig = New Panel()
        Label19 = New Label()
        CBB_TemplateType = New ComboBox()
        Label20 = New Label()
        CBB_TemplateGroup = New ComboBox()
        Label21 = New Label()
        DGV_Templates = New DataGridView()
        MenuStrip1 = New MenuStrip()
        OpenProjects = New ToolStripMenuItem()
        ProjektÖffnenToolStripMenuItem = New ToolStripMenuItem()
        TestConnection = New ToolStripMenuItem()
        VerbindungPrüfenToolStripMenuItem = New ToolStripMenuItem()
        BinErstellenUndFlashenToolStripMenuItem = New ToolStripMenuItem()
        YamlErstellenUndÖffnenToolStripMenuItem = New ToolStripMenuItem()
        ExtrasToolStripMenuItem = New ToolStripMenuItem()
        PythonWebseiteÖffnenToolStripMenuItem = New ToolStripMenuItem()
        PythonWebseiteÖffnenToolStripMenuItem1 = New ToolStripMenuItem()
        ESPHomePerPIPInstallierenToolStripMenuItem = New ToolStripMenuItem()
        OTAUpdateToolStripMenuItem = New ToolStripMenuItem()
        EinstellungenToolStripMenuItem = New ToolStripMenuItem()
        BusEinstellungenToolStripMenuItem = New ToolStripMenuItem()
        BuyMeACoffeeToolStripMenuItem = New ToolStripMenuItem()
        StatusStrip1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        Webserver.SuspendLayout()
        TabPage2.SuspendLayout()
        pnl_LiveCodingSensor.SuspendLayout()
        CType(DGV_Sensors, ComponentModel.ISupportInitialize).BeginInit()
        CM_EditSensor.SuspendLayout()
        TabPage3.SuspendLayout()
        pnl_LiveCodingDisplay.SuspendLayout()
        CType(DGV_Display, ComponentModel.ISupportInitialize).BeginInit()
        CM_EditDisplay.SuspendLayout()
        TabPage4.SuspendLayout()
        pnl_LiveCodingTemplate.SuspendLayout()
        CType(DGV_Templates, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
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
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Controls.Add(TabPage4)
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
        TabPage2.Controls.Add(Label16)
        TabPage2.Controls.Add(pnl_LiveCodingSensor)
        TabPage2.Controls.Add(BTN_AddSensor)
        TabPage2.Controls.Add(BTN_StopEditingSensor)
        TabPage2.Controls.Add(BTN_DeleteSelectedSensor)
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
        TabPage2.Size = New Size(819, 844)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Sensoren"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(483, 84)
        Label16.Name = "Label16"
        Label16.Size = New Size(70, 15)
        Label16.TabIndex = 15
        Label16.Text = "Live Coding"
        ' 
        ' pnl_LiveCodingSensor
        ' 
        pnl_LiveCodingSensor.Controls.Add(RTB_yamlPreviewSensor)
        pnl_LiveCodingSensor.Location = New Point(480, 102)
        pnl_LiveCodingSensor.Name = "pnl_LiveCodingSensor"
        pnl_LiveCodingSensor.Size = New Size(323, 465)
        pnl_LiveCodingSensor.TabIndex = 14
        ' 
        ' RTB_yamlPreviewSensor
        ' 
        RTB_yamlPreviewSensor.Location = New Point(3, 3)
        RTB_yamlPreviewSensor.Name = "RTB_yamlPreviewSensor"
        RTB_yamlPreviewSensor.Size = New Size(317, 459)
        RTB_yamlPreviewSensor.TabIndex = 0
        RTB_yamlPreviewSensor.Text = ""
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
        ' BTN_DeleteSelectedSensor
        ' 
        BTN_DeleteSelectedSensor.Location = New Point(3, 804)
        BTN_DeleteSelectedSensor.Name = "BTN_DeleteSelectedSensor"
        BTN_DeleteSelectedSensor.Size = New Size(808, 34)
        BTN_DeleteSelectedSensor.TabIndex = 9
        BTN_DeleteSelectedSensor.Text = "Ausgewählten Sensor Löschen"
        BTN_DeleteSelectedSensor.UseVisualStyleBackColor = True
        ' 
        ' pnl_SensorConfig
        ' 
        pnl_SensorConfig.AutoScroll = True
        pnl_SensorConfig.Location = New Point(16, 102)
        pnl_SensorConfig.Name = "pnl_SensorConfig"
        pnl_SensorConfig.Size = New Size(458, 505)
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
        TabPage3.Controls.Add(Label17)
        TabPage3.Controls.Add(pnl_LiveCodingDisplay)
        TabPage3.Controls.Add(BTN_AddDisplay)
        TabPage3.Controls.Add(BTN_StopEditingDisplay)
        TabPage3.Controls.Add(BTN_DeleteDisplay)
        TabPage3.Controls.Add(pnl_DisplayConfig)
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
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(490, 81)
        Label17.Name = "Label17"
        Label17.Size = New Size(70, 15)
        Label17.TabIndex = 28
        Label17.Text = "Live Coding"
        ' 
        ' pnl_LiveCodingDisplay
        ' 
        pnl_LiveCodingDisplay.Controls.Add(RTB_yamlPreviewDisplay)
        pnl_LiveCodingDisplay.Location = New Point(483, 102)
        pnl_LiveCodingDisplay.Name = "pnl_LiveCodingDisplay"
        pnl_LiveCodingDisplay.Size = New Size(323, 465)
        pnl_LiveCodingDisplay.TabIndex = 27
        ' 
        ' RTB_yamlPreviewDisplay
        ' 
        RTB_yamlPreviewDisplay.Location = New Point(3, 3)
        RTB_yamlPreviewDisplay.Name = "RTB_yamlPreviewDisplay"
        RTB_yamlPreviewDisplay.Size = New Size(317, 459)
        RTB_yamlPreviewDisplay.TabIndex = 0
        RTB_yamlPreviewDisplay.Text = ""
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
        ' BTN_DeleteDisplay
        ' 
        BTN_DeleteDisplay.Location = New Point(6, 804)
        BTN_DeleteDisplay.Name = "BTN_DeleteDisplay"
        BTN_DeleteDisplay.Size = New Size(808, 34)
        BTN_DeleteDisplay.TabIndex = 22
        BTN_DeleteDisplay.Text = "Ausgewählte Display Löschen"
        BTN_DeleteDisplay.UseVisualStyleBackColor = True
        ' 
        ' pnl_DisplayConfig
        ' 
        pnl_DisplayConfig.AutoScroll = True
        pnl_DisplayConfig.Location = New Point(19, 102)
        pnl_DisplayConfig.Name = "pnl_DisplayConfig"
        pnl_DisplayConfig.Size = New Size(458, 505)
        pnl_DisplayConfig.TabIndex = 20
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
        DGV_Display.ContextMenuStrip = CM_EditDisplay
        DGV_Display.Location = New Point(3, 628)
        DGV_Display.Name = "DGV_Display"
        DGV_Display.Size = New Size(811, 170)
        DGV_Display.TabIndex = 14
        ' 
        ' CM_EditDisplay
        ' 
        CM_EditDisplay.Items.AddRange(New ToolStripItem() {EditDisplay, AdvancedConfigurationDisplay})
        CM_EditDisplay.Name = "CM_EditSensor"
        CM_EditDisplay.Size = New Size(203, 48)
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
        ' TabPage4
        ' 
        TabPage4.Controls.Add(Label18)
        TabPage4.Controls.Add(pnl_LiveCodingTemplate)
        TabPage4.Controls.Add(BTN_AddTemplate)
        TabPage4.Controls.Add(BTN_StopEdingTemplate)
        TabPage4.Controls.Add(BTN_DeleteTemplate)
        TabPage4.Controls.Add(pnl_TemplateConfig)
        TabPage4.Controls.Add(Label19)
        TabPage4.Controls.Add(CBB_TemplateType)
        TabPage4.Controls.Add(Label20)
        TabPage4.Controls.Add(CBB_TemplateGroup)
        TabPage4.Controls.Add(Label21)
        TabPage4.Controls.Add(DGV_Templates)
        TabPage4.Location = New Point(4, 24)
        TabPage4.Name = "TabPage4"
        TabPage4.Padding = New Padding(3)
        TabPage4.Size = New Size(819, 844)
        TabPage4.TabIndex = 3
        TabPage4.Text = "Template"
        TabPage4.UseVisualStyleBackColor = True
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(491, 81)
        Label18.Name = "Label18"
        Label18.Size = New Size(70, 15)
        Label18.TabIndex = 40
        Label18.Text = "Live Coding"
        ' 
        ' pnl_LiveCodingTemplate
        ' 
        pnl_LiveCodingTemplate.Controls.Add(RTB_yamlPreviewTemplate)
        pnl_LiveCodingTemplate.Location = New Point(484, 102)
        pnl_LiveCodingTemplate.Name = "pnl_LiveCodingTemplate"
        pnl_LiveCodingTemplate.Size = New Size(323, 465)
        pnl_LiveCodingTemplate.TabIndex = 39
        ' 
        ' RTB_yamlPreviewTemplate
        ' 
        RTB_yamlPreviewTemplate.Location = New Point(3, 3)
        RTB_yamlPreviewTemplate.Name = "RTB_yamlPreviewTemplate"
        RTB_yamlPreviewTemplate.Size = New Size(317, 459)
        RTB_yamlPreviewTemplate.TabIndex = 0
        RTB_yamlPreviewTemplate.Text = ""
        ' 
        ' BTN_AddTemplate
        ' 
        BTN_AddTemplate.Location = New Point(484, 573)
        BTN_AddTemplate.Name = "BTN_AddTemplate"
        BTN_AddTemplate.Size = New Size(323, 34)
        BTN_AddTemplate.TabIndex = 36
        BTN_AddTemplate.Text = "Templates Hinzufügen"
        BTN_AddTemplate.UseVisualStyleBackColor = True
        ' 
        ' BTN_StopEdingTemplate
        ' 
        BTN_StopEdingTemplate.ForeColor = Color.Red
        BTN_StopEdingTemplate.Location = New Point(307, 62)
        BTN_StopEdingTemplate.Name = "BTN_StopEdingTemplate"
        BTN_StopEdingTemplate.Size = New Size(171, 34)
        BTN_StopEdingTemplate.TabIndex = 38
        BTN_StopEdingTemplate.Text = "Bearbeiten beenden"
        BTN_StopEdingTemplate.UseVisualStyleBackColor = True
        BTN_StopEdingTemplate.Visible = False
        ' 
        ' BTN_DeleteTemplate
        ' 
        BTN_DeleteTemplate.Location = New Point(7, 804)
        BTN_DeleteTemplate.Name = "BTN_DeleteTemplate"
        BTN_DeleteTemplate.Size = New Size(808, 34)
        BTN_DeleteTemplate.TabIndex = 37
        BTN_DeleteTemplate.Text = "Ausgewählte Template Löschen"
        BTN_DeleteTemplate.UseVisualStyleBackColor = True
        ' 
        ' pnl_TemplateConfig
        ' 
        pnl_TemplateConfig.AutoScroll = True
        pnl_TemplateConfig.Location = New Point(20, 102)
        pnl_TemplateConfig.Name = "pnl_TemplateConfig"
        pnl_TemplateConfig.Size = New Size(458, 505)
        pnl_TemplateConfig.TabIndex = 35
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(20, 55)
        Label19.Name = "Label19"
        Label19.Size = New Size(142, 15)
        Label19.TabIndex = 34
        Label19.Text = "Templates Typ auswählen"
        ' 
        ' CBB_TemplateType
        ' 
        CBB_TemplateType.FormattingEnabled = True
        CBB_TemplateType.Location = New Point(20, 73)
        CBB_TemplateType.Name = "CBB_TemplateType"
        CBB_TemplateType.Size = New Size(264, 23)
        CBB_TemplateType.TabIndex = 33
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(19, 6)
        Label20.Name = "Label20"
        Label20.Size = New Size(157, 15)
        Label20.TabIndex = 32
        Label20.Text = "Template Gruppe auswählen"
        ' 
        ' CBB_TemplateGroup
        ' 
        CBB_TemplateGroup.FormattingEnabled = True
        CBB_TemplateGroup.Location = New Point(19, 24)
        CBB_TemplateGroup.Name = "CBB_TemplateGroup"
        CBB_TemplateGroup.Size = New Size(265, 23)
        CBB_TemplateGroup.TabIndex = 31
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(20, 610)
        Label21.Name = "Label21"
        Label21.Size = New Size(136, 15)
        Label21.TabIndex = 30
        Label21.Text = "Hinzugefügte Templates"
        ' 
        ' DGV_Templates
        ' 
        DGV_Templates.BackgroundColor = SystemColors.ControlLight
        DGV_Templates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV_Templates.ContextMenuStrip = CM_EditSensor
        DGV_Templates.Location = New Point(4, 628)
        DGV_Templates.Name = "DGV_Templates"
        DGV_Templates.Size = New Size(811, 170)
        DGV_Templates.TabIndex = 29
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {OpenProjects, TestConnection, ExtrasToolStripMenuItem, EinstellungenToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(845, 24)
        MenuStrip1.TabIndex = 3
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' OpenProjects
        ' 
        OpenProjects.DropDownItems.AddRange(New ToolStripItem() {ProjektÖffnenToolStripMenuItem})
        OpenProjects.Name = "OpenProjects"
        OpenProjects.Size = New Size(46, 20)
        OpenProjects.Text = "Datei"
        ' 
        ' ProjektÖffnenToolStripMenuItem
        ' 
        ProjektÖffnenToolStripMenuItem.Name = "ProjektÖffnenToolStripMenuItem"
        ProjektÖffnenToolStripMenuItem.Size = New Size(149, 22)
        ProjektÖffnenToolStripMenuItem.Text = "Projekt öffnen"
        ' 
        ' TestConnection
        ' 
        TestConnection.DropDownItems.AddRange(New ToolStripItem() {VerbindungPrüfenToolStripMenuItem, BinErstellenUndFlashenToolStripMenuItem, YamlErstellenUndÖffnenToolStripMenuItem})
        TestConnection.Name = "TestConnection"
        TestConnection.Size = New Size(74, 20)
        TestConnection.Text = "Ausführen"
        ' 
        ' VerbindungPrüfenToolStripMenuItem
        ' 
        VerbindungPrüfenToolStripMenuItem.Name = "VerbindungPrüfenToolStripMenuItem"
        VerbindungPrüfenToolStripMenuItem.Size = New Size(209, 22)
        VerbindungPrüfenToolStripMenuItem.Text = "Verbindung prüfen"
        ' 
        ' BinErstellenUndFlashenToolStripMenuItem
        ' 
        BinErstellenUndFlashenToolStripMenuItem.Name = "BinErstellenUndFlashenToolStripMenuItem"
        BinErstellenUndFlashenToolStripMenuItem.Size = New Size(209, 22)
        BinErstellenUndFlashenToolStripMenuItem.Text = ".bin erstellen und flashen"
        ' 
        ' YamlErstellenUndÖffnenToolStripMenuItem
        ' 
        YamlErstellenUndÖffnenToolStripMenuItem.Name = "YamlErstellenUndÖffnenToolStripMenuItem"
        YamlErstellenUndÖffnenToolStripMenuItem.Size = New Size(209, 22)
        YamlErstellenUndÖffnenToolStripMenuItem.Text = "Yaml erstellen und öffnen"
        ' 
        ' ExtrasToolStripMenuItem
        ' 
        ExtrasToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {PythonWebseiteÖffnenToolStripMenuItem, OTAUpdateToolStripMenuItem, BuyMeACoffeeToolStripMenuItem})
        ExtrasToolStripMenuItem.Name = "ExtrasToolStripMenuItem"
        ExtrasToolStripMenuItem.Size = New Size(49, 20)
        ExtrasToolStripMenuItem.Text = "Extras"
        ' 
        ' PythonWebseiteÖffnenToolStripMenuItem
        ' 
        PythonWebseiteÖffnenToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {PythonWebseiteÖffnenToolStripMenuItem1, ESPHomePerPIPInstallierenToolStripMenuItem})
        PythonWebseiteÖffnenToolStripMenuItem.Name = "PythonWebseiteÖffnenToolStripMenuItem"
        PythonWebseiteÖffnenToolStripMenuItem.Size = New Size(180, 22)
        PythonWebseiteÖffnenToolStripMenuItem.Text = "Externe Programme"
        ' 
        ' PythonWebseiteÖffnenToolStripMenuItem1
        ' 
        PythonWebseiteÖffnenToolStripMenuItem1.Name = "PythonWebseiteÖffnenToolStripMenuItem1"
        PythonWebseiteÖffnenToolStripMenuItem1.Size = New Size(226, 22)
        PythonWebseiteÖffnenToolStripMenuItem1.Text = "Python Webseite öffnen"
        ' 
        ' ESPHomePerPIPInstallierenToolStripMenuItem
        ' 
        ESPHomePerPIPInstallierenToolStripMenuItem.Name = "ESPHomePerPIPInstallierenToolStripMenuItem"
        ESPHomePerPIPInstallierenToolStripMenuItem.Size = New Size(226, 22)
        ESPHomePerPIPInstallierenToolStripMenuItem.Text = "ESPHome per PIP installieren"
        ' 
        ' OTAUpdateToolStripMenuItem
        ' 
        OTAUpdateToolStripMenuItem.Name = "OTAUpdateToolStripMenuItem"
        OTAUpdateToolStripMenuItem.Size = New Size(180, 22)
        OTAUpdateToolStripMenuItem.Text = "OTA Update"
        ' 
        ' EinstellungenToolStripMenuItem
        ' 
        EinstellungenToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {BusEinstellungenToolStripMenuItem})
        EinstellungenToolStripMenuItem.Name = "EinstellungenToolStripMenuItem"
        EinstellungenToolStripMenuItem.Size = New Size(90, 20)
        EinstellungenToolStripMenuItem.Text = "Einstellungen"
        ' 
        ' BusEinstellungenToolStripMenuItem
        ' 
        BusEinstellungenToolStripMenuItem.Name = "BusEinstellungenToolStripMenuItem"
        BusEinstellungenToolStripMenuItem.Size = New Size(167, 22)
        BusEinstellungenToolStripMenuItem.Text = "Bus einstellungen"
        ' 
        ' BuyMeACoffeeToolStripMenuItem
        ' 
        BuyMeACoffeeToolStripMenuItem.Name = "BuyMeACoffeeToolStripMenuItem"
        BuyMeACoffeeToolStripMenuItem.Size = New Size(180, 22)
        BuyMeACoffeeToolStripMenuItem.Text = "Buy me a Coffee"
        ' 
        ' Main
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(845, 924)
        Controls.Add(TabControl1)
        Controls.Add(StatusStrip1)
        Controls.Add(MenuStrip1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MainMenuStrip = MenuStrip1
        Name = "Main"
        Text = "ESPFlasher"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        Webserver.ResumeLayout(False)
        Webserver.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        pnl_LiveCodingSensor.ResumeLayout(False)
        CType(DGV_Sensors, ComponentModel.ISupportInitialize).EndInit()
        CM_EditSensor.ResumeLayout(False)
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        pnl_LiveCodingDisplay.ResumeLayout(False)
        CType(DGV_Display, ComponentModel.ISupportInitialize).EndInit()
        CM_EditDisplay.ResumeLayout(False)
        TabPage4.ResumeLayout(False)
        TabPage4.PerformLayout()
        pnl_LiveCodingTemplate.ResumeLayout(False)
        CType(DGV_Templates, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TSSL_Python As ToolStripStatusLabel
    Friend WithEvents TSSL_ESPHome As ToolStripStatusLabel
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
    Friend WithEvents Label9 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
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
    Friend WithEvents CM_EditSensor As ContextMenuStrip
    Friend WithEvents Edit As ToolStripMenuItem
    Friend WithEvents BTN_StopEditingSensor As Button
    Friend WithEvents AdvancedConfiguration As ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents Label_ESPStatus As ToolStripStatusLabel
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents BTN_AddDisplay As Button
    Friend WithEvents BTN_StopEditingDisplay As Button
    Friend WithEvents BTN_DeleteDisplay As Button
    Friend WithEvents pnl_DisplayConfig As Panel
    Friend WithEvents Label40 As Label
    Friend WithEvents CBB_DisplayType As ComboBox
    Friend WithEvents Label41 As Label
    Friend WithEvents CBB_DisplayGroup As ComboBox
    Friend WithEvents Label42 As Label
    Friend WithEvents DGV_Display As DataGridView
    Friend WithEvents CM_EditDisplay As ContextMenuStrip
    Friend WithEvents EditDisplay As ToolStripMenuItem
    Friend WithEvents AdvancedConfigurationDisplay As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents OpenProjects As ToolStripMenuItem
    Friend WithEvents ProjektÖffnenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TestConnection As ToolStripMenuItem
    Friend WithEvents VerbindungPrüfenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BinErstellenUndFlashenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtrasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PythonWebseiteÖffnenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PythonWebseiteÖffnenToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ESPHomePerPIPInstallierenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents YamlErstellenUndÖffnenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EinstellungenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BusEinstellungenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents pnl_LiveCodingSensor As Panel
    Friend WithEvents RTB_yamlPreviewSensor As RichTextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents pnl_LiveCodingDisplay As Panel
    Friend WithEvents RTB_yamlPreviewDisplay As RichTextBox
    Friend WithEvents OTAUpdateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Label18 As Label
    Friend WithEvents pnl_LiveCodingTemplate As Panel
    Friend WithEvents RTB_yamlPreviewTemplate As RichTextBox
    Friend WithEvents BTN_AddTemplate As Button
    Friend WithEvents BTN_StopEdingTemplate As Button
    Friend WithEvents BTN_DeleteTemplate As Button
    Friend WithEvents pnl_TemplateConfig As Panel
    Friend WithEvents Label19 As Label
    Friend WithEvents CBB_TemplateType As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents CBB_TemplateGroup As ComboBox
    Friend WithEvents Label21 As Label
    Friend WithEvents DGV_Templates As DataGridView
    Friend WithEvents BuyMeACoffeeToolStripMenuItem As ToolStripMenuItem

End Class

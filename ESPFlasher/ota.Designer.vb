<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OTA
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        LB_Devices = New ListBox()
        Label1 = New Label()
        TB_SelectedIP = New TextBox()
        Label2 = New Label()
        BTN_Scan = New Button()
        BTN_FlashOTA = New Button()
        Label3 = New Label()
        TB_OtaPassword = New TextBox()
        rtb_compilelog = New RichTextBox()
        Label4 = New Label()
        BTN_PingAdress = New Button()
        StatusStrip1 = New StatusStrip()
        PB_Scan = New ToolStripProgressBar()
        CB_PortScan = New CheckBox()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' LB_Devices
        ' 
        LB_Devices.FormattingEnabled = True
        LB_Devices.ItemHeight = 15
        LB_Devices.Location = New Point(12, 27)
        LB_Devices.Name = "LB_Devices"
        LB_Devices.ScrollAlwaysVisible = True
        LB_Devices.Size = New Size(409, 154)
        LB_Devices.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(208, 15)
        Label1.TabIndex = 2
        Label1.Text = "Alle gefunden ESP geräte im Netzwerk"
        ' 
        ' TB_SelectedIP
        ' 
        TB_SelectedIP.Location = New Point(436, 47)
        TB_SelectedIP.Name = "TB_SelectedIP"
        TB_SelectedIP.Size = New Size(293, 23)
        TB_SelectedIP.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(436, 29)
        Label2.Name = "Label2"
        Label2.Size = New Size(130, 15)
        Label2.TabIndex = 4
        Label2.Text = "Ausgewählte IP adresse"
        ' 
        ' BTN_Scan
        ' 
        BTN_Scan.Location = New Point(12, 212)
        BTN_Scan.Name = "BTN_Scan"
        BTN_Scan.Size = New Size(409, 23)
        BTN_Scan.TabIndex = 5
        BTN_Scan.Text = "Nach geräten suchen"
        BTN_Scan.UseVisualStyleBackColor = True
        ' 
        ' BTN_FlashOTA
        ' 
        BTN_FlashOTA.Location = New Point(436, 212)
        BTN_FlashOTA.Name = "BTN_FlashOTA"
        BTN_FlashOTA.Size = New Size(293, 23)
        BTN_FlashOTA.TabIndex = 6
        BTN_FlashOTA.Text = "OTA Durchführen"
        BTN_FlashOTA.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(436, 165)
        Label3.Name = "Label3"
        Label3.Size = New Size(79, 15)
        Label3.TabIndex = 8
        Label3.Text = "OTA Passwort"
        ' 
        ' TB_OtaPassword
        ' 
        TB_OtaPassword.Location = New Point(436, 183)
        TB_OtaPassword.Name = "TB_OtaPassword"
        TB_OtaPassword.Size = New Size(293, 23)
        TB_OtaPassword.TabIndex = 7
        ' 
        ' rtb_compilelog
        ' 
        rtb_compilelog.Location = New Point(12, 256)
        rtb_compilelog.Name = "rtb_compilelog"
        rtb_compilelog.Size = New Size(709, 322)
        rtb_compilelog.TabIndex = 9
        rtb_compilelog.Text = ""
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 238)
        Label4.Name = "Label4"
        Label4.Size = New Size(82, 15)
        Label4.TabIndex = 10
        Label4.Text = "ESPHome Log"
        ' 
        ' BTN_PingAdress
        ' 
        BTN_PingAdress.Location = New Point(436, 76)
        BTN_PingAdress.Name = "BTN_PingAdress"
        BTN_PingAdress.Size = New Size(293, 23)
        BTN_PingAdress.TabIndex = 11
        BTN_PingAdress.Text = "IP Adresse anpingen"
        BTN_PingAdress.UseVisualStyleBackColor = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {PB_Scan})
        StatusStrip1.Location = New Point(0, 591)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(733, 22)
        StatusStrip1.TabIndex = 12
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' PB_Scan
        ' 
        PB_Scan.Name = "PB_Scan"
        PB_Scan.Size = New Size(670, 16)
        PB_Scan.Visible = False
        ' 
        ' CB_PortScan
        ' 
        CB_PortScan.AutoSize = True
        CB_PortScan.Location = New Point(12, 187)
        CB_PortScan.Name = "CB_PortScan"
        CB_PortScan.Size = New Size(103, 19)
        CB_PortScan.TabIndex = 13
        CB_PortScan.Text = "Port scannen ?"
        CB_PortScan.UseVisualStyleBackColor = True
        ' 
        ' OTA
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(733, 613)
        Controls.Add(CB_PortScan)
        Controls.Add(StatusStrip1)
        Controls.Add(BTN_PingAdress)
        Controls.Add(Label4)
        Controls.Add(rtb_compilelog)
        Controls.Add(Label3)
        Controls.Add(TB_OtaPassword)
        Controls.Add(BTN_FlashOTA)
        Controls.Add(BTN_Scan)
        Controls.Add(Label2)
        Controls.Add(TB_SelectedIP)
        Controls.Add(Label1)
        Controls.Add(LB_Devices)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "OTA"
        Text = "OTA"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LB_Devices As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TB_SelectedIP As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BTN_Scan As Button
    Friend WithEvents BTN_FlashOTA As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TB_OtaPassword As TextBox
    Friend WithEvents rtb_compilelog As RichTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents BTN_PingAdress As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents PB_Scan As ToolStripProgressBar
    Friend WithEvents CB_PortScan As CheckBox
End Class

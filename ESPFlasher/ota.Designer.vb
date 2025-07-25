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
        SuspendLayout()
        ' 
        ' LB_Devices
        ' 
        LB_Devices.FormattingEnabled = True
        LB_Devices.ItemHeight = 15
        LB_Devices.Location = New Point(12, 27)
        LB_Devices.Name = "LB_Devices"
        LB_Devices.ScrollAlwaysVisible = True
        LB_Devices.Size = New Size(409, 289)
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
        TB_SelectedIP.Location = New Point(428, 40)
        TB_SelectedIP.Name = "TB_SelectedIP"
        TB_SelectedIP.Size = New Size(293, 23)
        TB_SelectedIP.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(428, 22)
        Label2.Name = "Label2"
        Label2.Size = New Size(130, 15)
        Label2.TabIndex = 4
        Label2.Text = "Ausgewählte IP adresse"
        ' 
        ' BTN_Scan
        ' 
        BTN_Scan.Location = New Point(12, 322)
        BTN_Scan.Name = "BTN_Scan"
        BTN_Scan.Size = New Size(409, 23)
        BTN_Scan.TabIndex = 5
        BTN_Scan.Text = "Nach geräten suchen"
        BTN_Scan.UseVisualStyleBackColor = True
        ' 
        ' BTN_FlashOTA
        ' 
        BTN_FlashOTA.Location = New Point(428, 125)
        BTN_FlashOTA.Name = "BTN_FlashOTA"
        BTN_FlashOTA.Size = New Size(293, 23)
        BTN_FlashOTA.TabIndex = 6
        BTN_FlashOTA.Text = "OTA Durchführen"
        BTN_FlashOTA.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(428, 78)
        Label3.Name = "Label3"
        Label3.Size = New Size(79, 15)
        Label3.TabIndex = 8
        Label3.Text = "OTA Passwort"
        ' 
        ' TB_OtaPassword
        ' 
        TB_OtaPassword.Location = New Point(428, 96)
        TB_OtaPassword.Name = "TB_OtaPassword"
        TB_OtaPassword.Size = New Size(293, 23)
        TB_OtaPassword.TabIndex = 7
        ' 
        ' OTA
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(733, 358)
        Controls.Add(Label3)
        Controls.Add(TB_OtaPassword)
        Controls.Add(BTN_FlashOTA)
        Controls.Add(BTN_Scan)
        Controls.Add(Label2)
        Controls.Add(TB_SelectedIP)
        Controls.Add(Label1)
        Controls.Add(LB_Devices)
        Name = "OTA"
        Text = "OTA"
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
End Class

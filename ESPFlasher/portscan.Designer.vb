<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class portscan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(portscan))
        Label1 = New Label()
        TB_PortRange = New TextBox()
        LBL_Preview = New Label()
        BTN_OK = New Button()
        BTN_Cancel = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(197, 15)
        Label1.TabIndex = 0
        Label1.Text = "Port-Range (z.B. 80,8080,3000-3010):"
        ' 
        ' TB_PortRange
        ' 
        TB_PortRange.Location = New Point(12, 38)
        TB_PortRange.Name = "TB_PortRange"
        TB_PortRange.Size = New Size(267, 23)
        TB_PortRange.TabIndex = 1
        ' 
        ' LBL_Preview
        ' 
        LBL_Preview.AutoSize = True
        LBL_Preview.Location = New Point(12, 64)
        LBL_Preview.Name = "LBL_Preview"
        LBL_Preview.Size = New Size(0, 15)
        LBL_Preview.TabIndex = 2
        ' 
        ' BTN_OK
        ' 
        BTN_OK.Location = New Point(12, 95)
        BTN_OK.Name = "BTN_OK"
        BTN_OK.Size = New Size(75, 23)
        BTN_OK.TabIndex = 3
        BTN_OK.Text = "OK"
        BTN_OK.UseVisualStyleBackColor = True
        ' 
        ' BTN_Cancel
        ' 
        BTN_Cancel.Location = New Point(204, 95)
        BTN_Cancel.Name = "BTN_Cancel"
        BTN_Cancel.Size = New Size(75, 23)
        BTN_Cancel.TabIndex = 4
        BTN_Cancel.Text = "Abbrechen"
        BTN_Cancel.UseVisualStyleBackColor = True
        ' 
        ' portscan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(290, 124)
        Controls.Add(BTN_Cancel)
        Controls.Add(BTN_OK)
        Controls.Add(LBL_Preview)
        Controls.Add(TB_PortRange)
        Controls.Add(Label1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "portscan"
        Opacity = 0.9R
        StartPosition = FormStartPosition.CenterScreen
        Text = "Port eingeben"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TB_PortRange As TextBox
    Friend WithEvents LBL_Preview As Label
    Friend WithEvents BTN_OK As Button
    Friend WithEvents BTN_Cancel As Button
End Class

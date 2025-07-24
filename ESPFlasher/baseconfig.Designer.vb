<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class baseconfig
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
        Panel_BaseConfig = New Panel()
        StatusStrip1 = New StatusStrip()
        SuspendLayout()
        ' 
        ' Panel_BaseConfig
        ' 
        Panel_BaseConfig.Location = New Point(0, 0)
        Panel_BaseConfig.Name = "Panel_BaseConfig"
        Panel_BaseConfig.Size = New Size(737, 935)
        Panel_BaseConfig.TabIndex = 0
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Location = New Point(0, 938)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(737, 22)
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' baseconfig
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(737, 960)
        Controls.Add(StatusStrip1)
        Controls.Add(Panel_BaseConfig)
        Name = "baseconfig"
        Text = "Base Config"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel_BaseConfig As Panel
    Friend WithEvents StatusStrip1 As StatusStrip
End Class

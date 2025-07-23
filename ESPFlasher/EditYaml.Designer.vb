<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditYaml
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
        RTB_Yaml = New RichTextBox()
        MenuStrip1 = New MenuStrip()
        YamlCompilenToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' RTB_Yaml
        ' 
        RTB_Yaml.Dock = DockStyle.Fill
        RTB_Yaml.Location = New Point(0, 24)
        RTB_Yaml.Name = "RTB_Yaml"
        RTB_Yaml.Size = New Size(1806, 986)
        RTB_Yaml.TabIndex = 0
        RTB_Yaml.Text = ""
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {YamlCompilenToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1806, 24)
        MenuStrip1.TabIndex = 1
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' YamlCompilenToolStripMenuItem
        ' 
        YamlCompilenToolStripMenuItem.Name = "YamlCompilenToolStripMenuItem"
        YamlCompilenToolStripMenuItem.Size = New Size(98, 20)
        YamlCompilenToolStripMenuItem.Text = "Yaml compilen"
        ' 
        ' EditYaml
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1806, 1010)
        Controls.Add(RTB_Yaml)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "EditYaml"
        Text = "EditYaml"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents RTB_Yaml As RichTextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents YamlCompilenToolStripMenuItem As ToolStripMenuItem
End Class

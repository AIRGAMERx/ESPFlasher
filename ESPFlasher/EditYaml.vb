Imports System.IO
Imports System.Text

Public Class EditYaml
    Private Sub EditYaml_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sr As New StreamReader(yamlPath)
        Dim fileContent As String = sr.ReadToEnd()
        sr.Close()
        RTB_Yaml.Text = fileContent

    End Sub

    Private Sub YamlCompilenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YamlCompilenToolStripMenuItem.Click
        Dim sb As New StringBuilder()

        For Each line In RTB_Yaml.Lines
            sb.AppendLine(line)
        Next
        File.WriteAllText(yamlPath, sb.ToString(), Encoding.UTF8)
        Form1.generateBin(yamlPath)


    End Sub
End Class
﻿Module externalServices


    Function CheckEnviroment() As Task
        Dim pythonInstalled As Boolean = CheckCommand("python --version")
        Dim pipInstalled As Boolean = CheckCommand("pip --version")
        Dim esphomeInstalled As Boolean = CheckCommand("esphome --version")

        If pythonInstalled And pipInstalled Then
            Main.TSSL_Python.Text = "Python mit PIP wurde gefunden"
            Main.TSSL_Python.ForeColor = Color.Green
        Else
            Main.TSSL_Python.Text = "Python mit PIP wurde nicht gefunden, bitte Installieren siehe in Extras"
            Main.TSSL_Python.ForeColor = Color.Red
        End If


        If esphomeInstalled Then
            Main.TSSL_ESPHome.Text = "ESPHome wurde gefunden"
            Main.TSSL_ESPHome.ForeColor = Color.Green
        Else
            Main.TSSL_ESPHome.Text = "ESPHome wurde nicht gefunden, bitte Installieren siehe in Extras"
            Main.TSSL_ESPHome.ForeColor = Color.Red
        End If
        Return Task.CompletedTask
    End Function




    Private Function CheckCommand(command As String) As Boolean
        Try
            Dim psi As New ProcessStartInfo("cmd", "/c " & command)
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True
            psi.UseShellExecute = False
            psi.CreateNoWindow = True

            Dim proc As Process = Process.Start(psi)
            proc.WaitForExit()

            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim errorOutput As String = proc.StandardError.ReadToEnd()


            If String.IsNullOrEmpty(output) Then
                Return False
            End If
            ' Erfolg, wenn entweder stdout oder stderr was liefert
            Return Not String.IsNullOrWhiteSpace(output) OrElse Not String.IsNullOrWhiteSpace(errorOutput)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub InstallESPHome()
        Try
            Main.Cursor = Cursors.WaitCursor
            Dim psi As New ProcessStartInfo()
            psi.FileName = "cmd.exe"
            psi.Arguments = "/c pip install esphome"
            psi.UseShellExecute = False
            psi.CreateNoWindow = False ' Zeigt Konsole für Fortschritt
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True

            Dim proc As Process = Process.Start(psi)
            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim errorOutput As String = proc.StandardError.ReadToEnd()

            proc.WaitForExit()

            If proc.ExitCode = 0 Then
                Main.Cursor = Cursors.Default
                MessageBox.Show("✅ ESPHome wurde erfolgreich installiert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Main.Cursor = Cursors.Default
                MessageBox.Show("❌ Fehler bei der Installation von ESPHome:" & vbCrLf & errorOutput, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Main.Cursor = Cursors.Default
            MessageBox.Show("❌ Konnte den Installationsprozess nicht starten:" & vbCrLf & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Main.Cursor = Cursors.Default
        End Try
    End Sub


End Module

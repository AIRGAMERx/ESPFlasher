Public Class portscan
    Public Property ScanPorts As Boolean
    Public Property PortList As List(Of Integer)

    Private Sub PortScanDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Port-Scan Optionen"
        Me.Size = New Size(450, 300)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Standard-Werte
        TB_PortRange.Text = "80,88,8080,8081,3000,3001"  ' ← 88 für deinen ESP!
        TB_PortRange.Enabled = True
        UpdatePreview()
    End Sub



    Private Sub TB_PortRange_TextChanged(sender As Object, e As EventArgs) Handles TB_PortRange.TextChanged
        UpdatePreview()
    End Sub

    Private Sub UpdatePreview()

        Dim ports = ParsePortRange(TB_PortRange.Text)
            LBL_Preview.Text = $"Scan-Umfang: 254 IPs × {ports.Count} Ports = {254 * ports.Count} Kombinationen"

            If ports.Count * 254 > 5000 Then
                LBL_Preview.ForeColor = Color.Red
                LBL_Preview.Text += vbCrLf & "⚠️ WARNUNG: Kann sehr lange dauern!"
            ElseIf ports.Count * 254 > 1500 Then
                LBL_Preview.ForeColor = Color.Orange
                LBL_Preview.Text += vbCrLf & "⚠️ Mittlere Scan-Dauer erwartet"
            Else
                LBL_Preview.ForeColor = Color.Green
                LBL_Preview.Text += vbCrLf & "✅ Schneller Scan"
            End If



    End Sub

    Private Function ParsePortRange(input As String) As List(Of Integer)
        Dim ports = New List(Of Integer)()

        If String.IsNullOrWhiteSpace(input) Then
            Return New List(Of Integer) From {80}
        End If

        Try
            For Each part In input.Split(","c)
                part = part.Trim()

                If part.Contains("-") Then
                    Dim rangeParts = part.Split("-"c)
                    If rangeParts.Length = 2 Then
                        Dim startPort = Integer.Parse(rangeParts(0).Trim())
                        Dim endPort = Integer.Parse(rangeParts(1).Trim())

                        For port = startPort To endPort
                            If port >= 1 And port <= 65535 Then ports.Add(port)
                        Next
                    End If
                Else
                    Dim port = Integer.Parse(part)
                    If port >= 1 And port <= 65535 Then ports.Add(port)
                End If
            Next
        Catch
            Return New List(Of Integer) From {80}
        End Try

        Return ports.Distinct().OrderBy(Function(p) p).ToList()
    End Function


    Private Sub BTN_OK_Click(sender As Object, e As EventArgs) Handles BTN_OK.Click

        PortList = ParsePortRange(TB_PortRange.Text)
        ScanPorts = True



        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
Module helper
    Public Function pinInUse(pin As String) As Boolean

        For Each row As DataGridViewRow In Main.DGV_Sensors.Rows
            If row.IsNewRow Then Continue For
            Dim paramString As String = row.Cells("Parameter").Value?.ToString()
            If String.IsNullOrWhiteSpace(paramString) Then Continue For

            If paramString.Contains($"pin={pin}") OrElse
               paramString.Contains($"trigger_pin={pin}") OrElse
               paramString.Contains($"echo_pin={pin}") OrElse
               paramString.Contains($"cs_pin={pin}") Then
                Return True
            End If
        Next

        For Each row As DataGridViewRow In Main.DGV_Display.Rows
            If row.IsNewRow Then Continue For
            Dim paramString As String = row.Cells("Parameter").Value?.ToString()
            If String.IsNullOrWhiteSpace(paramString) Then Continue For

            If paramString.Contains($"pin={pin}") OrElse
               paramString.Contains($"cs_pin={pin}") OrElse
               paramString.Contains($"dc_pin={pin}") OrElse
               paramString.Contains($"reset_pin={pin}") Then
                Return True
            End If

        Next




        Return False

    End Function





End Module

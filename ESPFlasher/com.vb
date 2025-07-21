Imports System.Management
Module com
    Public Function FindeESPPort() As String
        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'")
        For Each obj As ManagementObject In searcher.Get()
            Dim name As String = obj("Name").ToString()
            If name.Contains("CH340") OrElse name.Contains("CP210") OrElse name.Contains("USB Serial") Then
                Dim match = System.Text.RegularExpressions.Regex.Match(name, "COM\d+")
                If match.Success Then
                    Return match.Value
                End If
            End If
        Next
        Return ""
    End Function

End Module

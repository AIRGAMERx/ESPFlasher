Imports System.IO
Imports Newtonsoft.Json.Linq
Module display
    Public displayData As JObject
    Dim GBLocation As New Point(480, 17)
    Function LoadDisplayData() As Task
        Dim filePath As String = Path.Combine(Application.StartupPath, "displays.json")
        If Not File.Exists(filePath) Then
            MessageBox.Show("Die Display.json wurde nicht gefunden.")
            Return Task.CompletedTask
            Exit Function
        End If

        Dim json = File.ReadAllText(filePath)
        displayData = JObject.Parse(json)

        Form1.CBB_DisplayGroup.Items.Clear()
        For Each group In displayData.Properties()
            Form1.CBB_DisplayGroup.Items.Add(group.Name)
        Next
        Return Task.CompletedTask



    End Function

    Public Function LoadDisplyFromJson(path As String) As List(Of DisplayCategory)
        Dim json = File.ReadAllText(path)
        Return System.Text.Json.JsonSerializer.Deserialize(Of List(Of DisplayCategory))(json)
    End Function

    Public Sub GenerateDisplayConfigFields(displayInfo As JObject, targetPanel As Panel)
        targetPanel.Controls.Clear()
        Dim yOffset As Integer = 10

        Dim uiFields As JObject = Nothing
        If displayInfo("ui_fields") IsNot Nothing Then
            uiFields = CType(displayInfo("ui_fields"), JObject)
        End If

        ' Required Felder
        If displayInfo("required") IsNot Nothing Then
            For Each r In displayInfo("required")
                Dim fieldName = r.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddDisplayField(fieldName, True, targetPanel, yOffset, fieldConfig)
            Next
        End If

        ' Optional Felder
        If displayInfo("optional") IsNot Nothing Then
            For Each o In displayInfo("optional")
                Dim fieldName = o.ToString()
                Dim fieldConfig As JObject = Nothing
                If uiFields IsNot Nothing AndAlso uiFields(fieldName) IsNot Nothing Then
                    fieldConfig = CType(uiFields(fieldName), JObject)
                End If
                AddDisplayField(fieldName, False, targetPanel, yOffset, fieldConfig)
            Next
        End If

        ' Verschachtelte Felder (nested_fields)
        If displayInfo("nested_fields") IsNot Nothing Then
            Dim nestedFields As JObject = CType(displayInfo("nested_fields"), JObject)

            For Each section In nestedFields
                Dim prefix As String = section.Key
                Dim fieldList As JArray = CType(section.Value, JArray)

                For Each field In fieldList
                    Dim fullFieldName As String = $"{prefix}_{field.ToString()}"
                    Dim fieldConfig As JObject = Nothing

                    If uiFields IsNot Nothing AndAlso uiFields(fullFieldName) IsNot Nothing Then
                        fieldConfig = CType(uiFields(fullFieldName), JObject)
                    End If

                    AddDisplayField(fullFieldName, False, targetPanel, yOffset, fieldConfig)
                Next
            Next
        End If

        ' Info-Felder
        If displayInfo("info") IsNot Nothing Then
            For Each o In displayInfo("info")
                AddInfoField(o.ToString(), targetPanel, yOffset)
            Next
        End If



        If displayInfo("bus") IsNot Nothing Then

            For Each o In displayInfo("bus")

                Dim busValue As String = o.ToString().ToLower()
                Dim targetTabPage As TabPage = Form1.TabPage3

                Select Case busValue
                    Case "onewire"
                        Form1.GB_OneWire.Visible = True
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = False
                        Form1.GB_OneWire.Location = GBLocation

                    Case "i2c"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = True
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = False
                        Form1.GB_I2C.Location = GBLocation

                    Case "spi"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = True
                        Form1.GB_Uart.Visible = False
                        Form1.GB_SPI.Location = GBLocation

                    Case "uart"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = True
                        Form1.GB_Uart.Location = GBLocation

                    Case "na"
                        Form1.GB_OneWire.Visible = False
                        Form1.GB_I2C.Visible = False
                        Form1.GB_SPI.Visible = False
                        Form1.GB_Uart.Visible = False
                End Select

                Form1.GB_OneWire.Parent = Nothing
                targetTabPage.Controls.add(Form1.GB_OneWire)
                Form1.GB_I2C.Location = GBLocation


            Next
        Else
            Form1.GB_OneWire.Visible = False
            Form1.GB_I2C.Visible = False
            Form1.GB_SPI.Visible = False


        End If
    End Sub
    Public Sub AddDisplayField(fieldName As String, isRequired As Boolean, targetPanel As Panel, ByRef yOffset As Integer, Optional fieldConfig As JObject = Nothing)
        Dim label As New Label With {
        .Text = If(isRequired, "* ", "") & fieldName,
        .Location = New Point(10, yOffset),
        .AutoSize = True
    }
        targetPanel.Controls.Add(label)

        Dim control As Control = Nothing

        ' Standard: TextBox
        Dim ctrlType As String = If(fieldConfig IsNot Nothing AndAlso fieldConfig("type") IsNot Nothing,
                                fieldConfig("type").ToString().ToLower(),
                                "textbox")

        Select Case ctrlType
            Case "textbox"
                control = New TextBox With {
                .Name = "TXT_" & fieldName,
                .Location = New Point(230, yOffset),
                .Width = 150
            }
                If fieldConfig?("multiline")?.ToObject(Of Boolean) = True Then
                    CType(control, TextBox).Multiline = True
                    control.Height = 60
                End If

            Case "combobox"
                control = New ComboBox With {
                .Name = "CMB_" & fieldName,
                .Location = New Point(230, yOffset),
                .Width = 150,
                .DropDownStyle = ComboBoxStyle.DropDownList
            }
                If fieldConfig?("values") IsNot Nothing Then
                    Dim items = fieldConfig("values").ToObject(Of List(Of String))
                    CType(control, ComboBox).Items.AddRange(items.ToArray())
                End If

            Case "numericupdown"
                control = New NumericUpDown With {
                .Name = "NUM_" & fieldName,
                .Location = New Point(230, yOffset),
                .Width = 100,
                .DecimalPlaces = 1
            }
                With CType(control, NumericUpDown)
                    .Minimum = If(fieldConfig?("min") IsNot Nothing, fieldConfig("min").ToObject(Of Decimal), 0)
                    .Maximum = If(fieldConfig?("max") IsNot Nothing, fieldConfig("max").ToObject(Of Decimal), 100)
                    .Increment = If(fieldConfig?("Step") IsNot Nothing, fieldConfig("Step").ToObject(Of Decimal), 1)
                End With

            Case Else
                ' Fallback: TextBox
                control = New TextBox With {
                .Name = "TXT_" & fieldName,
                .Location = New Point(150, yOffset),
                .Width = 150
            }
        End Select

        If control IsNot Nothing Then
            targetPanel.Controls.Add(control)
            yOffset += control.Height + 10
        End If
        yOffset += 10
    End Sub

    Private Sub AddInfoField(fieldName As String, panel As Panel, ByRef yOffset As Integer)
        Dim lbl As New Label With {
        .Text = fieldName,
        .Left = 0,
        .Top = yOffset,
        .Width = 265,
        .ForeColor = Color.Red
        }
        panel.Controls.Add(lbl)
    End Sub


    Public Sub AddDisplayToGrid(displayGroup As String, displayType As String, displayInfo As JObject, paneldisplay As Panel, dgv As DataGridView)




        ' 1. Pflichtfelder prüfen
        If displayInfo("required") IsNot Nothing Then
            For Each reqField In displayInfo("required")
                Dim fieldName = reqField.ToString()
                Dim ctrl = paneldisplay.Controls.Cast(Of Control).FirstOrDefault(Function(c) c.Name.ToLower().EndsWith("_" & fieldName.ToLower()))

                If ctrl Is Nothing Then
                    MessageBox.Show($"Das Pflichtfeld '{fieldName}' fehlt im UI.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim isEmpty As Boolean = False
                Select Case True
                    Case TypeOf ctrl Is TextBox
                        isEmpty = String.IsNullOrWhiteSpace(CType(ctrl, TextBox).Text)
                    Case TypeOf ctrl Is ComboBox
                        isEmpty = CType(ctrl, ComboBox).SelectedIndex = -1
                    Case TypeOf ctrl Is NumericUpDown
                        ' Optional: 0 als leer definieren
                        isEmpty = False ' Hier bewusst erlaubt
                End Select

                If isEmpty Then
                    MessageBox.Show($"Das Pflichtfeld '{fieldName}' muss ausgefüllt werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                Dim pinFields = New HashSet(Of String) From {"pin", "trigger_pin", "echo_pin", "cs_pin"}
                If pinFields.Contains(fieldName.ToLower()) Then
                    Dim pinValue As String = ""

                    If TypeOf ctrl Is TextBox Then
                        pinValue = CType(ctrl, TextBox).Text.Trim()
                    ElseIf TypeOf ctrl Is ComboBox Then
                        pinValue = CType(ctrl, ComboBox).Text.Trim()
                    End If
                    MsgBox(Form1.clickedRow.ToString)
                    If Form1.clickedRow = -1 Then
                        If Not String.IsNullOrEmpty(pinValue) AndAlso IsPinInUse(pinValue, dgv) Then
                            MessageBox.Show($"Der Pin '{pinValue}' wird bereits von einem anderen Sensor verwendet.", "Pin-Konflikt", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If
                    End If
                End If
            Next
        End If

        ' 2. Parameterliste erzeugen
        Dim parameter As New List(Of String)

        For Each ctrl As Control In paneldisplay.Controls
            If Not ctrl.Name.Contains("_"c) Then Continue For

            Dim parts = ctrl.Name.Split("_"c, 2)
            If parts.Length <> 2 Then Continue For

            Dim paramName As String = parts(1)
            Dim paramValue As String = ""

            Select Case True
                Case TypeOf ctrl Is TextBox
                    Dim tb = CType(ctrl, TextBox)
                    If Not String.IsNullOrWhiteSpace(tb.Text) Then paramValue = tb.Text

                Case TypeOf ctrl Is ComboBox
                    Dim cb = CType(ctrl, ComboBox)
                    If cb.SelectedIndex >= 0 Then paramValue = cb.SelectedItem.ToString()

                Case TypeOf ctrl Is NumericUpDown
                    Dim num = CType(ctrl, NumericUpDown)
                    paramValue = num.Value.ToString()
            End Select

            If Not String.IsNullOrWhiteSpace(paramValue) Then
                parameter.Add($"{paramName}={paramValue}")
            End If
        Next

        ' 3. Plattform ermitteln
        Dim platform As String = ""
        If displayInfo("platform") IsNot Nothing Then
            platform = displayInfo("platform").ToString()
        End If

        ' 4. Zeile zum DataGridView hinzufügen
        If Form1.clickedRow > -1 Then
            Try

                Dim row As DataGridViewRow = dgv.Rows(Form1.clickedRow)
                row.Cells(0).Value = displayGroup
                row.Cells(1).Value = displayType
                row.Cells(2).Value = platform
                row.Cells(3).Value = ""
                row.Cells(4).Value = String.Join(",", parameter)
                MsgBox("Sensor wurde gespeichert")

            Catch ex As Exception

            End Try
        Else
            dgv.Rows.Add(displayGroup, displayType, platform, "", String.Join(", ", parameter))

        End If

        Form1.clickedRow = -1
    End Sub



    Private Function IsPinInUse(pin As String, dgv As DataGridView) As Boolean
        For Each row As DataGridViewRow In dgv.Rows
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
        Return False
    End Function













End Module
Public Class DisplayCategory
    Public Property category As String
    Public Property displays As List(Of DisplayExportModel)
End Class
Public Class DisplayExportModel
    Public Property Plattform As String
    Public Property GlobalPins As Dictionary(Of String, String)
    Public Property DisplayEntries As List(Of Dictionary(Of String, String))
End Class
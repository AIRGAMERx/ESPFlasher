Imports System.Globalization
Imports Newtonsoft.Json.Linq

Public Class baseconfig
    Private SensorGroup As String
    Private SensorType As String
    Private CurrentRow As DataGridViewRow
    Private SensorDefinitions As JObject

    Public Sub New(sensorGroup As String, sensorType As String, currentRow As DataGridViewRow, sensorDefinitions As JObject)
        InitializeComponent()
        Me.SensorGroup = sensorGroup
        Me.SensorType = sensorType
        Me.CurrentRow = currentRow
        Me.SensorDefinitions = sensorDefinitions
    End Sub

    Private Sub baseconfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MsgBox("hier")
        Try
            Dim sensorInfo As JObject = CType(SensorDefinitions(SensorGroup)(SensorType), JObject)
            LoadBaseConfigFields(sensorInfo)
        Catch ex As Exception
            MessageBox.Show("Fehler beim Laden der Sensor-Konfiguration: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadBaseConfigFields(sensorInfo As JObject)
        Panel_BaseConfig.Controls.Clear()
        Panel_BaseConfig.AutoScroll = True

        Dim yOffset As Integer = 10

        If sensorInfo Is Nothing Then
            MsgBox("Es gibt keine eingetragenen Filter für diesen Sensor.")
            Me.Close()
            Return
        End If

        Dim baseConfigs = sensorInfo("baseconfig")?.ToObject(Of List(Of String))()
        Dim filterOptions = sensorInfo("filter")?.ToObject(Of List(Of String))()
        Dim nestedFields = sensorInfo("nested_fields")?.ToObject(Of JObject)()
        If baseConfigs Is Nothing Then Return

        ' Filtertypen, die mehrzeilig dargestellt werden sollen
        Dim multilineKeys As New HashSet(Of String) From {
        "median", "calibrate_linear", "calibrate_polynomial", "clamp", "filter_out", "lambda", "sliding_window_moving_average", "exponential_moving_average"
    }

        ' --- FILTERS ---
        If baseConfigs.Contains("filter") AndAlso nestedFields IsNot Nothing Then
            ' Verschachtelte Filter pro nested field
            For Each nested In nestedFields.Properties()
                Dim nestedKey = nested.Name ' z. B. temperature, humidity

                ' Abschnittstitel
                Dim titleLabel = New Label With {
                .Text = $"Filter für {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nestedKey.Trim())}",
                .Top = yOffset,
                .Left = 10,
                .Font = New Font(Font, FontStyle.Bold),
                .AutoSize = True
            }
                Panel_BaseConfig.Controls.Add(titleLabel)
                yOffset += 25

                ' Filter-Einträge
                For Each filterName In filterOptions
                    Dim isMultiline As Boolean = multilineKeys.Contains(filterName.ToLower())

                    Dim lbl = New Label With {
                    .Text = filterName,
                    .Top = yOffset,
                    .Left = 20,
                    .AutoSize = True
                }

                    Dim txt = New TextBox With {
                    .Name = $"filter_{nestedKey}_{filterName}",
                    .Top = yOffset,
                    .Left = 200,
                    .Width = 200,
                    .Multiline = isMultiline,
                    .ScrollBars = If(isMultiline, ScrollBars.Vertical, ScrollBars.None)
                }

                    If isMultiline Then txt.Height = 150

                    Panel_BaseConfig.Controls.Add(lbl)
                    Panel_BaseConfig.Controls.Add(txt)
                    yOffset += If(isMultiline, 160, 30)
                Next

                yOffset += 10
            Next

        ElseIf baseConfigs.Contains("filter") Then
            ' Globale Filter (keine Nested Fields)
            Dim titleLabel = New Label With {
            .Text = "Filter",
            .Top = yOffset,
            .Left = 10,
            .Font = New Font(Font, FontStyle.Bold),
            .AutoSize = True
        }
            Panel_BaseConfig.Controls.Add(titleLabel)
            yOffset += 25

            For Each filterName In filterOptions
                Dim isMultiline As Boolean = multilineKeys.Contains(filterName.ToLower())

                Dim lbl = New Label With {
                .Text = filterName,
                .Top = yOffset,
                .Left = 20,
                .AutoSize = True
            }

                Dim txt = New TextBox With {
                .Name = $"filter_{filterName}",
                .Top = yOffset,
                .Left = 120,
                .Width = 200,
                .Multiline = isMultiline,
                .ScrollBars = If(isMultiline, ScrollBars.Vertical, ScrollBars.None)
            }

                If isMultiline Then txt.Height = 150

                Panel_BaseConfig.Controls.Add(lbl)
                Panel_BaseConfig.Controls.Add(txt)
                yOffset += If(isMultiline, 160, 30)
            Next
        End If

        ' --- Andere BaseConfigs ---
        If baseConfigs.Contains("disabled_by_default") Then
            Dim lbl = New Label With {
            .Text = "Deaktiviert",
            .Top = yOffset,
            .Left = 20,
            .AutoSize = True
        }

            Dim cmb = New ComboBox With {
            .Name = "base_disabled_by_default",
            .Top = yOffset,
            .Left = 120,
            .Width = 200,
            .DropDownStyle = ComboBoxStyle.DropDownList
        }
            cmb.Items.AddRange(New String() {"True", "False"})

            Panel_BaseConfig.Controls.Add(lbl)
            Panel_BaseConfig.Controls.Add(cmb)
            yOffset += 30
        End If

        ' --- Speichern Button ---
        Dim btn = New Button With {
    .Text = "💾 Änderungen speichern",
    .Top = yOffset,
    .Left = 50,
    .Width = 250,
    .Height = 50,
    .BackColor = Color.FromArgb(50, 150, 250),
    .ForeColor = Color.White,
    .Font = New Font("Segoe UI", 12, FontStyle.Bold),
    .FlatStyle = FlatStyle.Flat
}
        btn.FlatAppearance.BorderSize = 0
        AddHandler btn.Click, AddressOf Speichern_Click
        Panel_BaseConfig.Controls.Add(btn)

        ' --- Dummy Spacer --- 
        Dim spacer = New Label With {
    .Height = 50,
    .Width = 200,
    .Top = yOffset,
    .Left = 0
}
        Panel_BaseConfig.Controls.Add(spacer)


        ' --- Vorhandene Filterdaten laden ---
        Dim rawFilter = CurrentRow.Cells("filter").Value?.ToString()
        If Not String.IsNullOrEmpty(rawFilter) Then
            Dim filterData As JObject = JObject.Parse(rawFilter)

            For Each ctrl In Panel_BaseConfig.Controls
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("filter_") Then
                    Dim parts = ctrl.Name.Split("_"c)
                    If parts.Length = 3 Then
                        Dim nestedKey = parts(1)
                        Dim filterName = parts(2)

                        If filterData(nestedKey)?(filterName) IsNot Nothing Then
                            CType(ctrl, TextBox).Text = filterData(nestedKey)(filterName).ToString()
                        End If
                    ElseIf parts.Length = 2 Then
                        Dim filterName = parts(1)
                        If filterData("global")?(filterName) IsNot Nothing Then
                            CType(ctrl, TextBox).Text = filterData("global")(filterName).ToString()
                        End If
                    End If
                End If
            Next
        End If
    End Sub





    Private Sub Speichern_Click(sender As Object, e As EventArgs)
        Dim filterData As New JObject()
        Try


            For Each ctrl In Panel_BaseConfig.Controls
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("filter_") Then
                    Dim parts = ctrl.Name.Split("_"c)
                    Dim value = CType(ctrl, TextBox).Text.Trim()

                    ' Leere Werte überspringen
                    If String.IsNullOrWhiteSpace(value) Then
                        Continue For
                    End If

                    If parts.Length = 3 Then
                        ' Format: filter_temperature_offset
                        Dim nestedKey = parts(1)
                        Dim filterName = parts(2)

                        If Not filterData.ContainsKey(nestedKey) Then
                            filterData(nestedKey) = New JObject()
                        End If

                        filterData(nestedKey)(filterName) = value

                    ElseIf parts.Length = 2 Then
                        ' Format: filter_offset (globaler Filter)
                        Dim filterName = parts(1)

                        If Not filterData.ContainsKey("global") Then
                            filterData("global") = New JObject()
                        End If

                        filterData("global")(filterName) = value
                    End If
                End If
            Next



            ' Filter als String serialisieren
            Dim jsonString = filterData.ToString(Newtonsoft.Json.Formatting.None)

            If jsonString.ToString = "{}" Then
                CurrentRow.Cells("filter").Value = ""
                Me.Close()
            Else
                CurrentRow.Cells("filter").Value = jsonString
                Me.Close()
            End If


        Catch ex As Exception
            MessageBox.Show("Filter konnte nicht gespeichert werden", "Fehler")
            Console.WriteLine(ex.Message & ex.StackTrace)
        End Try
    End Sub





End Class
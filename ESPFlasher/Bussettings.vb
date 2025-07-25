Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Bussettings
    Private Sub BTN_i2cSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_i2cSettingsSave.Click
        If txt_i2cscl.Text.Length > 0 AndAlso txt_i2csda.Text.Length > 0 Then
            Form1.i2cScl = txt_i2cscl.Text
            Form1.i2cSda = txt_i2csda.Text
            Form1.i2cScan = CB_i2cScan.Checked
            Form1.i2c = True
            lbl_i2csavestate.Text = "Gespeichert"
            lbl_i2csavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte scl und sda ausfüllen")
        End If
    End Sub

    Private Sub BTN_i2cSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_i2cSettingsDelete.Click
        Form1.i2cScl = ""
        Form1.i2cSda = ""
        Form1.i2cScan = False
        Form1.i2c = False
        lbl_i2csavestate.Text = "Nicht Gespeichert"
        lbl_i2csavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_OneWireSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_OneWireSettingsSave.Click
        If Txt_OneWireBusID.Text.Length > 0 AndAlso Txt_OneWireGPIOPin.Text.Length > 0 Then
            Form1.OneWireID = Txt_OneWireBusID.Text
            Form1.OneWirePIN = Txt_OneWireGPIOPin.Text
            Form1.OneWire = True
            lbl_onewiresavestate.Text = "Gespeichert"
            lbl_onewiresavestate.ForeColor = Color.Green

        Else
            MsgBox("Bitte ID und PIN ausfüllen")
        End If
    End Sub

    Private Sub BTN_OneWireSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_OneWireSettingsDelete.Click
        Form1.OneWireID = ""
        Form1.OneWirePIN = ""
        Form1.OneWire = False
        lbl_onewiresavestate.Text = "Nicht Gespeichert"
        lbl_onewiresavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_uartSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_uartSettingsSave.Click
        If Not CBB_UartBaudrate.SelectedItem = -1 AndAlso Not String.IsNullOrEmpty(txt_UartRx.Text) AndAlso Not String.IsNullOrEmpty(txt_UartTx.Text) Then
            Form1.uartBaudrate = CBB_UartBaudrate.SelectedItem.ToString
            Form1.uartRx = txt_UartRx.Text
            Form1.uartTx = txt_UartTx.Text
            Form1.uart = True
            lbl_uartsavestate.Text = "Gespeichert"
            lbl_uartsavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte Baudrate, Rx und Tx ausfüllen")
        End If
    End Sub

    Private Sub BTN_uartSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_uartSettingsDelete.Click
        Form1.uartBaudrate = ""
        Form1.uartRx = ""
        Form1.uartTx = ""
        Form1.uart = False
        lbl_uartsavestate.Text = "Nicht Gespeichert"
        lbl_uartsavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_spiSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_spiSettingsSave.Click
        If txt_spiclk.Text.Length > 0 AndAlso txt_spimosi.Text.Length > 0 AndAlso txt_spimiso.Text.Length > 0 Then
            Form1.spiclk = txt_spiclk.Text
            Form1.spimosi = txt_spimosi.Text
            Form1.spimiso = txt_spimiso.Text
            Form1.spi = True
            lbl_spisavestate.Text = "Gespeichert"
            lbl_spisavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte clk, mosi und miso ausfüllen")
        End If

    End Sub

    Private Sub BTN_spiSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_spiSettingsDelete.Click
        Form1.spiclk = ""
        Form1.spimosi = ""
        Form1.spimiso = ""
        Form1.spi = False
        lbl_spisavestate.Text = "Nicht Gespeichert"
        lbl_spisavestate.ForeColor = Color.Red
    End Sub
End Class
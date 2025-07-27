Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Bussettings
    Private Sub BTN_i2cSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_i2cSettingsSave.Click
        If txt_i2cscl.Text.Length > 0 AndAlso txt_i2csda.Text.Length > 0 Then
            Main.i2cScl = txt_i2cscl.Text
            Main.i2cSda = txt_i2csda.Text
            Main.i2cScan = CB_i2cScan.Checked
            Main.i2c = True
            lbl_i2csavestate.Text = "Gespeichert"
            lbl_i2csavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte scl und sda ausfüllen")
        End If
    End Sub

    Private Sub BTN_i2cSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_i2cSettingsDelete.Click
        Main.i2cScl = ""
        Main.i2cSda = ""
        Main.i2cScan = False
        Main.i2c = False
        lbl_i2csavestate.Text = "Nicht Gespeichert"
        lbl_i2csavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_OneWireSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_OneWireSettingsSave.Click
        If Txt_OneWireBusID.Text.Length > 0 AndAlso Txt_OneWireGPIOPin.Text.Length > 0 Then
            Main.OneWireID = Txt_OneWireBusID.Text
            Main.OneWirePIN = Txt_OneWireGPIOPin.Text
            Main.OneWire = True
            lbl_onewiresavestate.Text = "Gespeichert"
            lbl_onewiresavestate.ForeColor = Color.Green

        Else
            MsgBox("Bitte ID und PIN ausfüllen")
        End If
    End Sub

    Private Sub BTN_OneWireSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_OneWireSettingsDelete.Click
        Main.OneWireID = ""
        Main.OneWirePIN = ""
        Main.OneWire = False
        lbl_onewiresavestate.Text = "Nicht Gespeichert"
        lbl_onewiresavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_uartSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_uartSettingsSave.Click
        If Not CBB_UartBaudrate.SelectedItem = -1 AndAlso Not String.IsNullOrEmpty(txt_UartRx.Text) AndAlso Not String.IsNullOrEmpty(txt_UartTx.Text) Then
            Main.uartBaudrate = CBB_UartBaudrate.SelectedItem.ToString
            Main.uartRx = txt_UartRx.Text
            Main.uartTx = txt_UartTx.Text
            Main.uart = True
            lbl_uartsavestate.Text = "Gespeichert"
            lbl_uartsavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte Baudrate, Rx und Tx ausfüllen")
        End If
    End Sub

    Private Sub BTN_uartSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_uartSettingsDelete.Click
        Main.uartBaudrate = ""
        Main.uartRx = ""
        Main.uartTx = ""
        Main.uart = False
        lbl_uartsavestate.Text = "Nicht Gespeichert"
        lbl_uartsavestate.ForeColor = Color.Red
    End Sub

    Private Sub BTN_spiSettingsSave_Click(sender As Object, e As EventArgs) Handles BTN_spiSettingsSave.Click
        If txt_spiclk.Text.Length > 0 AndAlso txt_spimosi.Text.Length > 0 AndAlso txt_spimiso.Text.Length > 0 Then
            Main.spiclk = txt_spiclk.Text
            Main.spimosi = txt_spimosi.Text
            Main.spimiso = txt_spimiso.Text
            Main.spi = True
            lbl_spisavestate.Text = "Gespeichert"
            lbl_spisavestate.ForeColor = Color.Green
        Else
            MsgBox("Bitte clk, mosi und miso ausfüllen")
        End If

    End Sub

    Private Sub BTN_spiSettingsDelete_Click(sender As Object, e As EventArgs) Handles BTN_spiSettingsDelete.Click
        Main.spiclk = ""
        Main.spimosi = ""
        Main.spimiso = ""
        Main.spi = False
        lbl_spisavestate.Text = "Nicht Gespeichert"
        lbl_spisavestate.ForeColor = Color.Red
    End Sub
End Class
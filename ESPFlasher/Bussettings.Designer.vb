<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Bussettings
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
        GB_SPI = New GroupBox()
        txt_spimiso = New TextBox()
        Label23 = New Label()
        lbl_spisavestate = New Label()
        BTN_spiSettingsDelete = New Button()
        BTN_spiSettingsSave = New Button()
        txt_spimosi = New TextBox()
        Label21 = New Label()
        txt_spiclk = New TextBox()
        Label22 = New Label()
        GB_Uart = New GroupBox()
        txt_UartTx = New TextBox()
        Label18 = New Label()
        CBB_UartBaudrate = New ComboBox()
        lbl_uartsavestate = New Label()
        BTN_uartSettingsDelete = New Button()
        BTN_uartSettingsSave = New Button()
        txt_UartRx = New TextBox()
        Label25 = New Label()
        Label26 = New Label()
        GB_OneWire = New GroupBox()
        lbl_onewiresavestate = New Label()
        BTN_OneWireSettingsDelete = New Button()
        BTN_OneWireSettingsSave = New Button()
        Txt_OneWireBusID = New TextBox()
        Label17 = New Label()
        Txt_OneWireGPIOPin = New TextBox()
        Label16 = New Label()
        GB_I2C = New GroupBox()
        CB_i2cScan = New CheckBox()
        lbl_i2csavestate = New Label()
        BTN_i2cSettingsDelete = New Button()
        BTN_i2cSettingsSave = New Button()
        txt_i2cscl = New TextBox()
        Label19 = New Label()
        txt_i2csda = New TextBox()
        Label20 = New Label()
        GB_SPI.SuspendLayout()
        GB_Uart.SuspendLayout()
        GB_OneWire.SuspendLayout()
        GB_I2C.SuspendLayout()
        SuspendLayout()
        ' 
        ' GB_SPI
        ' 
        GB_SPI.Controls.Add(txt_spimiso)
        GB_SPI.Controls.Add(Label23)
        GB_SPI.Controls.Add(lbl_spisavestate)
        GB_SPI.Controls.Add(BTN_spiSettingsDelete)
        GB_SPI.Controls.Add(BTN_spiSettingsSave)
        GB_SPI.Controls.Add(txt_spimosi)
        GB_SPI.Controls.Add(Label21)
        GB_SPI.Controls.Add(txt_spiclk)
        GB_SPI.Controls.Add(Label22)
        GB_SPI.Location = New Point(12, 384)
        GB_SPI.Name = "GB_SPI"
        GB_SPI.Size = New Size(333, 148)
        GB_SPI.TabIndex = 17
        GB_SPI.TabStop = False
        GB_SPI.Text = "SPI"
        ' 
        ' txt_spimiso
        ' 
        txt_spimiso.Location = New Point(6, 83)
        txt_spimiso.Name = "txt_spimiso"
        txt_spimiso.Size = New Size(155, 23)
        txt_spimiso.TabIndex = 8
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(6, 65)
        Label23.Name = "Label23"
        Label23.Size = New Size(55, 15)
        Label23.TabIndex = 7
        Label23.Text = "miso PIN"
        ' 
        ' lbl_spisavestate
        ' 
        lbl_spisavestate.AutoSize = True
        lbl_spisavestate.Location = New Point(6, 118)
        lbl_spisavestate.Name = "lbl_spisavestate"
        lbl_spisavestate.Size = New Size(0, 15)
        lbl_spisavestate.TabIndex = 6
        ' 
        ' BTN_spiSettingsDelete
        ' 
        BTN_spiSettingsDelete.ForeColor = Color.Red
        BTN_spiSettingsDelete.Location = New Point(248, 110)
        BTN_spiSettingsDelete.Name = "BTN_spiSettingsDelete"
        BTN_spiSettingsDelete.Size = New Size(75, 23)
        BTN_spiSettingsDelete.TabIndex = 5
        BTN_spiSettingsDelete.Text = "Löschen"
        BTN_spiSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_spiSettingsSave
        ' 
        BTN_spiSettingsSave.Location = New Point(167, 110)
        BTN_spiSettingsSave.Name = "BTN_spiSettingsSave"
        BTN_spiSettingsSave.Size = New Size(75, 23)
        BTN_spiSettingsSave.TabIndex = 4
        BTN_spiSettingsSave.Text = "Speichern"
        BTN_spiSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' txt_spimosi
        ' 
        txt_spimosi.Location = New Point(167, 39)
        txt_spimosi.Name = "txt_spimosi"
        txt_spimosi.Size = New Size(155, 23)
        txt_spimosi.TabIndex = 3
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(167, 18)
        Label21.Name = "Label21"
        Label21.Size = New Size(55, 15)
        Label21.TabIndex = 2
        Label21.Text = "mosi PIN"
        ' 
        ' txt_spiclk
        ' 
        txt_spiclk.Location = New Point(6, 39)
        txt_spiclk.Name = "txt_spiclk"
        txt_spiclk.Size = New Size(155, 23)
        txt_spiclk.TabIndex = 1
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(6, 21)
        Label22.Name = "Label22"
        Label22.Size = New Size(44, 15)
        Label22.TabIndex = 0
        Label22.Text = "clk PIN"
        ' 
        ' GB_Uart
        ' 
        GB_Uart.Controls.Add(txt_UartTx)
        GB_Uart.Controls.Add(Label18)
        GB_Uart.Controls.Add(CBB_UartBaudrate)
        GB_Uart.Controls.Add(lbl_uartsavestate)
        GB_Uart.Controls.Add(BTN_uartSettingsDelete)
        GB_Uart.Controls.Add(BTN_uartSettingsSave)
        GB_Uart.Controls.Add(txt_UartRx)
        GB_Uart.Controls.Add(Label25)
        GB_Uart.Controls.Add(Label26)
        GB_Uart.Location = New Point(12, 230)
        GB_Uart.Name = "GB_Uart"
        GB_Uart.Size = New Size(333, 148)
        GB_Uart.TabIndex = 18
        GB_Uart.TabStop = False
        GB_Uart.Text = "Uart"
        ' 
        ' txt_UartTx
        ' 
        txt_UartTx.Location = New Point(6, 39)
        txt_UartTx.Name = "txt_UartTx"
        txt_UartTx.Size = New Size(155, 23)
        txt_UartTx.TabIndex = 9
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(6, 65)
        Label18.Name = "Label18"
        Label18.Size = New Size(54, 15)
        Label18.TabIndex = 8
        Label18.Text = "Baudrate"
        ' 
        ' CBB_UartBaudrate
        ' 
        CBB_UartBaudrate.FormattingEnabled = True
        CBB_UartBaudrate.Items.AddRange(New Object() {"9600", "115200", "38400", "19200"})
        CBB_UartBaudrate.Location = New Point(6, 85)
        CBB_UartBaudrate.Name = "CBB_UartBaudrate"
        CBB_UartBaudrate.Size = New Size(155, 23)
        CBB_UartBaudrate.TabIndex = 7
        ' 
        ' lbl_uartsavestate
        ' 
        lbl_uartsavestate.AutoSize = True
        lbl_uartsavestate.Location = New Point(6, 118)
        lbl_uartsavestate.Name = "lbl_uartsavestate"
        lbl_uartsavestate.Size = New Size(0, 15)
        lbl_uartsavestate.TabIndex = 6
        ' 
        ' BTN_uartSettingsDelete
        ' 
        BTN_uartSettingsDelete.ForeColor = Color.Red
        BTN_uartSettingsDelete.Location = New Point(248, 110)
        BTN_uartSettingsDelete.Name = "BTN_uartSettingsDelete"
        BTN_uartSettingsDelete.Size = New Size(75, 23)
        BTN_uartSettingsDelete.TabIndex = 5
        BTN_uartSettingsDelete.Text = "Löschen"
        BTN_uartSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_uartSettingsSave
        ' 
        BTN_uartSettingsSave.Location = New Point(167, 110)
        BTN_uartSettingsSave.Name = "BTN_uartSettingsSave"
        BTN_uartSettingsSave.Size = New Size(75, 23)
        BTN_uartSettingsSave.TabIndex = 4
        BTN_uartSettingsSave.Text = "Speichern"
        BTN_uartSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' txt_UartRx
        ' 
        txt_UartRx.Location = New Point(167, 39)
        txt_UartRx.Name = "txt_UartRx"
        txt_UartRx.Size = New Size(155, 23)
        txt_UartRx.TabIndex = 3
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(167, 18)
        Label25.Name = "Label25"
        Label25.Size = New Size(38, 15)
        Label25.TabIndex = 2
        Label25.Text = "rx PIN"
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(6, 21)
        Label26.Name = "Label26"
        Label26.Size = New Size(38, 15)
        Label26.TabIndex = 0
        Label26.Text = "tx PIN"
        ' 
        ' GB_OneWire
        ' 
        GB_OneWire.Controls.Add(lbl_onewiresavestate)
        GB_OneWire.Controls.Add(BTN_OneWireSettingsDelete)
        GB_OneWire.Controls.Add(BTN_OneWireSettingsSave)
        GB_OneWire.Controls.Add(Txt_OneWireBusID)
        GB_OneWire.Controls.Add(Label17)
        GB_OneWire.Controls.Add(Txt_OneWireGPIOPin)
        GB_OneWire.Controls.Add(Label16)
        GB_OneWire.Location = New Point(12, 121)
        GB_OneWire.Name = "GB_OneWire"
        GB_OneWire.Size = New Size(333, 103)
        GB_OneWire.TabIndex = 15
        GB_OneWire.TabStop = False
        GB_OneWire.Text = "1-Wire Bus"
        ' 
        ' lbl_onewiresavestate
        ' 
        lbl_onewiresavestate.AutoSize = True
        lbl_onewiresavestate.Location = New Point(6, 76)
        lbl_onewiresavestate.Name = "lbl_onewiresavestate"
        lbl_onewiresavestate.Size = New Size(0, 15)
        lbl_onewiresavestate.TabIndex = 6
        ' 
        ' BTN_OneWireSettingsDelete
        ' 
        BTN_OneWireSettingsDelete.ForeColor = Color.Red
        BTN_OneWireSettingsDelete.Location = New Point(247, 68)
        BTN_OneWireSettingsDelete.Name = "BTN_OneWireSettingsDelete"
        BTN_OneWireSettingsDelete.Size = New Size(75, 23)
        BTN_OneWireSettingsDelete.TabIndex = 5
        BTN_OneWireSettingsDelete.Text = "Löschen"
        BTN_OneWireSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_OneWireSettingsSave
        ' 
        BTN_OneWireSettingsSave.Location = New Point(167, 68)
        BTN_OneWireSettingsSave.Name = "BTN_OneWireSettingsSave"
        BTN_OneWireSettingsSave.Size = New Size(75, 23)
        BTN_OneWireSettingsSave.TabIndex = 4
        BTN_OneWireSettingsSave.Text = "Speichern"
        BTN_OneWireSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' Txt_OneWireBusID
        ' 
        Txt_OneWireBusID.Location = New Point(167, 39)
        Txt_OneWireBusID.Name = "Txt_OneWireBusID"
        Txt_OneWireBusID.Size = New Size(155, 23)
        Txt_OneWireBusID.TabIndex = 3
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(167, 18)
        Label17.Name = "Label17"
        Label17.Size = New Size(40, 15)
        Label17.TabIndex = 2
        Label17.Text = "Bus ID"
        ' 
        ' Txt_OneWireGPIOPin
        ' 
        Txt_OneWireGPIOPin.Location = New Point(6, 39)
        Txt_OneWireGPIOPin.Name = "Txt_OneWireGPIOPin"
        Txt_OneWireGPIOPin.Size = New Size(155, 23)
        Txt_OneWireGPIOPin.TabIndex = 1
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(6, 21)
        Label16.Name = "Label16"
        Label16.Size = New Size(56, 15)
        Label16.TabIndex = 0
        Label16.Text = "GPIO PIN"
        ' 
        ' GB_I2C
        ' 
        GB_I2C.Controls.Add(CB_i2cScan)
        GB_I2C.Controls.Add(lbl_i2csavestate)
        GB_I2C.Controls.Add(BTN_i2cSettingsDelete)
        GB_I2C.Controls.Add(BTN_i2cSettingsSave)
        GB_I2C.Controls.Add(txt_i2cscl)
        GB_I2C.Controls.Add(Label19)
        GB_I2C.Controls.Add(txt_i2csda)
        GB_I2C.Controls.Add(Label20)
        GB_I2C.Location = New Point(12, 12)
        GB_I2C.Name = "GB_I2C"
        GB_I2C.Size = New Size(333, 103)
        GB_I2C.TabIndex = 16
        GB_I2C.TabStop = False
        GB_I2C.Text = "I2C"
        ' 
        ' CB_i2cScan
        ' 
        CB_i2cScan.AutoSize = True
        CB_i2cScan.Location = New Point(6, 72)
        CB_i2cScan.Name = "CB_i2cScan"
        CB_i2cScan.Size = New Size(51, 19)
        CB_i2cScan.TabIndex = 7
        CB_i2cScan.Text = "Scan"
        CB_i2cScan.UseVisualStyleBackColor = True
        ' 
        ' lbl_i2csavestate
        ' 
        lbl_i2csavestate.AutoSize = True
        lbl_i2csavestate.Location = New Point(72, 74)
        lbl_i2csavestate.Name = "lbl_i2csavestate"
        lbl_i2csavestate.Size = New Size(0, 15)
        lbl_i2csavestate.TabIndex = 6
        ' 
        ' BTN_i2cSettingsDelete
        ' 
        BTN_i2cSettingsDelete.ForeColor = Color.Red
        BTN_i2cSettingsDelete.Location = New Point(247, 68)
        BTN_i2cSettingsDelete.Name = "BTN_i2cSettingsDelete"
        BTN_i2cSettingsDelete.Size = New Size(75, 23)
        BTN_i2cSettingsDelete.TabIndex = 5
        BTN_i2cSettingsDelete.Text = "Löschen"
        BTN_i2cSettingsDelete.UseVisualStyleBackColor = True
        ' 
        ' BTN_i2cSettingsSave
        ' 
        BTN_i2cSettingsSave.Location = New Point(167, 68)
        BTN_i2cSettingsSave.Name = "BTN_i2cSettingsSave"
        BTN_i2cSettingsSave.Size = New Size(75, 23)
        BTN_i2cSettingsSave.TabIndex = 4
        BTN_i2cSettingsSave.Text = "Speichern"
        BTN_i2cSettingsSave.UseVisualStyleBackColor = True
        ' 
        ' txt_i2cscl
        ' 
        txt_i2cscl.Location = New Point(167, 39)
        txt_i2cscl.Name = "txt_i2cscl"
        txt_i2cscl.Size = New Size(155, 23)
        txt_i2cscl.TabIndex = 3
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(167, 18)
        Label19.Name = "Label19"
        Label19.Size = New Size(43, 15)
        Label19.TabIndex = 2
        Label19.Text = "scl PIN"
        ' 
        ' txt_i2csda
        ' 
        txt_i2csda.Location = New Point(6, 39)
        txt_i2csda.Name = "txt_i2csda"
        txt_i2csda.Size = New Size(155, 23)
        txt_i2csda.TabIndex = 1
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(6, 21)
        Label20.Name = "Label20"
        Label20.Size = New Size(47, 15)
        Label20.TabIndex = 0
        Label20.Text = "sda PIN"
        ' 
        ' Bussettings
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(356, 552)
        Controls.Add(GB_SPI)
        Controls.Add(GB_Uart)
        Controls.Add(GB_OneWire)
        Controls.Add(GB_I2C)
        Name = "Bussettings"
        Text = "Bus einstellungen"
        GB_SPI.ResumeLayout(False)
        GB_SPI.PerformLayout()
        GB_Uart.ResumeLayout(False)
        GB_Uart.PerformLayout()
        GB_OneWire.ResumeLayout(False)
        GB_OneWire.PerformLayout()
        GB_I2C.ResumeLayout(False)
        GB_I2C.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GB_SPI As GroupBox
    Friend WithEvents txt_spimiso As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents lbl_spisavestate As Label
    Friend WithEvents BTN_spiSettingsDelete As Button
    Friend WithEvents BTN_spiSettingsSave As Button
    Friend WithEvents txt_spimosi As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents txt_spiclk As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents GB_Uart As GroupBox
    Friend WithEvents txt_UartTx As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents CBB_UartBaudrate As ComboBox
    Friend WithEvents lbl_uartsavestate As Label
    Friend WithEvents BTN_uartSettingsDelete As Button
    Friend WithEvents BTN_uartSettingsSave As Button
    Friend WithEvents txt_UartRx As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents GB_OneWire As GroupBox
    Friend WithEvents lbl_onewiresavestate As Label
    Friend WithEvents BTN_OneWireSettingsDelete As Button
    Friend WithEvents BTN_OneWireSettingsSave As Button
    Friend WithEvents Txt_OneWireBusID As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Txt_OneWireGPIOPin As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents GB_I2C As GroupBox
    Friend WithEvents CB_i2cScan As CheckBox
    Friend WithEvents lbl_i2csavestate As Label
    Friend WithEvents BTN_i2cSettingsDelete As Button
    Friend WithEvents BTN_i2cSettingsSave As Button
    Friend WithEvents txt_i2cscl As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txt_i2csda As TextBox
    Friend WithEvents Label20 As Label
End Class

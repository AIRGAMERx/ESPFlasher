# ESPFlasher

<div align="center">

![ESPFlasher Logo](https://lfdev.de/images/espflasher/logo.png)

**A powerful graphical user interface for ESPHome configuration**

*Say goodbye to manual YAML editing - hello to visual ESP32/ESP8266 setup!*

[![Release](https://img.shields.io/github/v/release/airgamerx/espflasher?style=for-the-badge)](https://github.com/AIRGAMERx/ESPFlasher/releases)
[![Downloads](https://img.shields.io/github/downloads/airgamerx/espflasher/total?style=for-the-badge)](../../releases)
[![Stars](https://img.shields.io/github/stars/airgamerx/espflasher?style=for-the-badge)](../../stargazers)
[![License](https://img.shields.io/github/license/airgamerx/espflasher?style=for-the-badge)](LICENSE)

[**Download Latest**](../../releases/latest) • [**Documentation**](#quick-start) • [**Support**](../../issues) • [**Buy me a coffee ☕**](https://coff.ee/airgamer)

</div>

---

## 🌟 Features

<table>
<tr>
<td width="50%">

### 🎯 **Visual Configuration**
- Drag & drop sensors and displays
- No more manual YAML editing
- 50+ pre-configured components
- Live configuration preview

### ⚡ **Smart & Fast**
- Auto-correction for syntax errors
- Pin conflict detection
- One-click ESP flashing
- Real-time YAML generation

</td>
<td width="50%">

### 🔧 **Professional Tools** 
- Built-in ESPHome integration
- Template system for quick setups
- Project management
- OTA updates support
- Automatic ESP device discovery on network with port configuration

### 🏠 **Home Assistant Ready**
- Perfect ESPHome integration
- Community-driven definitions
- Regular updates
- Open source & extensible

</td>
</tr>
</table>

---

## 📸 Screenshots

<table>
<tr>
<td width="33%">
<img src="https://lfdev.de/images/espflasher/Main.png" alt="Main Interface">
<p align="center"><b>Main Interface</b></p>
</td>
<td width="33%">
<img src="https://lfdev.de/images/espflasher/Sensor.png" alt="Sensor Configuration">
<p align="center"><b>Sensor Configuration</b></p>
</td>
<td width="33%">
<img src="https://lfdev.de/images/espflasher/SensorFill.png" alt="YAML Preview">
<p align="center"><b>Live YAML Preview</b></p>
</td>
</tr>
</table>

---

## 🚀 Quick Start

### 1️⃣ **Download & Install**

```bash
# Option 1: Download from Releases (Recommended)
1. Go to Releases → Download ESPFlasher-v1.0.0.zip
2. Extract to any folder
3. Run ESPFlasher.exe

# Option 2: Build from Source
git clone https://github.com/AIRGAMERx/ESPFlasher.git
cd espflasher
# Open in Visual Studio and build
```

### 2️⃣ **First Launch**

1. **Select Chip Type**: Choose ESP32 or ESP8266
2. **Basic Configuration**: Set device name and WiFi credentials
3. **Add Components**: Use the visual interface to add sensors
4. **Preview**: Watch your YAML update in real-time
5. **Flash**: Connect your ESP and click the flash button!

### 3️⃣ **Add Your First Sensor**

```
Sensors Tab → Temperature → DHT22 → Configure pins → Add Sensor
```

That's it! Your sensor is now configured and ready to flash.

---

## 📋 System Requirements

| Component | Requirement |
|-----------|-------------|
| **OS** | Windows 7/8/10/11 (64-bit) |
| **Framework** | .NET Framework 4.8+ |
| **Memory** | 512 MB RAM minimum |
| **Storage** | 100 MB free space |
| **Hardware** | ESP32/ESP8266 development board |
| **Connection** | USB cable for flashing |

### 📦 **Dependencies (Auto-Installed)**
- ESPHome (Python package)
- Python 3.8+ (if not present)

---

## 🛠️ Supported Components

<details>
<summary><b>📊 Sensors (25+ supported)</b></summary>

| Category | Components |
|----------|------------|
| **Temperature** | DHT11, DHT22, DS18B20, BME280, SHT30 |
| **Humidity** | DHT11, DHT22, BME280, SHT30, AM2320 |
| **Pressure** | BME280, BMP180, BMP280 |
| **Motion** | PIR, RCWL-0516, LD2410 |
| **Distance** | HC-SR04, VL53L0X, TOF |
| **Light** | BH1750, TSL2561, APDS-9960 |
| **Air Quality** | MQ-2, MQ-135, SDS011, PMS5003 |
| **Current** | INA219, INA226, PZEM-004T |

</details>

<details>
<summary><b>📺 Displays (10+ supported)</b></summary>

| Type | Models |
|------|--------|
| **LCD** | 16x2, 20x4 with I2C (PCF8574) |
| **OLED** | SSD1306, SSD1309, SH1106 |
| **E-Paper** | 2.9", 4.2", 7.5" Waveshare |
| **7-Segment** | TM1637, MAX7219 |
| **LED Matrix** | MAX7219, WS2812B strips |

</details>

<details>
<summary><b>🎛️ Templates (15+ included)</b></summary>

- Smart Switch with status LED
- Climate monitor (temp + humidity)
- Motion detector with notifications
- Air quality monitor
- Power meter
- Weather station
- And many more...

</details>

---

## 🎮 Usage Guide

### **Adding Sensors**
1. Navigate to **Sensors** tab
2. Select sensor category (Temperature, Motion, etc.)
3. Choose specific sensor type
4. Configure parameters in the form
5. Click **Add Sensor**

### **Live Preview**
- YAML updates automatically as you make changes
- Export or flash directly from preview

### **Pin Management**
- Visual representation of ESP pinout
- Automatic conflict detection
- I2C/SPI bus configuration
- OneWire setup for Dallas sensors

### **Project Management**
- Save/load complete projects
- Version control integration
- Export configurations
- Backup and restore

---

## 🤝 Contributing

We ❤️ contributions! Here's how you can help:

### **🐛 Report Bugs**
- Use the [Issue Tracker](../../issues)
- Include steps to reproduce
- Add screenshots if helpful
- Mention your Windows version

### **💡 Suggest Features**
- Open a [Feature Request](../../issues/new?template=feature_request.md)
- Describe the use case
- Explain the expected behavior

### **🔧 Add Components**
- Fork the repository
- Edit `sensors.json`, `displays.json`, or `templates.json`
- Test with real hardware
- Submit a Pull Request

### **📚 Improve Documentation**
- Fix typos and grammar
- Add examples and tutorials
- Translate to other languages

### **🎨 UI/UX Improvements**
- Suggest design improvements
- Create mockups or prototypes
- Enhance user experience

---


## 🐛 Troubleshooting

<details>
<summary><b>❌ "Application failed to start"</b></summary>

**Solution**: Install .NET Framework 4.8
- Download: https://dotnet.microsoft.com/download/dotnet-framework/net48
- Restart computer after installation
</details>

<details>
<summary><b>⚠️ "ESPHome not found"</b></summary>

**Solution**: The app will guide you through ESPHome installation
- Click "Install ESPHome" when prompted
- Follow the automatic installer
- Restart the application
</details>

<details>
<summary><b>🔌 "Device not detected"</b></summary>

**Solution**: Check USB connection and drivers
- Try different USB cable
- Install ESP32/ESP8266 drivers
- Check Device Manager for unknown devices
</details>

<details>
<summary><b>📝 "YAML compilation failed"</b></summary>

**Solution**: Check your configuration
- Review the live preview for errors
- Ensure all required fields are filled
- Check pin assignments for conflicts
</details>

---

## 🔄 Roadmap

### **🎯 Version 1.1** (Next Release)
- [ ] Dark theme support
- [ ] English language pack
- [ ] Advanced lambda editor
- [ ] Project templates gallery

### **🚀 Version 1.2** (Future)
- [ ] Linux compatibility (via Mono)
- [ ] Cloud project sync
- [ ] Plugin system for custom components
- [ ] Mobile app companion

### **🌟 Version 2.0** (Long-term)
- [ ] Complete UI redesign
- [ ] macOS native support
- [ ] Collaborative editing
- [ ] Advanced debugging tools

---

## 📊 Statistics

<div align="center">

![GitHub Stats](https://github-readme-stats.vercel.app/api?username=airgamerx&repo=espflasher&show_icons=true&theme=blue-green)

</div>

---

## 💖 Support Development

If ESPFlasher saves you time and makes your IoT projects easier, consider supporting development:

<div align="center">

[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-Support%20Development-yellow.svg?style=for-the-badge&logo=buymeacoffee)](https://coff.ee/airgamer)

</div>

Every coffee helps fuel new features and keeps the project maintained! Your support means the world to a solo developer working on this in spare time.

### **Other Ways to Support:**
- ⭐ **Star this repository**
- 🐛 **Report bugs** and suggest features
- 📢 **Share** with other Home Assistant users
- 🤝 **Contribute** code or documentation
- 💬 **Join discussions** and help other users

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

```
Copyright (c) 2024 Lukas

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software...
```

---

## 🙏 Acknowledgments

- **ESPHome Team** - For the incredible ESPHome framework
- **Home Assistant Community** - For inspiration and continuous feedback  
- **Contributors** - Everyone who helped improve this tool
- **Beta Testers** - Thank you for finding bugs before release!

---

## 📞 Contact & Links

<div align="center">

[![Website](https://img.shields.io/badge/Website-lfdev.de-blue?style=for-the-badge)](https://www.lfdev.de)
[![GitHub](https://img.shields.io/badge/GitHub-ESPFlasher-black?style=for-the-badge&logo=github)](https://github.com/yourusername/espflasher)
[![Issues](https://img.shields.io/badge/Issues-Report%20Bugs-red?style=for-the-badge)](../../issues)
[![Coffee](https://img.shields.io/badge/Coffee-Support-yellow?style=for-the-badge)](https://coff.ee/airgamer)

</div>

---

<div align="center">

**⭐ If ESPFlasher helps you, please give it a star! It really motivates continued development.**

*Made with ❤️ for the Home Assistant & ESPHome community*

</div>

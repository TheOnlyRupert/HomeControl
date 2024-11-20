using System.IO.Ports;
using HomeControl.Source;

public static class HvacCrossPlay {
    private static SerialPort _serialPort;
    private static bool _intMessageSent;

    public static async Task EstablishConnection() {
        try {
            SetupSerialPort();
            await ReadSerialDataAsync();
        } catch (Exception ex) {
            LogDebug($"Error establishing connection: {ex.Message}");
        }
    }

    private static void SetupSerialPort() {
        try {
            _serialPort = new SerialPort("COM3", 9600);
            _serialPort.Open();
        } catch (Exception ex) {
            LogDebug($"Error setting up serial port: {ex.Message}");
        }
    }

    private static async Task ReadSerialDataAsync() {
        // Continuously check for incoming data from the Arduino
        while (true) {
            try {
                await Task.Delay(1000);
                if (_serialPort.BytesToRead > 0) {
                    string data = _serialPort.ReadLine();
                    ProcessData(data);
                }
            } catch (Exception ex) {
                LogDebug($"Error reading serial data: {ex.Message}");
                break;
            }
        }
    }

    private static void ProcessData(string data) {
        try {
            if (data.Contains("<INT,")) {
                data = data.Substring(5, data.Length - 6);
                string[] parts = data.Split(',');

                if (parts.Length >= 2) {
                    ReferenceValues.TemperatureInside = float.Parse(parts[0].Trim());
                    ReferenceValues.HumidityInside = float.Parse(parts[1].Trim());
                    _intMessageSent = false;
                    LogDebug($"Temperature: {ReferenceValues.TemperatureInside} °C, Humidity: {ReferenceValues.HumidityInside}%");
                } else {
                    LogDebug("Invalid data format for temperature and humidity.");
                }
            } else if (data.Contains("<HvacFanOn>")) {
                ReferenceValues.IsFanAuto = false;
                LogDebug("HVAC: Fan On");
            } else if (data.Contains("<HvacFanAuto>")) {
                ReferenceValues.IsFanAuto = true;
                LogDebug("HVAC: Fan Auto");
            } else if (data.Contains("<HvacState_")) {
                int state = int.Parse(data.Substring(11, 1));
                ProcessHvacState(state);
            }
        } catch (Exception ex) {
            LogDebug($"Error processing data: {ex.Message}");
        }
    }

    private static void ProcessHvacState(int state) {
        try {
            ReferenceValues.HvacState = state;
            ReferenceValues.IsHeatingMode = state == 5 || state == 6;
            ReferenceValues.IsProgramRunning = state == 1 || state == 5;
            UpdateHvacDisplay();
        } catch (Exception ex) {
            LogDebug($"Error processing HVAC state: {ex.Message}");
        }
    }

    private static void UpdateHvacDisplay() {
        try {
            // Code to update HVAC state display (UI, etc.)
            // You can update a UI control like a label or a text box with the current state.
            Console.Clear();
            Console.WriteLine($"Fan Auto: {ReferenceValues.IsFanAuto}");
            Console.WriteLine($"Heating Mode: {ReferenceValues.IsHeatingMode}");
            Console.WriteLine($"Program Running: {ReferenceValues.IsProgramRunning}");
            Console.WriteLine($"Temperature: {ReferenceValues.TemperatureInside} °C");
            Console.WriteLine($"Humidity: {ReferenceValues.HumidityInside}%");
            Console.WriteLine($"HVAC State: {ReferenceValues.HvacState}");
        } catch (Exception ex) {
            LogDebug($"Error updating HVAC display: {ex.Message}");
        }
    }

    private static void LogDebug(string message) {
        // Log to debug (could write to a file or output to the console)
        Console.WriteLine($"DEBUG: {message}");
    }

    public static void ControlHvacFanMode(bool autoMode) {
        try {
            ReferenceValues.IsFanAuto = autoMode;
            SendCommandToArduino(autoMode ? "<HvacFanAuto>" : "<HvacFanOn>");
        } catch (Exception ex) {
            LogDebug($"Error controlling HVAC fan mode: {ex.Message}");
        }
    }

    public static void ControlHvacState(int state) {
        try {
            ReferenceValues.HvacState = state;
            SendCommandToArduino($"<HvacState_{state}>");
        } catch (Exception ex) {
            LogDebug($"Error controlling HVAC state: {ex.Message}");
        }
    }

    private static void SendCommandToArduino(string command) {
        try {
            if (_serialPort != null && _serialPort.IsOpen) {
                _serialPort.WriteLine(command);
                LogDebug($"Sent command: {command}");
            } else {
                LogDebug("Serial port is not open or initialized.");
            }
        } catch (Exception ex) {
            LogDebug($"Error sending command to Arduino: {ex.Message}");
        }
    }

    public static void SetTemperatureSetpoint(float tempSet) {
        try {
            string command = $"<TEMP_SET_{tempSet}>";
            SendCommandToArduino(command);
        } catch (Exception ex) {
            LogDebug($"Error setting temperature setpoint: {ex.Message}");
        }
    }
}
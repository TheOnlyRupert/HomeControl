using System.Text;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public static class HvacCrossPlay {
    private static bool _comPortMessage, _intMessageSent;
    private static readonly CrossViewMessenger SimpleMessenger = CrossViewMessenger.Instance;

    public static async void EstablishConnection() {
        try {
            ReferenceValues.JsonHvacMaster = JsonSerializer.Deserialize<JsonHvac>(FileHelpers.LoadFileText("hvac", true));
        } catch (Exception) {
            ReferenceValues.JsonHvacMaster = new JsonHvac();

            FileHelpers.SaveFileText("hvac", JsonSerializer.Serialize(ReferenceValues.JsonHvacMaster), true);
        }

        ReferenceValues.TemperatureInside = -99;

        try {
            if (!ReferenceValues.SerialPort.IsOpen) {
                ReferenceValues.SerialPort.Open();
                ReferenceValues.IsHvacComEstablished = true;
                _comPortMessage = false;
                _intMessageSent = false;

                ReferenceValues.SerialPort.ReadTimeout = 500;
                ReferenceValues.SerialPort.WriteTimeout = 500;
                /* Pull current state from Arduino */
                ReferenceValues.SerialPort.Write("0");
            }
        } catch (Exception) {
            if (!_comPortMessage) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = "Unable to open port: " + ReferenceValues.JsonSettingsMaster.ComPort
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                _comPortMessage = true;
            }

            ReferenceValues.IsHvacComEstablished = false;
        }

        if (ReferenceValues.IsHvacComEstablished) {
            try {
                string output = "";
                while (true) {
                    byte[] data = await ReferenceValues.SerialPort.ReadAsync(1);
                    output += Encoding.UTF8.GetString(data, 0, data.Length);

                    if (output.Contains('<') && output.Contains('>')) {
                        int start = output.IndexOf('<');
                        int end = output.IndexOf('>');

                        ProcessData(output.Substring(start, end + 1));
                        output = output.Remove(start, output.Length);
                    }
                }
            } catch (Exception) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = "Unable to receive serial data"
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                ReferenceValues.IsHvacComEstablished = false;
                SimpleMessenger.PushMessage("HvacUpdated", null);
            }
        }

        if (ReferenceValues.HvacState == ReferenceValues.JsonHvacMaster.HvacState) {
            ReferenceValues.HvacStateTime = ReferenceValues.JsonHvacMaster.HvacStateTime;
        }
    }

    /* 0 -> HvacCoolingOff, 1 -> HvacCoolingRunning, 2 -> HvacCoolingStandby, 3 -> HvacCoolingPurging,
       4 -> HvacHeatingOff, 5 -> HvacHeatingRunning, 6 -> HvacHeatingStandby, 7 -> HvacHeatingPurging */
    private static void ProcessData(string data) {
        if (data.Contains("<INT,")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(5, data.Length - 6);
                string[] parts = data.Split(',');

                ReferenceValues.TemperatureInside = (int)float.Parse(parts[0].Trim());
                ReferenceValues.HumidityInside = (int)float.Parse(parts[1].Trim());
                _intMessageSent = false;
            } catch (FormatException) {
                ReferenceValues.TemperatureInside = -99;
                ReferenceValues.HumidityInside = -99;
                if (!_intMessageSent) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "HvacCrossPlay",
                        Description = "Unable to get interior temp/humidity... Possibly offline?"
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                _intMessageSent = true;
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }
        } else if (data.Contains("<HvacFanOn>")) {
            ReferenceValues.IsFanAuto = false;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Fan On"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacFanAuto>")) {
            ReferenceValues.IsFanAuto = true;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Fan Auto"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_0>")) {
            ReferenceValues.HvacState = HvacStates.Off;
            ReferenceValues.IsProgramRunning = false;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Off"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_1>")) {
            ReferenceValues.HvacState = HvacStates.Running;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.IsProgramRunning = true;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Running"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_2>")) {
            ReferenceValues.HvacState = HvacStates.Standby;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.IsProgramRunning = true;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Standby"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_3>")) {
            ReferenceValues.HvacState = HvacStates.Purging;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.IsProgramRunning = true;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Purging"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_4>")) {
            ReferenceValues.HvacState = HvacStates.Off;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = false;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Off"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_5>")) {
            ReferenceValues.HvacState = HvacStates.Running;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = true;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Running"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_6>")) {
            ReferenceValues.HvacState = HvacStates.Standby;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = true;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Standby"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacState_7>")) {
            ReferenceValues.HvacState = HvacStates.Purging;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = true;
            ReferenceValues.HvacStateTime = 0;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Purging"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<TEMP_SET_")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(10, data.Length - 14);
                ReferenceValues.TemperatureSet = int.Parse(data);

                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "HvacCrossPlay",
                    Description = "HVAC: Changing Set Temperature to: " + ReferenceValues.TemperatureSet + "°C"
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }
        }

        SimpleMessenger.PushMessage("HvacUpdated", null);
    }
}
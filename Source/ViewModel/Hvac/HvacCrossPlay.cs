using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public static class HvacCrossPlay {
    private static bool comPortMessage, intMessageSent;
    private static readonly CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;

    /* 0 -> Force Refresh,
     * 1 -> Fan On, 2 -> Fan Auto,
     * 3 -> Program On, 4 -> Program Off,
     * 5 -> Heating Mode, 6 -> Cooling Mode */
    public static async void EstablishConnection() {
        ReferenceValues.TemperatureInside = -99;

        try {
            if (!ReferenceValues.SerialPort.IsOpen) {
                ReferenceValues.SerialPort.Open();
                ReferenceValues.IsHvacComEstablished = true;
                comPortMessage = false;
                intMessageSent = false;

                ReferenceValues.SerialPort.ReadTimeout = 500;
                ReferenceValues.SerialPort.WriteTimeout = 500;
                /* Pull current state from Arduino */
                ReferenceValues.SerialPort.Write("0");
            }
        } catch (Exception) {
            if (!comPortMessage) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = "Unable to open port: " + ReferenceValues.JsonSettingsMaster.ComPort
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                comPortMessage = true;
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
                simpleMessenger.PushMessage("HvacUpdated", null);
            }
        }
    }

    private static void ProcessData(string data) {
        if (data.Contains("<INT,")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(5, data.Length - 6);
                string[] parts = data.Split(',');

                ReferenceValues.TemperatureInside = (int)float.Parse(parts[0].Trim());
                ReferenceValues.HumidityInside = (int)float.Parse(parts[1].Trim());
                intMessageSent = false;
            } catch (FormatException) {
                ReferenceValues.TemperatureInside = -99;
                ReferenceValues.HumidityInside = -99;
                if (!intMessageSent) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "HvacCrossPlay",
                        Description = "Unable to get interior temp/humidity... Possibly offline?"
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                intMessageSent = true;
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
        } else if (data.Contains("<HvacCoolingOff>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Off;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.IsProgramRunning = false;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Off"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacCoolingRunning>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Running;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.IsProgramRunning = true;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Running"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacCoolingStandby>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Standby;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.IsProgramRunning = true;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Standby"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacCoolingPurging>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Purging;
            ReferenceValues.IsHeatingMode = false;
            ReferenceValues.IsProgramRunning = true;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Purging"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacHeatingOff>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Off;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = false;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Off"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacHeatingRunning>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Running;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = true;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Running"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacHeatingStandby>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Standby;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = true;

            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Standby"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HvacHeatingPurging>")) {
            ReferenceValues.HvacMode = ReferenceValues.HvacModes.Purging;
            ReferenceValues.IsHeatingMode = true;
            ReferenceValues.IsProgramRunning = true;

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

        simpleMessenger.PushMessage("HvacUpdated", null);
    }
}
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public static class HvacCrossPlay {
    private static bool comPortMessage, intMessageSent, extMessageSent;
    private static readonly CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;

    /* 1 = fan on, 2 = fan off, 3 = cooling on, 4 = cooling off, 5 = heat on, 6 = heat off, 7 = override on, 8 = override off */
    public static async void EstablishConnection() {
        try {
            if (!ReferenceValues.SerialPort.IsOpen) {
                ReferenceValues.SerialPort.Open();
                ReferenceValues.IsHvacComEstablished = true;
                comPortMessage = false;
                intMessageSent = false;
                extMessageSent = false;

                ReferenceValues.SerialPort.ReadTimeout = 500;
                ReferenceValues.SerialPort.WriteTimeout = 500;
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
        if (data.Contains("<INT:")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(6, data.Length - 7);
                string[] parts = data.Split(',');

                ReferenceValues.InteriorTemp = (int)float.Parse(parts[0].Trim());
                ReferenceValues.InteriorHumidity = (int)float.Parse(parts[1].Trim());
                intMessageSent = false;
            } catch (FormatException) {
                ReferenceValues.InteriorTemp = -99;
                ReferenceValues.InteriorHumidity = -99;
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
        } else if (data.Contains("<EXT:")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(6, data.Length - 7);
                string[] parts = data.Split(',');

                ReferenceValues.ExteriorTemp = (int)float.Parse(parts[0].Trim());
                ReferenceValues.ExteriorHumidity = (int)float.Parse(parts[1].Trim());
                extMessageSent = false;
            } catch (FormatException) {
                ReferenceValues.ExteriorTemp = -99;
                ReferenceValues.ExteriorHumidity = -99;
                if (!extMessageSent) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "HvacCrossPlay",
                        Description = "Unable to get exterior temp/humidity... Possibly offline?"
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                extMessageSent = true;
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }
        } else if (data.Contains("<HVAC: Fan On>")) {
            ReferenceValues.JsonHvacMaster.IsFanAuto = false;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing Fan Mode to On"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Fan Auto>")) {
            ReferenceValues.JsonHvacMaster.IsFanAuto = true;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing Fan Mode to Auto"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Cooling Mode>")) {
            ReferenceValues.JsonHvacMaster.IsHeatingMode = false;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing to Cooling Mode"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Heating Mode>")) {
            ReferenceValues.JsonHvacMaster.IsHeatingMode = true;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing to Heating Mode"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Program Off>")) {
            ReferenceValues.JsonHvacMaster.IsProgramRunning = false;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Program Off"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Heating Running>")) {
            ReferenceValues.JsonHvacMaster.IsStandby = false;
            ReferenceValues.JsonHvacMaster.IsProgramRunning = true;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Running"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Heating Standby>")) {
            ReferenceValues.JsonHvacMaster.IsStandby = true;
            ReferenceValues.JsonHvacMaster.IsProgramRunning = true;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Standby"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Cooling Running>")) {
            ReferenceValues.JsonHvacMaster.IsStandby = false;
            ReferenceValues.JsonHvacMaster.IsProgramRunning = true;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Running"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Cooling Standby>")) {
            ReferenceValues.JsonHvacMaster.IsStandby = true;
            ReferenceValues.JsonHvacMaster.IsProgramRunning = true;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Standby"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: TEMP_SET_")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(16, data.Length - 17);
                ReferenceValues.JsonHvacMaster.TemperatureSet = int.Parse(data);
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "HvacCrossPlay",
                    Description = "HVAC: Changing Set Temperature to: " + ReferenceValues.JsonHvacMaster.TemperatureSet + "°C"
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
        } else if (data.Contains("<HVAC: Override On>")) {
            ReferenceValues.JsonHvacMaster.IsOverride = true;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing Override to On"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        } else if (data.Contains("<HVAC: Override Off>")) {
            ReferenceValues.JsonHvacMaster.IsOverride = false;
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing Override to Off"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        simpleMessenger.PushMessage("HvacUpdated", null);
    }

    public static void SaveJson() {
        try {
            FileHelpers.SaveFileText("hvac", JsonSerializer.Serialize(ReferenceValues.JsonHvacMaster), true);
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
}
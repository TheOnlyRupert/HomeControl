using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public static class HvacCrossPlay {
    private static bool comPortMessage;
    private static readonly CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;

    /* 1 = fan on, 2 = fan off, 3 = cooling on, 4 = cooling off, 5 = heat on, 6 = heat off */
    public static async void EstablishConnection() {
        try {
            if (!ReferenceValues.SerialPortMaster.IsOpen) {
                ReferenceValues.SerialPortMaster.Open();
                ReferenceValues.IsHvacComEstablished = true;
                comPortMessage = false;
                ReferenceValues.SerialPortMaster.Write("0");
            }
        } catch (Exception) {
            if (!comPortMessage) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = "Unable to open port: " + ReferenceValues.JsonMasterSettings.ComPort
                });
                comPortMessage = true;
            }

            ReferenceValues.IsHvacComEstablished = false;
        }

        if (ReferenceValues.IsHvacComEstablished) {
            try {
                string output = "";
                while (true) {
                    byte[] data = await ReferenceValues.SerialPortMaster.ReadAsync(1);
                    output += Encoding.UTF8.GetString(data, 0, data.Length);

                    if (output.Contains('<') && output.Contains('>')) {
                        int start = output.IndexOf('<');
                        int end = output.IndexOf('>');

                        ProcessData(output.Substring(start, end + 1));
                        output = output.Remove(start, output.Length);
                    }
                }
            } catch (Exception) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "HvacCrossPlay",
                    Description = "Unable to receive serial data"
                });

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
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        } else if (data.Contains("<EXT:")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(6, data.Length - 7);
                string[] parts = data.Split(',');

                ReferenceValues.ExteriorTemp = (int)float.Parse(parts[0].Trim());
                ReferenceValues.ExteriorHumidity = (int)float.Parse(parts[1].Trim());
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        } else if (data.Contains("<HVAC: Fan On>")) {
            ReferenceValues.JsonHvacSettings.IsFanAuto = false;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing Fan Mode to On"
            });
        } else if (data.Contains("<HVAC: Fan Auto>")) {
            ReferenceValues.JsonHvacSettings.IsFanAuto = true;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing Fan Mode to Auto"
            });
        } else if (data.Contains("<HVAC: Cooling Mode>")) {
            ReferenceValues.JsonHvacSettings.IsHeatingMode = false;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing to Cooling Mode"
            });
        } else if (data.Contains("<HVAC: Heating Mode>")) {
            ReferenceValues.JsonHvacSettings.IsHeatingMode = true;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Changing to Heating Mode"
            });
        } else if (data.Contains("<HVAC: Program Off>")) {
            ReferenceValues.JsonHvacSettings.IsProgramRunning = false;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Program Off"
            });
        } else if (data.Contains("<HVAC: Override TRUE>")) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Override TRUE"
            });
        } else if (data.Contains("<HVAC: Override FALSE>")) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Override FALSE"
            });
        } else if (data.Contains("<HVAC: Heating Running>")) {
            ReferenceValues.JsonHvacSettings.IsStandby = false;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Running"
            });
        } else if (data.Contains("<HVAC: Heating Standby>")) {
            ReferenceValues.JsonHvacSettings.IsStandby = true;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Heating Standby"
            });
        } else if (data.Contains("<HVAC: Cooling Running>")) {
            ReferenceValues.JsonHvacSettings.IsStandby = false;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Running"
            });
        } else if (data.Contains("<HVAC: Cooling Standby>")) {
            ReferenceValues.JsonHvacSettings.IsStandby = true;
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "HvacCrossPlay",
                Description = "HVAC: Cooling Standby"
            });
        } else if (data.Contains("<HVAC: TEMP_SET_")) {
            try {
                data = data.Substring(data.IndexOf('<'));
                data = data.Substring(16, data.Length - 17);
                ReferenceValues.JsonHvacSettings.TemperatureSet = int.Parse(data);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        simpleMessenger.PushMessage("HvacUpdated", null);
    }

    public static void SaveJson() {
        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonHvacSettings);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "hvac.json", jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "HvacCrossPlay",
                Description = e.ToString()
            });
        }
    }
}
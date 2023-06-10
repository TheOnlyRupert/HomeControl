using System;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;

namespace HomeControl.Source.ViewModel.Hvac;

public static class CrossPlay {
    /* 1 = fan on, 2 = fan off, 3 = cooling on, 4 = cooling off, 5 = heat on, 6 = heat off */

    private static int _duration = 4;

    public static void UpdateHvacState() {
        if (ReferenceValues.IsHvacComEstablished) {
            /* Protection from short running durations */
            //if (_duration < 5) {
            switch (ReferenceValues.JsonHvacSettings.ProgramState) {
            case 0:
                StopHvac();

                break;
            case 1:
                /* If more then 2 degree difference */
                if (ReferenceValues.JsonHvacSettings.IsHeatingMode) {
                    if (ReferenceValues.JsonHvacSettings.TemperatureSet - ReferenceValues.InteriorTemp > 2) {
                        Console.Write("work Heat");
                        StartHvacHeating();
                    }
                } else {
                    if (ReferenceValues.InteriorTemp - ReferenceValues.JsonHvacSettings.TemperatureSet > 2) {
                        Console.Write("work Cool");
                        StartHvacCooling();
                    }
                }

                break;
            case 2:
                /* If the temps are the same */
                if (ReferenceValues.InteriorTemp == ReferenceValues.JsonHvacSettings.TemperatureSet) {
                    StopHvac();
                }

                break;
            }

            if (ReferenceValues.JsonHvacSettings.IsFanOnMode) {
                StartHvacFan();
            }

            _duration = 0;
            //}
        }
    }

    private static void StartHvacFan() {
        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "CrossPlay",
            Description = "Starting HVAC (Mode: Fan Only)"
        });

        ReferenceValues.SerialPortMaster.Write("1");
    }

    private static void StartHvacCooling() {
        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "CrossPlay",
            Description = "Starting HVAC (Mode: Cooling)"
        });

        ReferenceValues.JsonHvacSettings.ProgramState = 2;
        ReferenceValues.SerialPortMaster.Write("1");
        ReferenceValues.SerialPortMaster.Write("3");
        ReferenceValues.SerialPortMaster.Write("6");
    }

    private static void StartHvacHeating() {
        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "CrossPlay",
            Description = "Starting HVAC (Mode: Heating)"
        });

        ReferenceValues.JsonHvacSettings.ProgramState = 2;
        ReferenceValues.SerialPortMaster.Write("1");
        ReferenceValues.SerialPortMaster.Write("4");
        ReferenceValues.SerialPortMaster.Write("5");
    }

    /* Both heating and cooling */
    private static void StopHvac() {
        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "CrossPlay",
            Description = "Stopping HVAC"
        });

        ReferenceValues.SerialPortMaster.Write("4");
        ReferenceValues.SerialPortMaster.Write("6");

        if (!ReferenceValues.JsonHvacSettings.IsFanOnMode) {
            ReferenceValues.SerialPortMaster.Write("2");
        }

        if (ReferenceValues.JsonHvacSettings.ProgramState == 2) {
            ReferenceValues.JsonHvacSettings.ProgramState = 1;
        }
    }
}
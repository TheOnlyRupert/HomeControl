using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class BloodPressureVM : BaseViewModel {
    private readonly JsonBloodPressure jsonBloodPressure;
    private ObservableCollection<BloodPressure> _pressureList;
    private BloodPressure _pressureSelected;
    private string _pressureText, _noteText;

    public BloodPressureVM() {
        try {
            jsonBloodPressure = JsonSerializer.Deserialize<JsonBloodPressure>(FileHelpers.LoadFileText("bloodPressure", true));
        } catch (Exception) {
            jsonBloodPressure = new JsonBloodPressure();

            FileHelpers.SaveFileText("bloodPressure", JsonSerializer.Serialize(jsonBloodPressure), true);
        }

        PressureList = ReferenceValues.ActiveBehaviorUser switch {
            1 => jsonBloodPressure.User1List,
            2 => jsonBloodPressure.User2List,
            3 => jsonBloodPressure.User3List,
            4 => jsonBloodPressure.User4List,
            5 => jsonBloodPressure.User5List
        } ?? new ObservableCollection<BloodPressure>();

        PressureText = "";
        NoteText = "";
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(PressureText)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "BloodPressureVM",
                    Description = "Adding blood pressure: " + DateTime.Now.ToString("MM-dd") + ", " + PressureText + ", " + NoteText
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                PressureList.Add(new BloodPressure {
                    Date = DateTime.Now,
                    PressureText = PressureText,
                    NoteText = NoteText
                });

                ReferenceValues.SoundToPlay = "birthday";
                SoundDispatcher.PlaySound();
                PressureText = "";
                NoteText = "";

                SaveJson();
            }

            break;
        case "delete":
            try {
                if (PressureSelected.PressureText != null) {
                    MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this log entry?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BloodPressureVM",
                            Description = "Deleting blood pressure: " + DateTime.Now.ToString("MM-dd") + ", " + PressureText + ", " + NoteText
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        PressureList.Remove(PressureSelected);
                        PressureText = "";
                        NoteText = "";

                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "BloodPressureVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        }
    }

    private void PopulateDetailedView(BloodPressure value) {
        PressureText = value.PressureText;
        NoteText = value.NoteText;
    }

    private void SaveJson() {
        try {
            switch (ReferenceValues.ActiveBehaviorUser) {
            case 1:
                jsonBloodPressure.User1List = PressureList;
                break;
            case 2:
                jsonBloodPressure.User2List = PressureList;
                break;
            case 3:
                jsonBloodPressure.User3List = PressureList;
                break;
            case 4:
                jsonBloodPressure.User4List = PressureList;
                break;
            case 5:
                jsonBloodPressure.User5List = PressureList;
                break;
            }

            FileHelpers.SaveFileText("bloodPressure", JsonSerializer.Serialize(jsonBloodPressure), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BloodPressureVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }
    }

    #region Fields

    public string PressureText {
        get => _pressureText;
        set {
            _pressureText = value;
            RaisePropertyChangedEvent("PressureText");
        }
    }

    public string NoteText {
        get => _noteText;
        set {
            _noteText = value;
            RaisePropertyChangedEvent("NoteText");
        }
    }

    public ObservableCollection<BloodPressure> PressureList {
        get => _pressureList;
        set {
            _pressureList = value;
            RaisePropertyChangedEvent("PressureList");
        }
    }

    public BloodPressure PressureSelected {
        get => _pressureSelected;
        set {
            _pressureSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("PressureSelected");
        }
    }

    #endregion
}
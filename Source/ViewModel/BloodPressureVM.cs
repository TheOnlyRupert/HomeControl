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
    private readonly JsonBloodPressure _jsonBloodPressure;
    private ObservableCollection<BloodPressure> _pressureList = [];
    private BloodPressure _pressureSelected;
    private string _pressureText, _noteText;

    public BloodPressureVM() {
        _jsonBloodPressure = LoadJsonBloodPressure();
        InitializePressureList();
        PressureText = "";
        NoteText = "";
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    public string PressureText {
        get => _pressureText;
        set => SetProperty(ref _pressureText, value);
    }

    public string NoteText {
        get => _noteText;
        set => SetProperty(ref _noteText, value);
    }

    public ObservableCollection<BloodPressure> PressureList {
        get => _pressureList;
        set => SetProperty(ref _pressureList, value);
    }

    public BloodPressure PressureSelected {
        get => _pressureSelected;
        set {
            if (SetProperty(ref _pressureSelected, value))
                PopulateDetailedView(value);
        }
    }

    private static JsonBloodPressure LoadJsonBloodPressure() {
        try {
            return JsonSerializer.Deserialize<JsonBloodPressure>(FileHelpers.LoadFileText("bloodPressure", true));
        } catch {
            JsonBloodPressure newJson = new();
            FileHelpers.SaveFileText("bloodPressure", JsonSerializer.Serialize(newJson), true);
            return newJson;
        }
    }

    private void InitializePressureList() {
        PressureList = ReferenceValues.ActiveBehaviorUser switch {
            1 => _jsonBloodPressure.User1List,
            2 => _jsonBloodPressure.User2List,
            3 => _jsonBloodPressure.User3List,
            4 => _jsonBloodPressure.User4List,
            5 => _jsonBloodPressure.User5List
        } ?? [];
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "add":
            AddBloodPressure();
            break;

        case "delete":
            DeleteBloodPressure();
            break;
        }
    }

    private void AddBloodPressure() {
        if (string.IsNullOrWhiteSpace(PressureText)) {
            PlayMissingInfoSound();
            return;
        }

        LogDebug("Adding blood pressure: " + DateTime.Now.ToString("MM-dd") + ", " + PressureText + ", " + NoteText);
        PressureList?.Add(new BloodPressure {
            Date = DateTime.Now,
            PressureText = PressureText,
            NoteText = NoteText
        });

        PlayAddSound();
        ClearTextFields();
        SaveJson();
    }

    private void DeleteBloodPressure() {
        try {
            if (PressureSelected?.PressureText != null) {
                MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this log entry?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes) {
                    LogDebug("Deleting blood pressure: " + DateTime.Now.ToString("MM-dd") + ", " + PressureText + ", " + NoteText);
                    PressureList.Remove(PressureSelected);
                    ClearTextFields();
                    SaveJson();
                }
            }
        } catch (Exception e) {
            LogWarning(e.ToString());
        }
    }

    private static void LogDebug(string description) {
        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "BloodPressureVM",
            Description = description
        });
        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
    }

    private static void LogWarning(string description) {
        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "WARN",
            Module = "BloodPressureVM",
            Description = description
        });
        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
    }

    private static void PlayMissingInfoSound() {
        ReferenceValues.SoundToPlay = "missing_info";
        SoundDispatcher.PlaySound();
    }

    private static void PlayAddSound() {
        ReferenceValues.SoundToPlay = "birthday";
        SoundDispatcher.PlaySound();
    }

    private void ClearTextFields() {
        PressureText = "";
        NoteText = "";
    }

    private void SaveJson() {
        try {
            UpdateUserListBasedOnActiveBehavior();
            FileHelpers.SaveFileText("bloodPressure", JsonSerializer.Serialize(_jsonBloodPressure), true);
        } catch (Exception e) {
            LogWarning(e.ToString());
        }
    }

    private void UpdateUserListBasedOnActiveBehavior() {
        switch (ReferenceValues.ActiveBehaviorUser) {
        case 1: _jsonBloodPressure.User1List = PressureList; break;
        case 2: _jsonBloodPressure.User2List = PressureList; break;
        case 3: _jsonBloodPressure.User3List = PressureList; break;
        case 4: _jsonBloodPressure.User4List = PressureList; break;
        case 5: _jsonBloodPressure.User5List = PressureList; break;
        }
    }

    private void PopulateDetailedView(BloodPressure value) {
        PressureText = value.PressureText;
        NoteText = value.NoteText;
    }
}
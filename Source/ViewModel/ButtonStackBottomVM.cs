using System;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules;
using HomeControl.Source.Modules.Games;
using HomeControl.Source.ViewModel.Base;
using Tamagotchi = HomeControl.Source.Modules.Games.Tamagotchi.Tamagotchi;

namespace HomeControl.Source.ViewModel;

public class ButtonStackBottomVM : BaseViewModel {
    public ButtonStackBottomVM() {
        try {
            ReferenceValues.JsonGameStatsMaster = JsonSerializer.Deserialize<JsonGameStats>(FileHelpers.LoadFileText("gameStats", true));
        } catch (Exception) {
            ReferenceValues.JsonGameStatsMaster = new JsonGameStats();

            FileHelpers.SaveFileText("gameStats", JsonSerializer.Serialize(ReferenceValues.JsonGameStatsMaster), true);
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "contacts":
            Contacts contacts = new();
            contacts.ShowDialog();
            contacts.Close();
            break;
        case "workout":
            Modules.Workout.Workout workout = new();
            workout.ShowDialog();
            workout.Close();
            break;
        case "pictionary":
            Pictionary pictionary = new();
            pictionary.ShowDialog();
            pictionary.Close();
            break;
        case "coinFlip":
            CoinFlip coinFlip = new();
            coinFlip.ShowDialog();
            coinFlip.Close();
            break;
        case "tamagotchi":
            Tamagotchi tamagotchi = new();
            tamagotchi.ShowDialog();
            tamagotchi.Close();

            FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
            break;
        case "nhie":
            Nhie nhie = new();
            nhie.ShowDialog();
            nhie.Close();
            break;
        case "trafficLight":
            TrafficLight trafficLight = new();
            trafficLight.ShowDialog();
            trafficLight.Close();
            break;
        }
    }
}
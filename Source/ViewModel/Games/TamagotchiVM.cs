using System.Windows.Input;
using HomeControl.Source.Modules.Games;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class TamagotchiVM : BaseViewModel {
    public TamagotchiVM() {
        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") { }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "bed":
            TamagotchiWindowBed tamagotchiWindowBed = new();
            tamagotchiWindowBed.ShowDialog();
            tamagotchiWindowBed.Close();
            break;
        case "closet":
            TamagotchiWindowCloset tamagotchiWindowCloset = new();
            tamagotchiWindowCloset.ShowDialog();
            tamagotchiWindowCloset.Close();
            break;
        case "tub":
            break;
        case "toilet":
            break;
        case "sinkBath":
            break;
        case "couch":
            break;
        case "table":
            break;
        case "chairW":
            break;
        case "chairE":
            break;
        case "chairN":
            break;
        case "chairS":
            break;
        case "counter1":
            break;
        case "counter2":
            break;
        case "counter3":
            break;
        case "stove":
            break;
        case "sinkKitchen":
            break;
        case "fridge":
            break;
        case "washer":
            break;
        case "dryer":
            break;
        case "pantry":
            break;
        }
    }
}
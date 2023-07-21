using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class PasswordVM : BaseViewModel {
    private string _image;

    public PasswordVM() {
        ReferenceValues.LockUI = !ReferenceValues.JsonMasterSettings.IsDebugMode;
        Image = ReferenceValues.LockUI ? "./../../Resources/Images/icons/key_locked.png" : "./../../Resources/Images/icons/key_unlocked.png";

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    #region Fields

    public string Image {
        get => _image;
        set {
            _image = value;
            RaisePropertyChangedEvent("Image");
        }
    }

    #endregion

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "ScreenSaverOn") {
            Image = "./../../Resources/Images/icons/key_locked.png";
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "lock":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.LockUI = true;
                Image = "./../../Resources/Images/icons/key_locked.png";
            } else {
                //ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                //    Date = DateTime.Now,
                //    Level = "INFO",
                //    Module = "PasswordVM",
                //    Description = "Unlocking UI with correct password"
                //});
                //SaveDebugFile.Save();

                ReferenceValues.LockUI = false;
                Image = "./../../Resources/Images/icons/key_unlocked.png";
            }

            ReferenceValues.SoundToPlay = "unlock";
            SoundDispatcher.PlaySound();

            break;
        }
    }
}
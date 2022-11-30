using System.Windows;
using HomeControl.Source.ViewModel.Timer;

namespace HomeControl.Source.Modules.Timer;

public partial class EditTimer : Window {
    public EditTimer() {
        InitializeComponent();
        DataContext = new EditTimerVM();
    }
}
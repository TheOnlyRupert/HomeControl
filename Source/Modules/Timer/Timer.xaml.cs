using HomeControl.Source.ViewModel.Timer;

namespace HomeControl.Source.Modules.Timer;

public partial class Timer {
    public Timer() {
        InitializeComponent();
        DataContext = new TimerVM();
    }
}
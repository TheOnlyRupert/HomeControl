using System.ComponentModel;
using System.Windows;
using HomeControl.Source;
using HomeControl.Source.Control;
using HomeControl.Source.ViewModel;

namespace HomeControl;

public partial class MainWindow {
    public MainWindow() {
        InitializeComponent();
        DataContext = new MainWindowVM();

        ReferenceValues.ScreensaverMaster = new Screensaver(canvas);
    }

    protected override void OnClosing(CancelEventArgs e) {
        MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButton.YesNo,
            MessageBoxImage.Warning, MessageBoxResult.No);
        if (messageBoxResult == MessageBoxResult.No) {
            e.Cancel = true;
        }
    }
}
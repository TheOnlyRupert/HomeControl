using System.ComponentModel;
using System.Windows;
using HomeControl.Source.Control;
using HomeControl.Source.ViewModel;

namespace HomeControl;

public partial class MainWindow {
    private readonly SnowEngine snow, leaves;

    public MainWindow() {
        InitializeComponent();
        DataContext = new MainWindowVM();

        snow = new SnowEngine(canvas, 25, "pack://application:,,,/Resources/Images/snowflakes/snow1.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow2.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow3.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow4.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow5.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow6.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow7.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow8.png",
            "pack://application:,,,/Resources/Images/snowflakes/snow9.png");
        snow.Start();

        //leaves = new SnowEngine(canvas, 10, "pack://application:,,,/Resources/Images/leaves/leaf1.png",
        //    "pack://application:,,,/Resources/Images/leaves/leaf2.png",
        //    "pack://application:,,,/Resources/Images/leaves/leaf3.png",
        //    "pack://application:,,,/Resources/Images/leaves/leaf4.png",
        //    "pack://application:,,,/Resources/Images/leaves/leaf5.png",
        //    "pack://application:,,,/Resources/Images/leaves/leaf6.png");
        //leaves.Start();
    }

    protected override void OnClosing(CancelEventArgs e) {
        MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButton.YesNo,
            MessageBoxImage.Warning, MessageBoxResult.No);
        if (messageBoxResult == MessageBoxResult.No) {
            e.Cancel = true;
        }
    }
}
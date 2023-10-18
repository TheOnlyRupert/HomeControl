using System;
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

        string[] items;
        switch (DateTime.Now.Month) {
        case 12:
        case 1:
        case 2:
            items = new[] {
                "pack://application:,,,/Resources/Images/snowflakes/snow1.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow2.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow3.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow4.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow5.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow6.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow7.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow8.png",
                "pack://application:,,,/Resources/Images/snowflakes/snow9.png"
            };
            break;
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 8:
            items = new[] {
                "pack://application:,,,/Resources/Images/test/test.png"
            };
            break;
        default:
            if (DateTime.Now.Month == 3 || DateTime.Now.Month == 4 || DateTime.Now.Month == 5) {
                items = new[] {
                    "pack://application:,,,/Resources/Images/test/test.png"
                };
            } else {
                items = new[] {
                    "pack://application:,,,/Resources/Images/leaves/leaf1.png",
                    "pack://application:,,,/Resources/Images/leaves/leaf2.png",
                    "pack://application:,,,/Resources/Images/leaves/leaf3.png",
                    "pack://application:,,,/Resources/Images/leaves/leaf4.png",
                    "pack://application:,,,/Resources/Images/leaves/leaf5.png",
                    "pack://application:,,,/Resources/Images/leaves/leaf6.png"
                };
            }

            break;
        }

        ReferenceValues.SnowEngineMaster = new SnowEngine(canvas, 25, items);
    }

    protected override void OnClosing(CancelEventArgs e) {
        MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButton.YesNo,
            MessageBoxImage.Warning, MessageBoxResult.No);
        if (messageBoxResult == MessageBoxResult.No) {
            e.Cancel = true;
        }
    }
}
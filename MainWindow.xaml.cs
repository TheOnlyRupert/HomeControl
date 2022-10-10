using System.ComponentModel;
using System.Windows;
using HomeControl.Source.ViewModel;

namespace HomeControl {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }

        protected override void OnClosing(CancelEventArgs e) {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButton.YesNo,
                MessageBoxImage.Warning, MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.No) {
                e.Cancel = true;
            }
        }
    }
}
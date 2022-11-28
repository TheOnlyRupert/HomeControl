using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresMonth : Window {
    public ChoresMonth() {
        InitializeComponent();
        DataContext = new ChoresMonthVM();
    }
}
using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresMonthUser1 : Window {
    public ChoresMonthUser1() {
        InitializeComponent();
        DataContext = new ChoresMonthUser1VM();
    }
}
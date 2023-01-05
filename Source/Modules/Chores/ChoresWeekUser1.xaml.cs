using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresWeekUser1 : Window {
    public ChoresWeekUser1() {
        InitializeComponent();
        DataContext = new ChoresWeekUser1VM();
    }
}
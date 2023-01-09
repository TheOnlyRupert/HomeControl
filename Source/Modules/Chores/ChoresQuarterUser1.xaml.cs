using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresQuarterUser1 : Window {
    public ChoresQuarterUser1() {
        InitializeComponent();
        DataContext = new ChoresQuarterUser1VM();
    }
}
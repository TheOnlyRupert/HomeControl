using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresQuarter : Window {
    public ChoresQuarter() {
        InitializeComponent();
        DataContext = new ChoresQuarterVM();
    }
}
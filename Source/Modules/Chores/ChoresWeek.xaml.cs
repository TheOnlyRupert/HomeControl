using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresWeek : Window {
    public ChoresWeek() {
        InitializeComponent();
        DataContext = new ChoresWeekVM();
    }
}
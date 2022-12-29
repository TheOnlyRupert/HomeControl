using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresDay : Window {
    public ChoresDay() {
        InitializeComponent();
        DataContext = new ChoresDayVM();
    }
}
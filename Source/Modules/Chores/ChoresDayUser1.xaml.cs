using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresDayUser1 : Window {
    public ChoresDayUser1() {
        InitializeComponent();
        DataContext = new ChoresDayUser1VM();
    }
}
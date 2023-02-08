using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Groceries {
    public Groceries() {
        InitializeComponent();
        DataContext = new GroceriesVM();
    }
}
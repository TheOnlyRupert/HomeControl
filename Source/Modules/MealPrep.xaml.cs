using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class MealPrep {
    public MealPrep() {
        InitializeComponent();
        DataContext = new MealPrepVM();
    }
}
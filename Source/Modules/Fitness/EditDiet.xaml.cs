using HomeControl.Source.ViewModel.Exercise;

namespace HomeControl.Source.Modules.Fitness;

public partial class EditDiet {
    public EditDiet() {
        InitializeComponent();
        DataContext = new EditDietVM();
    }
}
using HomeControl.Source.ViewModel.Daily;

namespace HomeControl.Source.Modules.Daily;

public partial class EditDaily {
    public EditDaily() {
        InitializeComponent();
        DataContext = new EditDailyVM();
    }
}
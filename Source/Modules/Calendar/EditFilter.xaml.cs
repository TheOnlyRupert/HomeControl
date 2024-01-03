using HomeControl.Source.ViewModel.Calendar;

namespace HomeControl.Source.Modules.Calendar;

public partial class EditFilter {
    public EditFilter() {
        InitializeComponent();
        DataContext = new EditFilterVM();
    }
}
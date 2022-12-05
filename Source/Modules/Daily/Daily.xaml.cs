using HomeControl.Source.ViewModel.Daily;

namespace HomeControl.Source.Modules.Daily;

public partial class Daily {
    public Daily() {
        InitializeComponent();
        DataContext = new DailyVM();
    }
}
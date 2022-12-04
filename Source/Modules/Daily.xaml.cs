using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Daily {
    public Daily() {
        InitializeComponent();
        DataContext = new DailyVM();
    }
}
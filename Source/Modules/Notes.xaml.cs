using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Notes {
    public Notes() {
        InitializeComponent();
        DataContext = new NotesVM();
    }
}
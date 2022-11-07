using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games; 

public partial class Pictionary {
    public Pictionary() {
        InitializeComponent();
        DataContext = new PictionaryVM();
        Loaded += (s, e) => {
            // ReSharper disable once PossibleNullReferenceException
            GetWindow(this).Closing += (s1, e1) => PictionaryVM.DisposeWindow();
        };
    }
}
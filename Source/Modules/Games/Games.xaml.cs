using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games {
    public partial class Games {
        public Games() {
            InitializeComponent();
            DataContext = new GamesVM();
        }
    }
}
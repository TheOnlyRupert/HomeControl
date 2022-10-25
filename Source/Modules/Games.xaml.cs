using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules {
    public partial class Games {
        public Games() {
            InitializeComponent();
            DataContext = new GamesVM();
        }
    }
}
using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules {
    public partial class Chores {
        public Chores() {
            InitializeComponent();
            DataContext = new ChoresVM();
        }
    }
}
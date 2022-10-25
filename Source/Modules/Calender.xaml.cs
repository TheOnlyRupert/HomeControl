using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules {
    public partial class Calender {
        public Calender() {
            InitializeComponent();
            DataContext = new CalenderVM();
        }
    }
}
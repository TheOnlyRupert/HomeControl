using System.Windows.Input;
using HomeControl.Source.Modules;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class NotesVM : BaseViewModel {
    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "contacts":
            Contacts contacts = new();
            contacts.ShowDialog();
            contacts.Close();
            break;
        case "mealPrep":
            MealPrep mealPrep = new();
            mealPrep.ShowDialog();
            mealPrep.Close();
            break;
        case "groceries":
            Groceries groceries = new();
            groceries.ShowDialog();
            groceries.Close();
            break;
        }
    }
}
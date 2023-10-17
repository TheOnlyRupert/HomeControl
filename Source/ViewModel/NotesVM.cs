﻿using System.Windows.Input;
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
        case "security":
            Security security = new();
            security.ShowDialog();
            security.Close();
            break;
        }
    }
}
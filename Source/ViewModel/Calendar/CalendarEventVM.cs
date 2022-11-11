using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class CalendarEventVM : BaseViewModel {
    private string _eventDate, _eventText, _locationText, _descriptionText, _saveButtonText;
    private ObservableCollection<Events> _eventList;
    private Events _eventSelected;

    public CalendarEventVM() {
        EventDate = ReferenceValues.CalendarEventDate.ToLongDateString();
        EventList = new ObservableCollection<Events>();

        SaveButtonText = "Save";
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void PopulateDetailedView(Events value) {
        EventText = value.name;
        LocationText = value.location;
        DescriptionText = value.description;
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "save":

            break;
        case "delete":
            break;
        }
    }

    #region Fields

    public string EventDate {
        get => _eventDate;
        set {
            _eventDate = value;
            RaisePropertyChangedEvent("EventDate");
        }
    }

    public string EventText {
        get => _eventText;
        set {
            _eventText = value;
            RaisePropertyChangedEvent("EventText");
        }
    }

    public string SaveButtonText {
        get => _saveButtonText;
        set {
            _saveButtonText = value;
            RaisePropertyChangedEvent("SaveButtonText");
        }
    }

    public string LocationText {
        get => _locationText;
        set {
            _locationText = value;
            RaisePropertyChangedEvent("LocationText");
        }
    }

    public string DescriptionText {
        get => _descriptionText;
        set {
            _descriptionText = value;
            RaisePropertyChangedEvent("DescriptionText");
        }
    }

    public ObservableCollection<Events> EventList {
        get => _eventList;
        set {
            _eventList = value;
            RaisePropertyChangedEvent("EventList");
        }
    }

    public Events EventSelected {
        get => _eventSelected;
        set {
            _eventSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("EventSelected");
        }
    }

    #endregion
}
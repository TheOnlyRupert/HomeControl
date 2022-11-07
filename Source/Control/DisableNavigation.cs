using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HomeControl.Source.Control; 

public static class DisableNavigation {
    public static readonly DependencyProperty DisableProperty = DependencyProperty.RegisterAttached(
        "Disable", typeof(bool), typeof(DisableNavigation), new PropertyMetadata(false, DisableChanged));

    public static bool GetDisable(DependencyObject o) {
        return (bool)o.GetValue(DisableProperty);
    }

    public static void SetDisable(DependencyObject o, bool value) {
        o.SetValue(DisableProperty, value);
    }

    public static void DisableChanged(object sender, DependencyPropertyChangedEventArgs e) {
        Frame frame = (Frame)sender;
        frame.Navigated += DontNavigate;
        frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
    }

    public static void DontNavigate(object sender, NavigationEventArgs e) {
        ((Frame)sender).NavigationService.RemoveBackEntry();
    }
}
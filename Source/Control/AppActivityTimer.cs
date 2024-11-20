using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace HomeControl.Source.Control;

public class AppActivityTimer {
    private readonly DispatcherTimer _inactivityTimer;
    private readonly bool _MonitorMousePosition;
    private readonly DispatcherTimer _timePassedTimer;
    private Point _inactiveMousePosition = new(0, 0);

    public AppActivityTimer(int timePassedInMS, int IdleTimeInMS, bool WillMonitorMousePosition) {
        _MonitorMousePosition = WillMonitorMousePosition;
        InputManager.Current.PreProcessInput += OnActivity;

        // Time Passed Timer
        _timePassedTimer = new DispatcherTimer();
        TimeSpan timePassed = TimeSpan.FromMilliseconds(timePassedInMS);
        // Start the time passed timer
        _timePassedTimer.Tick += OnTimePassedHandler;
        _timePassedTimer.Interval = timePassed;
        _timePassedTimer.IsEnabled = true;

        // Inactivity Timer
        _inactivityTimer = new DispatcherTimer();
        InactivityThreshold = TimeSpan.FromMilliseconds(IdleTimeInMS);
        // Start the inactivity timer
        _inactivityTimer.Tick += OnInactivity;
        _inactivityTimer.Interval = InactivityThreshold;
        _inactivityTimer.IsEnabled = true;
    }

    private TimeSpan InactivityThreshold { get; }
    public event PreProcessInputEventHandler OnActive;
    public event EventHandler OnInactive;
    public event EventHandler OnTimePassed;

    private void OnActivity(object sender, PreProcessInputEventArgs e) {
        InputEventArgs inputEventArgs = e.StagingItem.Input;
        if (inputEventArgs is MouseEventArgs || inputEventArgs is KeyboardEventArgs) {
            if (inputEventArgs is MouseEventArgs mea) {
                // no button is pressed and the position is still the same as the application became inactive
                if (mea.LeftButton == MouseButtonState.Released &&
                    mea.RightButton == MouseButtonState.Released &&
                    mea.MiddleButton == MouseButtonState.Released &&
                    mea.XButton1 == MouseButtonState.Released &&
                    mea.XButton2 == MouseButtonState.Released &&
                    (_MonitorMousePosition == false ||
                     _MonitorMousePosition && _inactiveMousePosition == mea.GetPosition(Application.Current.MainWindow))) {
                    return;
                }
            }

            // Reset idle timer
            _inactivityTimer.IsEnabled = false;
            _inactivityTimer.IsEnabled = true;
            _inactivityTimer.Stop();
            _inactivityTimer.Start();
            if (OnActive != null) {
                OnActive(sender, e);
            }
        }
    }

    private void OnInactivity(object sender, EventArgs e) {
        _inactiveMousePosition = Mouse.GetPosition(Application.Current.MainWindow);
        _inactivityTimer.Stop();
        if (OnInactive != null) {
            OnInactive(sender, e);
        }
    }

    private void OnTimePassedHandler(object sender, EventArgs e) {
        if (OnTimePassed != null) {
            OnTimePassed(sender, e);
        }
    }
}
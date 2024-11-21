using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace HomeControl.Source.Control;

public class AppActivityTimer {
    private readonly DispatcherTimer _inactivityTimer;
    private readonly bool _monitorMousePosition;
    private readonly DispatcherTimer _timePassedTimer;
    private Point _lastKnownMousePosition = new(0, 0);

    public AppActivityTimer(int timePassedInMS, int idleTimeInMS, bool monitorMousePosition) {
        _monitorMousePosition = monitorMousePosition;

        InputManager.Current.PreProcessInput += OnActivity;

        // Time Passed Timer
        _timePassedTimer = new DispatcherTimer {
            Interval = TimeSpan.FromMilliseconds(timePassedInMS),
            IsEnabled = true
        };
        _timePassedTimer.Tick += OnTimePassedHandler;

        // Inactivity Timer
        _inactivityTimer = new DispatcherTimer {
            Interval = TimeSpan.FromMilliseconds(idleTimeInMS),
            IsEnabled = true
        };
        _inactivityTimer.Tick += OnInactivity;
    }

    public event PreProcessInputEventHandler? OnActive;
    public event EventHandler? OnInactive;
    public event EventHandler? OnTimePassed;

    private void OnActivity(object sender, PreProcessInputEventArgs e) {
        InputEventArgs inputEventArgs = e.StagingItem.Input;

        // Check if the input is a mouse or keyboard event
        if (inputEventArgs is MouseEventArgs mea || inputEventArgs is KeyboardEventArgs) {
            if (inputEventArgs is MouseEventArgs mouseEventArgs) {
                // Check if no button is pressed and optionally check for mouse movement
                if (IsMouseIdle(mouseEventArgs)) {
                    return;
                }
            }

            // Reset and restart inactivity timer
            ResetInactivityTimer();
            OnActive?.Invoke(sender, e);
        }
    }

    private bool IsMouseIdle(MouseEventArgs mouseEventArgs) {
        return mouseEventArgs.LeftButton == MouseButtonState.Released &&
               mouseEventArgs.RightButton == MouseButtonState.Released &&
               mouseEventArgs.MiddleButton == MouseButtonState.Released &&
               mouseEventArgs.XButton1 == MouseButtonState.Released &&
               mouseEventArgs.XButton2 == MouseButtonState.Released &&
               (!_monitorMousePosition || _lastKnownMousePosition == mouseEventArgs.GetPosition(Application.Current.MainWindow));
    }

    private void ResetInactivityTimer() {
        _inactivityTimer.Stop();
        _inactivityTimer.Start();
    }

    private void OnInactivity(object sender, EventArgs e) {
        _lastKnownMousePosition = Mouse.GetPosition(Application.Current.MainWindow);
        _inactivityTimer.Stop();
        OnInactive?.Invoke(sender, e);
    }

    private void OnTimePassedHandler(object sender, EventArgs e) {
        OnTimePassed?.Invoke(sender, e);
    }
}
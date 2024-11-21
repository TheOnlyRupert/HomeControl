using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;

namespace HomeControl.Source.Control;

public class Screensaver {
    private const int MaxHorizontalSpeed = 2;
    private const int MaxStartingSpeed = 10;
    private const int MinHorizontalSpeed = 1;
    private const int MinRadius = 54;
    private const int MaxRadius = 280;
    private const int MinStartingSpeed = 2;
    private const double VerticalSpeedRatio = 0.1;
    private const double HorizontalSpeedRatio = 0.08;

    private readonly Canvas _canvas;
    private readonly ushort _coverage;
    private readonly List<string> _flakeImages;
    private readonly List<SnowInfo> _flakes = new();
    private bool _isWorking;
    private int _maxFlakes;

    public Screensaver(Canvas canvas) {
        _coverage = 25;
        _canvas = canvas;
        _canvas.SizeChanged += Canvas_SizeChanged;
        _flakeImages = GetFlakeImagesForCurrentMonth();
    }

    private List<string> GetFlakeImagesForCurrentMonth() {
        List<string>? images = new();

        // Add seasonal images based on the current month
        switch (DateTime.Now.Month) {
        case 12:
        case 1:
        case 2:
            images.AddRange(GetWinterFlakeImages());
            break;
        case 3:
        case 4:
        case 5:
            images.Add("pack://application:,,,/Resources/Images/monthly/rain/rain1.png");
            break;
        case 6:
        case 7:
        case 8:
            images.AddRange(GetSummerSunImages());
            break;
        default:
            images.AddRange(GetFallLeafImages());
            break;
        }

        // Add holiday-specific images
        List<string>? holidayImages = GetHolidayImages();
        images.AddRange(holidayImages);

        return images;
    }

    private List<string> GetWinterFlakeImages() {
        return new List<string> {
            "pack://application:,,,/Resources/Images/monthly/snowflakes/snow1.png",
            "pack://application:,,,/Resources/Images/monthly/snowflakes/snow2.png"
            // Add the rest of the snowflakes...
        };
    }

    private List<string> GetSummerSunImages() {
        return new List<string> {
            "pack://application:,,,/Resources/Images/monthly/sun/sun1.png",
            "pack://application:,,,/Resources/Images/monthly/sun/sun2.png"
            // Add the rest of the sun images...
        };
    }

    private List<string> GetFallLeafImages() {
        return new List<string> {
            "pack://application:,,,/Resources/Images/monthly/leaves/leaf1.png",
            "pack://application:,,,/Resources/Images/monthly/leaves/leaf2.png"
            // Add the rest of the leaf images...
        };
    }

    private List<string> GetHolidayImages() {
        List<string>? images = new();
        List<Holidays.HolidayBlock>? holidays = Holidays.GetHolidays(DateTime.Now.Year);
        foreach (Holidays.HolidayBlock? holiday in holidays) {
            if (DateTime.Now.Month == holiday.Date.Month && DateTime.Now.Day == holiday.Date.Day) {
                images.AddRange(GetImagesForHoliday(holiday.Holiday));
            }
        }

        return images;
    }

    private List<string> GetImagesForHoliday(string holiday) {
        switch (holiday) {
        case "9/11":
            return Get911Images();
        case "Veterans":
            return GetVeteransDayImages();
        case "Thanksgiving":
            return GetThanksgivingImages();
        case "Christmas":
        case "Christmas Eve":
            return GetChristmasImages();
        default:
            return new List<string>();
        }
    }

    private List<string> Get911Images() {
        return new List<string> {
            "pack://application:,,,/Resources/Images/monthly/911/1.png"
            // Add the rest of the 9/11 images...
        };
    }

    private List<string> GetVeteransDayImages() {
        return new List<string> {
            "pack://application:,,,/Resources/Images/monthly/gun/gun1.png"
            // Add the rest of the Veterans Day images...
        };
    }

    private List<string> GetThanksgivingImages() {
        return new List<string> {
            "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving1.png"
            // Add the rest of the Thanksgiving images...
        };
    }

    private List<string> GetChristmasImages() {
        return new List<string> {
            "pack://application:,,,/Resources/Images/monthly/christmas/christmas1.png"
            // Add the rest of the Christmas images...
        };
    }

    private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e) {
        RecalcMaxFlakes();
        SetFlakes(true);
    }

    public void Start() {
        _isWorking = true;
        RecalcMaxFlakes();
        SetFlakes(true);
        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    public void Stop() {
        _isWorking = false;
        CompositionTarget.Rendering -= CompositionTarget_Rendering;
        ClearSnow();
    }

    private void RecalcMaxFlakes() {
        double flakesInCanvas = _canvas.ActualHeight * _canvas.ActualWidth / (MaxRadius * MaxRadius);
        _maxFlakes = (int)(flakesInCanvas * _coverage / 100);
    }

    private static BitmapImage CreateImage(string path) {
        try {
            if (!path.StartsWith("pack://") && !File.Exists(path)) {
                return null;
            }

            BitmapImage? imgTemp = new();
            imgTemp.BeginInit();
            imgTemp.CacheOption = BitmapCacheOption.OnLoad;
            imgTemp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            imgTemp.UriSource = new Uri(path);
            imgTemp.EndInit();
            imgTemp.Freeze();
            return imgTemp;
        } catch (Exception e) {
            LogError("SnowEngine", e);
            return null;
        }
    }

    private static void LogError(string module, Exception e) {
        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "WARN",
            Module = module,
            Description = e.ToString()
        });
    }

    private void SetFlakes(bool top = false) {
        Random? rand = new();

        for (int i = _flakes.Count; i < _maxFlakes; i++) {
            Image? flake = new() {
                Source = CreateImage(_flakeImages[rand.Next(0, _flakeImages.Count)]),
                Stretch = Stretch.Uniform
            };

            SnowInfo? info = new(flake, VerticalSpeedRatio * rand.Next(MinStartingSpeed, MaxStartingSpeed), rand.Next(MinRadius, MaxRadius));

            // Ensure the flake spawns anywhere across the entire canvas width
            Canvas.SetLeft(flake, rand.NextDouble() * _canvas.ActualWidth); // Use NextDouble() for float precision
            Canvas.SetTop(flake, top ? -info.Radius * 2 : rand.Next(0, (int)_canvas.ActualHeight)); // Random Y position, or start from top if "top" is true

            _canvas.Children.Add(flake);
            info.VelocityX = rand.Next(MinHorizontalSpeed, MaxHorizontalSpeed);
            _flakes.Add(info);
        }
    }

    private void ClearSnow() {
        for (int i = _flakes.Count - 1; i >= 0; i--) {
            _canvas.Children.Remove(_flakes[i].Flake);
            _flakes[i].Flake = null;
            _flakes.RemoveAt(i);
        }
    }

    private void CompositionTarget_Rendering(object sender, EventArgs e) {
        if (!_isWorking) return;

        if (_flakes.Count < _maxFlakes) {
            SetFlakes(true);
            return;
        }

        for (int i = _flakes.Count - 1; i >= 0; i--) {
            SnowInfo? info = _flakes[i];
            double left = Canvas.GetLeft(info.Flake);
            double top = Canvas.GetTop(info.Flake);

            info.VelocityX += 0.5 * HorizontalSpeedRatio;
            Canvas.SetLeft(info.Flake, left + Math.Cos(info.VelocityX));
            Canvas.SetTop(info.Flake, top + 1 * info.VelocityY);

            if (top >= _canvas.ActualHeight + info.Radius * 2) {
                _flakes.RemoveAt(i);
                _canvas.Children.Remove(info.Flake);
            }
        }
    }
}

public class SnowInfo {
    public SnowInfo(Image flake, double velocityY, int radius) {
        Flake = flake;
        VelocityY = velocityY;
        Radius = radius;
        VelocityX = 0;
    }

    public Image Flake { get; set; }
    public double VelocityX { get; set; }
    public double VelocityY { get; set; }
    public int Radius { get; set; }
}
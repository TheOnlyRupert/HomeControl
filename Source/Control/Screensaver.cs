using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;

namespace HomeControl.Source.Control;

public class Screensaver {
    private const int maxHorizontalSpeed = 3;
    private const int maxStartingSpeed = 10;
    private const int minHorizontalSpeed = 1;
    private const int minRadius = 30;
    private const int maxRadius = 60;
    private const int minStartingSpeed = 3;
    private const double verticalSpeedRatio = 0.1;
    private const double horizontalSpeedRatio = 0.08;
    private readonly Canvas canvas;
    private readonly ushort coverage;
    private readonly List<string> flakeImages;
    private readonly List<SnowInfo> flakes = new();
    private bool isWorking;
    private int maxFlakes;

    public Screensaver(Canvas canvas) {
        coverage = 25;
        this.canvas = canvas;
        canvas.SizeChanged += canvas_SizeChanged;

        switch (DateTime.Now.Month) {
        case 12:
        case 1:
        case 2:
            flakeImages = new List<string> {
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow1.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow2.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow3.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow4.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow5.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow6.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow7.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow8.png",
                "pack://application:,,,/Resources/Images/monthly/snowflakes/snow9.png"
            };

            break;
        case 3:
        case 4:
        case 5:
            flakeImages = new List<string> {
                "pack://application:,,,/Resources/Images/monthly/rain/rain1.png"
            };

            break;
        case 6:
        case 7:
        case 8:
            flakeImages = new List<string> {
                "pack://application:,,,/Resources/Images/monthly/sun/sun1.png",
                "pack://application:,,,/Resources/Images/monthly/sun/sun2.png",
                "pack://application:,,,/Resources/Images/monthly/sun/sun3.png",
                "pack://application:,,,/Resources/Images/monthly/sun/sun4.png",
                "pack://application:,,,/Resources/Images/monthly/sun/sun5.png",
                "pack://application:,,,/Resources/Images/monthly/sun/sun6.png",
                "pack://application:,,,/Resources/Images/monthly/sun/sun7.png",
                "pack://application:,,,/Resources/Images/monthly/sun/sun8.png"
            };

            break;
        default:
            flakeImages = new List<string> {
                "pack://application:,,,/Resources/Images/monthly/leaves/leaf1.png",
                "pack://application:,,,/Resources/Images/monthly/leaves/leaf2.png",
                "pack://application:,,,/Resources/Images/monthly/leaves/leaf3.png",
                "pack://application:,,,/Resources/Images/monthly/leaves/leaf4.png",
                "pack://application:,,,/Resources/Images/monthly/leaves/leaf5.png",
                "pack://application:,,,/Resources/Images/monthly/leaves/leaf6.png"
            };

            break;
        }

        foreach (string text in from holiday in Holidays.GetHolidays(DateTime.Now.Year)
                 where DateTime.Now.Month == holiday.Date.Month && DateTime.Now.Day == holiday.Date.Day
                 select holiday.Holiday) {
            switch (text) {
            //case "New Year's":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly/new_year/new_year1.png"
            //    };

            //    break;
            //case "Valentine's":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            //case "President's":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            //case "Easter":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            //case "Mother's":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            //case "Memorial":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            //case "Father's":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            //case "Independence":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            //case "Labor":
            //    flakeImages = new List<string> {
            //        "pack://application:,,,/Resources/Images/monthly//.png"
            //    };

            //    break;
            case "Veterans":
                flakeImages = new List<string> {
                    "pack://application:,,,/Resources/Images/monthly/gun/gun1.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun2.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun3.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun4.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun5.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun6.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun7.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun8.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun9.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun10.png",
                    "pack://application:,,,/Resources/Images/monthly/gun/gun11.png"
                };

                //case "Halloween":
                //    flakeImages = new List<string> {
                //        "pack://application:,,,/Resources/Images/monthly/halloween/halloween1.png"
                //    };

                break;
            case "Thanksgiving":
                flakeImages = new List<string> {
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving1.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving2.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving3.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving4.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving5.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving6.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving7.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving8.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving9.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving10.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving11.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving12.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving13.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving14.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving15.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving16.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving17.png",
                    "pack://application:,,,/Resources/Images/monthly/thanksgiving/thanksgiving18.png"
                };

                break;
            case "Christmas":
            case "Christmas Eve":
                flakeImages = new List<string> {
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas1.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas2.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas3.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas4.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas5.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas6.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas7.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas8.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas9.png",
                    "pack://application:,,,/Resources/Images/monthly/christmas/christmas10.png"
                };

                break;
            }
        }
    }

    private void canvas_SizeChanged(object sender, SizeChangedEventArgs e) {
        RecalcMaxFlakes();
        SetFlakes(true);
    }

    public void Start() {
        isWorking = true;
        RecalcMaxFlakes();
        SetFlakes(true);
        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    public void Stop() {
        isWorking = false;
        CompositionTarget.Rendering -= CompositionTarget_Rendering;
        ClearSnow();
    }

    private void RecalcMaxFlakes() {
        //Approximate maximum flakes in canvas
        double flakesInCanvas = canvas.ActualHeight * canvas.ActualWidth / (maxRadius * maxRadius);

        maxFlakes = (int)(flakesInCanvas * coverage / 100);
    }

    private static BitmapImage CreateImage(string path) {
        BitmapImage imgTemp = new();
        try {
            if (!path.StartsWith("pack://") && !File.Exists(path)) {
                return null;
            }
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "SnowEngine",
                Description = e.ToString()
            });
        }

        imgTemp.BeginInit();
        imgTemp.CacheOption = BitmapCacheOption.OnLoad;
        imgTemp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        imgTemp.UriSource = new Uri(path);
        imgTemp.EndInit();
        if (imgTemp.CanFreeze) {
            imgTemp.Freeze();
        }

        return imgTemp;
    }

    private void SetFlakes(bool top = false) {
        int halfCanvasWidth = (int)canvas.ActualWidth / 2;
        Random rand = new();

        for (int i = flakes.Count; i < maxFlakes; i++) {
            Image flake = new() {
                Source = CreateImage(flakeImages[rand.Next(0, flakeImages.Count)]),
                Stretch = Stretch.Uniform
            };

            SnowInfo info = new(flake, verticalSpeedRatio * rand.Next(minStartingSpeed, maxStartingSpeed), rand.Next(minRadius, maxRadius));


            // Placing image  
            Canvas.SetLeft(flake, halfCanvasWidth + rand.Next(-halfCanvasWidth, halfCanvasWidth));
            if (!top) {
                Canvas.SetTop(flake, rand.Next(0, (int)canvas.ActualHeight));
            } else {
                Canvas.SetTop(flake, -info.Radius * 2);
            }

            canvas.Children.Add(flake);

            info.VelocityX = rand.Next(minHorizontalSpeed, maxHorizontalSpeed);
            flakes.Add(info);
        }
    }

    private void ClearSnow() {
        for (int i = flakes.Count - 1; i >= 0; i--) {
            canvas.Children.Remove(flakes[i].Flake);
            flakes[i].Flake = null;
            flakes.RemoveAt(i);
        }
    }

    private void CompositionTarget_Rendering(object sender, EventArgs e) {
        if (!isWorking) {
            return;
        }

        //Add missing flakes
        if (flakes.Count < maxFlakes) {
            SetFlakes(true);
            return;
        }

        //Setting position of all flakes
        for (int i = flakes.Count - 1; i >= 0; i--) {
            SnowInfo info = flakes[i];
            double left = Canvas.GetLeft(info.Flake);
            double top = Canvas.GetTop(info.Flake);

            //.5 is magic number. Don't use magic numbers! :)
            flakes[i].VelocityX += .5 * horizontalSpeedRatio;

            Canvas.SetLeft(flakes[i].Flake, left + Math.Cos(flakes[i].VelocityX));
            Canvas.SetTop(info.Flake, top + 1 * info.VelocityY);

            //Remove image from canvas when it leaves canvas
            if (top >= canvas.ActualHeight + info.Radius * 2) {
                flakes.Remove(info);
                canvas.Children.Remove(info.Flake);
            }
        }
    }
}

internal class SnowInfo {
    public SnowInfo(Image flake, double velocityY, int radius) {
        VelocityY = velocityY;
        Flake = flake;
        flake.Width = radius;
        Radius = radius;
    }

    public Image Flake { get; set; }

    public double VelocityY { get; set; }

    public double VelocityX { get; set; }

    public int Radius { get; set; }
}
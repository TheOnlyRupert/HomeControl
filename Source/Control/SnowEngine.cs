using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using Image = System.Windows.Controls.Image;

namespace HomeControl.Source.Control;

public class SnowEngine {
    //Canvas to draw flakes in
    private readonly Canvas canvas;
    private readonly List<string> flakeImages = new();

    private readonly List<SnowInfo> flakes = new();
    private readonly int maxHorizontalSpeed = 3;

    private readonly int maxStartingSpeed = 10;
    private readonly int minHorizontalSpeed = 1;

    private readonly int minRadius = 5;

    private readonly int minStartingSpeed = 3;

    //Maximum flakes. It's calculating at the beginning and after canvas resize and depends on SnowCoverage property
    private int maxFlakes;

    public SnowEngine(Canvas canvas, ushort snowCoverage, params string[] flakeImages) {
        SnowCoverage = snowCoverage;
        this.canvas = canvas;
        canvas.IsHitTestVisible = false;
        canvas.SizeChanged += canvas_SizeChanged;
        this.flakeImages.AddRange(flakeImages);
    }

    public int MinRadius {
        get => minRadius;
        set => MaxRadius = value;
    }

    private int MaxRadius { get; set; } = 30;

    private ushort SnowCoverage { get; }

    private double VerticalSpeedRatio { get; } = 0.1;

    private double HorizontalSpeedRatio { get; } = 0.08;

    private bool IsWorking { get; set; }

    private void canvas_SizeChanged(object sender, SizeChangedEventArgs e) {
        RecalcMaxFlakes();
        SetFlakes(true);
    }

    public void Start() {
        IsWorking = true;
        RecalcMaxFlakes();
        SetFlakes(true);
        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    public void Stop() {
        IsWorking = false;
        CompositionTarget.Rendering -= CompositionTarget_Rendering;
        ClearSnow();
    }

    private void RecalcMaxFlakes() {
        //Approximate maximum flakes in canvas
        double flakesInCanvas = canvas.ActualHeight * canvas.ActualWidth / (MaxRadius * MaxRadius);

        maxFlakes = (int)(flakesInCanvas * SnowCoverage / 100);
    }

    private static BitmapImage CreateImage(string path) {
        BitmapImage imgTemp = new();
        try {
            if (!path.StartsWith("pack://") && !File.Exists(path)) {
                return null;
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
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

            SnowInfo info = new(flake, VerticalSpeedRatio * rand.Next(minStartingSpeed, maxStartingSpeed), rand.Next(minRadius, MaxRadius));


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
        if (!IsWorking) {
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
            flakes[i].VelocityX += .5 * HorizontalSpeedRatio;

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
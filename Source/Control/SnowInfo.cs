using System.Windows.Controls;

namespace HomeControl.Source.Control;

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
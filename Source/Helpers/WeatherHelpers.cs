using System.Linq;
using System.Text.RegularExpressions;

namespace HomeControl.Source.Helpers;

public static class WeatherHelpers {
    public static string GetWeatherIcon(string weather, bool isDayTime) {
        switch (weather) {
        case "Sunny":
        case "Mostly Sunny":
        case "Clear":
        case "Mostly Clear":
            return isDayTime
                ? "../../../Resources/Images/weather/weather_clear.png"
                : "../../../Resources/Images/weather/weather_clear_night.png";
        case "Partly Cloudy":
        case "Partly Sunny":
            return isDayTime
                ? "../../../Resources/Images/weather/weather_part_cloudy.png"
                : "../../../Resources/Images/weather/weather_cloudy_night.png";
        case "Cloudy":
        case "Mostly Cloudy":
            return "../../../Resources/Images/weather/weather_cloudy.png";
        case "Patchy Fog":
        case "Areas Of Fog":
            return "../../../Resources/Images/weather/weather_fog.png";
        case "Slight Chance Very Light Rain":
        case "Slight Chance Light Rain":
        case "Chance Very Light Rain":
        case "Chance Light Rain":
        case "Areas Of Drizzle":
            return "../../../Resources/Images/weather/weather_rain_light.png";
        case "Rain Showers Likely":
        case "Rain Likely":
        case "Rain Showers":
        case "Chance Rain Showers":
        case "Slight Chance Rain Showers":
            return "../../../Resources/Images/weather/weather_rain_medium.png";
        case "Showers And Thunderstorms":
        case "Showers And Thunderstorms Likely":
        case "Chance Showers And Thunderstorms":
        case "Slight Chance Showers And Thunderstorms":
            return "../../../Resources/Images/weather/weather_storm.png";
        case "Widespread Frost":
            return "../../../Resources/Images/weather/weather_frost.png";
        case "Snow Showers":
            return "../../../Resources/Images/weather/weather_snow_heavy.png";
        case "Isolated Snow Showers":
        case "Scattered Snow Showers":
        case "Chance Snow Showers":
        case "Slight Chance Snow Showers":
            return "../../../Resources/Images/weather/weather_snow_light.png";
        case "Slight Chance Rain And Snow Showers":
        case "Slight Chance Rain And Snow":
        case "Chance Rain And Snow Showers":
        case "Rain And Snow Showers":
            return "../../../Resources/Images/weather/weather_snow_rain_mixed.png";
        default:
            return "null";
        }
    }

    public static string GetRainChance(string icon) {
        Regex rg = new(@"[,]\d+");
        string output = rg.Matches(icon).Cast<Match>().Aggregate("", (current, match) => current + match.Value.Substring(1) + "% - ");

        return string.IsNullOrEmpty(output) ? "0%" : output.Substring(0, output.Length - 2);
    }

    public static int GetWindRotation(string direction) {
        switch (direction) {
        case "N":
            return 0;
        case "S":
            return 180;
        case "E":
            return 90;
        case "W":
            return 270;
        case "NE":
            return 45;
        case "SE":
            return 135;
        case "SW":
            return 225;
        case "NW":
            return 315;
        case "SSE":
            return 158;
        case "SSW":
            return 202;
        case "ENE":
            return 68;
        case "ESE":
            return 112;
        case "NNW":
            return 338;
        case "NNE":
            return 22;
        case "WSW":
            return 248;
        case "WNW":
            return 292;
        default:
            return 0;
        }
    }

    public static string GetRainIcon(string input) {
        switch (input) {
        case "Snow Showers":
        case "Chance Snow Showers":
        case "Chance Rain And Snow Showers":
        case "Scattered Snow Showers":
        case "Isolated Snow Showers":
        case "Slight Chance Snow Showers":
            return "../../../Resources/Images/weather/weather_snow_heavy.png";
        default:
            return "../../../Resources/Images/weather/rain_drop.png";
        }
    }
}
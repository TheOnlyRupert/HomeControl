using HomeControl.Source.Json;

namespace HomeControl.Source.Helpers;

public static class WeatherHelpers {
    public static string? GetWeatherIcon(string? weather, bool isDayTime, int temp, string? windSpeed) {
        switch (weather) {
        case "Sunny":
        case "Mostly Sunny":
            return temp > 89
                ? "../../../Resources/Images/weather/sun-hot.gif"
                : "../../../Resources/Images/weather/clear-day.gif";

        case "Mostly Clear":
            return "../../../Resources/Images/weather/clear-night.gif";

        case "Clear":
            return "../../../Resources/Images/weather/starry-night.gif";

        case "Partly Cloudy":
        case "Partly Sunny":
            return isDayTime
                ? "../../../Resources/Images/weather/partly-cloudy-day.gif"
                : "../../../Resources/Images/weather/partly-cloudy-night.gif";

        case "Cloudy":
        case "Mostly Cloudy":
            return "../../../Resources/Images/weather/cloudy.gif";

        case "Patchy Fog":
        case "Areas Of Fog":
            return isDayTime
                ? "../../../Resources/Images/weather/fog-day.gif"
                : "../../../Resources/Images/weather/fog-night.gif";

        case "Slight Chance Very Light Rain":
        case "Slight Chance Light Rain":
        case "Chance Very Light Rain":
        case "Chance Light Rain":
        case "Light Rain Likely":
        case "Light Rain":
        case "Areas Of Drizzle":
        case "Patchy Drizzle":
        case "Chance Drizzle":
        case "Slight Chance Drizzle":
            return "../../../Resources/Images/weather/drizzle.gif";

        case "Rain Showers Likely":
        case "Rain Likely":
        case "Rain":
        case "Rain Showers":
        case "Chance Rain":
        case "Chance Rain Showers":
        case "Slight Chance Rain Showers":
        case "Isolated Rain Showers":
            return "../../../Resources/Images/weather/rain.gif";

        case "Showers And Thunderstorms":
        case "Showers And Thunderstorms Likely":
        case "Chance Showers And Thunderstorms":
        case "Slight Chance Showers And Thunderstorms":
        case "Slight Chance T-storms":
            if (temp > 35) {
                return isDayTime
                    ? "../../../Resources/Images/weather/thunderstorms-day-extreme-rain.gif"
                    : "../../../Resources/Images/weather/thunderstorms-night-extreme-rain.gif";
            }

            return isDayTime
                ? "../../../Resources/Images/weather/thunderstorms-day-extreme-snow.gif"
                : "../../../Resources/Images/weather/thunderstorms-night-extreme-snow.gif";

        case "Patchy Frost":
        case "Areas Of Frost":
        case "Widespread Frost":
            return "../../../Resources/Images/weather/mist.gif";

        case "Snow Showers":
        case "Chance Snow Showers":
        case "Snow Showers Likely":
        case "Isolated Snow Showers":
        case "Scattered Snow Showers":
        case "Light Snow":
        case "Light Snow Likely":
        case "Chance Light Snow":
        case "Slight Chance Snow Showers":
        case "Slight Chance Light Snow":
            try {
                int wind = int.Parse(windSpeed);
                if (wind > 19) {
                    return "../../../Resources/Images/weather/wind-snow.gif";
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "WeatherHelpers",
                    Description = e.ToString()
                });
            }

            return "../../../Resources/Images/weather/snow.gif";

        case "Slight Chance Rain And Snow Showers":
        case "Slight Chance Rain And Snow":
        case "Chance Rain And Snow":
        case "Chance Rain And Snow Showers":
        case "Chance Freezing Rain":
        case "Freezing Rain Likely":
        case "Rain And Snow Showers":
        case "Rain And Snow Showers Likely":
            return "../../../Resources/Images/weather/sleet.gif";

        case "Haze":
            return isDayTime
                ? "../../../Resources/Images/weather/haze-day.gif"
                : "../../../Resources/Images/weather/haze-night.gif";

        case "Smoke":
        case "PatchySmoke":
            return "../../../Resources/Images/weather/smoke.gif";

        default:
            return "";
        }
    }

    public static int GetWindRotation(string direction) {
        return direction switch {
            "N" => 0,
            "S" => 180,
            "E" => 90,
            "W" => 270,
            "NE" => 45,
            "SE" => 135,
            "SW" => 225,
            "NW" => 315,
            "SSE" => 158,
            "SSW" => 202,
            "ENE" => 68,
            "ESE" => 112,
            "NNW" => 338,
            "NNE" => 22,
            "WSW" => 248,
            "WNW" => 292,
            _ => 0
        };
    }

    public static string GetRainIcon(string? input) {
        switch (input) {
        case "Snow Showers":
        case "Chance Snow Showers":
        case "Scattered Snow Showers":
        case "Isolated Snow Showers":
        case "Slight Chance Snow Showers":
        case "Slight Chance Light Snow":
        case "Chance Light Snow":
        case "Snow Showers Likely":
            return "../../../Resources/Images/weather/snowflake.gif";
        default:
            return "../../../Resources/Images/weather/raindrop.gif";
        }
    }

    public static string?[] RegexWeatherForecast(string? input) {
        return input.Split(new[] {
            " then "
        }, StringSplitOptions.None);
    }
}
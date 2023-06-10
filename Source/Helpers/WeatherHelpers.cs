using System;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;

namespace HomeControl.Source.Helpers;

public static class WeatherHelpers {
    public static string GetWeatherIcon(string weather, bool isDayTime, int temp, string windSpeed, string firstText) {
        switch (firstText) {
        case "Showers And Thunderstorms":
        case "Showers And Thunderstorms Likely":
        case "Rain Likely":
        case "Rain":
        case "Rain Showers":
            switch (weather) {
            case "Sunny":
            case "Mostly Sunny":
                return "../../../Resources/Images/weather/rainbow-clear.gif";

            case "Partly Cloudy":
            case "Partly Sunny":
                return "../../../Resources/Images/weather/rainbow.gif";
            }

            break;
        }

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
            return "../../../Resources/Images/weather/rain.gif";

        case "Showers And Thunderstorms":
        case "Showers And Thunderstorms Likely":
        case "Chance Showers And Thunderstorms":
        case "Slight Chance Showers And Thunderstorms":
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
            return "../../../Resources/Images/weather/frost.gif";

        case "Snow Showers":
        case "Chance Snow Showers":
        case "Snow Showers Likely":
        case "Isolated Snow Showers":
        case "Scattered Snow Showers":
        case "Light Snow":
        case "Chance Light Snow":
        case "Slight Chance Snow Showers":
        case "Slight Chance Light Snow":
            try {
                int wind = int.Parse(windSpeed);
                if (wind > 19) {
                    return "../../../Resources/Images/weather/wind-snow.gif";
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "WeatherHelpers",
                    Description = e.ToString()
                });
            }

            return "../../../Resources/Images/weather/snow.gif";

        case "Slight Chance Rain And Snow Showers":
        case "Slight Chance Rain And Snow":
        case "Chance Rain And Snow Showers":
        case "Chance Freezing Rain":
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
            return "../../../Resources/Images/weather/na.gif";
        }
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
}
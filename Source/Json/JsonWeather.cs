using System;
using System.Collections.Generic;

namespace HomeControl.Source.Json;

public class JsonWeather {
    public Properties properties { get; set; }

    public class Properties {
        public List<Periods> periods { get; set; }
    }

    public class Periods {
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public bool isDaytime { get; set; }
        public int temperature { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
        public RelativeHumidity relativeHumidity { get; set; }
        public ProbabilityOfPrecipitation probabilityOfPrecipitation { get; set; }
    }

    public class RelativeHumidity {
        public int value { get; set; }
    }

    public class ProbabilityOfPrecipitation {
        public object value { get; set; }
    }
}

public class ApiStatus {
    public string status { get; set; }
}

public class WeatherHourlyBlock {
    public string Time { get; set; }
    public string WeatherIcon { get; set; }
    public string Temp { get; set; }
    public string RainIcon { get; set; }
    public string RainChance { get; set; }
    public string WindSpeed { get; set; }
    public int WindDirectionIcon { get; set; }
    public string Humidity { get; set; }
    public string ShortForecast { get; set; }
}

public class JsonWeatherLocation {
    public Properties properties { get; set; }

    public class Properties {
        public int gridX { get; set; }
        public int gridY { get; set; }
        public string gridId { get; set; }
        public RelativeLocation relativeLocation { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }

    public class RelativeLocation {
        public Properties properties { get; set; }
    }
}
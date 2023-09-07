using System;
using System.Collections.Generic;

namespace HomeControl.Source.Json;

public class JsonWeather {
    public Properties properties { get; set; }

    public class Properties {
        public DateTime updated { get; set; }
        public List<Periods> periods { get; set; }
    }

    public class Periods {
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
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
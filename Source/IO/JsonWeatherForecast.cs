using System;
using System.Collections.Generic;

namespace HomeControl.Source.IO;

public class JsonWeatherForecast {
    public Properties properties { get; set; }

    public class Properties {
        public DateTime updated { get; set; }
        public List<Periods> periods { get; set; }
    }

    public class Periods {
        public int number { get; set; }
        public string name { get; set; }
        public bool isDaytime { get; set; }
        public int temperature { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
    }
}

public class ApiStatus {
    public string status { get; set; }
}
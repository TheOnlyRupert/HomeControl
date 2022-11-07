using System;
using System.Collections.Generic;

namespace HomeControl.Source.Helpers; 

public class JsonWeatherForecastHourly {
    public Properties properties { get; set; }

    public class Properties {
        public DateTime updated { get; set; }
        public List<Periods> periods { get; set; }
    }

    public class Periods {
        public int number { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool isDaytime { get; set; }
        public int temperature { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string icon { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
    }
}
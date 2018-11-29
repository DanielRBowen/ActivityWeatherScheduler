using System;

namespace ActivityWeatherSchedulerLibraryNETStandard.Models
{
    public class Activity
    {
        public string Summary { get; set; }

        public DateTimeOffset Time { get; set; }

        public decimal TemperatureF { get; set; }

        public bool Above { get; set; }
    }
}

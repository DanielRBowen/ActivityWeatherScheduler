using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityWeatherSchedulerLibraryNETStandard.Models
{
    public class CalendarEvent
    {
        public string Summary { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }
    }
}

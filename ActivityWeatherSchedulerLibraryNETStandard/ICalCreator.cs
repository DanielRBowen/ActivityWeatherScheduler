using ActivityWeatherSchedulerLibraryNETStandard.Models;
using System;
using System.IO;
using System.Text;

namespace ActivityWeatherSchedulerLibraryNETStandard
{
	public class ICalCreator
	{
		public static CalendarEvent CreateCalendarEventForActivity(Activity activity)
		{
			var hourAfterStart = activity.Time + new TimeSpan(1, 0, 0);
			var aboveOrBelow = activity.Above ? "above" : "below";

			var calendarEvent = new CalendarEvent
			{
				Summary = activity.Summary,
				Description = $"Go {activity.Summary} when the temperature is {aboveOrBelow} {activity.TemperatureF} degrees fahrenheit",
				Start = activity.Time,
				End = hourAfterStart
			};

			return calendarEvent;
		}

		public static void SaveCalendarLocally(string filePath, CalendarEvent calendarEvent)
		{
			File.WriteAllText(filePath, MakeCalendarString(calendarEvent));
		}

		public static string MakeCalendarString(CalendarEvent calendarEvent)
		{
			var iCalStringBuilder = new StringBuilder();
			iCalStringBuilder.AppendLine("BEGIN:VCALENDAR");
			iCalStringBuilder.AppendLine("PRODID:Daniel Richard Bowen");
			iCalStringBuilder.AppendLine("VERSION:2.0");
			iCalStringBuilder.AppendLine("BEGIN:VEVENT");
			iCalStringBuilder.AppendLine($"DESCRIPTION:{calendarEvent.Description}");
			iCalStringBuilder.AppendLine($"DTEND:{calendarEvent.End.LocalDateTime.ToString("yyyyMMddTHHmmss")}");
			iCalStringBuilder.AppendLine($"DTSTAMP:{DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ")}");
			iCalStringBuilder.AppendLine($"DTSTART:{calendarEvent.Start.LocalDateTime.ToString("yyyyMMddTHHmmss")}");
			iCalStringBuilder.AppendLine("SEQUENCE:0");
			iCalStringBuilder.AppendLine($"SUMMARY:{calendarEvent.Summary}");
			iCalStringBuilder.AppendLine($"UID:{new Guid().ToString()}");
			iCalStringBuilder.AppendLine("END:VEVENT");
			iCalStringBuilder.AppendLine("END:VCALENDAR");
			return iCalStringBuilder.ToString();
		}

		public static string MakeCalendarString(Activity activity)
		{
			var calendarEvent = CreateCalendarEventForActivity(activity);
			return MakeCalendarString(calendarEvent);
		}

		public static string FileNameIcalExtensionAppend(string fileName)
		{
			return $"{fileName}.ics";
		}
	}
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ActivityWeatherSchedulerLibraryNETStandard.Models
{
	public class Activity
	{
		[Key]
		public int Id { get; set; }

		[StringLength(254)]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }

		[StringLength(5)]
		public string ZipCode { get; set; }

		[StringLength(254)]
		public string Summary { get; set; }

		[DataType(DataType.DateTime)]
		public DateTimeOffset Time { get; set; }

		public decimal TemperatureF { get; set; }

		public bool Above { get; set; }

		public string CalendarString { get; set; }
	}
}

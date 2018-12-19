using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActivityWeatherSchedulerLibraryNETStandard.Models
{
	public class CachedWeather
	{
		[Key]
		public int Id { get; set; }

		[StringLength(5)]
		public string ZipCode { get; set; }

		public DateTimeOffset DateCached { get; set; }

		public List<WeatherForecast> WeatherForecasts { get; set; }
	}
}

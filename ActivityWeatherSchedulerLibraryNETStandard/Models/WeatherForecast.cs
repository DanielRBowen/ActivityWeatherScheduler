using System;
using System.ComponentModel.DataAnnotations;

namespace ActivityWeatherSchedulerLibraryNETStandard.Models
{
	public class WeatherForecast
	{
		[Key]
		public int Id { get; set; }

		public DateTimeOffset Time { get; set; }

		public decimal TemperatureK { get; set; }

		public decimal TemperatureF => (TemperatureK - (decimal)273.15) * (decimal)1.8 + 32;

		public decimal TemperatureC => TemperatureK - (decimal)273.15;

		public string Summary
		{
			get
			{
				if (TemperatureF >= 105)
				{
					return "Scorching";
				}

				if (TemperatureF < 105 && TemperatureF >= 95)
				{
					return "Sweltering";
				}

				if (TemperatureF < 95 && TemperatureF >= 84)
				{
					return "Hot";
				}

				if (TemperatureF < 84 && TemperatureF >= 77)
				{
					return "Balmy";
				}

				if (TemperatureF < 77 && TemperatureF >= 69)
				{
					return "Warm";
				}

				if (TemperatureF < 69 && TemperatureF >= 63)
				{
					return "Mild";
				}

				if (TemperatureF < 63 && TemperatureF >= 50)
				{
					return "Cool";
				}

				if (TemperatureF < 50 && TemperatureF >= 43)
				{
					return "Chilly";
				}

				if (TemperatureF < 43 && TemperatureF >= 33)
				{
					return "Bracing";
				}

				return "Freezing";
			}
		}
	}
}

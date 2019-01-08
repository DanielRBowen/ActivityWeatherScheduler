// https://openweathermap.org/forecast5#JSON
// https://www.newtonsoft.com/json/help/html/QueryJsonLinq.htm

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using ActivityWeatherSchedulerLibraryNETStandard.Models;
using Newtonsoft.Json.Linq;

namespace ActivityWeatherSchedulerLibraryNETStandard
{
	public static class GetWeather
	{
		public static string GetWeatherForGeolocation(double latitude, double longitude, bool test = false)
		{
			var latitudeString = latitude.ToString();
			var longitudeString = longitude.ToString();

			using (var webClient = new WebClient())
			{
				webClient.BaseAddress = "https://api.openweathermap.org/data/2.5/";
				var content = webClient.DownloadString($"forecast?lat={latitudeString}&lon={longitudeString}&APPID={ConfigHelper.GetOpenWeatherMapKey(test)}");

				return content;
			}
		}

		public static string GetWeatherForZip(string zip, bool test = false)
		{
			if (!Regex.IsMatch(zip, @"^\d{5}(?:[-\s]\d{4})?$"))
			{
				throw new InvalidOperationException("Zip code is invalid");
			}

			using (var webClient = new WebClient())
			{
				webClient.BaseAddress = "https://api.openweathermap.org/data/2.5/";
				var content = webClient.DownloadString($"forecast?zip={zip},us&APPID={ConfigHelper.GetOpenWeatherMapKey(test)}");

				return content;
			}
		}

		public static IEnumerable<WeatherForecast> GetWeatherForecasts(string jsonString)
		{
			var weatherJ = JObject.Parse(jsonString);

			var weatherForecasts =
				from weatherForecast in weatherJ["list"].Children()
				select new WeatherForecast
				{
					Time = DateTimeOffset.ParseExact(weatherForecast["dt_txt"].Value<string>(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToLocalTime(),
					TemperatureK = WeatherHelper.ToDecimal(weatherForecast["main"]["temp"].Value<string>())
				};

			return weatherForecasts;
		}

		public static IEnumerable<WeatherForecast> GetWeatherForecastsForGeolocation(double latitude, double longitude, bool test = false)
		{
			var weather = GetWeatherForGeolocation(latitude, longitude, test);
			return GetWeatherForecasts(weather);
		}

		public static IEnumerable<WeatherForecast> GetWeatherForecastsForZip(string zip, bool test = false)
		{
			var weather = GetWeatherForZip(zip, test);
			return GetWeatherForecasts(weather);
		}

		public static IList<WeatherForecast> FiveDayAround1500Forecast(IEnumerable<WeatherForecast> weatherForecasts)
		{
			var weatherForecasts1500 = weatherForecasts.Where(weatherForecast => weatherForecast.Time.Hour == 15);

			while (!weatherForecasts1500.Any())
			{
				weatherForecasts1500 = weatherForecasts.Where(weatherForecast => weatherForecast.Time.Hour == 14);

				if (weatherForecasts1500.Any())
				{
					break;
				}

				weatherForecasts1500 = weatherForecasts.Where(weatherForecast => weatherForecast.Time.Hour == 16);

				if (weatherForecasts1500.Any())
				{
					break;
				}

				weatherForecasts1500 = weatherForecasts.Where(weatherForecast => weatherForecast.Time.Hour == 13);

				if (weatherForecasts1500.Any())
				{
					break;
				}

				weatherForecasts1500 = weatherForecasts.Where(weatherForecast => weatherForecast.Time.Hour == 17);

				if (weatherForecasts1500.Any())
				{
					break;
				}

				weatherForecasts1500 = weatherForecasts.Where(weatherForecast => weatherForecast.Time.Hour == 12);

				if (weatherForecasts1500.Any())
				{
					break;
				}

				weatherForecasts1500 = weatherForecasts.Where(weatherForecast => weatherForecast.Time.Hour == 18);

				if (!weatherForecasts1500.Any())
				{
					throw new InvalidOperationException("There were no forcasts around 1500");
				}
				else
				{
					break;
				}
			}

			return weatherForecasts1500.ToList();
		}

		public static IList<WeatherForecast> FiveDayAround1500Forecast(string zip)
		{
			var weatherForecasts = GetWeatherForecastsForZip(zip);
			return FiveDayAround1500Forecast(weatherForecasts);
		}

		public static IList<WeatherForecast> FiveDayAround1500Forecast(double latitude, double longitude)
		{
			var weatherForecasts = GetWeatherForecastsForGeolocation(latitude, longitude);
			return FiveDayAround1500Forecast(weatherForecasts);
		}
	}
}


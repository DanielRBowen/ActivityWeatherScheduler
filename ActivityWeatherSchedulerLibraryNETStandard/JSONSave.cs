using System;
using System.Collections.Generic;
using System.IO;
using ActivityWeatherSchedulerLibraryNETStandard.Models;
using Newtonsoft.Json;

namespace ActivityWeatherSchedulerLibraryNETStandard
{
	public enum JSONFileType
	{
		Activities,
		Weather
	}

	public class JSONSave
	{
		public static void SaveActivities(string filePath, IEnumerable<Activity> activities)
		{
			var jsonSerializer = new JsonSerializer();

			using (var streamWriter = File.CreateText(filePath))
			{
				jsonSerializer.Serialize(streamWriter, activities);
			}
		}

		public static IList<Activity> LoadActivities(string filePath)
		{
			if (File.Exists(filePath))
			{
				var jsonSerializer = new JsonSerializer();

				using (var streamReader = File.OpenText(filePath))
				{
					var jsonReader = new JsonTextReader(streamReader);
					return jsonSerializer.Deserialize<IList<Activity>>(jsonReader);
				}
			}
			else
			{
				return null;
			}
		}

		public static void SaveWeather(string filePath, IEnumerable<WeatherForecast> weatherForecasts)
		{
			var jsonSerializer = new JsonSerializer();

			using (var streamWriter = File.CreateText(filePath))
			{
				jsonSerializer.Serialize(streamWriter, weatherForecasts);
			}
		}

		public static IList<WeatherForecast> LoadWeather(string filePath)
		{
			if (File.Exists(filePath))
			{
				var jsonSerializer = new JsonSerializer();

				using (var streamReader = File.OpenText(filePath))
				{
					var jsonReader = new JsonTextReader(streamReader);
					return jsonSerializer.Deserialize<IList<WeatherForecast>>(jsonReader);
				}
			}
			else
			{
				return null;
			}
		}

		public static string JSONAppDataFilePath(JSONFileType fileType)
		{
			var myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var directoryPath = myDocumentsPath + "\\ActivityWeatherScheduler";
			Directory.CreateDirectory(directoryPath);

			switch (fileType)
			{
				case JSONFileType.Activities:
					return directoryPath + "\\Activities.json";
				case JSONFileType.Weather:
					return directoryPath + "\\Weather.json";
				default:
					return string.Empty;
			}
		}

		public static void SaveActivitiesToAppData(IEnumerable<Activity> activities)
		{
			SaveActivities(JSONAppDataFilePath(JSONFileType.Activities), activities);
		}

		public static IList<Activity> LoadActivitiesFromAppData()
		{
			return LoadActivities(JSONAppDataFilePath(JSONFileType.Activities));
		}

		public static void SaveWeatherToAppData(IEnumerable<WeatherForecast> weatherForecasts)
		{
			SaveWeather(JSONAppDataFilePath(JSONFileType.Weather), weatherForecasts);
		}

		public static IList<WeatherForecast> LoadWeatherFromAppData(IEnumerable<WeatherForecast> weatherForecasts)
		{
			return LoadWeather(JSONAppDataFilePath(JSONFileType.Weather));
		}
	}
}

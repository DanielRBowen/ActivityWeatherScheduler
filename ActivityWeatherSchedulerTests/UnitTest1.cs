using System;
using System.Collections.Generic;
using ActivityWeatherSchedulerLibraryNETStandard;
using ActivityWeatherSchedulerLibraryNETStandard.Models;
using Xunit;

namespace ActivityWeatherSchedulerTests
{
	public class UnitTest1
	{
		/// <summary>
		/// Method Name: Function_Scenario_ExpectedBehavior()
		/// Assert.IsTrue(result);
		/// Assert.That(result, Is.True);
		/// Assert.That(result == true);
		/// </summary>
		[Fact]
		public void Test1()
		{

		}

		[Fact]
		public void GetWeatherForGeolocation_40_And_N111_JSONString()
		{
			var weatherString = GetWeather.GetWeatherForGeolocation(40, -111, true);
		}

		[Fact]
		public void GetWeatherForZip_84037_JSONString()
		{
			var weatherString = GetWeather.GetWeatherForZip("84103");
		}

		[Fact]
		public void GetWeatherDatesForZip_84037_WeatherForecasts()
		{
			var weather = GetWeather.GetWeatherForecastsForZip("84103");
		}

		[Fact]
		public void FiveDayAround1500Forecast_84037()
		{
			GetWeather.FiveDayAround1500Forecast(GetWeather.GetWeatherForecastsForZip("84103"));
		}

		[Fact]
		public void CreateICalForActivity_CustomActivity()
		{
			var activity = new Activity
			{
				Summary = "Test",
				Time = DateTime.Now + new TimeSpan(1, 0, 0, 0),
				Above = true,
				TemperatureF = 70
			};

			ICalCreator.CreateCalendarEventForActivity(activity);
		}

		[Fact]
		public void SaveCalendarLocally_ToDownloads()
		{
			var activity = new Activity
			{
				Summary = "Test",
				Time = DateTime.Now + new TimeSpan(1, 0, 0, 0),
				Above = true,
				TemperatureF = 70
			};

			var calendarEvent = ICalCreator.CreateCalendarEventForActivity(activity);
			var directoryPath = "D:\\Users\\Daniel\\Downloads";
			var filePath = directoryPath + $"\\{activity.Summary}.ics";
			ICalCreator.SaveCalendarLocally(filePath, calendarEvent);
		}

		[Fact]
		public void GetOpenWeatherMapKey_MyKey()
		{
			var weatherKey = ConfigHelper.GetOpenWeatherMapKey(true);
		}

		[Fact]
		public void SaveActivities_ActivitiesSavedToMyDocuments()
		{
			var activities = new List<Activity>
			{
				new Activity
				{
					Id = 1,
					EmailAddress = "test@test.com",
					ZipCode = "84103",
					TemperatureF = 32,
					Above = false,
					Summary = "Snowboarding"
				},

				new Activity
				{
					Id = 2,
					EmailAddress = "test@test.com",
					ZipCode = "84103",
					TemperatureF = 80,
					Above = true,
					Summary = "Swimming"
				}
			};

			JSONSave.SaveActivitiesToAppData(activities);
		}

		[Fact]
		public void LoadActivities_ActivitiesInList()
		{
			var activities = JSONSave.LoadActivitiesFromAppData();
		}

		[Fact]
		public void SaveWeather_WeatherSavedToMyDocuments()
		{
			JSONSave.SaveWeatherToAppData(GetWeather.GetWeatherForecastsForZip("84103", true));
		}
	}
}

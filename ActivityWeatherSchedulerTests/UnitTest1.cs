using ActivityWeatherSchedulerLibraryNETStandard;
using ActivityWeatherSchedulerLibraryNETStandard.Models;
using System;
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
        public void GetWeatherForGeolocation_41_0391_And_N111_9792_JSONString()
        {
            var weatherString = GetWeather.GetWeatherForGeolocation(41.0391, -111.9792, true);

            Assert.Equal("", "");
        }

        [Fact]
        public void GetWeatherForZip_84037_JSONString()
        {
            var weatherString = GetWeather.GetWeatherForZip("84037");

            Assert.Equal("", "");
        }

        [Fact]
        public void GetWeatherDatesForZip_84037_WeatherForecasts()
        {
            var weather = GetWeather.GetWeatherForecastsForZip("84037");

            Assert.Equal("", "");
        }

        [Fact]
        public void FiveDayAround1500Forecast_84037()
        {
            GetWeather.FiveDayAround1500Forecast(GetWeather.GetWeatherForecastsForZip("84037"));

            Assert.Equal("", "");
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

            ICalCreator.CreateICalForActivity(activity);

            Assert.Equal("", "");
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

            var calendarEvent = ICalCreator.CreateICalForActivity(activity);
            var directoryPath = "D:\\Users\\Daniel\\Downloads";
            var filePath = directoryPath + $"\\{activity.Summary}.ics";
            ICalCreator.SaveCalendarLocally(filePath, calendarEvent);
            Assert.Equal("", "");
        }

        [Fact]
        public void GetOpenWeatherMapKey_MyKey()
        {
            var weatherKey = ConfigHelper.GetOpenWeatherMapKey(true);
            Assert.Equal("", "");
        }
    }
}

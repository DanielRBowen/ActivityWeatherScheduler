using ActivityWeatherSchedulerLibraryNETStandard;
using ActivityWeatherSchedulerLibraryNETStandard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivityWeatherSchedulerBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    public class WeatherForecastController : Controller
    {
        [HttpGet("[action]/{zip}")]
        public IEnumerable<WeatherForecast> FiveDayWeatherForecast(string zip)
        {
            if (string.IsNullOrWhiteSpace(zip))
            {
                return null;
            }

            return GetWeather.FiveDayAround1500Forecast(zip);
        }

        [HttpGet("[action]/{latitude}&{longitude}")]
        public IEnumerable<WeatherForecast> FiveDayWeatherForecast(double latitude, double longitude)
        {
            return GetWeather.FiveDayAround1500Forecast(latitude, longitude);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityWeatherSchedulerBlazor.Server.Data;
using ActivityWeatherSchedulerLibraryNETStandard;
using ActivityWeatherSchedulerLibraryNETStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivityWeatherSchedulerBlazor.Server.Controllers
{
	[Route("api/[controller]")]
	public class WeatherForecastController : Controller
	{
		private ActivityContext ActivityDbContext { get; }

		public WeatherForecastController(ActivityContext activityContext)
		{
			ActivityDbContext = activityContext ?? throw new ArgumentNullException(nameof(activityContext));
		}

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

		[HttpPost("[action]")]
		public async Task AddActivity([FromBody]Activity activity)
		{
			ActivityDbContext.Activities.Add(activity);
			await ActivityDbContext.SaveChangesAsync();
		}

		[HttpGet("[action]/{email}")]
		public async Task<IList<Activity>> GetActivities(string email)
		{
			var activities = await ActivityDbContext.Activities
				.Where(activity => activity.Email == email)
				.ToListAsync();
			return activities;
		}
	}
}

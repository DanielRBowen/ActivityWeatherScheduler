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
		public async Task<IList<WeatherForecast>> FiveDayWeatherForecast(string zip)
		{
			return await CurrentOrPreviousFiveDayWeatherForecast(zip);
		}

		private async Task<IList<WeatherForecast>> CurrentOrPreviousFiveDayWeatherForecast(string zip)
		{
			if (string.IsNullOrWhiteSpace(zip))
			{
				return null;
			}

			var utcNow = DateTimeOffset.UtcNow;
			var weatherForecasts = new List<WeatherForecast>();
			var cachedWeather = await ActivityDbContext.CachedWeathers
				.AsNoTracking()
				.Include(cachedWeather0 => cachedWeather0.WeatherForecasts)
				.SingleOrDefaultAsync(cachedWeather0 => cachedWeather0.ZipCode == zip && cachedWeather0.DateCached.LocalDateTime.ToShortDateString() == utcNow.LocalDateTime.ToShortDateString());


			if (cachedWeather == null)
			{
				weatherForecasts = GetWeather.FiveDayAround1500Forecast(zip).ToList();

				cachedWeather = new CachedWeather
				{
					ZipCode = zip,
					DateCached = utcNow,
					WeatherForecasts = weatherForecasts
				};

				var previousCachedWeathersForZipcodeQuery = ActivityDbContext.CachedWeathers
					.Where(previousCachedWeather => previousCachedWeather.ZipCode == zip);

				if (previousCachedWeathersForZipcodeQuery.Count() > 0)
				{
					foreach (var previousCachedWeather in previousCachedWeathersForZipcodeQuery)
					{
						ActivityDbContext.CachedWeathers.Remove(previousCachedWeather);
					}
				}

				ActivityDbContext.CachedWeathers.Add(cachedWeather);
				await ActivityDbContext.SaveChangesAsync();
				return weatherForecasts;
			}
			else
			{
				return cachedWeather.WeatherForecasts;
			}
		}

		[HttpGet("[action]/{latitude}&{longitude}")]
		public IEnumerable<WeatherForecast> FiveDayWeatherForecast(double latitude, double longitude)
		{
			return GetWeather.FiveDayAround1500Forecast(latitude, longitude);
		}

		[HttpPost("[action]")]
		public async Task AddActivity([FromBody]Activity activity)
		{
			var scheduledActivity = ScheduleActivity(activity);
			await ActivityDbContext.Activities.AddAsync(scheduledActivity);
			await ActivityDbContext.SaveChangesAsync();
		}

		private Activity ScheduleActivity(Activity activity)
		{
			var weatherForecasts = CurrentOrPreviousFiveDayWeatherForecast(activity.ZipCode).Result;
			var scheduledWeatherForecast = activity.Above
				? weatherForecasts.FirstOrDefault(weatherForecast => weatherForecast.TemperatureF >= activity.TemperatureF)
				: weatherForecasts.FirstOrDefault(weatherForecast => weatherForecast.TemperatureF <= activity.TemperatureF);

			if (scheduledWeatherForecast != null)
			{
				activity.Time = scheduledWeatherForecast.Time;
				activity.CalendarString = ICalCreator.MakeCalendarString(activity);
				return activity;
			}
			else
			{
				return activity;
			}
		}

		[HttpGet("[action]/{email}")]
		[Produces(typeof(IList<Activity>))]
		public async Task<IActionResult> ScheduleActivities(string email)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(email))
				{
					return NotFound();
				}

				var activities = ActivityDbContext.Activities.Where(activity => activity.EmailAddress == email);

				if (!activities.Any())
				{
					return NotFound();
				}

				await activities.ForEachAsync(activity => activity = ScheduleActivity(activity));
				ActivityDbContext.Activities.UpdateRange(activities);
				await ActivityDbContext.SaveChangesAsync();
				return await GetActivitiesInternal(email);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpDelete("[action]/{email}")]
		public async Task<IActionResult> ClearActivities(string email)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(email))
				{
					return NotFound();
				}

				var activitiesToBeRemoved = ActivityDbContext.Activities.Where(activity => activity.EmailAddress == email);

				if (!activitiesToBeRemoved.Any())
				{
					return NotFound();
				}

				ActivityDbContext.RemoveRange(activitiesToBeRemoved);
				await ActivityDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateActivity([FromBody]Activity activity)
		{
			try
			{
				if (activity == null)
				{
					return NotFound();
				}

				ActivityDbContext.Activities.Update(activity);
				await ActivityDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpDelete("[action]/{Id}")]
		public async Task<IActionResult> DeleteActivity(int Id)
		{
			try
			{
				var activityToDelete = await ActivityDbContext.Activities.FirstOrDefaultAsync(activity => activity.Id == Id);

				if (activityToDelete == null)
				{
					return NotFound();
				}

				ActivityDbContext.Remove(activityToDelete);
				await ActivityDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpGet("[action]/{email}")]
		[Produces(typeof(IList<Activity>))]
		public async Task<IActionResult> GetActivities(string email)
			{
			try
			{
				return await GetActivitiesInternal(email);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		private async Task<IActionResult> GetActivitiesInternal(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return NotFound();
			}

			var activities = await ActivityDbContext.Activities
					.Where(activity => activity.EmailAddress == email)
					.ToListAsync();
			return Ok(activities);
		}
	}
}

using ActivityWeatherSchedulerLibraryNETStandard.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivityWeatherSchedulerBlazor.Server.Data
{
    public class ActivityContext : DbContext
    {
        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
    }
}

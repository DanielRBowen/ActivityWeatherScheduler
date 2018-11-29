using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ActivityWeatherSchedulerLibraryNETStandard
{
    public class ConfigHelper
    {
        public static string GetOpenWeatherMapKey(bool test = false)
        {
            var openWeatherKeyConfigFile = "";

            if (test == false)
            {
                openWeatherKeyConfigFile = File.ReadAllText($"{Environment.CurrentDirectory}//..//ActivityWeatherSchedulerLibraryNETStandard//OpenWeatherKeyConfig.json");
            }
            else
            {
                openWeatherKeyConfigFile = File.ReadAllText($"{Environment.CurrentDirectory}//..//..//..//..//ActivityWeatherSchedulerLibraryNETStandard//OpenWeatherKeyConfig.json");
            }

            var openWeatherKeyConfigJ = JObject.Parse(openWeatherKeyConfigFile);
            var openWeatherMapKey = openWeatherKeyConfigJ["OpenWeatherMapKeys"]["DefaultKey"];
            return openWeatherMapKey.ToString();
        }
    }
}

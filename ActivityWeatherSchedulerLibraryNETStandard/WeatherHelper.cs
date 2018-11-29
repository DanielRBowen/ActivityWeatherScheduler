using System;

namespace ActivityWeatherSchedulerLibraryNETStandard
{
    public static class WeatherHelper
    {
        /// <summary>
        /// Gives the Average Temperature of a max and min temperature.
        /// </summary>
        /// <param name="maxTempString">Temperature in Kelvin</param>
        /// <param name="minTempString">Temperature in Kelvin</param>
        /// <returns>The average temperature in Fahrenheit</returns>
        public static decimal AverageTemperatureF(string maxTempString, string minTempString)
        {
            decimal.TryParse(maxTempString, out decimal maxTemp);
            decimal.TryParse(minTempString, out decimal minTemp);

            var averageTempKelvin = (maxTemp + minTemp) / 2;

            return KelvinToFahrenheit(averageTempKelvin);
        }

        public static decimal ToDecimal(string decimalS)
        {
            if (!decimal.TryParse(decimalS, out decimal result))
            {
                throw new InvalidOperationException("A decimal number could not be parsed");
            }

            return result;
        }

        public static decimal KelvinToFahrenheit(decimal kelvin)
        {
            return ((kelvin - (decimal)273.15) * (decimal)1.8 + 32);
        }

        public static decimal KelvinToFahrenheit(string kelvin)
        {
            decimal.TryParse(kelvin, out decimal kelvinNumber);
            
            return ((kelvinNumber - (decimal)273.15) * (decimal)1.8 + 32);
        }
    }
}

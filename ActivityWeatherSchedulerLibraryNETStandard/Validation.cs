using System;
using System.Net.Mail;

namespace ActivityWeatherSchedulerLibraryNETStandard
{
    public class Validation
    {
        public static bool IsEmail(string email)
        {
            try
            {
                email = new MailAddress(email).Address;
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

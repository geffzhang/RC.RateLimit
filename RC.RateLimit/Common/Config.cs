using System;
using System.Data;
using static System.Int32;

namespace RC.RateLimit.Common
{
    public class Config
    {
        public static string RedisHost => ReadConfiguration("RateLimit.RedisHost", "");

        public static int RedisPort => Parse(ReadConfiguration("RateLimit.RedisPort", ""));

        public static int PerSecondRateLimit => Parse(ReadConfiguration("RateLimit.PerSecondRateLimit", ""));

        public static int PerMinuteRateLimit => Parse(ReadConfiguration("RateLimit.PerMinuteRateLimit", ""));

        public static int PerHourRateLimit => Parse(ReadConfiguration("RateLimit.PerHourRateLimit", ""));

        public static string ReadConfiguration(string key, string defaultValue = null)
        {
            var keyVal = System.Configuration.ConfigurationManager.AppSettings[key];
            return !string.Equals(keyVal, null, StringComparison.Ordinal) ? keyVal : defaultValue;
        }
    }
}




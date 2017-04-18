using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace RC.RateLimit.Common
{
    public class Config
    {
        public static string RedisHost
        {
            get { return ReadConfiguration("RateLimit.RedisHost", ""); }
        }

        public static int RedisPort
        {
            get { return Int32.Parse(ReadConfiguration("RateLimit.RedisPort", "")); }
        }

        public static int PerSecondRateLimit
        {
            get { return Int32.Parse(ReadConfiguration("RateLimit.PerSecondRateLimit", "")); }
        }

        public static int PerMinuteRateLimit
        {
            get { return Int32.Parse(ReadConfiguration("RateLimit.PerMinuteRateLimit", "")); }
        }

        public static int PerHourRateLimit
        {
            get { return Int32.Parse(ReadConfiguration("RateLimit.PerHourRateLimit", "")); }
        }

        public static string ReadConfiguration(string key, string defaultValue = null)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}




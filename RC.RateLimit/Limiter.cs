using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using RC.RateLimit.Common;
using ServiceStack.Redis;

namespace RC.RateLimit
{
    public class Limiter
    {

        string redisHost = Config.RedisHost;

        int redisPort = Config.RedisPort;
        int perSecondLimit = Config.PerSecondRateLimit;
        int perMinuteLimit = Config.PerMinuteRateLimit;

        int perHourLimit = Config.PerHourRateLimit;

        public RedisClient redisCl;

        public Limiter()
        {
            redisCl = new RedisClient(redisHost, redisPort);

        }


        public void CheckLimit(string key)
        {

            try
            {
                PerSecondLimiter(redisCl, key, perSecondLimit);
                PerMinuteLimiter(redisCl, key, perMinuteLimit);
                PerHourLimiter(redisCl, key, perHourLimit);

            }
            catch (ServiceStack.Redis.RedisException ex)
            {
                throw ex;
            }

        }


        public void PerSecondLimiter(RedisClient client, string key, int limit)
        {
            key += ":Second";

            long rqs = client.LLen(key);
            if (rqs <= limit)
            {
                client.LPush(key, BitConverter.GetBytes(DateTime.Now.Ticks));
            }
            else
            {
            
                DateTime time = new DateTime(BitConverter.ToInt64(client.LIndex(key, -1),0));
                if (DateTime.Now - time < new TimeSpan(0, 0, 1))
                {
                    throw new RedisException("Per Second Rate limit exceeded.");
                }
                else
                {
                    client.LPush(key, BitConverter.GetBytes(DateTime.Now.Ticks));
                }
            }

            client.LTrim(key, 0, limit);

        }


        public void PerMinuteLimiter(RedisClient client, string key, int limit)
        {
            key += ":Minute";

            long rqs = client.LLen(key);
            if (rqs <= limit)
            {
                client.LPush(key, BitConverter.GetBytes(DateTime.Now.Ticks));
            }
            else
            {
                DateTime time = new DateTime(BitConverter.ToInt64(client.LIndex(key, -1), 0));
                if (DateTime.Now - time < new TimeSpan(0, 1, 0))
                {
                    throw new RedisException("Per Minute Rate limit exceeded.");
                }
                else
                {
                    client.LPush(key, BitConverter.GetBytes(DateTime.Now.Ticks));
                }
            }

            client.LTrim(key, 0, limit);

        }


        public void PerHourLimiter(RedisClient client, string key, int limit)
        {
            key += ":Hour";

            long rqs = client.LLen(key);
            if (rqs <= limit)
            {
                client.LPush(key, BitConverter.GetBytes(DateTime.Now.Ticks));
            }
            else
            {
                DateTime time = new DateTime(BitConverter.ToInt64(client.LIndex(key, -1),0));
                if (DateTime.Now - time < new TimeSpan(1, 0, 0))
                {
                    throw new RedisException("Per Hour Rate limit exceeded.");
                }
                else
                {
                    client.LPush(key, BitConverter.GetBytes(DateTime.Now.Ticks));
                }
            }

            client.LTrim(key, 0, limit);

        }


    }

}
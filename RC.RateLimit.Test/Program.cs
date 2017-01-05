using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC.RateLimit;

namespace RC.RateLimit.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Limiter RateLimiter = new Limiter();

            for (int i = 0; i < 25; i++)
            {

                try
                {
                    //do stuff
                    RateLimiter.CheckLimit("GetMyItems");
                    Console.WriteLine("Call completed");
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }


            }

            Console.ReadLine();

        }
    }
}

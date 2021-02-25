using System;

namespace Buisness.CCS
{
    public class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("veri tabanına loglandı");
        }
    }
}
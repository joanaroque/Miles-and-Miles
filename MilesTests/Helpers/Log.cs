using MilesTests.Data.Repositories;
using System;

namespace MilesTests.Helpers
{
    public class Log : ILog
    {
        public void Append(string message)
        {
            Console.WriteLine(DateTime.Now + "|" + message);
        }
    }
}

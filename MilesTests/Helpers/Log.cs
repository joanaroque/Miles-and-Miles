using MilesBackOffice.Web.Data.Repositories;
using System;

namespace MilesBackOffice.Web.Helpers
{
    public class Log : ILog
    {
        public void Append(string message)
        {
            Console.WriteLine(DateTime.Now + "|" + message);
        }
    }
}

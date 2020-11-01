namespace MilesBackOffice.Web.Helpers
{
    using System;

    public class Log : ILog
    {
        public void Append(string message)
        {
            Console.WriteLine(DateTime.Now + "|" + message);
        }
    }
}

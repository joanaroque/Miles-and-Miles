namespace MilesTests.Data.Repositories
{
    public interface ILog
    {
        /// <summary>
        /// shows the date now with the information
        /// </summary>
        /// <param name="message">information</param>
        void Append(string message);
    }
}

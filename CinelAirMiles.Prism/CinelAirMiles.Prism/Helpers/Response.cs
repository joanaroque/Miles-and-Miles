namespace CinelAirMiles.Prism.Helpers
{
    public class Response<T> where T : class
    {
        public string Message { get; set; }


        public object Attachment { get; set; }


        public bool Success { get; set; }


        public T Result { get; set; }
    }
}

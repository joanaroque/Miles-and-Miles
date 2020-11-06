namespace CinelAirMilesLibrary.Common.Helpers
{
    using System;

    public static class GuidHelper
    {
        public static string CreatedGuid()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToUInt32(buffer).ToString().Substring(0, 9);
        }
    }
}

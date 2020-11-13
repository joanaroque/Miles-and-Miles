﻿namespace CinelAirMilesLibrary.Common.Helpers
{
    using Microsoft.AspNetCore.Mvc;

    using System.Net;

    public class NotFoundViewResult : ViewResult
    {
        public NotFoundViewResult(string viewName)
        {
            ViewName = viewName;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}

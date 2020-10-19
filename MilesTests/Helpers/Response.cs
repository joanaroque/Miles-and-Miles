using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Helpers
{
    public class Response
    {

        public string Message { get; set; }


        public object Attachment { get; set; }


        public bool Success { get; set; }
    }
}

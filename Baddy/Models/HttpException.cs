using System;
using System.Net;

namespace Baddy.Models
{
    public class HttpException : Exception
    {
        public HttpStatusCode Code { private set; get; }

        public HttpException(HttpStatusCode code, string message = null) : base(message)
        {
            Code = code;
        }
    }
}

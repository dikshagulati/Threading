using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class HttpException : Exception
    {
        public HttpException(HttpStatusCode statusCode) { StatusCode = statusCode; }
        public HttpStatusCode StatusCode { get; private set; }
    }
}

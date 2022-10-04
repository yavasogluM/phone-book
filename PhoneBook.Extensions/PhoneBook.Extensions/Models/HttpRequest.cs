using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Extensions.Models
{
    public sealed class HttpRequest<T>
    {
        public T RequestObject { get; set; }
        public string RequestUrl { get; set; }

        /// <summary>
        /// GET - POST - PUT - DELETE
        /// </summary>
        public HttpRequestType RequestType { get; set; }
    }

    public enum HttpRequestType
    {
        GET = 1,
        POST = 2,
        PUT = 3,
        DELETE = 4,
        PATCH = 5
    }
}

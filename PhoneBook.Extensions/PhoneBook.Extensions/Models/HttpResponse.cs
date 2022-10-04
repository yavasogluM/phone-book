using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Extensions.Models
{
    public sealed class HttpResponse<T>
    {
        public T ResponseObject { get; set; }
    }
}

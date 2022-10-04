using System;
using System.Net;
namespace PhoneBook.Extensions
{
    public interface IHttpExtension
    {
        Models.HttpResponse<T> GetResponse<T>(Models.HttpRequest<T> request);
    }

    public class HttpExtension : IHttpExtension
    {
        private Models.HttpResponse<T> GetMethodResponse<T>(WebClient wc, Models.HttpRequest<T> request)
        {
            string result = wc.DownloadString(request.RequestUrl);
            return new Models.HttpResponse<T>
            {
                ResponseObject = result.DeSerialize<T>()
            };
        }

        private Models.HttpResponse<T> NotGetMethodResponse<T>(WebClient wc, Models.HttpRequest<T> request)
        {
            string result = wc.UploadString(request.RequestUrl, request.RequestType switch
            {
                Models.HttpRequestType.POST => "POST",
                Models.HttpRequestType.PUT => "PUT",
                Models.HttpRequestType.DELETE => "DELETE",
                Models.HttpRequestType.PATCH => "PATCH"
            }, request.RequestObject.Serialize());
            return new Models.HttpResponse<T>
            {
                ResponseObject = result.DeSerialize<T>()
            };
        }

        public Models.HttpResponse<T> GetResponse<T>(Models.HttpRequest<T> request)
        {
            using (WebClient wc = new WebClient())
            {
                if (request.RequestType == Models.HttpRequestType.GET)
                {
                    return GetMethodResponse<T>(wc, request);
                }
                else if (request.RequestType == Models.HttpRequestType.POST
                    || request.RequestType == Models.HttpRequestType.PUT
                    || request.RequestType == Models.HttpRequestType.PATCH
                    || request.RequestType == Models.HttpRequestType.DELETE)
                {
                    return NotGetMethodResponse<T>(wc, request);
                }
                else
                {
                    throw new Exception("Invalid HttpRequest!");
                }
            }
        }
    }
}

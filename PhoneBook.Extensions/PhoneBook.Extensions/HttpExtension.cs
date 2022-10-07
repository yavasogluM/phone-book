using System;
using System.Net;
namespace PhoneBook.Extensions
{
    public interface IHttpExtension
    {
        Models.HttpResponse<T> GetResponse<T, Y>(Models.HttpRequest<Y> request);
    }

    public class HttpExtension : IHttpExtension
    {
        private Models.HttpResponse<T> GetMethodResponse<T, Y>(WebClient wc, Models.HttpRequest<Y> request)
        {
            string result = wc.DownloadString(request.RequestUrl);
            return new Models.HttpResponse<T>
            {
                ResponseObject = result.DeSerialize<T>()
            };
        }

        private Models.HttpResponse<T> NotGetMethodResponse<T, Y>(WebClient wc, Models.HttpRequest<Y> request)
        {
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
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

        public Models.HttpResponse<T> GetResponse<T, Y>(Models.HttpRequest<Y> request)
        {
            using (WebClient wc = new WebClient())
            {
                if (request.RequestType == Models.HttpRequestType.GET)
                {
                    return GetMethodResponse<T, Y>(wc, request);
                }
                else if (request.RequestType == Models.HttpRequestType.POST
                    || request.RequestType == Models.HttpRequestType.PUT
                    || request.RequestType == Models.HttpRequestType.PATCH
                    || request.RequestType == Models.HttpRequestType.DELETE)
                {
                    return NotGetMethodResponse<T, Y>(wc, request);
                }
                else
                {
                    throw new Exception("Invalid HttpRequest!");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MajorRoadStatusSystem
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private readonly HttpClient _httpClient;
        public HttpRequestBuilder(HttpClient client)
        {
            _httpClient = client;
        }
        public HttpResponseMessage Send(HttpMethod method, string url)
        {
            WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
            var httpRequestMessage = new HttpRequestMessage(method, url);
            // sending the request headers first, to see if the server will allow (accept) the request
            _httpClient.DefaultRequestHeaders.ExpectContinue = true;
            _httpClient.Timeout = TimeSpan.FromMinutes(2); 

            return _httpClient.SendAsync(httpRequestMessage).Result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MajorRoadStatusSystem
{
    public interface IHttpRequestBuilder
    {
        HttpResponseMessage Send(HttpMethod method, string url);
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MajorRoadStatusSystem.Model;
using Microsoft.Extensions.Options;

namespace MajorRoadStatusSystem
{
    public class MajorRoadStatus : IMajorRoadStatus
    {
        private readonly MajorRoadStatusSystemConfig _majorRoadStatusConfig;
        private readonly IHttpRequestBuilder _httpRequestBuilder;
        public MajorRoadStatus(IOptionsMonitor<MajorRoadStatusSystemConfig> majorRoadStatusConfig, IHttpRequestBuilder httpRequestBuilder)
        {
            _majorRoadStatusConfig = majorRoadStatusConfig.CurrentValue;
            _httpRequestBuilder = httpRequestBuilder;
        }
        public Response GetMajorRoadStatus(string roadName)
        {
            var response = new Response();
            try
            {
                var res = _httpRequestBuilder.Send(HttpMethod.Get, CreateUri(roadName));

                if (res.IsSuccessStatusCode)
                {
                    var responseAttributes = FormatResponse.FormatAsType<List<ResponseAttributes>>(res);
                    response.Status = Status.Valid;
                    response.ResponseAttributes = responseAttributes;
                    return response;
                }

                response.Status = Status.InValid;
            }
            catch
            {
                response.Status = Status.Error;
            }

            return response;
        }

        private string CreateUri(string roadName)
        {
            return string.Format(_majorRoadStatusConfig.Url + "{0}?app_id={1}&app_key={2}", roadName, _majorRoadStatusConfig.AppId, _majorRoadStatusConfig.AppKey);
        }
    }
}

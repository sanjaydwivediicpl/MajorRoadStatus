using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MajorRoadStatus;
using MajorRoadStatusSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace MajorRoadStatusTest
{
    class TestInit
    {
        public Mock<CheckMajorRoadStatus> CheckMajorRoadStatus;
        public Mock<MajorRoadStatusSystem.MajorRoadStatus> MajorRoadStatus;
        public Mock<HttpRequestBuilder> HttpRequestBuilder;

        public TestInit()
        {
            var config = InitConfiguration();
            var settings = new MajorRoadStatusSystemConfig()
            {

                Url = config["Url"],
                AppId = config["AppId"],
                AppKey = config["AppKey"]
            };

            var monitor = Mock.Of<IOptionsMonitor<MajorRoadStatusSystemConfig>>(_ => _.CurrentValue == settings);
            HttpRequestBuilder = new Mock<HttpRequestBuilder>(new HttpClient());
            MajorRoadStatus = new Mock<MajorRoadStatusSystem.MajorRoadStatus>(monitor, HttpRequestBuilder.Object);
            CheckMajorRoadStatus = new Mock<CheckMajorRoadStatus>(MajorRoadStatus.Object);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
              .AddJsonFile("AppSettings_Test.json")
              .Build();
            return config;
        }
    }
}

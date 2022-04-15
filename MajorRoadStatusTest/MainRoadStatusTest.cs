using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using MajorRoadStatusSystem;
using MajorRoadStatusSystem.Model;
using NUnit.Framework;

namespace MajorRoadStatusTest
{
    [TestFixture]
    public class MajorRoadStatusTest
    {
        private readonly TestInit testInit;

        public MajorRoadStatusTest()
        {
            testInit = new TestInit();
        }

        [TestCase]
        public void Valid_Road_Valid_Code()
        {

            var code = testInit.CheckMajorRoadStatus.Object.GetMajorRoadStatus("A2");
            Assert.Equals(0, code);
        }

        [TestCase]
        public void InValid_Road_InValid_Code()
        {
            var code = testInit.CheckMajorRoadStatus.Object.GetMajorRoadStatus("A233");
            Assert.Equals(1, code);
        }

        [TestCase]
        public void Valid_Road_Valid_Status()
        {
            var status = new List<ResponseAttributes>
          {
            new ResponseAttributes
            {
              displayName = "A3",
              statusSeverity = "Good",
              statusSeverityDescription = "Good",
              id = "a23"
            }
          };
            var message = testInit.CheckMajorRoadStatus.Object.CreateValidStatusMessage(status);
            var line1 = message.Split(new[] { '\r', '\n' }).FirstOrDefault();
            Assert.That(line1, Does.StartWith("The Status of the A3 is as follows"));
        }

        [TestCase]
        public void MajorRoadStatusUrl__Json()
        {
            string url = "https://api.tfl.gov.uk/Road/A2";

            var response = testInit.HttpRequestBuilder.Object.Send(HttpMethod.Get, url);

            Assert.Equals("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Theory]
        [TestCase("A2", Status.Valid)]       
        [TestCase("A333", Status.InValid)]
        public void MajorRoadStatusService_Status(string road, Status expectedStatus)
        {
            var response = testInit.MajorRoadStatus.Object.GetMajorRoadStatus(road);
            Assert.Equals(expectedStatus, response.Status);
        }

        [TestCase]
        public void MajorRoadStatusService_FormatAsType_Status()
        {
            var responseMessage = new HttpResponseMessage
            {
                Content = new StringContent("[" +
                         "{" +
                         "  \"$type\": \"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\"," +
                         "  \"id\": \"a2\"," +
                         "  \"displayName\": \"A2\"," +
                         "  \"statusSeverity\": \"Good\"," +
                         "  \"statusSeverityDescription\": \"No Exceptional Delays\"," +
                         "  \"bounds\": \"[[-0.0857,51.44091],[0.17118,51.49438]]\"," +
                         "  \"envelope\": \"[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]\"," +
                         "  \"url\": \"/Road/a2\" " +
                        "}]")
            };

            var status = FormatResponse.FormatAsType<List<ResponseAttributes>>(responseMessage);

            Assert.Equals("A2", status.FirstOrDefault().displayName);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MajorRoadStatusSystem;
using MajorRoadStatusSystem.Model;

namespace MajorRoadStatus
{
    public class CheckMajorRoadStatus : ICheckMajorRoadStatus
    {
        private readonly IMajorRoadStatus _majorRoadStatus;

        public CheckMajorRoadStatus(IMajorRoadStatus majorRoadStatus)
        {
            _majorRoadStatus = majorRoadStatus;
        }
        public int GetMajorRoadStatus(string roadName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(roadName))
                {
                    var response = _majorRoadStatus.GetMajorRoadStatus(roadName);

                    if (response != null)
                    {
                        switch (response.Status)
                        {
                            case Status.Valid:
                                {
                                    var status = CreateValidStatusMessage(response.ResponseAttributes);
                                    PrintStatus(status);
                                    return 0;
                                }
                            case Status.InValid:
                                {
                                    PrintStatus(roadName + " is not a valid road");
                                    return 1;
                                }
                            case Status.Error:
                                {
                                    PrintStatus("Unable to retreive the status of " + roadName);
                                    return 2;
                                }
                        }
                    }
                }
            }
            catch {/* log exception*/}
            return 1;
        }

        public string CreateValidStatusMessage(List<ResponseAttributes> Status)
        {
            var message = new StringBuilder();
            if (Status != null && Status.Any())
            {
                Status.ForEach(road => message.AppendLine($"The Status of the {road.displayName} is as follows")
                    .AppendLine($"Road Status is {road.statusSeverity}")
                    .AppendLine($"Road Status Description is {road.statusSeverityDescription}"));
            }
            return message.ToString();
        }

        private void PrintStatus(string status)
        {
            Console.WriteLine(status);
        }
    }

}

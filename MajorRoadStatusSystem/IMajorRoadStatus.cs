using System;
using System.Collections.Generic;
using System.Text;
using MajorRoadStatusSystem.Model;

namespace MajorRoadStatusSystem
{
    public interface IMajorRoadStatus
    {
        Response GetMajorRoadStatus(string road);
    }
}

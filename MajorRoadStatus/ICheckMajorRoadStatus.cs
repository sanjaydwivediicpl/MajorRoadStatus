using System;
using System.Collections.Generic;
using System.Text;

namespace MajorRoadStatus
{
    public interface ICheckMajorRoadStatus
    {
        int GetMajorRoadStatus(string roadName);
    }
}

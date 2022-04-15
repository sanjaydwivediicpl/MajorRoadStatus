using System;
using System.Collections.Generic;
using System.Text;

namespace MajorRoadStatusSystem.Model
{
    public enum Status
    {
        Valid, 
        InValid,
        Error
    }
    public class Response
    {
        public List<ResponseAttributes> ResponseAttributes;
        public Status Status;
    }
}

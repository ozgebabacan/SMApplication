using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMApplication.Models.Enum;

namespace SMApplication.Models
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Status = Status.Success;
        }

        public Status Status { get; set; }
        public string Message { get; set; }
    }
}

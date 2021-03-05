using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMApplication.Models.Enum;

namespace SMApplication.Models
{
    /// <summary>
    /// Tüm response modelleri bu classdan türetilerek kullanılır.
    /// </summary>
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

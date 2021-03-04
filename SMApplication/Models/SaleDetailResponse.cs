using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMApplication.Models
{
    public class SaleDetailResponse:BaseResponse
    {
        public Order Order { get; set; }

    }

    public class SaleResponse : BaseResponse
    {
        public List<Order> Orders { get; set; }

    }


    public class SaleDetailServiceResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public Response Response { get; set; }
    }

    public class Response
    {
        public List<Order> Orders { get; set; }
    }
}

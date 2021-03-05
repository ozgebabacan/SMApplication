using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMApplication.Models
{
    /// <summary>
    /// GetSales servisi için kullanılan request modeli
    /// </summary>
    public class SaleDetailRequest
    {
        public string ApiCode { get; set; }
        public int StoreId { get; set; }
        public string OrderStatus { get; set; }
        public int InvoiceStatus { get; set; }

    }
}

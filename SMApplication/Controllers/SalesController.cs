using Microsoft.AspNetCore.Mvc;
using SMApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMApplication.Models;

namespace SMApplication.Controllers
{
    public class SalesController : Controller
    {
        public SalesService SalesService { get; set; }

        public SalesController(SalesService salesService)
        {
            SalesService = salesService;
        }

        public IActionResult Detail(string detail)
        {
            //var order = JsonConvert.DeserializeObject<Order>(detail);

            return View(detail);
        }

        public IActionResult List()
        {
            var tokenResult = SalesService.GetToken();

            SaleResponse result = new SaleResponse();

            result = SalesService.GetSalesDetail(new SaleDetailRequest()
            {
                ApiCode = tokenResult.ApiCode,
                StoreId = 37814,
                OrderStatus = "Completed",
                InvoiceStatus = 0
            });


            return View(result);
        }
    }
}

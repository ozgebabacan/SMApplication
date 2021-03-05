using Microsoft.AspNetCore.Mvc;
using SMApplication.Service;
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

        public IActionResult Detail(int orderId)
        {
            SaleDetailResponse result = new SaleDetailResponse();

            result = SalesService.GetOrderDetail(orderId);

            return View(result);
        }

        public IActionResult List()
        {
            SaleResponse result = new SaleResponse();

            result = SalesService.GetOrder();

            return View(result);
        }
    }
}

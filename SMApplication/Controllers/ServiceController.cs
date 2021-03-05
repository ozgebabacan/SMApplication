using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMApplication.Models;
using SMApplication.Service;

namespace SMApplication.Controllers
{
    public class ServiceController : Controller
    {
        public ServiceReportService ServiceReportService { get; set; }

        public ServiceController(ServiceReportService serviceReportService)
        {
            ServiceReportService = serviceReportService;
        }

        public IActionResult Index()
        {
            var result = new LogResponse();

            result = ServiceReportService.ReadLog();

            return View(result);
        }
    }
}

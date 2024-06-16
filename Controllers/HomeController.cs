using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesForecasting.Models;
using SalesForecasting.Services;
using SalesForecasting.Services.Interfaces;
using System.Diagnostics;

namespace SalesForecasting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISalesService _salesService;

        public HomeController(ILogger<HomeController> logger,ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
        }

        public IActionResult Index()
        {
            IEnumerable<string> states=_salesService.GetAllStates();
            ViewBag.States = new SelectList(states);
            return View(new SalesViewModel());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

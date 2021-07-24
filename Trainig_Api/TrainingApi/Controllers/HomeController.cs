using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlProject.Services.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrainingApi.Models;

namespace TrainingApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger, IDataService dataService, IConfiguration configuration)
        {
            _logger = logger;
            _dataService = dataService;
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:Csv"];
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            ViewBag.Message = "Employee Sign Up";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult LoadTable()
        {
            return View();
        }
        
        public IActionResult GetTableData()
        {
            var table = _dataService.GetData(_connectionString);
            return new JsonResult(null); //{ Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

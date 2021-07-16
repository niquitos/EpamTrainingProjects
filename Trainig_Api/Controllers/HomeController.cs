using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TrainingApi.Models;

namespace TrainingApi.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Creates an instance of HomeController and saves the standard logger
        /// </summary>
        /// <param name="logger">Standard logger</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Methods
        /// <summary>
        /// Creates an Index view
        /// </summary>
        /// <returns>The result of an Action Method</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Creates a privacy view
        /// </summary>
        /// <returns>The result of an Action Method</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Creates an Error view.
        /// <para>Turns off cache while executing</para>
        /// </summary>
        /// <returns>The result of an Action Methods</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using TrainingApi.Models;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Messages;
using TrainingApi.Services.Repositories;

namespace TrainingApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeUpdateSender _employeeUpdateSender;
        private readonly IHostedService _employeeConsumerService;
        private readonly IMapper _mapper;
        private readonly IDataRepository<EmployeeDomainModel> _employeeRepository;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeUpdateSender employeeUpdateSender,
                                  IHostedService employeeConsumerService,
                                  IDataRepository<EmployeeDomainModel> employeeRepository, IMapper mapper)
        {
            _logger = logger;
            _employeeUpdateSender = employeeUpdateSender;
            _employeeConsumerService = employeeConsumerService;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            CancellationTokenSource source = new CancellationTokenSource();
            _employeeConsumerService.StartAsync(source.Token);
        }

        /// <summary>
        /// Loads employees from the database and passes them to the view
        /// </summary>
        /// <returns>A view that displays a table with all employees</returns>
        [HttpGet("Index")]
        public  ActionResult Index()
        {
            List<EmployeeModel> employees = _mapper.Map<List<EmployeeModel>>(_employeeRepository.GetAll());
            
            return View(employees);
        }

        /// <summary>
        /// Does nothing for now
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Details")]
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// Loads a form to create an employee
        /// </summary>
        /// <returns>A view with the form fields and a submit button</returns>
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Saves an employee to the database and sends a message to RabbitMq
        /// </summary>
        /// <param name="model">An employee model created in the form</param>
        /// <returns>If the model is valid redirects to the view with employee table. If not then loads a form to create an employee.</returns>
        [ValidateAntiForgeryToken]
        [HttpPost("Create")]
        public ActionResult Create([FromForm] EmployeeModel model)
        {
            if (!ModelState.IsValid) return ValidationProblem();
            if (ModelState.IsValid)
            {
                EmployeeDomainModel employee = _mapper.Map<EmployeeModel, EmployeeDomainModel>(model);

                _employeeRepository.CreateImmediately(employee);

                _employeeUpdateSender.SendEmployee(employee);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        /// <summary>
        /// Does nothing for now
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Edit")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        /// <summary>
        /// Does nothing for now
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost("Edit")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Does nothing for now
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        /// <summary>
        /// Does nothing for now
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpDelete("Delete")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

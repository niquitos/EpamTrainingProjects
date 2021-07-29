using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TrainingApi.Models;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Repositories;

namespace TrainingApi.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IDataRepository<EmployeeDomainModel> _employeeRepository;

        public EmployeeController(ILogger<EmployeeController> logger,
                                  IDataRepository<EmployeeDomainModel> employeeRepository, IMapper mapper)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("Employee/Index")]
        public ActionResult Index()
        {
            List<EmployeeModel> employees = _mapper.Map<List<EmployeeModel>>(_employeeRepository.GetAll());

            return View(employees);
        }

        // GET: EmployeeController/Details/5
        [HttpGet("Employee/Details")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        [HttpGet("Employee/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [ValidateAntiForgeryToken]
        [HttpPost("Employee/Create")]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {  
                EmployeeDomainModel employee = _mapper.Map<EmployeeModel, EmployeeDomainModel>(model);

                _employeeRepository.CreateImmediately(employee);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: EmployeeController/Edit/5
        [HttpGet("Employee/Edit")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost("Employee/Edit")]
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

        // GET: EmployeeController/Delete/5
        [HttpGet("Employee/Delete")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost("Employee/Delete")]
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

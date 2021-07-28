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
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public IDataRepository<EmployeeDomainModel> DataRepository { get; }

        public EmployeeController(ILogger<EmployeeController> logger, IDataRepository<EmployeeDomainModel> dataRepository)
        {
            _logger = logger;
            DataRepository = dataRepository;
            //DataService = dataService;
        }

        public ActionResult EmployeeIndex()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDomainModel, EmployeeModel>());
            var mapper = new Mapper(config);
            var employees = mapper.Map<List<EmployeeModel>>(DataRepository.GetAll());

            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeModel, EmployeeDomainModel>());
                var mapper = new Mapper(config);
                EmployeeDomainModel employee = mapper.Map<EmployeeModel, EmployeeDomainModel>(model);

                DataRepository.CreateImmediately(employee);

                return RedirectToAction(nameof(EmployeeIndex));
            }
            return View();
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

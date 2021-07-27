using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TrainingApi.Models;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Repositories;

namespace TrainingApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public IDataRepository<EmployeeDomainModel> DataRepository { get; }

        public EmployeeController(ILogger<EmployeeController> logger,  IDataRepository<EmployeeDomainModel> dataRepository)
        {
            _logger = logger;
            DataRepository = dataRepository;
            //DataService = dataService;
        }

        public ActionResult EmployeeIndex()
        { 
            List<EmployeeModel> data = new();
            var items = DataRepository.GetAll();

            //convert from domain model to the view model
            foreach (var item in items)
            {
                data.Add(new EmployeeModel()
                {
                    EmployeeId = item.EmployeeId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Age = item.Age,
                    EmailAddress = item.EmailAddress
                });
            }
            return View(data);
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
            try
            {
                //convert from the view model to domain model
                var item = new EmployeeDomainModel()
                {
                    EmployeeId = model.EmployeeId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    EmailAddress = model.EmailAddress
                };
                DataRepository.CreateImmediately(item);
                return RedirectToAction(nameof(EmployeeIndex));
            }
            catch(Exception ex)
            {
                string mes = ex.Message;
                return View();
            }
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

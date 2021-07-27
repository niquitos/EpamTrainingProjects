using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TrainingApi.Mapping;
using TrainingApi.Models;
using TrainingApi.Services;
using TrainingApi.Services.DataAccess;

namespace TrainingApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        //public IDataService<EmployeeModel> DataService { get; }
        public IEmployeeProcessor EmployeeProcessor { get; }

        public EmployeeController(ILogger<EmployeeController> logger, /*, IDataService<EmployeeModel> dataService,*/ IEmployeeProcessor employeeProcessor)
        {
            _logger = logger;
            //DataService = dataService;
            EmployeeProcessor = employeeProcessor;
        }

        public ActionResult EmployeeIndex()
        {
            //DataService.CreateInstance = CreateInstanceSql;
            //IEnumerable<EmployeeModel> data = DataService.GetData().ToList();
            IEnumerable<EmployeeModel> data = EmployeeProcessor.LoadEmployees();
            return View(data);
        }

        private EmployeeModel CreateInstanceSql(DataRow row)
        {
            return new EmployeeModel
            {
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Age = Convert.ToInt32(row["Age"]),
                EmailAdress = row["EmailAdress"].ToString()
            };
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
                int recordsCreated = EmployeeProcessor.CreateEmployee(model.EmployeeId, model.FirstName, model.LastName, model.Age, model.EmailAdress);
                return RedirectToAction(nameof(EmployeeIndex));
            }
            catch
            {
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

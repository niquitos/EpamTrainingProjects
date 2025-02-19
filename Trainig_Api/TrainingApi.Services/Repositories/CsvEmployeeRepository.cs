﻿using CsvHelper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class CsvEmployeeRepository : ConnectionBase, IEmployeeRepository<EmployeeDomainModel>
    {
        private int _id;
        public CsvEmployeeRepository(IConfiguration configuration) : base(configuration["ConnectionStrings:Csv"])
        {

        }

        public void CreateImmediately(EmployeeDomainModel item)
        {
            item.Id = ++_id;
            using StreamWriter sw = new(DataConnection, true);
            using CsvWriter csvWriter = new(sw, CultureInfo.InvariantCulture);
            sw.WriteLine();
            csvWriter.WriteRecord(item);
        }

        public void DeleteImmediately(int id)
        {
            var items = GetAll().ToList();

            using StreamWriter sw = new(DataConnection, false);
            using CsvWriter csvWriter = new(sw, CultureInfo.InvariantCulture);
            var item = items.First(empl => empl.Id == id);
            if (items.Any())
            {
                items.Remove(item);
                csvWriter.WriteRecords(items);
            }
        }

        public EmployeeDomainModel Get(int id)
        {
            using StreamReader sr = new(DataConnection);
            using CsvReader scvReader = new(sr, CultureInfo.InvariantCulture);
            return scvReader.GetRecords<EmployeeDomainModel>().First(empl => empl.Id == id);
        }

        public IEnumerable<EmployeeDomainModel> GetAll()
        {
            using StreamReader sr = new(DataConnection);
            using CsvReader scvReader = new(sr, CultureInfo.InvariantCulture);
            var employees = scvReader.GetRecords<EmployeeDomainModel>().ToList();
            _id = employees.Max(employee => employee.Id);
            return employees;
        }
    }
}

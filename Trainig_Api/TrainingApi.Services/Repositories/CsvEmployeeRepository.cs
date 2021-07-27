using CsvHelper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class CsvEmployeeRepository : IDataRepository<EmployeeDomainModel>
    {
        private readonly string _connectionString;

        public CsvEmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:Csv"];
        }

        public void CreateImmediately(EmployeeDomainModel item)
        {
            using StreamWriter sw = new(_connectionString, true);
            using CsvWriter csvWriter = new(sw, CultureInfo.InvariantCulture);
            sw.WriteLine();
            csvWriter.WriteRecord(item);
        }

        public void DeleteImmediately(int id)
        {
            var items = GetAll().ToList();

            using StreamWriter sw = new(_connectionString, false);
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
            using StreamReader sr = new(_connectionString);
            using CsvReader scvReader = new(sr, CultureInfo.InvariantCulture);
            return scvReader.GetRecords<EmployeeDomainModel>().First(empl => empl.Id == id);
        }

        public IEnumerable<EmployeeDomainModel> GetAll()
        {
            using StreamReader sr = new(_connectionString);
            using CsvReader scvReader = new(sr, CultureInfo.InvariantCulture);
            return scvReader.GetRecords<EmployeeDomainModel>().ToList();
        }
    }
}

using CsvHelper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class CsvEmployeeRepository : ConnectionBase, IDataRepository<EmployeeDomainModel>
    {
        private readonly IMemoryCache _memoryCache;

        public CsvEmployeeRepository(IConfiguration configuration,IMemoryCache memoryCache) : base(configuration["ConnectionStrings:Csv"])
        {
            _memoryCache = memoryCache;
        }

        public void CreateImmediately(EmployeeDomainModel item)
        {
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
            var cacheKey = DataConnection + "employee";
            if (!_memoryCache.TryGetValue(cacheKey, out List<EmployeeDomainModel> employeeList))
            {
                using StreamReader sr = new(DataConnection);
                using CsvReader scvReader = new(sr, CultureInfo.InvariantCulture);
                employeeList = scvReader.GetRecords<EmployeeDomainModel>().ToList();

                var cacheExpirationOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _memoryCache.Set(cacheKey, employeeList, cacheExpirationOptions);
            }

            return employeeList.First(empl => empl.Id == id);
        }

        public IEnumerable<EmployeeDomainModel> GetAll()
        {
            var cacheKey = DataConnection + "employee";
            if(!_memoryCache.TryGetValue(cacheKey, out List<EmployeeDomainModel> employeeList))
            {
                using StreamReader sr = new(DataConnection);
                using CsvReader scvReader = new(sr, CultureInfo.InvariantCulture);
                employeeList = scvReader.GetRecords<EmployeeDomainModel>().ToList();

                var cacheExpirationOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _memoryCache.Set(cacheKey, employeeList, cacheExpirationOptions);
            }
            
            return employeeList;
        }
    }
}

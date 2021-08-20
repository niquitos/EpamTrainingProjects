using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class CsvDataRepositoryDecorator : ConnectionBase, IDataRepository<EmployeeDomainModel>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDataRepository<EmployeeDomainModel> _csvEmployeeRepository;

        public CsvDataRepositoryDecorator(IConfiguration configuration, IMemoryCache memoryCache) :base(configuration["ConnectionStrings:Csv"])
        {
            _memoryCache = memoryCache;
            _csvEmployeeRepository = new CsvEmployeeRepository(configuration);
        }
        public void CreateImmediately(EmployeeDomainModel item)
        {
            _csvEmployeeRepository.CreateImmediately(item);
        }

        public void DeleteImmediately(int id)
        {
            _csvEmployeeRepository.DeleteImmediately(id);
        }

        public EmployeeDomainModel Get(int id)
        {
            var cacheKey = DataConnection + "employee";
            if (!_memoryCache.TryGetValue(cacheKey, out List<EmployeeDomainModel> employeeList))
            {
                employeeList = _csvEmployeeRepository.GetAll().ToList();
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
            if (!_memoryCache.TryGetValue(cacheKey, out List<EmployeeDomainModel> employeeList))
            {
                employeeList = _csvEmployeeRepository.GetAll().ToList();
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

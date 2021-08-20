using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Services.CacheKeys;
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
            string key = id.ToString();
            if (!_memoryCache.TryGetValue(key, out EmployeeDomainModel employee))
            {
                employee = _csvEmployeeRepository.Get(id);
                _memoryCache.Set(key, employee, CreateCacheOptions());
            }

            return employee;
        }

        public IEnumerable<EmployeeDomainModel> GetAll()
        {
            if (!_memoryCache.TryGetValue(EmployeesCacheKeys.AllEmployeesKey, out List<EmployeeDomainModel> employeeList))
            {
                employeeList = _csvEmployeeRepository.GetAll().ToList();
                _memoryCache.Set(EmployeesCacheKeys.AllEmployeesKey, employeeList, CreateCacheOptions());
            }

            return employeeList;
        }

        private MemoryCacheEntryOptions CreateCacheOptions()
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(6),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
        }
    }
}

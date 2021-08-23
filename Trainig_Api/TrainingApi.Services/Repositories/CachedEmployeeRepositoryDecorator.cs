using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Services.CacheKeys;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class CachedEmployeeRepositoryDecorator : IEmployeeRepository<EmployeeDomainModel>
    {
        private readonly IEmployeeRepository<EmployeeDomainModel> _employeeRepository;
        private readonly IMemoryCache _memoryCache;

        public CachedEmployeeRepositoryDecorator(IEmployeeRepository<EmployeeDomainModel> employeeRepository, IMemoryCache memoryCache)
        {
            _employeeRepository = employeeRepository;
            _memoryCache = memoryCache;
        }

        public void CreateImmediately(EmployeeDomainModel item)
        {
            if (!_memoryCache.TryGetValue(EmployeesCacheKeys.AllEmployeesKey, out List<EmployeeDomainModel> employeeList))
            {
                employeeList = _employeeRepository.GetAll().ToList();
            }
            employeeList.Add(item);
            _memoryCache.Set(EmployeesCacheKeys.AllEmployeesKey, employeeList, CreateCacheOptions());
            _employeeRepository.CreateImmediately(item);
        }

        public void DeleteImmediately(int id)
        {
            _employeeRepository.DeleteImmediately(id);
        }

        public EmployeeDomainModel Get(int id)
        {
            string key = $"{typeof(EmployeeDomainModel)} {id}";
            if (!_memoryCache.TryGetValue(key, out EmployeeDomainModel employee))
            {
                employee = _employeeRepository.Get(id);
                _memoryCache.Set(key, employee, CreateCacheOptions());
            }

            return employee;
        }

        public IEnumerable<EmployeeDomainModel> GetAll()
        {
            if (!_memoryCache.TryGetValue(EmployeesCacheKeys.AllEmployeesKey, out List<EmployeeDomainModel> employeeList))
            {
                employeeList = _employeeRepository.GetAll().ToList();
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

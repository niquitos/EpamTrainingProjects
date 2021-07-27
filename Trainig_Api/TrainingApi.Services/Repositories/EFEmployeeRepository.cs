
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Services.Context;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class EFEmployeeRepository : IDataRepository<EmployeeDomainModel>
    {
        private EmployeeContext _db;
        private bool _disposed = false;

        public EFEmployeeRepository(IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<EmployeeContext>().UseSqlServer(configuration["ConnectionStrings:Sql"]).Options;
            _db = new EmployeeContext(options);
        }

        public IEnumerable<EmployeeDomainModel> GetAll()
        {
            return _db.Employees.ToList();
        }

        public EmployeeDomainModel Get(int id)
        {
            return _db.Employees.Find(id);
        }

        public void Create(EmployeeDomainModel item)
        {
            _db.Employees.Add(item);
            Save();
        }

        public void Update(EmployeeDomainModel item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            EmployeeDomainModel item = _db.Employees.Find(id);
            if (item != null) _db.Employees.Remove(item);
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

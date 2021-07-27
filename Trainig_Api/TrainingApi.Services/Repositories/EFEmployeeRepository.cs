
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Services.Context;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class EFEmployeeRepository : IDataRepository<EmployeeDomainModel>
    {
        private readonly EmployeeContext _db;

        public EFEmployeeRepository(EmployeeContext db)
        {
            _db = db;
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
    }
}


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
            return _db.Employee.ToList();
        }

        public EmployeeDomainModel Get(int id)
        {
            return _db.Employee.Find(id);
        }

        public void CreateImmediately(EmployeeDomainModel item)
        {
            _db.Employee.Add(item);
            Save();
        }

        public void DeleteImmediately(int id)
        {
            EmployeeDomainModel item = _db.Employee.Find(id);
            if (item != null) _db.Employee.Remove(item);
            Save();
        }

        private void Save()
        {
            _db.SaveChanges();
        }
    }
}

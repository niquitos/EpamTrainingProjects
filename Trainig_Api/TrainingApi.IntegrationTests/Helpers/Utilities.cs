using System.Collections.Generic;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Repositories;

namespace TrainingApi.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(IDataRepository<EmployeeDomainModel> db)
        {
            var list = GetDummyEmployees();
            foreach (var emp in list)
            {
                db.CreateImmediately(emp);
            }
        }

        public static List<EmployeeDomainModel> GetDummyEmployees()
        {
            var list = new List<EmployeeDomainModel>
            {
                new EmployeeDomainModel { EmployeeId = 111, FirstName = "aaa", LastName = "aaa", Age = 111, EmailAddress = "aaa@aaa.com" },
                new EmployeeDomainModel { EmployeeId = 222, FirstName = "bbb", LastName = "bbb", Age = 222, EmailAddress = "bbb@aaa.com" },
                new EmployeeDomainModel { EmployeeId = 222, FirstName = "ccc", LastName = "ccc", Age = 333, EmailAddress = "ccc@aaa.com" }
            };

            return list;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Models;
using TrainingApi.Services;

namespace TrainingApi.Tests
{
    [TestFixture]
    public class EFDataServiceTest
    {
        string _connectionString;
        private IDataService<EmployeeModel> _ds;
        int _count;

        [SetUp]
        public void Setup_EF()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrainingApiDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            var options = new DbContextOptionsBuilder<EmployeeContext>().UseSqlServer(_connectionString).Options;
            var dbContext = new EmployeeContext(options);
            _ds = new EntFrDataService<EmployeeModel, EmployeeContext>(dbContext);

            _count = 4;
        }

        [Test]
        public void EF_GetData_Test()
        {
            Setup_EF();

            IEnumerable<EmployeeModel> employeeList = _ds.GetData();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(_count, employeeList.Count());
        }
    }
}

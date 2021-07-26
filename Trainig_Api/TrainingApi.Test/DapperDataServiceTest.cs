using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Models;
using TrainingApi.Services;

namespace TrainingApi.Tests
{
    [TestFixture]
    public class DapperDataServiceTest
    {
        private Mock<IConfiguration> _configMock;
        string _connectionString;
        private IDataService<EmployeeModel> _ds;
        int _count;

        [SetUp]
        public void Setup_Dapper()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrainingApiDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(r => r["ConnectionStrings:Sql"]).Returns(_connectionString);

            _ds = new DapperDataService<EmployeeModel>(_configMock.Object);

            _count = 4;
        }

        [Test]
        public void Dapper_GetData_Test()
        {
            Setup_Dapper();

            IEnumerable<EmployeeModel> employeeList = _ds.GetData();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(_count, employeeList.Count());
        }
    }
}

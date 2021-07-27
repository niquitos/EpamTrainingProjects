using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Repositories;

namespace TrainingApi.Tests
{
    [TestFixture]
    public class CsvEmployeeRepositoryTest
    {
        private Mock<IConfiguration> _configMock;
        string _connectionString;
        private IDataRepository<EmployeeDomainModel> _dr;

        [SetUp]
        public void Setup()
        {
            _connectionString = "Data\\DataCsv.csv";

            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(r => r["ConnectionStrings:Csv"]).Returns(_connectionString);

            _dr = new CsvEmployeeRepository(_configMock.Object);
        }

        [Test]
        public void CsvEmployeeRepository_GetData_IsNotNullAndCountIsMoreThanZero()
        {
            List<EmployeeDomainModel> employeeList = _dr.GetAll().ToList();
            int count = employeeList.Count;

            Assert.IsNotNull(employeeList);
            Assert.IsTrue(count > 0);
        }

        [Test]
        public void CsvEmployeeRepository_Create_NotNullAndCountIncremented()
        {
            List<EmployeeDomainModel> employeeList = _dr.GetAll().ToList();
            int count = employeeList.Count + 1;
            var lastItem = employeeList.Last();
            var item = new EmployeeDomainModel()
            {
                Id = ++lastItem.Id,
                EmployeeId = ++lastItem.EmployeeId,
                FirstName = "Abra",
                LastName = "Cadabra",
                Age = 100,
                EmailAddress = "abracadabra@something.com"
            };
            _dr.Create(item);
            employeeList = _dr.GetAll().ToList();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(count, employeeList.Count);
        }

        [Test]
        public void CsvEmployeeRepository_Delete_NotNullAndCountDecremented()
        {
            List<EmployeeDomainModel> employeeList = _dr.GetAll().ToList();
            int count = employeeList.Count - 1;
            var lastItem = employeeList.Last();
            _dr.Delete(lastItem.Id);
            employeeList = _dr.GetAll().ToList();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(count, employeeList.Count);
        }
    }
}

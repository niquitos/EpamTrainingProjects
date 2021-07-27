using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Repositories;

namespace TrainingApi.Tests
{
    public class DapperEmployeeRepositoryTest
    {
        private Mock<IConfiguration> _configMock;
        string _connectionString;
        private IDataRepository<EmployeeDomainModel> _dr;

        //[SetUp]
        public void Setup()
        {
            _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrainingApiDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(r => r["ConnectionStrings:Sql"]).Returns(_connectionString);

            _dr = new DapperEmployeeRepository(_configMock.Object);
        }

        //[Test]
        public void EFEmployeeRepository_GetData_IsNotNullAndCountIsMoreThanZero()
        {
            List<EmployeeDomainModel> employeeList = _dr.GetAll().ToList();
            int count = employeeList.Count;

            Assert.IsNotNull(employeeList);
            Assert.IsTrue(count > 0);
        }

        //[Test]
        public void EFEmployeeRepository_Create_NotNullAndCountIncremented()
        {
            List<EmployeeDomainModel> employeeList = _dr.GetAll().ToList();
            int count = employeeList.Count + 1;
            var lastItem = employeeList.Last();
            var item = new EmployeeDomainModel()
            {
                EmployeeId = ++lastItem.EmployeeId,
                FirstName = "Abra",
                LastName = "Cadabra",
                Age = 100,
                EmailAddress = "abracadabra@something.com"
            };
            _dr.CreateImmediately(item);
            employeeList = _dr.GetAll().ToList();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(count, employeeList.Count);
        }

        //[Test]
        public void EFEmployeeRepository_Delete_NotNullAndCountDecremented()
        {
            List<EmployeeDomainModel> employeeList = _dr.GetAll().ToList();
            int count = employeeList.Count - 1;
            var lastItem = employeeList.Last();
            _dr.DeleteImmediately(lastItem.Id);
            employeeList = _dr.GetAll().ToList();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(count, employeeList.Count);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TrainingApi.Mapping;
using TrainingApi.Models;
using TrainingApi.Services;

namespace TrainingApi.Tests
{
    [TestFixture]
    public class CsvDataServiceTest
    {
        private Mock<IConfiguration> _configMock;
        string _connectionString;
        private IDataService<EmployeeModel> _ds;
        int _count;

        [SetUp]
        public void Setup_Csv()
        {
            _connectionString = "Data\\DataCsv.csv";

            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(r => r["ConnectionStrings:Csv"]).Returns(_connectionString);

            _ds = new CsvDataService<EmployeeModel, EmployeeModelMap>(_configMock.Object);

            _count = 5;
        }

        [Test]
        public void CsvDataService_GetData_IsNotNullAndCountEqualsFromSetUp()
        {
            Setup_Csv();

            IEnumerable<EmployeeModel> employeeList = _ds.GetData();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(_count, employeeList.Count());
        }
    }
}

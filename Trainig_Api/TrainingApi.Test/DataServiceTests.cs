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
    public class DataServiceTests
    {
        private Mock<IConfiguration> _configMock;
        string _connectionString;
        private IDataService<EmployeeModel> _ds;
        int _count;

        public DataServiceTests()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrainingApiDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(r => r["ConnectionStrings:Sql"]).Returns(_connectionString);

            _ds = new SqlDataService<EmployeeModel>(_configMock.Object);

            _ds.CreateInstance = CreateInstanceSql;

            _count = 4;
        }

        //[SetUp]
        public void Setup()
        {
            _connectionString = "Data\\DataCsv.csv";

            _configMock.Setup(r => r["ConnectionStrings:Csv"]).Returns(_connectionString);

            _ds = new CsvDataService<EmployeeModel, EmployeeModelMap>(_configMock.Object);

            _count = 5;
        }

        [Test]
        public void Sql_GetData_Test()
        {
            IEnumerable<EmployeeModel> employeeList = _ds.GetData();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(_count, employeeList.Count());
        }

        [Test]
        public void Csv_GetData_Test()
        {
            Setup();

            IEnumerable<EmployeeModel> employeeList = _ds.GetData();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(_count, employeeList.Count());
        }

        private EmployeeModel CreateInstanceSql(DataRow row)
        {
            return new EmployeeModel
            {
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Age = Convert.ToInt32(row["Age"]),
                EmailAdress = row["EmailAdress"].ToString()
            };
        }
    }
}

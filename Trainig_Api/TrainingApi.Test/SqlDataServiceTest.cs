using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TrainingApi.Models;
using TrainingApi.Services;

namespace TrainingApi.Tests
{
    [TestFixture]
    public class SqlDataServiceTest
    {
        [Test]
        public void GetData_Test()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrainingApiDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(r => r["ConnectionStrings:Sql"]).Returns(connectionString);

            var ss = new SqlDataService<EmployeeModel>(configMock.Object);
            ss.CreateInstance = CreateInstanceSql;

            IEnumerable<EmployeeModel> employeeList = ss.GetData();

            Assert.IsNotNull(employeeList);
            Assert.AreEqual(4, employeeList.Count());
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

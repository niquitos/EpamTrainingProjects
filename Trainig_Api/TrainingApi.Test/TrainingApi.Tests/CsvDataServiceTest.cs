using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using TrainingApi.Mapping;
using TrainingApi.Models;
using TrainingApi.Services;

namespace TrainingApi.Tests
{
    [TestFixture]
    public class CsvDataServiceTest
    {
        [Test]
        public void GetData_Test()
        {
            string connectionString = "Data\\DataCsv.csv";

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(r => r["ConnectionStrings:Csv"]).Returns(connectionString);

            var dc = new ScvDataService<EmployeeModel, EmployeeModelMap>(configMock.Object);

            var employeeList = dc.GetData();

            Assert.IsNotNull(dc.Data);
            Assert.AreEqual(employeeList, dc.Data);
        }
    }
}

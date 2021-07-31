using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApi.IntegrationTests
{
    public class EmployeeControllerTests
    {
        private readonly HttpClient _client;

        public EmployeeControllerTests()
        {
            var appFactory = new WebApplicationFactory <Startup>();
            _client = appFactory.CreateClient();
        }

        [Test]
        public async Task EmployeeController_GetEmployeePage_IsNotNullAndPrintTheContentToOutput()
        {
            HttpResponseMessage result = await _client.GetAsync("https://localhost:44392/Employee/Index");
            string s = await result.Content.ReadAsStringAsync();
            TestContext.WriteLine(s);
            Assert.IsNotNull(result);

        }

    }
}

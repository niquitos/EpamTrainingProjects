using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApi.IntegrationTests.Basic
{
    public class IntegrationTests
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTests()
        {
            _factory = new WebApplicationFactory<Startup>();
        }
        
        [TestCase("/")]
        [TestCase("Home/Privacy")]
        [TestCase("Home/Error")]
        [TestCase("Employee/Index")]
        [TestCase("Employee/Create")]
        public async Task Integration_Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}

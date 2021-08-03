using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TrainingApi.IntegrationTests.Helpers;
using TrainingApi.Services.Context;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Repositories;

namespace TrainingApi.IntegrationTests.Basic
{
    public class IntegrationTests
    {
        private WebApplicationFactory<Startup> _factory;

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

        [Test]
        public async Task GetAllEmployees_CountIsMoreThanThree()
        {
            // Arrange
            var client = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(EmployeeContext));
                    services.RemoveAll(typeof(IDataRepository<EmployeeDomainModel>));
                    services.AddDbContext<EmployeeContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                    services.AddScoped<IDataRepository<EmployeeDomainModel>, EFEmployeeRepository>();
                    var serviceProvider = services.BuildServiceProvider();
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var repository = scopedServices.GetRequiredService<IDataRepository<EmployeeDomainModel>>();
                        var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<Startup>>>();

                        try
                        {
                            Utilities.InitializeDbForTests(repository);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred getting the Database employees: {Message}", ex.Message);
                        }

                    }
                });
            }).CreateClient();

            //Act
            System.Net.Http.HttpResponseMessage defaultPage = await client.GetAsync("Employee/Index");
            IHtmlDocument content = await HtmlHelper.GetDocumentAsync(defaultPage);

            var tableEntries = content.QuerySelector("tbody").ChildElementCount;
            var message = content.QuerySelector("tbody").TextContent;
            // Assert
            Assert.IsTrue(tableEntries > 2);
        }
    }
}

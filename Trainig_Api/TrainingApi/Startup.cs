using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainingApi.Mapping;
using TrainingApi.Models;
using TrainingApi.Services;

namespace TrainingApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddSingleton<IDataService<EmployeeModel>, CsvDataService<EmployeeModel, EmployeeModelMap>>();

            //services.AddSingleton<IDataService<EmployeeModel>, SqlDataService<EmployeeModel>>();

            //var options = new DbContextOptionsBuilder<EmployeeContext>().UseSqlServer(Configuration["ConnectionStrings:Sql"]).Options;
            //var dbContext = new EmployeeContext(options);
            //services.AddSingleton<IDataService<EmployeeModel>>(new EntFrDataService<EmployeeModel,EmployeeContext>(dbContext));

            services.AddSingleton<IDataService<EmployeeModel>, DapperDataService<EmployeeModel>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

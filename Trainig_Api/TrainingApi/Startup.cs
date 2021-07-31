using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainingApi.Mapping;
using TrainingApi.Services.Context;
using TrainingApi.Services.DomainModels;
using TrainingApi.Services.Repositories;
using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

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

            services.AddSingleton<IMapper, Mapper>(mapper => new Mapper(new MapperConfiguration(cfg =>
                                                                        {
                                                                            cfg.AddProfile(new EmployeeModelToDtoProfile());
                                                                            cfg.AddProfile(new DtoToEmployeeModelProfile());
                                                                        })));

            //ef implementation
            //services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Sql"]));
            //services.AddScoped<IDataRepository<EmployeeDomainModel>, EFEmployeeRepository>();

            //Dapper implementation
            //services.AddScoped<IDataRepository<EmployeeDomainModel>, DapperEmployeeRepository>();

            //csv implementation
            services.AddScoped<IDataRepository<EmployeeDomainModel>, CsvEmployeeRepository>();

            services.AddSwaggerGen(c=> 
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Version ="v1",
                    Title = "Training API",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Alexander Nikitin"
                    },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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

            
            app.UseSwagger(c=> { c.SerializeAsV2 = true; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Training API V1");
            });

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

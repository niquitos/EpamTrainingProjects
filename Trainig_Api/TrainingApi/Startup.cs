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

            services.AddSwaggerGen();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
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

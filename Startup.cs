using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainingApi.Data;

namespace TrainingApi
{
    /// <summary>
    /// Stores configuration, adds services and configures HTTP request pipeline
    /// </summary>
    public class Startup
    {
        #region Properties
        /// <summary>
        /// A set of key-value application configuration properties privided by the constructor
        /// </summary>
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Startup constructor. Accepts configuration and saves it Configuration property
        /// </summary>
        /// <param name="configuration">A set of key-value application configuration properties</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Adds services to the container
        /// </summary>
        /// <param name="services">A collection of service descriptors</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        /// <summary>
        /// Configures the HTTp request pipeline
        /// </summary>
        /// <param name="app">A class that provides the mechanisms to configure an application's request pipeline</param>
        /// <param name="env">Information about webhosting environment an application is running in</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        #endregion
    }
}

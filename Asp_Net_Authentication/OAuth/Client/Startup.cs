using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config=> 
            {
                config.DefaultAuthenticateScheme = "ClientCookie";
                config.DefaultSignInScheme = "ClientCookie";
                config.DefaultChallengeScheme = "OurServer";
            })
            .AddCookie("ClientCookie")
            .AddOAuth("OurServer", config =>
            {
                config.ClientId = "client_id";
                config.ClientSecret = "client_secret";
                config.CallbackPath = "/oauth/callback";
                config.AuthorizationEndpoint = "https://localhost:44377/oauth/authorize";
                config.TokenEndpoint = "https://localhost:44377/oauth/token";
            });
            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config=> 
            {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookie")
            .AddOpenIdConnect("oidc", config=> 
            {
                config.Authority = "https://localhost:44360/";
                config.ClientId = "client_id_mvc";
                config.ClientSecret = "client_secret_mvc";
                config.SaveTokens = true;
                config.ResponseType = "code";
                
                config.SignedOutCallbackPath = "/Home/Index";

                //configure Cookie claim mapping
                config.ClaimActions.DeleteClaim("s_hash");
                config.ClaimActions.MapUniqueJsonKey("AlexNikitin.Grandma", "an.grandma");

                //trips to load claims into the cookie
                //but the id token is smaller
                config.GetClaimsFromUserInfoEndpoint = true;

                //configure scopes
                config.Scope.Clear();
                config.Scope.Add("openid");
                config.Scope.Add("an.scope");
                config.Scope.Add("ApiOne");
                config.Scope.Add("ApiTwo");
                config.Scope.Add("offline_access");
            });

            services.AddHttpClient();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

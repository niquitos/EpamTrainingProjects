using IdentityModel.OidcClient;
using Prism.Ioc;
using System.Windows;
using WpfClient.Infrastructure.Browser;
using WpfClient.Infrastructure.Interfaces;
using WpfClient.Infrastructure.Services;
using WpfClient.Views;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var options = new OidcClientOptions()
            {
                Authority = "https://localhost:44360/",
                ClientId = "client_id_wpf",
                Scope = "openid ApiOne",
                RedirectUri = "http://localhost/sample-wpf-app",
                Browser = new WpfEmbeddedBrowser(Container)
            };

            containerRegistry.RegisterSingleton<IAuthorizationService>(() => new AuthorizationService(options));
        }
    }
}

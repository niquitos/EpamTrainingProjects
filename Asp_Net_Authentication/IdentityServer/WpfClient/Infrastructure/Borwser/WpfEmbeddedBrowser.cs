
using IdentityModel.OidcClient.Browser;
using Prism.Ioc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WpfClient.Views;

namespace WpfClient.Infrastructure.Browser
{
    public class WpfEmbeddedBrowser : IBrowser
    {
        private readonly IContainerProvider _containerProvider;
        private BrowserOptions _options = null;

        public WpfEmbeddedBrowser(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            _options = options;

            var window = _containerProvider.Resolve<WpfBrowserWindow>();
            window.Width = 900;
            window.Height = 625;
            window.Title = "IdentityServer Demo Login";

            var signal = new SemaphoreSlim(0, 1);

            var result = new BrowserResult()
            {
                ResultType = BrowserResultType.UserCancel
            };

            window._wpfBrowser.Navigating += (s, e) =>
            {
                if (BrowserIsNavigatingToRedirectUri(e.Uri))
                {
                    e.Cancel = true;

                    result = new BrowserResult()
                    {
                        ResultType = BrowserResultType.Success,
                        Response = e.Uri.AbsoluteUri
                    };

                    signal.Release();

                    window.Close();
                }
            };

            window.Closing += (s, e) =>
            {
                signal.Release();
            };

            window.Show();
            window._wpfBrowser.Source = new Uri(_options.StartUrl);

            await signal.WaitAsync();

            return result;
        }

        private bool BrowserIsNavigatingToRedirectUri(Uri uri)
        {
            return uri.AbsoluteUri.StartsWith(_options.EndUrl);
        }
    }
}
using Prism.Commands;
using Prism.Mvvm;
using System.Net.Http;
using System.Net.Http.Headers;
using WpfClient.Infrastructure.Interfaces;

namespace WpfClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Wpf Client";
        private readonly IAuthorizationService _authorizationService;
        private readonly HttpClient _httpClient;
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DelegateCommand _authorizeCommand;
        public DelegateCommand AuthorizeCommand => _authorizeCommand ??= new DelegateCommand(ExecuteAuthorizeCommandAsync);

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MainWindowViewModel(IAuthorizationService authorizationService, HttpClient httpClient)
        {
            Message = "Authorization...";
            _authorizationService = authorizationService;
            _httpClient = httpClient;
        }

        private async void ExecuteAuthorizeCommandAsync()
        {
            var result = await _authorizationService.AuthorizeAsync();



            if (result.IsError)
            {
                Message = result.Error == "UserCancel" ? "The sign-in window was closed before authorization was completed." : result.Error;
            }
            else
            {
                var name = result.User.Identity.Name;

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue($"Bearer", result.AccessToken);

                var response = await _httpClient.GetAsync("https://localhost:44312/secret");

                Message = await response.Content.ReadAsStringAsync();
            }
        }
    }
}

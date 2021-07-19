using Prism.Regions;
using SqlProject.CoreLibrary.Mvvm;

namespace SqlProject.Modules.Second.ViewModels
{
    public class SecondTestViewModel : ActiveAwareViewModelBase
    {
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set { _ = SetProperty(ref _welcomeMessage, value); }
        }

        public SecondTestViewModel(IRegionManager regionManager) : base(regionManager)
        {
            WelcomeMessage = "Second module is in progress. Homework content will appear here as soon as it is done.";
        }
    }
}

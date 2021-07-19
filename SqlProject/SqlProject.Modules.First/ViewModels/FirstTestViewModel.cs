using Prism.Regions;
using SqlProject.CoreLibrary.Mvvm;

namespace SqlProject.Modules.First.ViewModels
{
    public class FirstTestViewModel : ActiveAwareViewModelBase
    {
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set { _ = SetProperty(ref _welcomeMessage, value); }
        }

        public FirstTestViewModel(IRegionManager regionManager) : base(regionManager)
        {
            WelcomeMessage = "First Module was an over view lesson... so no homework was done here...";
        }
    }
}

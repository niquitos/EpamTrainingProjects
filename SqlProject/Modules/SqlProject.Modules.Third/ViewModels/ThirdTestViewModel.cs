using Prism.Regions;
using SqlProject.CoreLibrary.Mvvm;

namespace SqlProject.Modules.Third.ViewModels
{
    public class ThirdTestViewModel : ActiveAwareViewModelBase
    {
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set => _ = SetProperty(ref _welcomeMessage, value);
        }

        public ThirdTestViewModel(IRegionManager regionManager) : base(regionManager)
        {
            WelcomeMessage = "Third homework is empty for now.";
        }
    }
}

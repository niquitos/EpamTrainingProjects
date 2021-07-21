using Prism.Ioc;
using Prism.Modularity;
using SqlProject.Modules.Third.ViewModels;
using SqlProject.Modules.Third.Views;

namespace SqlProject.Modules.Third
{
    public class ThirdModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ThirdTest, ThirdTestViewModel>("ThirdTestView");
        }
    }
}

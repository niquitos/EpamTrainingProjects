using Prism.Ioc;
using Prism.Modularity;
using SqlProject.Modules.Second.ViewModels;
using SqlProject.Modules.Second.Views;

namespace SqlProject.Modules.Second
{
    public class SecondModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SecondTest, SecondTestViewModel>("SecondTestView");
        }
    }
}

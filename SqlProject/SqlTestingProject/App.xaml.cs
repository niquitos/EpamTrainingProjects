using Prism.Ioc;
using Prism.Modularity;
using SqlTestingProject.ViewModels;
using System.Windows;

namespace SqlTestingProject
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
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<SqlProject.Modules.First.FirstModule>();
            moduleCatalog.AddModule<SqlProject.Modules.Second.SecondModule>();
        }
    }
}

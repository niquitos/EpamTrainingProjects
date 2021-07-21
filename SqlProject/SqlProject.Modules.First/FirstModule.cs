using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SqlProject.CoreLibrary.Regions;
using SqlProject.Modules.First.Infrastructure.Repositories;
using SqlProject.Modules.First.ViewModels;
using SqlProject.Modules.First.Views;
using SqlProject.Services.Data;

namespace SqlProject.Modules.First
{
    public class FirstModule : IModule
    {
        public IRegionManager RegionManager { get; }
        public FirstModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, "FirstTestView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FirstTest, FirstTestViewModel>("FirstTestView");
            containerRegistry.RegisterSingleton<IDataTablesRepository, DataTablesRepository>();

            //read from csv
            containerRegistry.RegisterSingleton<IDataService, CsvDataService>();

            //read from sql
            //containerRegistry.RegisterSingleton<IDataService, SqlDataService>();
        }
    }
}

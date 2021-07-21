using Microsoft.Data.SqlClient;
using Prism.Commands;
using Prism.Regions;
using SqlProject.CoreLibrary.Mvvm;
using SqlProject.Modules.First.Infrastructure.Repositories;
using SqlProject.Services.Data;
using System.Data;

namespace SqlProject.Modules.First.ViewModels
{
    public class FirstTestViewModel : ActiveAwareViewModelBase
    {
        
        public IDataTablesRepository DataTablesRepository { get; }
        public IDataService DataService { get; }

        #region Raising properties
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set { _ = SetProperty(ref _welcomeMessage, value); }
        }
        #endregion

        #region Commands
        private DelegateCommand _connectToDataBase;
        public DelegateCommand ConnectToDataBase => _connectToDataBase ??= new DelegateCommand(ExecuteConnectToDataBase);
        #endregion

        public FirstTestViewModel(IRegionManager regionManager, IDataTablesRepository dataTablesRepository,
                                  IDataService dataService) : base(regionManager)
        {
            WelcomeMessage = "First Module was an over view lesson... so no homework was done here...";
            DataTablesRepository = dataTablesRepository;
            DataService = dataService;
        }

        private void ExecuteConnectToDataBase()
        {
            DataTablesRepository.RecipesList = DataService.GetData();
        }
    }
}

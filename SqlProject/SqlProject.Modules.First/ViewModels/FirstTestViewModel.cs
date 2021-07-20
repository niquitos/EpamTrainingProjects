using Microsoft.Data.SqlClient;
using Prism.Commands;
using Prism.Regions;
using SqlProject.CoreLibrary.Mvvm;
using SqlProject.Modules.First.Infrastructure.Repositories;
using System.Data;

namespace SqlProject.Modules.First.ViewModels
{
    public class FirstTestViewModel : ActiveAwareViewModelBase
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=D:\EPAM\TRAININGAPI\TRAININGAPI\SQLPROJECT\SQLPROJECT.MODULES.FIRST\DATA\COOKBOOK.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection _connection;

        public IDataTablesRepository DataTablesRepository { get; }

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

        public FirstTestViewModel(IRegionManager regionManager, IDataTablesRepository dataTablesRepository) : base(regionManager)
        {
            WelcomeMessage = "First Module was an over view lesson... so no homework was done here...";
            DataTablesRepository = dataTablesRepository;
        }

        private void ExecuteConnectToDataBase()
        {
            using (_connection = new SqlConnection(_connectionString))
            using (SqlDataAdapter adapter = new("SELECT * FROM Recipe", _connection))
            {
                _connection.Open();
                DataTable recipeTable = new();
                adapter.Fill(recipeTable);
                DataTablesRepository.RecipesList = recipeTable;
            }
        }
    }
}

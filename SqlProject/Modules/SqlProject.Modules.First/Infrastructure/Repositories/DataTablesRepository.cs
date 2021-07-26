using Prism.Mvvm;
using System.Data;

namespace SqlProject.Modules.First.Infrastructure.Repositories
{

    public class DataTablesRepository : BindableBase, IDataTablesRepository
    {
        private DataTable _recipes;
        public DataTable RecipesList
        {
            get => _recipes;
            set => _ = SetProperty(ref _recipes, value);
        }
    }
}

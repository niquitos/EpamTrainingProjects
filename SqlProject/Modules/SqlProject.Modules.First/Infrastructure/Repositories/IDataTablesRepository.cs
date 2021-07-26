using System.Data;

namespace SqlProject.Modules.First.Infrastructure.Repositories
{
    public interface IDataTablesRepository
    {
        DataTable RecipesList { get; set; }
    }
}

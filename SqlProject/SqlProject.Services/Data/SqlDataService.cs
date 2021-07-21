using Microsoft.Data.SqlClient;
using System.Data;

namespace SqlProject.Services.Data
{
    public class SqlDataService : IDataService
    {
        public string Source { get; set; } = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Recipes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DataTable? GetData()
        {
            using (SqlConnection sq = new(Source))
            using (SqlDataAdapter adapter = new("SELECT * FROM Recipe", sq))
            {
                DataTable recipeTable = new();
                adapter.Fill(recipeTable);
                return recipeTable;
            }
        }
    }
}

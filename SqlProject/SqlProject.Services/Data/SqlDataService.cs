using Microsoft.Data.SqlClient;
using System.Data;

namespace SqlProject.Services.Data
{
    public class SqlDataService : IDataService
    {
        public DataTable GetData(string connectionString)
        {
            using (SqlConnection sq = new(connectionString))
            using (SqlDataAdapter adapter = new("SELECT * FROM dbo.Employee", sq))
            {
                DataTable recipeTable = new();
                adapter.Fill(recipeTable);
                return recipeTable;
            }
        }
    }
}

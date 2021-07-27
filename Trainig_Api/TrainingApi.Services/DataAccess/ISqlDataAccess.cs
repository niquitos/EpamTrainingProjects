using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TrainingApi.Services.DataAccess
{
    public interface ISqlDataAccess
    {
        IConfiguration Configuration { get; }
        string ConnectionString { get; set; }

        string GetConnectionString(string connectionName = "ConnectionStrings:Sql");
        IEnumerable<T> LoadData<T>(string sql);
        int SaveData<T>(string sql, T data);
    }
}
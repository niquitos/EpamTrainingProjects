using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TrainingApi.Services.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public string ConnectionString { get; set; }
        public IConfiguration Configuration { get; }

        public SqlDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = GetConnectionString();
        }

        public string GetConnectionString(string connectionName = "ConnectionStrings:Sql")
        {
            return Configuration[connectionName];
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            using IDbConnection cnn = new SqlConnection(ConnectionString);
            return cnn.Query<T>(sql).ToList();
        }

        public int SaveData<T>(string sql, T data)
        {
            using IDbConnection cnn = new SqlConnection(ConnectionString);
            return cnn.Execute(sql, data);
        }
    }
}

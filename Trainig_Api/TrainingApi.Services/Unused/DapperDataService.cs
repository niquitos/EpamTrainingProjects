using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace TrainingApi.Services
{
    public class DapperDataService<TModel> : IDataService<TModel>
    {
        public string ConnectionString { get; set; }
        public Func<DataRow, TModel> CreateInstance { get; set; }

        public DapperDataService(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionStrings:Sql"];
        }


        public void Configure(Func<DataRow, TModel> action)
        {

        }

        public IEnumerable<TModel> GetData()
        {
            return GetData(CultureInfo.InvariantCulture);
        }

        public IEnumerable<TModel> GetData(CultureInfo culture)
        {
            string command = "SELECT * FROM dbo.Employee";
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                return cnn.Query<TModel>(command).ToList();
            }
        }

        public void Save(IEnumerable<TModel> data)
        {

        }

        public void Save(IEnumerable<TModel> data, CultureInfo culture)
        {

        }
    }
}

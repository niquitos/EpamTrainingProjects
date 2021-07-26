using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace TrainingApi.Services
{
    public class SqlDataService<TModel> : IDataService<TModel>
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }

        public List<TModel> Data { get; set; } = new List<TModel>();

        public Func<DataRow, TModel> CreateInstance { get; set; }

        public SqlDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionStrings:Sql"];
        }

        public void Configure(Func<DataRow, TModel> action)
        {
            if (action == null) return;
            CreateInstance = action;
        }

        public List<TModel> GetData()
        {
            return GetData(CultureInfo.InvariantCulture);
        }

        public List<TModel> GetData(CultureInfo culture)
        {
            using (SqlConnection sq = new(ConnectionString))
            using (SqlDataAdapter adapter = new("SELECT * FROM dbo.Employee", sq))
            {
                DataTable recipeTable = new();
                adapter.Fill(recipeTable);
                foreach (DataRow row in recipeTable.Rows)
                {
                    if (CreateInstance != null) Data.Add(CreateInstance.Invoke(row));
                }
                return Data;
            }
        }

        public void WriteData(List<TModel> data) => WriteData(data, CultureInfo.InvariantCulture);

        public void WriteData(List<TModel> data, CultureInfo culture)
        {

        }

    }
}

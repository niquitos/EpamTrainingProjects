using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TrainingApi.Services
{
    public class CsvDataService<TModel, TMap> : IDataService<TModel> where TMap : ClassMap<TModel>
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }

        public Func<DataRow, TModel> CreateInstance { get; set; }

        public CsvDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionStrings:Csv"];
        }

        public IEnumerable<TModel> GetData()
        {
            return GetData(CultureInfo.InvariantCulture);
        }

        public IEnumerable<TModel> GetData(CultureInfo culture)
        {
            using (StreamReader sr = new(ConnectionString))
            using (CsvReader scvReader = new(sr, culture))
            {
                scvReader.Context.RegisterClassMap<TMap>();
                return scvReader.GetRecords<TModel>().ToList();
            }
        }

        public void Save(IEnumerable<TModel> data) => Save(data, CultureInfo.InvariantCulture);

        public void Save(IEnumerable<TModel> data, CultureInfo culture)
        {
            using (StreamWriter sw = new(ConnectionString))
            using (CsvWriter csvWriter = new(sw, culture))
            {
                csvWriter.WriteRecords(data);
            }
        }

        public void Configure(Func<DataRow, TModel> action)
        {
            CreateInstance = action;
        }
    }
}

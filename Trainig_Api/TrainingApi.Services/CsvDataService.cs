using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TrainingApi.Services
{
    public class ScvDataService<TModel, TMap> : IDataService<TModel> where TMap : ClassMap<TModel>
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }

        public List<TModel> Data { get; set; }

        public ScvDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionStrings:Csv"];
        }

        public List<TModel> GetData()
        {
            return GetData(CultureInfo.InvariantCulture);
        }

        public List<TModel> GetData(CultureInfo culture)
        {
            using (StreamReader sr = new(ConnectionString))
            using (CsvReader scvReader = new(sr, culture))
            {
                scvReader.Context.RegisterClassMap<TMap>();
                Data = scvReader.GetRecords<TModel>().ToList();
                return Data;
            }
        }

        public void WriteData(List<TModel> data) => WriteData(data, CultureInfo.InvariantCulture);

        public void WriteData(List<TModel> data, CultureInfo culture)
        {
            using (StreamWriter sw = new(ConnectionString))
            using (CsvWriter csvWriter = new(sw, culture))
            {
                csvWriter.WriteRecords(data);
            }
        }

    }
}

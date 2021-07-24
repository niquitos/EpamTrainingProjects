using System.Collections.Generic;
using System.Globalization;

namespace TrainingApi.Services
{
    public interface IDataService<TModel>
    {
        string ConnectionString { get; set; }
        List<TModel> Data { get; set; }

        List<TModel> GetData();
        List<TModel> GetData(CultureInfo culture);
        void WriteData(List<TModel> data);
        void WriteData(List<TModel> data, CultureInfo culture);
    }
}
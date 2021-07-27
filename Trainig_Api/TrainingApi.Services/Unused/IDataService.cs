using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace TrainingApi.Services
{
    public interface IDataService<TModel>
    {
        string ConnectionString { get; set; }
        Func<DataRow, TModel> CreateInstance { get; set; }

        void Configure(Func<DataRow, TModel> action);
        IEnumerable<TModel> GetData();
        IEnumerable<TModel> GetData(CultureInfo culture);
        void Save(IEnumerable<TModel> data);
        void Save(IEnumerable<TModel> data, CultureInfo culture);
    }
}
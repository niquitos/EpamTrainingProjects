using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace TrainingApi.Services
{
    public class EntFrDataService<TModel, TContext> : IDataService<TModel> where TContext : DbContext
                                                                           where TModel : class
    {
        private readonly IConfiguration _configuration;
        private readonly TContext _context;

        public string ConnectionString { get; set; }
        public Func<DataRow, TModel> CreateInstance { get; set; }

        public EntFrDataService(TContext context)
        {
            _context = context;
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
            DbSet<TModel> set = _context.Set<TModel>();
            var list = new List<TModel>();
            var asyncSet = set.AsAsyncEnumerable<TModel>();
            Task task = Task.Run(async () =>
            {
                try
                {
                    await foreach (var entity in asyncSet)
                    {
                        list.Add(entity);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
            task.Wait();

            return list;
        }

        public void Save(IEnumerable<TModel> data)
        {

        }

        public void Save(IEnumerable<TModel> data, CultureInfo culture)
        {

        }
    }
}

using System;
using System.Collections.Generic;

namespace TrainingApi.Services.Repositories
{
    public interface IDataRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
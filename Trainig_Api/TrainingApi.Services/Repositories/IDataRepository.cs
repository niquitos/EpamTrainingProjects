using System.Collections.Generic;

namespace TrainingApi.Services.Repositories
{
    public interface IDataRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void CreateImmediately(T item);
        void DeleteImmediately(int id);
    }
}
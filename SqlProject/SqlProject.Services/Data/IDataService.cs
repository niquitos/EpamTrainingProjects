using System.Data;

namespace SqlProject.Services.Data
{
    public interface IDataService
    {
        DataTable GetData(string connectionString);
    }
}
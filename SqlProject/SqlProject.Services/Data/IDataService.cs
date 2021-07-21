using System.Data;

namespace SqlProject.Services.Data
{
    public interface IDataService
    {
        string Source { get; set; }

        DataTable? GetData();
    }
}
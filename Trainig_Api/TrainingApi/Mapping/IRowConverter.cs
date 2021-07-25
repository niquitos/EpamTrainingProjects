using System.Data;
using TrainingApi.Models;

namespace TrainingApi.Mapping
{
    public interface IRowConverter
    {
        EmployeeModel Map(DataRow row);
    }
}
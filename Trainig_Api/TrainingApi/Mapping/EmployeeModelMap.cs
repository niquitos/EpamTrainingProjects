using CsvHelper.Configuration;
using TrainingApi.Models;

namespace TrainingApi.Mapping
{
    public class EmployeeModelMap : ClassMap<EmployeeModel>
    {
        public EmployeeModelMap()
        {
            Map(m => m.EmployeeId).Name("EmployeeId");
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.LastName).Name("LastName");
            Map(m => m.Age).Name("Age");
            Map(m => m.EmailAddress).Name("EmailAddress");
        }
    }
}

using CsvHelper.Configuration;
using TrainingApi.Models;

namespace TrainingApi.Mapping
{
    public class EmployeeModelMap : ClassMap<EmployeeModel>
    {
        public EmployeeModelMap()
        {
            Map(m => m.EmployeeId).Name("Id");
            Map(m => m.FirstName).Name("Name");
            Map(m => m.LastName).Name("Surname");
            Map(m => m.Age).Name("Age");
            Map(m => m.EmailAdress).Name("Email");
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using TrainingApi.Models;
using TrainingApi.Services.DataAccess;

namespace TrainingApi.Mapping
{
    public class EmployeeProcessor : IEmployeeProcessor
    {
        public ISqlDataAccess SqlDataAccess { get; }

        public EmployeeProcessor(ISqlDataAccess sqlDataAccess)
        {
            SqlDataAccess = sqlDataAccess;
        }

        public IEnumerable<EmployeeModel> LoadEmployees()
        {
            string sql = @"select * from dbo.Employee;";

            return SqlDataAccess.LoadData<EmployeeModel>(sql).ToList();
        }

        public int CreateEmployee(int employeeId, string firstName, string lastName, int age, string emailAddress)
        {
            EmployeeModel data = new()
            {
                EmployeeId = employeeId,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                EmailAdress = emailAddress
            };
            string sql = @"insert into dbo.Employee (EmployeeId, FirstName, LastName, Age, EmailAdress) 
                           values (@EmployeeId, @FirstName, @LastName, @Age, @EmailAdress);";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}

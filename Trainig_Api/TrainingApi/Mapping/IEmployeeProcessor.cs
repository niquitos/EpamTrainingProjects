using System.Collections.Generic;
using TrainingApi.Models;
using TrainingApi.Services.DataAccess;

namespace TrainingApi.Mapping
{
    public interface IEmployeeProcessor
    {
        ISqlDataAccess SqlDataAccess { get; }

        int CreateEmployee(int employeeId, string firstName, string lastName, int age, string emailAddress);
        IEnumerable<EmployeeModel> LoadEmployees();
    }
}
using System;
using System.Data;
using TrainingApi.Models;

namespace TrainingApi.Mapping
{
    public class RowConverter : IRowConverter
    {
        public EmployeeModel Map(DataRow row)
        {
            return new EmployeeModel
            {
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Age = Convert.ToInt32(row["Age"]),
                EmailAdress = row["EmailAdress"].ToString()
            };
        }
    }
}

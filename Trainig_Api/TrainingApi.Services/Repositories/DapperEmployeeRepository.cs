using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Repositories
{
    public class DapperEmployeeRepository : IDataRepository<EmployeeDomainModel>
    {
        private bool _disposed = false;
        private readonly string _connectionString;

        public DapperEmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:Sql"];
        }

        public void Create(EmployeeDomainModel item)
        {
            string sql = @"INSERT INTO dbo.Employees (EmployeeId, FirstName, LastName, Age, EmailAddress) 
                           VALUES (@EmployeeId, @FirstName, @LastName, @Age, @EmailAddress)";
            using IDbConnection cnn = new SqlConnection(_connectionString);
            cnn.Execute(sql, item);
        }

        public void Delete(int id)
        {
            string sql = @"DELETE dbo.Employees 
                           WHERE Id = @Id";
            using IDbConnection cnn = new SqlConnection(_connectionString);
            cnn.Execute(sql, new { Id = id });
        }

        public EmployeeDomainModel Get(int id)
        {
            string sql = @"SELECT *
                           FROM dbo.Employees
                           WHERE Id = @Id";
            using IDbConnection cnn = new SqlConnection(_connectionString);
            return cnn.QuerySingle<EmployeeDomainModel>(sql, new { Id = id });
        }

        public IEnumerable<EmployeeDomainModel> GetAll()
        {
            string sql = @"SELECT * 
                           FROM dbo.Employees";
            using IDbConnection cnn = new SqlConnection(_connectionString);
            return cnn.Query<EmployeeDomainModel>(sql).ToList();
        }

        public void Save()
        {

        }

        public void Update(EmployeeDomainModel item)
        {

        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

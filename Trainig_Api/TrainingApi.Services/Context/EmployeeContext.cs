using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Context
{
    public class EmployeeContext : DbContext
    {
        public DbSet<EmployeeDomainModel> Employees { get; set; }

        public EmployeeContext(DbContextOptions options) : base(options) { }
    }
}

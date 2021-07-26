using Microsoft.EntityFrameworkCore;

namespace TrainingApi.Models
{
    public class EmployeeContext : DbContext
    {
        public DbSet<EmployeeModel> Employee { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
    }
}

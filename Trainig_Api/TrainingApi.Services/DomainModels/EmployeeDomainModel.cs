using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingApi.Services.DomainModels
{
    public class EmployeeDomainModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("Age")]
        public int Age { get; set; }
        [Column("EmailAddress")]
        public string EmailAddress { get; set; }
    }
}

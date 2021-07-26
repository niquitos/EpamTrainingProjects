using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainingApi.Models
{
    [Keyless]
    public class EmployeeModel
    {
        [Display(Name = "Employee ID")]
        [Required(ErrorMessage = "You need to give us your employee id.")]
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to give us your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to give us your last name.")]
        public string LastName { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "You need to give us your age.")]
        public int Age { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "You need to give us your email adress.")]
        public string EmailAdress { get; set; }
    }
}

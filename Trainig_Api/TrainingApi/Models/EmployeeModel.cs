using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TrainingApi.Models
{
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

        [Display(Name = "Confirm email")]
        [Compare("EmailAdress", ErrorMessage = "The Email and Confirm email must match")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Your rpassword isn't long enough.")]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "You need to give us your password.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The Password and Confirm password must match")]
        public string ConfirmPassword { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models
{
    [Table("AspNetUsers")]
    public class UserModel : IdentityUser
    {
        public UserModel(string name):base(name)
        {

        }
    }
}

using Microsoft.Build.Framework;
using System.ComponentModel;

namespace ProjectManagementAPl.Models
{
    public class UserRegiter
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Models.DIO
{
    public class RegistrationModel
    {
        //[Required]
        //public string Name { get; set; }

        [Required(ErrorMessage = "Enter the User Name!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter the Email!")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter the Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public string ? Role { get; set; }
    }
}

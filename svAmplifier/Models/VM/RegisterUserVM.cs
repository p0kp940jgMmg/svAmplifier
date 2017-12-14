using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class RegisterUserVM
    {
        [Required(ErrorMessage = "Please enter a valid username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string Password2 { get; set; }
    }
}

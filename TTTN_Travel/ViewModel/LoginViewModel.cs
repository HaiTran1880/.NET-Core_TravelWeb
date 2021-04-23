using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe
        {
            get; set;
        }
    }
}

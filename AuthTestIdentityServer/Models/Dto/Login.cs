using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTestIdentityServer.Models.Dto
{
    public class Login
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[MaxLength(5)]
        public string Password { get; set; }

        public bool Persist { get; set; }
    }
}

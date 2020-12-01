using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTestIdentityServer.Models
{
    public class User:IdentityUser
    {
        //[Required]
        //public string FirstName { get; set; }
    }
}

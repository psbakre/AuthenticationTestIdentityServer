using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthTestIdentityServer.ViewModels.Login
{
    public class LoginViewModel
    {
        public Models.Dto.Login login { get; set; }

        
    }
}

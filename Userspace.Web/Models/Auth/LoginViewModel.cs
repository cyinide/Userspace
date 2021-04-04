﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Web.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email must be in correct format")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace GreatWorld.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

}

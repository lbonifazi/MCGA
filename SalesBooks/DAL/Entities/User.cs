﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public DateTime CreateDt { get; set; }
        public bool Disable { get; set; }
        public bool PasswordTempInd { get; set; }
        public string ActivationCode { get; set; }

    }
}

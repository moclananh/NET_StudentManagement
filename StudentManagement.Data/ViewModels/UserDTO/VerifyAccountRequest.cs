﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.ViewModels.UserDTO
{
    public class VerifyAccountRequest
    {
        public string Email { get; set; }
        public string VerifyCode { get; set; }
    }
}

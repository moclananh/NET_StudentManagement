using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.Models
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string VerifyCode { get; set; }
        public bool AccountStatus { get; set; } = false;
    }
}

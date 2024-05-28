using System;
using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string PhoneNumber { get; set; }
        public string VerifyCode { get; set; }
        public bool AccountStatus { get; set; } = false;
    }
}

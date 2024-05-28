using Microsoft.AspNetCore.Identity;
using System;

namespace StudentManagement.Data.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}

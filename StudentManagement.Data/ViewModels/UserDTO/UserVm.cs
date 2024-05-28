using System;
using System.Collections.Generic;

namespace StudentManagement.Data.ViewModels.UserDTO
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool AccountStatus { get; set; }
        public IList<string> Roles { get; set; }
    }
}

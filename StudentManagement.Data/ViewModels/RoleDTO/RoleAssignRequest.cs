using StudentManagement.Data.ViewModels.Commons;
using System;
using System.Collections.Generic;

namespace StudentManagement.Data.ViewModels.RoleDTO
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}

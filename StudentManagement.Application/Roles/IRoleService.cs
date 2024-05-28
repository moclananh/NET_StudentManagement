using StudentManagement.Data.ViewModels.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAll();
    }
}

using StudentManagement.Data.ViewModels.Commons;
using StudentManagement.Data.ViewModels.RoleDTO;
using StudentManagement.Data.ViewModels.UserDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Application.Users
{
   public interface IUserService
    {
        Task<LoginRespone<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<List<UserVm>> GetAll(string keyword);
        Task<ApiResult<UserVm>> GetById(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        Task<ApiResult<string>> GetVerifyCode(string email);
        Task<ApiResult<string>> VerifyAccount(VerifyAccountRequest request);
    }
}

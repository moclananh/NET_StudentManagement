using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using StudentManagement.Data.Models;
using StudentManagement.Data;
using StudentManagement.Data.ViewModels.Commons;
using StudentManagement.Data.ViewModels.RoleDTO;
using StudentManagement.Data.ViewModels.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Application.EmailService;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace StudentManagement.Application.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config,
            ApplicationDbContext context,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _emailService = emailService;
        }
        public async Task<LoginRespone<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new LoginErrorRespone<string>("Account is not exist");
            if (user.AccountStatus == false) return new LoginErrorRespone<string>("Account is not verify");

            /* var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, lockoutOnFailure: true);
             if (!result.Succeeded)
             {
                 return new LoginErrorRespone<string>("Wrong username or password");
             }
 */
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return new LoginErrorRespone<string>("Wrong username or password");
            }


            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.Role, string.Join(";", roles)),
                   new Claim(ClaimTypes.Name, request.UserName),
                   new Claim(ClaimTypes.Dsa, user.Id.ToString()),
               };

            var loginResult = new LoginResult
            {
                ID = user.Id,
            };
            Guid id = loginResult.ID;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new LoginRespone<string>(new JwtSecurityTokenHandler().WriteToken(token), id);
        }


        public async Task<List<UserVm>> GetAll(string keyword)
        {

            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.UserName.Contains(keyword)
                                        || x.PhoneNumber.Contains(keyword));
            }

            var users = await query.Select(x => new UserVm()
            {
                Id = x.Id,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,

            }).Distinct().ToListAsync();
            return users;
        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("User is not exist");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles,
                AccountStatus = user.AccountStatus,
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<string>> GetVerifyCode(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ApiErrorResult<string>("Email not found");
            }
            Random generator = new Random();
            string r = generator.Next(0, 1000000).ToString("D6");
            user.VerifyCode = r;
            await _context.SaveChangesAsync();
            var subject = "Account verify";
            var body = $"This is your verify code: <strong>{r}</strong>";
            try
            {
                await _emailService.SendPasswordResetEmailAsync(email, subject, body);
                return new ApiSuccessMessage<string>(" Verify email sent");
            }
            catch
            {
                // Handle the exception as needed
                return new ApiErrorResult<string>("Error sending verify email");
            }
        }

        public async Task<ApiResult<string>> VerifyAccount(VerifyAccountRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Handle case where email doesn't exist
                return new ApiErrorResult<string>("Email not found");
            }
            if (request.VerifyCode == user.VerifyCode)
            {
                user.AccountStatus = true;
                await _context.SaveChangesAsync();
                return new ApiSuccessMessage<string>("Verify account successful");
            }
            return new ApiErrorResult<string>("Verify code not correct");
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Account is exist");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email is exist");
            }

            user = new AppUser()
            {
                Email = request.Email,
                UserName = request.UserName,
                AccountStatus = false,

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Register fail");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Account is not exist");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }

       
    }
}

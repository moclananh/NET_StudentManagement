
using System.ComponentModel.DataAnnotations;


namespace StudentManagement.Data.ViewModels.UserDTO
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

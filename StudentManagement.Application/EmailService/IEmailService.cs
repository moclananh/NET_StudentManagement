
using System.Threading.Tasks;

namespace StudentManagement.Application.EmailService
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string subject, string body);
        Task SendEmailAsync(string email, string subject, string body);
    }
}

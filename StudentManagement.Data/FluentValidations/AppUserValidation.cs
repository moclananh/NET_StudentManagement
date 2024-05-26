using FluentValidation;
using StudentManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.FluentValidations
{
    public class AppUserValidation : AbstractValidator<AppUser>
    {
        public AppUserValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Please specify a first name");
            RuleFor(x => x.Password).Length(20, 250);
        }
    }
}

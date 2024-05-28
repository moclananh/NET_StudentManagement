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
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Not Empty");

        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Models
{
    public class User: Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(u => u.Password)
                .NotEmpty()
                .Length(1, 100);
        }
    }
}

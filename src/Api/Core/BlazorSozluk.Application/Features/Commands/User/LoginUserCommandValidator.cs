﻿using BlazorSozluk.Common.ViewModels.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Features.Commands.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(i => i.EmailAddress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");

            RuleFor(i => i.Password)
                .NotNull()
                .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinimumLength} characters");
        }
    }
}

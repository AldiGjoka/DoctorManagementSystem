using Domain.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService.Command.UpdateDoctor
{
    public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorValidator()
        {
            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("Email must not be empty")
                .NotNull();

            RuleFor(d => d.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("PhoneNumber must not be more than 50 chars");

            RuleFor(d => d.State)
                .NotEmpty().WithMessage("State must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("State must not be more than 50 chars");

            RuleFor(d => d.City)
                .NotEmpty().WithMessage("City must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("City must not be more than 50 chars");

            RuleFor(d => d.Country)
                .NotEmpty().WithMessage("Country must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("Country must not be more than 50 chars");

            RuleFor(d => d.StreetName)
                .NotEmpty().WithMessage("StreetName must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("StreetName must not be more than 50 chars");

            RuleFor(d => d.PostalCode)
                .NotEmpty().WithMessage("PostalCode must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("PostalCode must not be more than 50 chars");
        }
    }
}

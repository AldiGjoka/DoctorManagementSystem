using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Command.CreatePatient
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientValidator()
        {
            RuleFor(d => d.DoctorId)
                .NotEmpty().WithMessage("DoctorId must not be empty")
                .NotNull().WithMessage("DoctorId must not be empty");

            RuleFor(d => d.PersonalNumber)
                .NotEmpty().WithMessage("PersonalNumber must not be empty")
                .NotNull()
                .MinimumLength(10).WithMessage("PersonalNumber Must not be less than 10 chars")
                .MaximumLength(10).WithMessage("PersonalNumber Must not be more than 10 chars");

            RuleFor(d => d.FirstName)
                .NotEmpty().WithMessage("Name must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("Name must not be more than 50 chars");

            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage("Surname must not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("Surname must not be more than 50 chars");

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

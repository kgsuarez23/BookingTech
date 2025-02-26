using Api.Manager.Application.Mediator.Commands;
using Api.Manager.Application.Mediator.Queries;
using FluentValidation;

namespace Api.Manager.Application.Validator
{
    public class GetByIdBookingsQueryValidator : AbstractValidator<GetByIdBookingsQuery>
    {
        public GetByIdBookingsQueryValidator()
        {
            RuleFor(r => r.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }

    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.CheckIn)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Check-in date must be today or later.");

            RuleFor(x => x.CheckOut)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out date must be after the check-in date.");

            RuleFor(x => x.NumberGuests)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage("Number of guests must be greater than 0.");

            RuleFor(x => x.Rooms)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Rooms list cannot be null.")
                .NotEmpty()
                .WithMessage("At least one room must be selected.");

            RuleFor(x => x.Guests)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Guests list cannot be null.")
                .NotEmpty()
                .WithMessage("At least one guest must be added.");

            RuleForEach(x => x.Guests)
                .SetValidator(new GuestBookingValidator());

            RuleFor(x => x.EmergencyContact)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Emergency contact is required.");

            RuleFor(x => x.EmergencyContact)
                .SetValidator(new EmergencyContactValidator());
        }
    }

    public class GuestBookingValidator : AbstractValidator<GuestBooking>
    {
        public GuestBookingValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("First name is required.")
                .Length(5, 50);

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .Length(5, 50);

            RuleFor(x => x.BirthDate)
                .Cascade(CascadeMode.Stop)
                .LessThan(DateTime.Today)
                .WithMessage("The date of birth must be less than today.");

            RuleFor(x => x.Gender)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Gender is required.")
                .Length(1);

            RuleFor(x => x.DocumentType)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(2)
                .WithMessage("Document type is required.");

            RuleFor(x => x.DocumentNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Document number is required.")
                .Length(5, 50);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .Length(12, 50);

            RuleFor(x => x.ContactPhone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Contact phone is required.")
                .Length(5, 50);
        }
    }

    public class EmergencyContactValidator : AbstractValidator<EmergencyContact>
    {
        public EmergencyContactValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Emergency contact's first name is required.")
                .Length(2, 100);

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Emergency contact's last name is required.")
                .Length(2, 100);

            RuleFor(x => x.ContactPhone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Emergency contact's phone number is required.")
                .Length(5, 50);
        }
    }

}

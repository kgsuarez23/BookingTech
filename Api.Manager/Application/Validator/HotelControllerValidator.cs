using Api.Manager.Application.Mediator.Commands;
using Api.Manager.Application.Mediator.Queries;
using FluentValidation;

namespace Api.Manager.Application.Validator
{
    public class GetByIdHotelQueryValidator : AbstractValidator<GetByIdHotelQuery>
    {
        public GetByIdHotelQueryValidator()
        {
            RuleFor(r => r.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }

    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Hotel name is required.")
                .Length(5, 100);

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Address is required.")
                .Length(5, 200);

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("City is required.")
                .Length(5, 50);

            RuleFor(x => x.State)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("State is required.")
                .Length(5, 50);

            RuleFor(x => x.Country)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Country is required.")
                .Length(5, 50);

            RuleFor(x => x.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Length(5, 20);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("A valid email address is required.")
                .Length(5, 50);
        }
    }

    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(r => r.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Hotel name is required.")
                .Length(5, 100);

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Address is required.")
                .Length(5, 200);

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("City is required.")
                .Length(5, 50);

            RuleFor(x => x.State)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("State is required.")
                .Length(5, 50);

            RuleFor(x => x.Country)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Country is required.")
                .Length(5, 50);

            RuleFor(x => x.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Length(5, 20);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("A valid email address is required.")
                .Length(5, 50);
        }
    }

    public class UpdateStateHotelCommandValidator : AbstractValidator<UpdateStateHotelCommand>
    {
        public UpdateStateHotelCommandValidator()
        {
            RuleFor(r => r.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}

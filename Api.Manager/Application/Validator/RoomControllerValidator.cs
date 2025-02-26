using Api.Manager.Application.Mediator.Commands;
using Api.Manager.Application.Mediator.Queries;
using FluentValidation;

namespace Api.Manager.Application.Validator
{
    public class GetByIdRoomQueryValidator : AbstractValidator<GetByIdRoomQuery>
    {
        public GetByIdRoomQueryValidator()
        {
            RuleFor(r => r.RoomId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(r => r.HotelId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }

    public class FindRoomsByFilterQueryValidator : AbstractValidator<FindRoomsByFilterQuery>
    {
        public FindRoomsByFilterQueryValidator()
        {
            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Hotel name is required.")
                .Length(5, 50);

            RuleFor(x => x.CheckIn)
                .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Check-in date must be today or later.");

            RuleFor(x => x.CheckOut)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out date must be after the check-in date.");
        }
    }

    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.HotelId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Number)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(1, 10);

            RuleFor(x => x.Type)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.BaseCost)
                .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Base cost must be a non-negative value.");

            RuleFor(x => x.Taxes)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Taxes must be a non-negative value.");

            RuleFor(x => x.Location)
            .NotEmpty()
                .NotNull()
                .Length(1, 100);
        }
    }

    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.HotelId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Number)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(1, 10);

            RuleFor(x => x.Type)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.BaseCost)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Base cost must be a non-negative value.");

            RuleFor(x => x.Taxes)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Taxes must be a non-negative value.");

            RuleFor(x => x.Location)
                .NotEmpty()
                .NotNull()
                .Length(1, 100);
        }
    }

    public class UpdateStateRoomCommandValidator : AbstractValidator<UpdateStateRoomCommand>
    {
        public UpdateStateRoomCommandValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.HotelId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}

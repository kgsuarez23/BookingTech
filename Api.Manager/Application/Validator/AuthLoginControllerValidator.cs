using Api.Manager.Application.Mediator.Commands;
using Api.Manager.Application.Mediator.Queries;
using FluentValidation;

namespace Api.Manager.Application.Validator
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(r => r.UserName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
        }
    }

    public class AddRoleToUserCommandValidator : AbstractValidator<AddRoleToUserCommand>
    {
        public AddRoleToUserCommandValidator()
        {
            RuleFor(r => r.UserId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.RoleId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
        }
    }

    public class UpdateRoleToUserCommandValidator : AbstractValidator<UpdateRoleToUserCommand>
    {
        public UpdateRoleToUserCommandValidator()
        {
            RuleFor(r => r.UserId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.RoleId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(r => r.UserName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(5, 50);
            RuleFor(r => r.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(5, 15);
            RuleFor(r => r.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(12, 50)
                .EmailAddress();
            RuleFor(r => r.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(2, 100);
            RuleFor(r => r.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(2, 100);
        }
    }
}

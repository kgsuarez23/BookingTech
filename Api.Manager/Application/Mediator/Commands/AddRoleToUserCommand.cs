using Api.Manager.Application.UnitOfWork;
using Api.Manager.Base.Entity;
using Api.Manager.Domain.Exceptions;
using MediatR;

namespace Api.Manager.Application.Mediator.Commands
{
    public class AddRoleToUserCommand : IRequest<BasicResult>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    internal class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, BasicResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddRoleToUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BasicResult> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var roles = _unitOfWork.UserRepository.SearchUserRolById(request.UserId).ToList();

            var result = false;
            if (roles.Exists(rol => rol.RoleID == request.RoleId))
                result = _unitOfWork.UserRepository.UpdateRoleToUser(request.UserId, request.RoleId, true);
            else
                result = _unitOfWork.UserRepository.AddRoleToUser(request.UserId, request.RoleId);

            if (result)
            {
                _unitOfWork.SaveChangesAsync();
                return Task.FromResult(new BasicResult()
                {
                    Message = "Successful registration...",
                    Result = true
                });
            }

            throw new ApiException("The role could not be assigned to the user.");
        }
    }
}
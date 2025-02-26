using Api.Manager.Application.UnitOfWork;
using Api.Manager.Base.Entity;
using Api.Manager.Domain.Exceptions;
using MediatR;

namespace Api.Manager.Application.Mediator.Commands
{
    public class UpdateRoleToUserCommand : IRequest<BasicResult>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool State { get; set; }
    }

    internal class UpdateRoleToUserCommandHandler : IRequestHandler<UpdateRoleToUserCommand, BasicResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRoleToUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BasicResult> Handle(UpdateRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var roles = _unitOfWork.UserRepository.SearchUserRolById(request.UserId).ToList();

            var result = false;
            if (roles.Exists(rol => rol.RoleID == request.RoleId))
                result = _unitOfWork.UserRepository.UpdateRoleToUser(request.UserId, request.RoleId, request.State);

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
using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetAllRoleUserQuery : IRequest<Response<RoleDto>> { }

    internal class GetAllTypeRoleUserQueryHandler : IRequestHandler<GetAllRoleUserQuery, Response<RoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllTypeRoleUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<RoleDto>> Handle(GetAllRoleUserQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.RoleRepository.GetAllTypeRoleUserQuery();

            if (result.Count > 0)
            {
                return Task.FromResult(new Response<RoleDto>(data: result.Select(RoleDto.MapFrom), count: result.Count));
            }

            return Task.FromResult(new Response<RoleDto>(count: 0));
        }
    }
}
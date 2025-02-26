using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetAllGuestsQuery : IRequest<Response<GuestDto>> { }

    internal class GetAllGuestsQueryHandler : IRequestHandler<GetAllGuestsQuery, Response<GuestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllGuestsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<GuestDto>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.GuestRepository.GetAll();

            if (result.Count > 0)
            {
                return Task.FromResult(new Response<GuestDto>(data: result.Select(GuestDto.MapFrom), count: result.Count));
            }

            return Task.FromResult(new Response<GuestDto>(count: 0));
        }
    }
}
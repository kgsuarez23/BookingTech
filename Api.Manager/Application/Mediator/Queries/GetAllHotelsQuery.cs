using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetAllHotelsQuery : IRequest<Response<HotelDto>> { }

    internal class GetHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, Response<HotelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetHotelsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.HotelRepository.GetAll();

            if (result.Count > 0)
            {
                return Task.FromResult(new Response<HotelDto>(data: result.Select(HotelDto.MapFrom), count: result.Count));
            }

            return Task.FromResult(new Response<HotelDto>(count: 0));
        }
    }
}

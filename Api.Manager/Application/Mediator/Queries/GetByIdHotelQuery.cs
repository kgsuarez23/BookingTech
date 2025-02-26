using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetByIdHotelQuery : IRequest<Response<HotelDto>>
    {
        public int Id { get; set; }
    }

    internal class GetByIdHotelQueryHandler : IRequestHandler<GetByIdHotelQuery, Response<HotelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetByIdHotelQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<HotelDto>> Handle(GetByIdHotelQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.HotelRepository.GetById(request.Id);

            if (result.ExistsInDB)
            {
                return Task.FromResult(new Response<HotelDto>(
                    data: [HotelDto.MapFrom(result)],
                    count: 1));
            }

            return Task.FromResult(new Response<HotelDto>(count: 0));
        }
    }
}
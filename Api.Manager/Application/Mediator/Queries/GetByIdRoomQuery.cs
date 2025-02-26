using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetByIdRoomQuery : IRequest<Response<RoomDto>>
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
    }

    internal class GetByIdRoomQueryHandler : IRequestHandler<GetByIdRoomQuery, Response<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetByIdRoomQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<RoomDto>> Handle(GetByIdRoomQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.RoomRepository.GetById(request.RoomId, request.HotelId);

            if (result.ExistsInDB)
            {
                return Task.FromResult(new Response<RoomDto>(
                    data: [RoomDto.MapFrom(result)],
                    count: 1));
            }

            return Task.FromResult(new Response<RoomDto>(count: 0));
        }
    }
}
using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetAllTypeRooms : IRequest<Response<TypeRoomDto>> { }

    internal class GetAllTypeRoomsHandler : IRequestHandler<GetAllTypeRooms, Response<TypeRoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllTypeRoomsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<TypeRoomDto>> Handle(GetAllTypeRooms request, CancellationToken cancellationToken)
        {
            var result_bookings = _unitOfWork.RoomRepository.GetAllTypeRooms();

            if (result_bookings.Count > 0)
            {
                return Task.FromResult(new Response<TypeRoomDto>(
                    data: result_bookings.Select(TypeRoomDto.MapFrom),
                    count: result_bookings.Count));
            }

            return Task.FromResult(new Response<TypeRoomDto>(count: 0));
        }
    }
}
using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using Api.Manager.Domain.Exceptions;
using MediatR;

namespace Api.Manager.Application.Mediator.Commands
{
    public class UpdateStateRoomCommand : IRequest<Response<RoomDto>>
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public bool IsActive { get; set; }
    }

    internal class UpdateStateRoomCommandHandler : IRequestHandler<UpdateStateRoomCommand, Response<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStateRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<RoomDto>> Handle(UpdateStateRoomCommand request, CancellationToken cancellationToken)
        {
            var rooms = _unitOfWork.RoomRepository.GetAll();

            var room_found = rooms.Where(room => room.Id == request.Id).FirstOrDefault();

            if (room_found == null)
                throw new ApiException("The room to be updated has not been located.");

            _unitOfWork.RoomRepository.UpdateStateRoom(request.Id, request.IsActive);

            room_found = _unitOfWork.RoomRepository.GetById(request.Id, request.HotelId);

            if (room_found.ExistsInDB)
            {
                _ = _unitOfWork.SaveChangesAsync();
                return Task.FromResult(new Response<RoomDto>(data: [RoomDto.MapFrom(room_found)], count: 1));
            }

            return Task.FromResult(new Response<RoomDto>(count: 0));
        }
    }
}
using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using Api.Manager.Domain.Exceptions;
using MediatR;
using Newtonsoft.Json;

namespace Api.Manager.Application.Mediator.Commands
{
    public class UpdateRoomCommand : IRequest<Response<RoomDto>>
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Number { get; set; }
        public int Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
    }

    internal class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Response<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<RoomDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var rooms = _unitOfWork.RoomRepository.GetAll();
            var room_found = rooms.Where(room => room.Id == request.Id).FirstOrDefault();

            if (room_found == null)
                throw new ApiException("The room to be updated has not been located.");

            rooms = rooms.Where(room => room.Id != request.Id).ToList();

            if (rooms.Exists(room => room.HotelId == request.HotelId && room.Number == request.Number))
                throw new ArgumentException("In the indicated hotel there is already a room with the indicated number.");

            var result = _unitOfWork.RoomRepository.UpdateRoom(JsonConvert.SerializeObject(request));

            if (!result)
            {
                _unitOfWork.Dispose();
                throw new ArgumentException("It was not possible to update the hotel. Try again.");
            }

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
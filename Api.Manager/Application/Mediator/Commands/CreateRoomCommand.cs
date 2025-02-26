using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;
using Newtonsoft.Json;

namespace Api.Manager.Application.Mediator.Commands
{
    public class CreateRoomCommand : IRequest<Response<RoomDto>>
    {
        public int HotelId { get; set; }
        public string Number { get; set; }
        public int Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
    }

    internal class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Response<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<RoomDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var rooms = _unitOfWork.RoomRepository.GetAll();

            if (rooms.Exists(room => room.HotelId == request.HotelId && room.Number == request.Number))
                throw new ArgumentException("There is already a room with the indicated number.");

            int idNewRoom = _unitOfWork.RoomRepository.Create(JsonConvert.SerializeObject(request));

            if (idNewRoom <= 0)
            {
                _unitOfWork.Dispose();
                throw new ArgumentException("It was not possible to create the room. Try again.");
            }

            var room = _unitOfWork.RoomRepository.GetById(idNewRoom, request.HotelId);

            _ = _unitOfWork.SaveChangesAsync();
            return Task.FromResult(new Response<RoomDto>(data: [RoomDto.MapFrom(room)], count: 1));
        }
    }
}
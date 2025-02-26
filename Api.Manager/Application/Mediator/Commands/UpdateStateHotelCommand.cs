using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using Api.Manager.Domain.Exceptions;
using MediatR;

namespace Api.Manager.Application.Mediator.Commands
{
    public class UpdateStateHotelCommand : IRequest<Response<HotelDto>>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    internal class UpdateStateHotelCommandHandler : IRequestHandler<UpdateStateHotelCommand, Response<HotelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStateHotelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<HotelDto>> Handle(UpdateStateHotelCommand request, CancellationToken cancellationToken)
        {
            var rooms = _unitOfWork.HotelRepository.GetAll();

            var room_found = rooms.Where(room => room.Id == request.Id).FirstOrDefault();

            if (room_found == null)
                throw new ApiException("The hotel to be updated has not been located.");

            _unitOfWork.HotelRepository.UpdateStateHotel(request.Id, request.IsActive);

            room_found = _unitOfWork.HotelRepository.GetById(request.Id);

            if (room_found.ExistsInDB)
            {
                _ = _unitOfWork.SaveChangesAsync();
                return Task.FromResult(new Response<HotelDto>(data: [HotelDto.MapFrom(room_found)], count: 1));
            }

            return Task.FromResult(new Response<HotelDto>(count: 0));
        }
    }
}
using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using Api.Manager.Domain.Entity;
using MediatR;
using Newtonsoft.Json;

namespace Api.Manager.Application.Mediator.Commands
{
    public class CreateHotelCommand : IRequest<Response<HotelDto>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    internal class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Response<HotelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateHotelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<HotelDto>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            List<HotelEntity> hotels = _unitOfWork.HotelRepository.GetAll();

            if (hotels.Exists(hotel => hotel.Name == request.Name && hotel.Email == request.Email))
                throw new ArgumentException("There is already a hotel registered with the name and email entered.");

            int idNewHotel = _unitOfWork.HotelRepository.CreateHotel(JsonConvert.SerializeObject(request));

            if (idNewHotel < 1)
            {
                _unitOfWork.Dispose();
                throw new ArgumentException("It was not possible to create the hotel. Try again.");
            }

            var hotel = _unitOfWork.HotelRepository.GetById(idNewHotel);

            if (hotel.ExistsInDB)
            {
                _ = _unitOfWork.SaveChangesAsync();
                return Task.FromResult(new Response<HotelDto>(data: [HotelDto.MapFrom(hotel)], count: 1));
            }

            return Task.FromResult(new Response<HotelDto>(count: 0));
        }
    }
}
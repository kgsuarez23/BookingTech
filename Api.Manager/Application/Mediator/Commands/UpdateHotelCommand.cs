using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using Api.Manager.Domain.Exceptions;
using MediatR;
using Newtonsoft.Json;

namespace Api.Manager.Application.Mediator.Commands
{
    public class UpdateHotelCommand : IRequest<Response<HotelDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    internal class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Response<HotelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateHotelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<HotelDto>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotels = _unitOfWork.HotelRepository.GetAll();
            var hotel_found = hotels.Where(hotel => hotel.Id == request.Id).FirstOrDefault();

            if (hotel_found == null)
                throw new ApiException("The hotel to be updated has not been located.");

            hotels = hotels.Where(hot => hot.Id != request.Id).ToList();

            if (hotels.Exists(hotel => hotel.Name == request.Name && hotel.Email == request.Email))
                throw new ArgumentException("There is already a hotel registered with the name and email entered.");

            var result = _unitOfWork.HotelRepository.UpdateHotel(JsonConvert.SerializeObject(request));

            if (!result)
            {
                _unitOfWork.Dispose();
                throw new ArgumentException("It was not possible to update the hotel. Try again.");
            }

            hotel_found = _unitOfWork.HotelRepository.GetById(request.Id);

            if (hotel_found.ExistsInDB)
            {
                _ = _unitOfWork.SaveChangesAsync();
                return Task.FromResult(new Response<HotelDto>(data: [HotelDto.MapFrom(hotel_found)], count: 1));
            }

            return Task.FromResult(new Response<HotelDto>(count: 0));
        }
    }
}
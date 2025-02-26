using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Utils;
using Api.Manager.Application.Wrappers;
using Api.Manager.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Api.Manager.Application.Mediator.Commands
{
    public class CreateBookingCommand : IRequest<Response<BookingDto>>
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberGuests { get; set; }
        public List<int> Rooms { get; set; }
        public List<GuestBooking> Guests { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
    }

    public class GuestBooking
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
    }

    public class EmergencyContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
    }

    internal class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<BookingDto>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtils _utils;
        private readonly IMailService _mailService;
        public CreateBookingCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IUtils utils, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _utils = utils;
            _mailService = mailService;
        }

        public Task<Response<BookingDto>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId_encrypt = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userId = _utils.DecryptString(userId_encrypt);

                //Create Booking
                int booking = _unitOfWork.BookingRepository.CreateBooking(JsonConvert.SerializeObject(new
                {
                    UserID = userId,
                    request.CheckIn,
                    request.CheckOut,
                    request.NumberGuests,
                }));

                //Creete Rooms
                request.Rooms.ForEach(room => _unitOfWork.BookingRepository.CreateBookingRooms(room, booking));

                //Create Guests
                request.Guests.ForEach(guest => _unitOfWork.GuestRepository.CreateGuest(JsonConvert.SerializeObject(guest), booking));

                //Create EmergencyContact
                _unitOfWork.EmergencyContactRepository.Create(JsonConvert.SerializeObject(request.EmergencyContact), booking);

                var result = _unitOfWork.BookingRepository.GetById(booking);

                if (result.ExistsInDB)
                {
                    _unitOfWork.SaveChangesAsync();

                    var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

                    _mailService.SendMail(email, "Reservation confirmation", result);

                    return Task.FromResult(new Response<BookingDto>(
                        data: [BookingDto.MapFrom(result)],
                        count: 1));
                }

                return Task.FromResult(new Response<BookingDto>(count: 0));
            }
            catch (Exception ex)
            {
                throw new ApiException("It was not possible to create the reservation, try again later.", ex);
            }
        }
    }
}
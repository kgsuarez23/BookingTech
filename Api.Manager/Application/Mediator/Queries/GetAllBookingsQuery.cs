using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetAllBookingsQuery : IRequest<Response<BookingDto>> { }

    internal class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, Response<BookingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllBookingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<BookingDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var result_bookings = _unitOfWork.BookingRepository.GetAll();

            if (result_bookings.Count > 0)
            {
                return Task.FromResult(new Response<BookingDto>(
                    data: result_bookings.Select(BookingDto.MapFrom),
                    count: result_bookings.Count));
            }

            return Task.FromResult(new Response<BookingDto>(count: 0));
        }
    }
}

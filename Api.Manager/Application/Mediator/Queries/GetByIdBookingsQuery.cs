using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetByIdBookingsQuery : IRequest<Response<BookingDto>>
    {
        public int Id { get; set; }
    }

    internal class GetByIdBookingsQueryHandler : IRequestHandler<GetByIdBookingsQuery, Response<BookingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetByIdBookingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<BookingDto>> Handle(GetByIdBookingsQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.BookingRepository.GetById(request.Id);

            if (result.ExistsInDB)
            {
                return Task.FromResult(new Response<BookingDto>(
                    data: [BookingDto.MapFrom(result)],
                    count: 1));
            }

            return Task.FromResult(new Response<BookingDto>(count: 0));
        }
    }
}

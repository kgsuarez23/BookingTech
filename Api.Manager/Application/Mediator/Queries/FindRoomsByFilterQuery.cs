using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class FindRoomsByFilterQuery : IRequest<Response<RoomFilterDto>>
    {
        public string City { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }

    internal class FindRoomsByFilterQueryHandler : IRequestHandler<FindRoomsByFilterQuery, Response<RoomFilterDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FindRoomsByFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<RoomFilterDto>> Handle(FindRoomsByFilterQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.RoomRepository.FindByFilters(request.City, request.CheckIn, request.CheckOut);

            if (result.Count > 0)
            {
                return Task.FromResult(new Response<RoomFilterDto>(
                    data: result.Select(RoomFilterDto.MapFrom),
                    count: result.Count));
            }

            return Task.FromResult(new Response<RoomFilterDto>(count: 0));
        }
    }
}
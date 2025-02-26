using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Wrappers;
using MediatR;

namespace Api.Manager.Application.Mediator.Queries
{
    public class GetAllRoomsQuery : IRequest<Response<RoomDto>> { }

    internal class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, Response<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllRoomsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.RoomRepository.GetAll();

            if (result.Count > 0)
            {
                return Task.FromResult(new Response<RoomDto>(
                    data: result.Select(RoomDto.MapFrom),
                    count: result.Count));
            }

            return Task.FromResult(new Response<RoomDto>(count: 0));
        }
    }
}
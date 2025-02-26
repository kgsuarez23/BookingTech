using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Utils;
using Api.Manager.Base.Entity;
using Api.Manager.Domain.Entity;
using MediatR;
using Newtonsoft.Json;

namespace Api.Manager.Application.Mediator.Commands
{
    public class CreateUserCommand : IRequest<BasicResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class RegisterCommandHandler : IRequestHandler<CreateUserCommand, BasicResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtils _utils;
        public RegisterCommandHandler(IUnitOfWork unitOfWork, IUtils utils)
        {
            _unitOfWork = unitOfWork;
            _utils = utils;
        }

        public Task<BasicResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            List<UserEntity> users = _unitOfWork.UserRepository.ListUsers().ToList();

            if (users.Exists(user => user.UserName == request.UserName))
            {
                return Task.FromResult(new BasicResult()
                {
                    Message = "A registered user already exists. If you do not remember your password, we invite you to reset it or try another username.",
                    Result = false
                });
            }

            // Hashear credencial
            var contrasenaHasheada = _utils.EncryptPassword(
                new DataUser() { UserName = request.UserName, Password = request.Password },
                request.Password);

            request.Password = contrasenaHasheada;

            int idUser = _unitOfWork.UserRepository.RegisterUser(JsonConvert.SerializeObject(request));

            bool result = _unitOfWork.UserRepository.AddRoleToUser(idUser, 1);

            if (result)
            {
                _unitOfWork.SaveChangesAsync();
                return Task.FromResult(new BasicResult()
                {
                    Message = "Successful registration...",
                    Result = true
                });
            }

            _unitOfWork.Dispose();
            return Task.FromResult(new BasicResult()
            {
                Message = "Registration has not been possible.",
                Result = false
            });
        }
    }
}

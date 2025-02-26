using Api.Manager.Application.Entities;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Utils;
using Api.Manager.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Manager.Application.Mediator.Queries
{
    public class LoginQuery : IRequest<TokenLogin>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    internal class LoginQueryHandler : IRequestHandler<LoginQuery, TokenLogin>
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtils _utils;
        public LoginQueryHandler(IUnitOfWork unitOfWork, IConfiguration configuration, IUtils utils)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _utils = utils;
        }

        public async Task<TokenLogin> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.SearchUserByUserName(request.UserName);

            if (user.ExistsInDB)
            {
                var resultado = _utils.VerifyPassword(new DataUser() { UserName = request.UserName, Password = request.Password }, user.Password, request.Password);

                if (!resultado)
                    throw new ApiException("Invalid credentials, verify them and try again.");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, _utils.EncryptString(user.UserID.ToString())),
                };

                var roles = _unitOfWork.UserRepository.SearchUserRolById(user.UserID);
                foreach (var claim in roles.Where(rol => rol.IsActive))
                    claims.Add(new Claim(ClaimTypes.Role, claim.RoleName));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                    signingCredentials: creds);

                return await Task.FromResult(new TokenLogin()
                {
                    Result = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            throw new ApiException("The user does not exist or the password is incorrect.");
        }
    }
}

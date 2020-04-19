using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Norhtwind.UnitOfWork;
using Northwind.Models;
using Northwind.WebApi.Authentication;
using System;

namespace Northwind.WebApi.Controllers
{
    public class TokenController : NorthwindController
    {
        private readonly ITokenProvider _tokenProvider;

        public TokenController(ITokenProvider provider, ILoggerFactory loggerFactory,  IUnitOfWork unitOfWork) : base(loggerFactory, unitOfWork) 
        {
            _tokenProvider = provider;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonWebToken Post([FromBody] User userLogin)
        {
            var user = _unitOfWork.User.ValidateUser(userLogin.Email, userLogin.Password);
            if (user == null)
                throw new UnauthorizedAccessException();

            var token = new JsonWebToken
            {
                AccessToken = _tokenProvider.GetToken(user, DateTime.UtcNow.AddHours(8)),
                ExpiresInMinutes = 480,
                TokenType = "Bearer"
            };
            _logger.LogInformation($"User {userLogin.Email} has successfully logged IN.");

            return token;
        }
    }
}

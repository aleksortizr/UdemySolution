using Microsoft.IdentityModel.Tokens;
using Northwind.Models;
using System;

namespace Northwind.WebApi.Authentication
{
    public interface ITokenProvider
    {
        public string GetToken(User user, DateTime expiration);

        TokenValidationParameters GetValidationParameters();
    }
}

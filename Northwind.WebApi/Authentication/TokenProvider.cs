using Microsoft.IdentityModel.Tokens;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Northwind.WebApi.Authentication
{
    public class TokenProvider : ITokenProvider
    {
        private readonly RsaSecurityKey _key;
        private readonly string _algorithm;
        private readonly string _issuer;
        private readonly string _audience;


        public TokenProvider(string issuer, string audience, string keyName)
        {
            var parameters = new CspParameters { KeyContainerName = keyName };
            var provider = new RSACryptoServiceProvider(2048, parameters);

            _algorithm = SecurityAlgorithms.RsaSha256Signature;
            _audience = audience;
            _issuer = issuer;
            _key = new RsaSecurityKey(provider);
        }

        public string GetToken(User user, DateTime expiration)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Roles),
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
            }, "Custom");

            SecurityToken token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience            = _audience,
                Issuer              = _issuer,
                SigningCredentials  = new SigningCredentials(_key, _algorithm),
                Expires             = expiration.ToUniversalTime(),
                Subject             = identity
            });

            return tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ClockSkew           = TimeSpan.FromSeconds(0),
                IssuerSigningKey    = _key,
                ValidateLifetime    = true,
                ValidAudience       = _audience,
                ValidIssuer         = _issuer
            };
        }
    }
}

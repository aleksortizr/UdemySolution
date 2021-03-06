﻿using System;

namespace Northwind.WebApi.Authentication
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresInMinutes { get; set; }

        public string RefreshToken { get; set; }
    }
}

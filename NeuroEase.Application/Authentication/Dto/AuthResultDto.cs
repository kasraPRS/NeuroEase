﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroEase.Application.Authentication.Dto
{
    public class AuthResultDto
    {

            public string Token { get; set; } = "";
            public string Email { get; set; }
            public string? PhoneNumber { get; set; }
            public bool Result { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
            public string UserId { get; set; }
    }
}

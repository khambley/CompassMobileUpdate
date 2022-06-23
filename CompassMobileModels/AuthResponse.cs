using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileModels
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
    }
}

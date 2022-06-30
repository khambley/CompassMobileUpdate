using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileUpdate.Helpers.Exceptions
{
    public class AuthenticationRequiredException : Exception
    {
        public AuthenticationRequiredException(string message) : base(message)
        {

        }
    }
}

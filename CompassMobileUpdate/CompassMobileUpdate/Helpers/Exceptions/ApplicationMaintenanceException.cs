using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileUpdate.Helpers.Exceptions
{
    public class ApplicationMaintenanceException : Exception
    {
        public ApplicationMaintenanceException() : base()
        {

        }

        public ApplicationMaintenanceException(string message) : base(message)
        {

        }
    }
}

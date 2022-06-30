using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileUpdate.Helpers.Exceptions
{
    public class MeterSerialNumberFormatException : Exception
    {
        public MeterSerialNumberFormatException()
        {

        }
        public MeterSerialNumberFormatException(string message) : base(message)
        {

        }

        public MeterSerialNumberFormatException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}

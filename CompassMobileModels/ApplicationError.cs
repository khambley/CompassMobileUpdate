using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileModels
{
    public class ApplicationError
    {
        public ApplicationErrorType ErrorType { get; set; }
        public string UserID { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}

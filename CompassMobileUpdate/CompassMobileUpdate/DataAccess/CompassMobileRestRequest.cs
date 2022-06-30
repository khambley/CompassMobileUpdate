using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileUpdate.DataAccess
{
    public class CompassMobileRestRequest : RestRequest
    {
        public CompassMobileRestRequest() : base()
        {
            SetHeaders();
        }
        public CompassMobileRestRequest(string resource) : base(resource)
        {
            SetHeaders();
        }

        public CompassMobileRestRequest(Uri resource, Method method) : base(resource, method)
        {
            SetHeaders();
        }
        public CompassMobileRestRequest(string resource, Method method) : base(resource, method)
        {
            SetHeaders();
        }

        private void SetHeaders()
        {
            this.AddHeader("X-API-KEY", "B2BB5894FE9211E5CAAF893377B1F6EC5A5702147B6A9D276DAEEEDB9912A2D0");

            if (AppVariables.User != null)
            {
                if (!string.IsNullOrWhiteSpace(AppVariables.User.UserID))
                {
                    this.AddHeader("X-API-USER", AppVariables.User.UserID);
                }
                if (!string.IsNullOrWhiteSpace(AppVariables.User.JWT))
                {
                    this.AddHeader("Authorization", "bearer " + AppVariables.User.JWT);
                }
            }
        }
    }

    public class NullResponseException : Exception
    {
        public NullResponseException()
        {

        }

        public NullResponseException(string message)
            : base(message)
        {

        }

        public NullResponseException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}

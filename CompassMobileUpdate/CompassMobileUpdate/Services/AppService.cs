using Acr.UserDialogs;
using CompassMobileModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompassMobileUpdate.Services
{
    public class AppService
    {
        private string devBaseUrl = "https://compassmobiledev.azurewebsites.net/api/";

        //public async Task<AuthResponse> Authenticate(AuthRequest currentUser, CancellationToken token)
        //{
        //    var client = new HttpClient();
        //    var json = JsonConvert.SerializeObject(currentUser);
        //    StringContent content = new StringContent(json);
        //    content.Headers.Add("X-API-KEY", "B2BB5894FE9211E5CAAF893377B1F6EC5A5702147B6A9D276DAEEEDB9912A2D0");
        //}

        // From AppVariables L77: may need to add headers here. The old way is below, new way is here, https://restsharp.dev/v107/#headers
        //AuthorizationHeaderParameter = new RestSharp.Parameter() 
        //    {
        //        Type = RestSharp.ParameterType.HttpHeader,
        //        Name = "WWW-Authenticate",
        //        Value = "Required"
        //    };
        //ExtendedJWTHeaderParameter = new RestSharp.Parameter()
        //    {
        //        Type = RestSharp.ParameterType.HttpHeader,
        //        Name = "X-EXTENDED-JWT"
        //    };
        //// MAJ 12/18/2017 BEGIN
        //CompassMaintenanceParameter = new RestSharp.Parameter()
        //{
        //    Type = RestSharp.ParameterType.HttpHeader,
        //    Name = "compass-maintenance"
        //};
        public async Task ShowMessage(string header, string message)
        {
            var config = new AlertConfig()
            {
                Title = header,
                Message = message,
                OkText = "OK"
            };

            await UserDialogs.Instance.AlertAsync(config);
        }
    }
}

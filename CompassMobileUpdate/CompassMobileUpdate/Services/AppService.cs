using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompassMobileUpdate.Services
{
    public class AppService
    {
        private string devBaseUrl = "https://compassmobiledev.azurewebsites.net/api/";

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

using CompassMobileModels;
using CompassMobileUpdate.Helpers;
using CompassMobileUpdate.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompassMobileUpdate.ViewModels
{
    public class LoginPageViewModel
    {
        private AppService _appService = new AppService();

        public CompassAuthenticationRequest CompassAuthenticationRequest { get; set; } = new CompassAuthenticationRequest();
        public ICommand LoginCommand { get; }

        private Page _page;

        public LoginPageViewModel(Page page)
        {
            _page = page;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (!ValidationHelper.IsFormValid(CompassAuthenticationRequest, _page)) { return; }
            await _page.DisplayAlert("Success", "Validation Success!", "OK");
        }
        private async Task ShowAlert()
        {
            var message = "You clicked the Login button";

            await _appService.ShowMessage("Login Button Clicked", message);
        }
    }
}

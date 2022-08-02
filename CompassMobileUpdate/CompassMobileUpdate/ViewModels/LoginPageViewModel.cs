using CompassMobileModels;
using CompassMobileUpdate.Helpers;
using CompassMobileUpdate.Pages;
using CompassMobileUpdate.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompassMobileUpdate.ViewModels
{
    public class LoginPageViewModel
    {
        private AppService _appService = new AppService();

        public AuthRequest authRequest { get; set; } = new AuthRequest();
        public ICommand LoginCommand { get; }

        private Page _page;

        public LoginPageViewModel(Page page)
        {
            _page = page;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (!ValidationHelper.IsFormValid(authRequest, _page)) { return; }
            var currentUserCredentials = new AuthRequest()
            {
                UserID = authRequest.UserID,
                Password = authRequest.Password,
                RememberMe = authRequest.RememberMe
            };

            AuthResponse response = null;

            var cancelled = false;

            CancellationTokenSource cts = new CancellationTokenSource();

            cts.CancelAfter(60000);

            try
            {
                //TODO: Add internet connection check here.
                response = await _appService.Authenticate(currentUserCredentials, cts.Token);
            }
            catch
            {
                await _page.DisplayAlert("Service Error", "The Service timed out. Please check your connection and try again.", "Close");
                cancelled = true;
            }
            if (response != null)
            {
                if (response.IsAuthenticated)
                {
                    var user = new AppUser(currentUserCredentials.UserID)
                    {
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        JWT = response.Token,
                        JWTExpirationUTC = JWTHelper.GetExpirationTimeFromJWT(response.Token)
                    };

                    AppVariables.User = user;
                    await AppVariables.LocalAppSql.AddUser(AppVariables.User);
                    //TODO: Implement App Center for Insights (Analytics and Crash Reports) Xamarin.Insights no longer supported.

                    App.Current.MainPage = new NavigationPage(new NavPage())
                    {
                        
                    };
                }
            }
            else
            {
                await _page.DisplayAlert("Authentication Error", response.Message, "Close");
            }
            //if(App.Current.MainPage is LoginPage)
        }
        private async Task ShowAlert()
        {
            var message = "You clicked the Login button";

            await _appService.ShowMessage("Login Button Clicked", message);
        }
    }
}

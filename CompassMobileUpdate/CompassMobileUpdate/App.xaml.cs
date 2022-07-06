using CompassMobileUpdate.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompassMobileUpdate
{
    public partial class App : Application
    {
        bool JWTIsExpired = false;
        public App()
        {
            InitializeComponent();

            try
            {
                AppVariables.StartTime = DateTimeOffset.Now;
                AppVariables.Application = this;

                var rootPage = new NavigationPage(new NavPage());

                AppVariables.User = AppVariables.LocalAppSql.GetAppUser();

                CheckJWTExpiration();

                if(AppVariables.User == null || JWTIsExpired)
                {
                    MainPage = new LoginPage();
                }
                else
                {
                    //TODO: Implement App Center for Insights (Analytics and Crash Reports) Xamarin.Insights no longer supported.
                    //TODO: Add ResetVoltageRules method here.
                    MainPage = rootPage;
                }
            }
            catch (Exception e)
            {
                MainPage.DisplayAlert("Error", e.Message, "OK");
                throw;
            }
        }
        private void CheckJWTExpiration()
        {
            if (AppVariables.User != null)
            {
                if (!string.IsNullOrWhiteSpace(AppVariables.User.JWT))
                {
                    if (AppVariables.User.JWTExpirationUTC < DateTime.UtcNow)
                    {
                        JWTIsExpired = true;
                    }
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

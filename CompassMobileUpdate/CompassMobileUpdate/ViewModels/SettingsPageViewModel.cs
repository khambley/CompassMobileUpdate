using CompassMobileUpdate.Pages;
using CompassMobileUpdate.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CompassMobileUpdate.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        
        public ICommand LogoutButtonCommand => new Command(() =>
        {
            Logout(false);
        });
        public void Logout(bool IsSessionTimeout)
        {
            //TODO: Implement DeleteUsers method on LocalAppSql
            //AppVariables.LocalAppSql.DeleteUsers();

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushModalAsync(new LoginPage(IsSessionTimeout));
            });
        }
    }
}

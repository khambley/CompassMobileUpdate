using CompassMobileUpdate.Pages;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CompassMobileUpdate.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel()
        {
        
        }
        
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

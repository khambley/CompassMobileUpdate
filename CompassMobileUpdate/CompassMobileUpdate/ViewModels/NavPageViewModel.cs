using CompassMobileUpdate.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompassMobileUpdate.ViewModels
{
    public class NavPageViewModel : ViewModelBase
    {
        public ICommand SettingsButtonCommand => new Command(async () =>
        {
            await Navigation.PushAsync(new SettingsPage());
        });
    }
}

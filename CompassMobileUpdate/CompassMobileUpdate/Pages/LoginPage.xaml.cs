using CompassMobileUpdate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompassMobileUpdate.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        bool _IsSessionTimeout = false;
        public LoginPage(bool isSessionTimeout)
        {
            _IsSessionTimeout = isSessionTimeout;
            InitializeComponent();
            this.BindingContext = new LoginPageViewModel(this);
            //TODO: Implement Remember My Login toggle
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_IsSessionTimeout)
            {
                DisplayAlert("Session Timeout", "Please authenticate.", "Close");
            }

        }
    }
}
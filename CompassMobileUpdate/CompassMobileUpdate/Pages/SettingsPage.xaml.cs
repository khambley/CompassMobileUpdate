using CompassMobileModels;
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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            var viewModel = new SettingsPageViewModel();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

            envPicker.SelectedIndex = envPicker.Items.IndexOf(AppVariables.AppEnvironment.ToString());
            envPicker.SelectedIndexChanged += picker_SelectedIndexChanged;

            if (AppVariables.User.UserID.ToLower().Equals("kellpy") // Patrick Kelly
                || AppVariables.User.UserID.ToLower().Equals("jordmx") // Mark Jordan
                || AppVariables.User.UserID.ToLower().Equals("chenww") // Wei Chen
                || AppVariables.User.UserID.ToLower().Equals("rizvmr") // Rameez Rizvi
                || AppVariables.User.UserID.ToLower().Equals("ctsrr") // Bob Ruehrdanz
                || AppVariables.User.UserID.ToLower().Equals("ddcb2") // Mark Sayers
                || AppVariables.User.UserID.ToLower().Equals("mayerw") // Ryan Mayer
                || AppVariables.User.UserID.ToLower().Equals("c117324") // Adrian Arva
                || AppVariables.User.UserID.ToLower().Equals("c971939") // Katherine Hambley
            )
            {
                envPicker.IsEnabled = true;
            }
            else
            {
                envPicker.IsEnabled = false;
            }


        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;

            AppEnvironment appEnvironment;

            if (Enum.TryParse<AppEnvironment>(picker.Items[picker.SelectedIndex], true, out appEnvironment))
            {
                AppVariables.AppEnvironment = appEnvironment;
            }
            else
            {
                DisplayAlert("Error", "Application Environment not found", "Close");
            }
        }
    }
}
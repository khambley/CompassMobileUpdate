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
    public partial class NavPage : ContentPage
    {
        public NavPage()
        {
            InitializeComponent();
            //TODO: Add reusable resource dictionary standalone XAML file for one source styling.
            //TODO: Add images and icon assets to iOS app.
        }
    }
}
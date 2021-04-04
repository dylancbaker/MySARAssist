using MySARAssist.ViewModels;
using MySARAssist.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MySARAssist
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.HowToRangeOfDetectionPage), typeof(Views.HowToRangeOfDetectionPage));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
           
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

using MySARAssist.ViewModels;
using MySARAssist.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using MySARAssist.ResourceClasses;

namespace MySARAssist
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.HowToRangeOfDetectionPage), typeof(Views.HowToRangeOfDetectionPage));

            //Manage saved members
            Routing.RegisterRoute(nameof(Views.ListOfSavedTeamMembersPage), typeof(Views.ListOfSavedTeamMembersPage));
            Routing.RegisterRoute(nameof(Views.EditSavedTeamMemberPage), typeof(Views.EditSavedTeamMemberPage));

            //Sign in management pages
            Routing.RegisterRoute(nameof(Views.SignInQRPage), typeof(Views.SignInQRPage));
            Routing.RegisterRoute(nameof(Views.SignOutQRPage), typeof(Views.SignOutQRPage));
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

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
            Routing.RegisterRoute(nameof(WelcomeScreenView), typeof(WelcomeScreenView));

            Routing.RegisterRoute(nameof(Views.HowToRangeOfDetectionPage), typeof(Views.HowToRangeOfDetectionPage));

            //Manage saved members
            Routing.RegisterRoute( nameof(Views.ListOfSavedTeamMembersPage), typeof(Views.ListOfSavedTeamMembersPage));
            Routing.RegisterRoute(nameof(Views.ListOfSavedTeamMembersPage) + "/" + nameof(Views.EditSavedTeamMemberPage), typeof(Views.EditSavedTeamMemberPage));
            Routing.RegisterRoute(nameof(Views.SignInManagementPage) + "/" + nameof(Views.EditSavedTeamMemberPage), typeof(Views.EditSavedTeamMemberPage));

            //Sign in management pages
            Routing.RegisterRoute(nameof(Views.SignInManagementPage), typeof(Views.SignInManagementPage));
            Routing.RegisterRoute(nameof(Views.SignInQRPage), typeof(Views.SignInQRPage));
            Routing.RegisterRoute(nameof(Views.SignOutQRPage), typeof(Views.SignOutQRPage));

            //Calculators
            Routing.RegisterRoute(nameof(Views.CalculatorsPage), typeof(Views.CalculatorsPage));
            Routing.RegisterRoute(nameof(Views.CalculatorsPage) + "/" + nameof(Views.LinearWorkEstimationPage), typeof(Views.LinearWorkEstimationPage));
            Routing.RegisterRoute(nameof(Views.CalculatorsPage) + "/" + nameof(Views.VisualSweepCalculatorPage), typeof(Views.VisualSweepCalculatorPage));
            Routing.RegisterRoute(nameof(Views.CalculatorsPage) + "/" + nameof(Views.GridWorkEstimationPage), typeof(Views.GridWorkEstimationPage));
            //Routing.RegisterRoute(nameof(Views.CalculatorsPage) + "/" + nameof(Views.PacingCalculatorPage), typeof(Views.PacingCalculatorPage));
            Routing.RegisterRoute(nameof(Views.CalculatorsPage) + "/" + nameof(Views.CalculatorPacingPage), typeof(Views.CalculatorPacingPage));
            Routing.RegisterRoute(nameof(Views.CalculatorsPage) + "/" + nameof(Views.VisualSearchResourceEstimation), typeof(Views.VisualSearchResourceEstimation));
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

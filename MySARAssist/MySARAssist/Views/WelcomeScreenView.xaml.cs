using System;
using System.Collections.Generic;
using MySARAssist.ResourceClasses;
using Xamarin.Forms;

namespace MySARAssist.Views
{
    public partial class WelcomeScreenView : ContentPage
    {
        public WelcomeScreenView()
        {
            InitializeComponent();
        }

        async void btnSignInOut_Clicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Views.SignInManagementPage));
        }

        async void btnCalculator_Clicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Views.CalculatorsPage));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }

        private void bnRADEMS_Clicked(object sender, EventArgs e)
        {

        }
    }
}

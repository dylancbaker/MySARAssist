using System;
using System.Collections.Generic;
using MySARAssist.ResourceClasses;
using Xamarin.Forms;


namespace MySARAssist.Views
{
    public partial class CalculatorPacingPage : ContentPage
    {
        public CalculatorPacingPage()
        {
            InitializeComponent();
		
			
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }

    }
}

using System;
using System.Collections.Generic;
using MySARAssist.ResourceClasses;
using Xamarin.Forms;

namespace MySARAssist.Views
{
    public partial class CalculatorVisualSearchSpacing : ContentPage
    {
        public CalculatorVisualSearchSpacing()
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

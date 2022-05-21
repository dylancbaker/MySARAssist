using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySARAssist.ResourceClasses;
using MySARAssist.ViewModels;

namespace MySARAssist.Views
{
    public partial class VisualSearchResourceEstimation : ContentPage
    {
        VisualSearchResourceEstimationViewModel _viewModel = null;

        public VisualSearchResourceEstimation()
        {
            InitializeComponent();
            _viewModel = new ViewModels.VisualSearchResourceEstimationViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}

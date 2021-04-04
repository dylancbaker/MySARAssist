using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySARAssist.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinearWorkEstimationPage : ContentPage
    {
        ViewModels.LinearWorkEstimationViewModel _viewModel;
        public LinearWorkEstimationPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.LinearWorkEstimationViewModel();
            this.BindingContext = _viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}
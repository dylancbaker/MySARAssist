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
    public partial class GridWorkEstimationPage : ContentPage
    {

        ViewModels.GridWorkEstimationViewModel _viewModel;
        public GridWorkEstimationPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.GridWorkEstimationViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}
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
    public partial class VisualSweepCalculatorPage : ContentPage
    {
        ViewModels.VisualSweepCalculatorViewModel _viewModel;
        public VisualSweepCalculatorPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.VisualSweepCalculatorViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}
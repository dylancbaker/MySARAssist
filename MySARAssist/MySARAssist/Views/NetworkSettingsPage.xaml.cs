using MySARAssist.ResourceClasses;
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
    public partial class NetworkSettingsPage : ContentPage
    {
        ViewModels.NetworkSettingsViewModel _viewModel;

        public NetworkSettingsPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.NetworkSettingsViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}
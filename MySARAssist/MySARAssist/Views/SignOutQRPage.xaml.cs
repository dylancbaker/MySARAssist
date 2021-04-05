using MySARAssist.ResourceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.QrCode;

namespace MySARAssist.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignOutQRPage : ContentPage
    {
        ViewModels.SignOutQRViewModel _viewModel;
        public SignOutQRPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.SignOutQRViewModel();
            this.BindingContext = _viewModel;
            BarcodeImageView.BarcodeOptions = new QrCodeEncodingOptions
            {
                Height = 350,
                Width = 350
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}
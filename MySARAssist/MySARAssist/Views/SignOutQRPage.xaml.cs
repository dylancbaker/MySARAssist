using MySARAssist.Interfaces;
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
        float _lastBrightness = 0.5f;

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
            var brightnessService = DependencyService.Get<IBrightnessService>();
            _lastBrightness = brightnessService.GetBrightness();
            brightnessService.SetBrightness((float)1.0);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                slPageContent.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                slPageContent.Orientation = StackOrientation.Vertical;
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
            var brightnessService = DependencyService.Get<IBrightnessService>();
            _lastBrightness = brightnessService.GetBrightness();
            brightnessService.SetBrightness((float)1.0);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var brightnessService = DependencyService.Get<IBrightnessService>();

            brightnessService.SetBrightness(_lastBrightness);
        }
    }
}
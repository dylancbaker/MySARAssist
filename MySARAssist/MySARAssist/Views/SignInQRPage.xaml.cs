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
    public partial class SignInQRPage : ContentPage
    {
        ViewModels.SignInQRViewModel _viewModel;

        public SignInQRPage()
        {
            InitializeComponent();
            tpSignInTime.Time = DateTime.Now.TimeOfDay;
            tpMustBeOut.Time = DateTime.MinValue.TimeOfDay;
            BarcodeImageView.BarcodeOptions = new QrCodeEncodingOptions
            {
                Height = 350,
                Width = 350
            };

            _viewModel = new ViewModels.SignInQRViewModel();
            this.BindingContext = _viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }

        private void chkMustBeOut_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            tpMustBeOut.IsEnabled = chkMustBeOut.IsChecked;
            if (!chkMustBeOut.IsChecked) { tpMustBeOut.Time = DateTime.MinValue.TimeOfDay; }
        }
    }
}
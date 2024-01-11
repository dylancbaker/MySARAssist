
using MySARAssist.Interfaces;
using MySARAssist.ResourceClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace MySARAssist.ViewModels
{
    class CoordinateConverterViewModel : BaseViewModel
    {
        private Coordinate _coordinate = new Coordinate();
        public Command GetLocationCommand { get; }
        public Command CopyUTMCommand { get; }
        public Command CopyShortUTMCommand { get; }
        public Command CopyDDCommand { get; }
        public Command CopyDMSCommand { get; }
        public Command CopyMGRSCommand { get; }
        public Command OpenMapCommand { get; }

        public CoordinateConverterViewModel()
        {
            GetLocationCommand = new Command(OnGetLocationCommand);
            CopyUTMCommand = new Command(async () => await OnCopyUTMCommandAsync());
            CopyShortUTMCommand = new Command(async () => await OnCopyShortUTMCommandAsync());
            CopyDDCommand = new Command(async () => await OnCopyDDCommandAsync());
            CopyDMSCommand = new Command(async () => await OnCopyDMSCommandAsync());
            CopyMGRSCommand = new Command(async () => await OnCopyMGRSCommandAsync());
            OpenMapCommand = new Command(async () => await OnOpenMapCommandAsync());
        }


        private void OnGetLocationCommand()
        {
        }


        private async Task OnCopyUTMCommandAsync() { await Clipboard.SetTextAsync(UTM); DependencyService.Get<Toast>().Show("Copied!"); }
        private async Task OnCopyShortUTMCommandAsync() { await Clipboard.SetTextAsync(ShortUTM); DependencyService.Get<Toast>().Show("Copied!"); }
        private async Task OnCopyDDCommandAsync() { await Clipboard.SetTextAsync(DecimalDegrees); DependencyService.Get<Toast>().Show("Copied!"); }
        private async Task OnCopyDMSCommandAsync() { await Clipboard.SetTextAsync(DMS); DependencyService.Get<Toast>().Show("Copied!"); }
        private async Task OnCopyMGRSCommandAsync() { await Clipboard.SetTextAsync(MGRS); DependencyService.Get<Toast>().Show("Copied!"); }
        private async Task OnOpenMapCommandAsync()
        {
            if (!string.IsNullOrEmpty(DecimalDegrees))
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    // https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                    await Launcher.OpenAsync("http://maps.apple.com/?q=" +_coordinate.DecimalDegreesForURL);
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    // open the maps app directly
                    await Launcher.OpenAsync("geo:0,0?q=" + _coordinate.DecimalDegreesForURL);
                }
                else if (Device.RuntimePlatform == Device.UWP)
                {
                    await Launcher.OpenAsync("bingmaps:?where=" + _coordinate.DecimalDegreesForURL);
                }
            }
        }

        private void TrySettingCoordinate()
        {
            if (string.IsNullOrEmpty(CoordinateInputText)) { CoordinatesOk = false; _coordinate = new Coordinate(); }
            else if (_coordinate.TryParseCoordinate(CoordinateInputText, out _coordinate))
            {
                CoordinatesOk = true;
            }
            else
            {
                CoordinatesOk = false; _coordinate = new Coordinate();
            }
            OnPropertyChanged(nameof(CoordinateParseResult));
            RefreshCoordinates();


        }

        private string _CoordinateInputText = "";
        public string CoordinateInputText
        {
            get { return _CoordinateInputText; }
            set { _CoordinateInputText = value; TrySettingCoordinate(); }
        }

        private bool CoordinatesOk;
        public string CoordinateParseResult
        {
            get
            {
                if (CoordinatesOk) { return "Coordinates OK"; }
                else { return "Coordinates not understood"; }

            }
        }

        public string UTM { get { if (CoordinatesOk) { return _coordinate.UTM; } else { return string.Empty; } } }
        public string MGRS { get { if (CoordinatesOk) { return _coordinate.MGRS; } else { return string.Empty; } } }
        public string DecimalDegrees { get { if (CoordinatesOk) { return _coordinate.DecimalDegrees; } else { return string.Empty; } } }
        public string DMS { get { if (CoordinatesOk) { return _coordinate.DegreesMinutesSeconds; } else { return string.Empty; } } }
        public string ShortUTM { get { if (CoordinatesOk) { return _coordinate.ShortUTM; } else { return string.Empty; } } }

        private void RefreshCoordinates()
        {
            OnPropertyChanged(nameof(UTM));
            OnPropertyChanged(nameof(MGRS));
            OnPropertyChanged(nameof(DecimalDegrees));
            OnPropertyChanged(nameof(DMS));
            OnPropertyChanged(nameof(ShortUTM));
        }
    }
}

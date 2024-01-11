using MySARAssist.Interfaces;
using MySARAssist.ResourceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySARAssist.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoordinateConversionPage : ContentPage
    {
        public CoordinateConversionPage()
        {
            InitializeComponent();

            

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


        #region Location
        private async void setLocation()
        {
            try
            {
                var locationStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null && location.Accuracy < 60)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    Coordinate c = new Coordinate();
                    c.Latitude = location.Latitude;
                    c.Longitude = location.Longitude;
                    txtCoordinates.Text = c.DecimalDegrees;
                    //DisplayQuestion();

                    DependencyService.Get<Toast>().Show("Accuracy: " + location.Accuracy + "m");

                }
                else
                {
                    //sendToast("The location was not accurate enough, please try again (" + location.Accuracy + "m)");
                    await GetCurrentLocation();
                }
            }
            catch (FeatureNotSupportedException)
            {
                DependencyService.Get<Toast>().Show("Sorry, this feature is not supported on this device.");
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {
                DependencyService.Get<Toast>().Show("Sorry, geolocation has not been enabled on this device. Check your device settings.");
                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                DependencyService.Get<Toast>().Show("Sorry, you must enable permissions to get your current location.");
                // Handle permission exception
            }
            catch (Exception)
            {
                DependencyService.Get<Toast>().Show("Sorry, a problem occurred");
                // Unable to get location
            }
        }

        CancellationTokenSource cts;

        async Task GetCurrentLocation()
        {
            try
            {
                var locationStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null && location.Accuracy < 60)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    Coordinate c = new Coordinate();
                    c.Latitude = location.Latitude;
                    c.Longitude = location.Longitude;
                    txtCoordinates.Text = c.DecimalDegrees;
                    //DisplayQuestion();
                    DependencyService.Get<Toast>().Show("Accuracy: " + location.Accuracy + "m");

                }
                else if (location != null)
                {
                    DependencyService.Get<Toast>().Show("The location was not accurate enough (" + location.Accuracy + "m), please try again.");
                }
            }
            catch (FeatureNotSupportedException)
            {
                DependencyService.Get<Toast>().Show("Sorry, this feature is not supported on this device.");
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {
                DependencyService.Get<Toast>().Show("Sorry, geolocation has not been enabled on this device. Check your device settings.");
                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                DependencyService.Get<Toast>().Show("Sorry, you must enable permissions to get your current location.");
                // Handle permission exception
            }
            catch (Exception)
            {
                DependencyService.Get<Toast>().Show("Sorry, a problem occurred");
                // Unable to get location
            }
        }

        #endregion

        private void btnGetCurrent_Clicked(object sender, EventArgs e)
        {
            setLocation();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

          
        }
    }
}
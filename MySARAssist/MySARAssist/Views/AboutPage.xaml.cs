using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySARAssist.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            VersionTracking.Track();

            lblVersionNumber.Text = "Version " + VersionTracking.CurrentVersion;
        }
    }
}
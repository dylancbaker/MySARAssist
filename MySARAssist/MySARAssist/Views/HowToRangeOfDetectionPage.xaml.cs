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
    public partial class HowToRangeOfDetectionPage : ContentPage
    {
        public HowToRangeOfDetectionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}
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
    public partial class CalculatorsPage : ContentPage
    {
        ViewModels.CalculatorsViewModel _viewModel;
        public CalculatorsPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.CalculatorsViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
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
    }
}
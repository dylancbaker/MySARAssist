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
    public partial class SignInManagementPage : ContentPage
    {
        ViewModels.SignInManagementViewModel _viewModel;
        public SignInManagementPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.SignInManagementViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
            _viewModel.OnAppearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                slCurrentMemberExists.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                slCurrentMemberExists.Orientation = StackOrientation.Vertical;
            }

        }
    }
}
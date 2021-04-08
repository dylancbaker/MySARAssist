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
    public partial class PacingCalculatorPage : ContentPage
    {
        ViewModels.PacingCalculatorViewModel _viewModel;
        public PacingCalculatorPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.PacingCalculatorViewModel();
            this.BindingContext = _viewModel;
        }
    }
}
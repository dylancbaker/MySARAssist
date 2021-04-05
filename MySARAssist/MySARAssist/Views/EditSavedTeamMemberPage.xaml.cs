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
    [QueryProperty(nameof(strTeamMemberID), nameof(strTeamMemberID))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSavedTeamMemberPage : ContentPage
    {
        ViewModels.EditSavedTeamMemberViewModel _viewModel;
        public EditSavedTeamMemberPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.EditSavedTeamMemberViewModel();
            this.BindingContext = _viewModel;
        }

        public string strTeamMemberID
        {
            set
            {
                string temp = Uri.UnescapeDataString(value ?? string.Empty);
                if (!string.IsNullOrEmpty(temp))
                {
                    try { _viewModel.TeamMemberID = new Guid(temp); }
                    catch { _viewModel.TeamMemberID = Guid.Empty; }
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResourceHelper.setThemeColor();
        }
    }
}
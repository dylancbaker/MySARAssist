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

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete member?", "Are you sure you want to delete this member?", "Yes", "No");
            if (answer)
            {
                _viewModel.DeleteCommand.Execute(null);
            }
        }

        private void pickParentOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            pickOrganization.ItemsSource = null;
            pickOrganization.ItemsSource = _viewModel.Organizations;
            
        }
    }
}
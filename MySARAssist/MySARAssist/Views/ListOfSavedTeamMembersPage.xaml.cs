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
    public partial class ListOfSavedTeamMembersPage : ContentPage
    {
        ViewModels.ListOfSavedTeamMembersViewModel _viewModel;


        public ListOfSavedTeamMembersPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.ListOfSavedTeamMembersViewModel();
            this.BindingContext = _viewModel;

        }

        private void lbMemberList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.TeamMember member = (Models.TeamMember)e.SelectedItem;
            _viewModel.OnItemSelected(member);
        }
    }
}
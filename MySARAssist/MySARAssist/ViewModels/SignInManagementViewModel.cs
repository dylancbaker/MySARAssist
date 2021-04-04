using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MySARAssist.ViewModels
{
    public class SignInManagementViewModel : BaseViewModel
    {
        public SignInManagementViewModel()
        {
            SignInCommand = new Command(OnSignInCommand);
            SignOutCommand = new Command(OnSignOutCommand);
            EditTeamMembersCommand = new Command(OnEditTeamMembersCommand);
            ChangeSelectedMemberCommand = new Command(OnChangeSelectedMemberCommand);

        }

        public Command SignInCommand { get; }
        public Command SignOutCommand { get; }
        public Command EditTeamMembersCommand { get; }
        public Command ChangeSelectedMemberCommand { get; }


        public string CurrentMemberName
        {
            get
            {
                if (App.CurrentTeamMember != null) { return App.CurrentTeamMember.NameWithGroup; }
                else { return "-No member selected-"; }
            }
        }

        private async void OnSignInCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.SignInQRPage));
        }

        private async void OnSignOutCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.SignOutQRPage));
        }

        private async void OnEditTeamMembersCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.ListOfSavedTeamMembersPage));
        }

        private async void OnChangeSelectedMemberCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.ListOfSavedTeamMembersPage));
        }
    }
}

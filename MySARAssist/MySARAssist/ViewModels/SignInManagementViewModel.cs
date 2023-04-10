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
            AddMemberCommand = new Command(OnAddMember);
            EditMemberCommand = new Command(OnEditMember);

        }

        public Command SignInCommand { get; }
        public Command SignOutCommand { get; }
        public Command EditTeamMembersCommand { get; }
        public Command ChangeSelectedMemberCommand { get; }
        public Command AddMemberCommand { get; set; }
        public Command EditMemberCommand { get; }

        public void OnAppearing()
        {
            OnPropertyChanged(nameof(CurrentMemberName));
            OnPropertyChanged(nameof(AllowSignInAndOut));
        }

        public string CurrentMemberName
        {
            get
            {
                if (App.CurrentTeamMember != null) { return App.CurrentTeamMember.Name; }
                else { return "-No member selected-"; }
            }
        }
        public string CurrentMemberDetails
        {
            get
            {
                if (App.CurrentTeamMember != null) {
                    StringBuilder details = new StringBuilder();
                    if (App.CurrentTeamMember.SARM) { details.Append("SARM"); }
                    else if (App.CurrentTeamMember.GSTL) { details.Append("GSTL"); }
                    if(details.Length > 0) { details.Append(" | "); }
                    details.Append(App.CurrentTeamMember.Group);
                    return details.ToString();
                }
                else { return string.Empty; }
            }
        }

        public bool AllowSignInAndOut
        {
            get
            {
                return App.CurrentTeamMember != null;
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

        public async void OnAddMember()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.SignInManagementPage) + "/" + nameof(Views.EditSavedTeamMemberPage)}");
        }

        public async void OnEditMember()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.SignInManagementPage) + "/" + nameof(Views.EditSavedTeamMemberPage)}?strTeamMemberID={App.CurrentTeamMember.PersonID}");
            
        }
    }
}

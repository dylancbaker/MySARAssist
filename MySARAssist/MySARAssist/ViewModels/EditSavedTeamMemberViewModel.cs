using System;
using System.Collections.Generic;
using MySARAssist.Interfaces;
using MySARAssist.Models;
using MySARAssist.ResourceClasses;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using ProtoBuf.WellKnownTypes;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels
{
    class EditSavedTeamMemberViewModel : BaseViewModel
    {
        public EditSavedTeamMemberViewModel()
        {
            Organizations = OrganizationTools.GetOrganizations(Guid.Empty);
            CancelCommand = new Command(OnCancelCommand);
            SaveCommand = new Command(OnSaveCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            CurrentMember = new TeamMember();
            Organization mostPopularOrg = App.TeamMemberManager.GetMostCommonOrganization();
            if (mostPopularOrg != null) { CurrentMember.MemberOrganization = mostPopularOrg; }
        }

        public List<Organization> Organizations { get; private set; }
        public TeamMember CurrentMember { get; private set; } = new TeamMember();

        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        public Guid TeamMemberID
        {
            set
            {
                Guid ID = value;
                if (ID != Guid.Empty)
                {
                    SetTeamMember(ID);
                }
                else
                {
                    CurrentMember = new TeamMember();
                    

                }
                OnPropertyChanged(nameof(CurrentMember));
            }
        }

        private async  void SetTeamMember(Guid ID)
        {
            CurrentMember  = await App.TeamMemberManager.GetItemAsync(ID);
        }

    


        private async void OnCancelCommand()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void OnSaveCommand()
        {
            CleanInputs();
            if (ValidateMemberInfo())
            {
                if (await App.TeamMemberManager.UpsertItemAsync(CurrentMember))
                {
                    if (App.TeamMemberManager.GetCurrentTeamMember() == null)
                    {
                        App.TeamMemberManager.setCurrentTeamMember(CurrentMember.PersonID);
                        App.CurrentTeamMember = App.TeamMemberManager.GetCurrentTeamMember();
                        OnPropertyChanged(nameof(App.CurrentTeamMember));
                    }

                    //DependencyService.Get<Toast>().Show("Team Member Saved");
                    
                    await Application.Current.MainPage.DisplaySnackBarAsync("Saved", null, null, TimeSpan.FromSeconds(3));
                    
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    //DependencyService.Get<Toast>().Show("ERROR: Member not saved");
                    
                    await Application.Current.MainPage.DisplaySnackBarAsync("ERROR: Member not saved", null, null, TimeSpan.FromSeconds(3));
                }
            } else {
                //DependencyService.Get<Toast>().Show("ERROR: Member not saved. You must enter a name and valid email address.");
               
                await Application.Current.MainPage.DisplaySnackBarAsync("ERROR: Member not saved. You must enter a name and valid email address.", null, null, TimeSpan.FromSeconds(3));
            }
        }

    
        private bool ValidateMemberInfo() {
            bool isValid = true;
            if (CurrentMember != null)
            {
                if (string.IsNullOrEmpty(CurrentMember.Name)) { isValid = false; }
                if (string.IsNullOrEmpty(CurrentMember.Email)) { isValid = false; }
                if (!CurrentMember.Email.isValidEmailAddress()) { isValid = false; }
            }
            return isValid;
        }

        private void CleanInputs()
        {
            if (CurrentMember != null)
            {
                if (CurrentMember.Name != null) { CurrentMember.Name = CurrentMember.Name.Replace(";", ""); }
                if (CurrentMember.Address != null) { CurrentMember.Address = CurrentMember.Address.Replace(";", ""); }
                if (CurrentMember.Phone != null) { CurrentMember.Phone = CurrentMember.Phone.Replace(";", ""); }
                if (CurrentMember.Email != null) { CurrentMember.Email = CurrentMember.Email.Replace(";", ""); }
                if (CurrentMember.Reference != null) { CurrentMember.Reference = CurrentMember.Reference.Replace(";", ""); }
                if (CurrentMember.Callsign != null) { CurrentMember.Callsign = CurrentMember.Callsign.Replace(";", ""); }
                if (CurrentMember.NOKName != null) { CurrentMember.NOKName = CurrentMember.NOKName.Replace(";", ""); }
                if (CurrentMember.NOKRelation != null) { CurrentMember.NOKRelation = CurrentMember.NOKRelation.Replace(";", ""); }
                if (CurrentMember.NOKPhone != null) { CurrentMember.NOKPhone = CurrentMember.NOKPhone.Replace(";", ""); }
            }
        }

        private async void OnDeleteCommand()
        {
            if(await App.TeamMemberManager.DeleteItemAsync(CurrentMember.PersonID))
            {
                if(App.CurrentTeamMember.PersonID == CurrentMember.PersonID)
                {
                    App.CurrentTeamMember = null;
                }
                await Application.Current.MainPage.DisplaySnackBarAsync("Team Member Deleted", null, null, TimeSpan.FromSeconds(3));
                //DependencyService.Get<Toast>().Show("Team Member Deleted");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplaySnackBarAsync("ERROR: Member not deleted", null, null, TimeSpan.FromSeconds(3));
                //DependencyService.Get<Toast>().Show("ERROR: Member not deleted");
            }
        }
    }
}

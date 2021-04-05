using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySARAssist.Interfaces;
using MySARAssist.Models;
using Xamarin.Forms;

namespace MySARAssist.ViewModels
{
    class EditSavedTeamMemberViewModel : BaseViewModel
    {
        public EditSavedTeamMemberViewModel()
        {
            Organizations = new Organization().getStaticOrganizationList();
            CancelCommand = new Command(OnCancelCommand);
            SaveCommand = new Command(OnSaveCommand);
            CurrentMember = new TeamMember();
        }

        public List<Organization> Organizations { get; private set; }
        public TeamMember CurrentMember { get; private set; }

        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

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
            if (await App.TeamMemberManager.UpsertItemAsync(CurrentMember))
            {
                DependencyService.Get<Toast>().Show("Team Member Saved");
                await Shell.Current.GoToAsync("..");
            } else
            {
                DependencyService.Get<Toast>().Show("ERROR: Member not saved");
            }
        }
    }
}

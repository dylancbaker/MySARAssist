using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using MySARAssist.Models;
using System.Threading.Tasks;
using MySARAssist.Interfaces;

namespace MySARAssist.ViewModels
{
    class ListOfSavedTeamMembersViewModel :BaseViewModel
    {
        public ObservableCollection<Models.TeamMember> Items { get; private set; }
        TeamMember SelectedItem;

        public ListOfSavedTeamMembersViewModel()
        {
            Items = new ObservableCollection<TeamMember>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //ItemTapped = new Command<TeamMember>(OnItemSelected);
            EditTeamMemberCommand = new Command((e) =>
            {
                Guid selected_memberID = new Guid(e.ToString());

                Shell.Current.GoToAsync($"{nameof(Views.EditSavedTeamMemberPage)}?strTeamMemberID={selected_memberID}");
            });

            SelectTeamMemberCommand = new Command((e) =>
            {
                Guid selected_memberID = new Guid(e.ToString());

                App.TeamMemberManager.setCurrentTeamMember(selected_memberID);
                App.CurrentTeamMember = App.TeamMemberManager.GetCurrentTeamMember();
                OnPropertyChanged(nameof(App.CurrentTeamMember));

                DependencyService.Get<Toast>().Show("Selected Member Updated");
                try
                {
                    ExecuteLoadItemsCommand();
                } catch (Exception)
                {

                }
                
            });

            AddMemberCommand = new Command(OnAddMember);
        }


        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            await ExecuteLoadItemsCommand();
            IsBusy = false;
        }


        public Command EditTeamMemberCommand { get; }
        public Command SelectTeamMemberCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command<TeamMember> ItemTapped { get; }
        public bool IsRefreshing { get; set; }
        public Command AddMemberCommand { get; set; }

        async public Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));
            try
            {

                Items.Clear();
                //new Mitigation_Assessment().deleteBlankAssessments(); //for debug reasons
                var allItems = await App.TeamMemberManager.GetItemsAsync();

                foreach (TeamMember m in allItems)
                {
                    Items.Add(m);
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public async void OnItemSelected(TeamMember item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(Views.EditSavedTeamMemberPage)}?strTeamMemberID={item.PersonID}");
        }
        public async void OnAddMember()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.EditSavedTeamMemberPage)}");
        }
    }
}

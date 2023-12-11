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
        public ObservableCollection<Models.Personnel> Items { get; private set; }
        Personnel SelectedItem;

        public ListOfSavedTeamMembersViewModel()
        {
            Items = new ObservableCollection<Personnel>();

            LoadItemsCommand = new Command(async () => ExecuteLoadItemsCommand());

            //ItemTapped = new Command<TeamMember>(OnItemSelected);
            EditTeamMemberCommand = new Command((e) =>
            {
                Guid selected_memberID = new Guid(e.ToString());

                Shell.Current.GoToAsync($"{nameof(Views.ListOfSavedTeamMembersPage) + "/" + nameof(Views.EditSavedTeamMemberPage)}?strTeamMemberID={selected_memberID}");
            });

            SelectTeamMemberCommand = new Command((e) =>
            {
                Guid selected_memberID = new Guid(e.ToString());

                App.PersonnelManager.setCurrentTeamMember(selected_memberID);
                App.CurrentTeamMember = App.PersonnelManager.GetCurrentTeamMember();
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
           
            SelectedItem = null;
            ExecuteLoadItemsCommand();
           
        }


        public Command EditTeamMemberCommand { get; }
        public Command SelectTeamMemberCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command<Personnel> ItemTapped { get; }
        public bool IsRefreshing { get; set; }
        public Command AddMemberCommand { get; set; }

        public void ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));
            try
            {

                Items.Clear();
                //new Mitigation_Assessment().deleteBlankAssessments(); //for debug reasons
                var allItems = App.PersonnelManager.GetItems();

                foreach (Personnel m in allItems)
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

        public async void OnItemSelected(Personnel item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(Views.ListOfSavedTeamMembersPage) + "/" + nameof(Views.EditSavedTeamMemberPage)}?strTeamMemberID={item.PersonID}");
        }
        public async void OnAddMember()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.ListOfSavedTeamMembersPage) + "/" + nameof(Views.EditSavedTeamMemberPage)}");
        }
    }
}

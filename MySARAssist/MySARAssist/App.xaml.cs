using MySARAssist.Models;
using MySARAssist.Services;
using MySARAssist.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySARAssist
{
    public partial class App : Application
    {
        public static Personnel CurrentTeamMember { get; set; }
        public static Services.TeamMember_ManagerService TeamMemberManager { get; private set; }
        public static Services.Personnel_ManagerService PersonnelManager { get; private set; }


        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        public App(string databaseLocation)
        {
            DatabaseLocation = databaseLocation;
            TeamMemberManager = new Services.TeamMember_ManagerService();
            PersonnelManager = new Personnel_ManagerService();
            InitializeComponent();
            MainPage = new AppShell();

            CurrentTeamMember = PersonnelManager.GetCurrentTeamMember();
            if(CurrentTeamMember != null && CurrentTeamMember.PersonID == Guid.Empty)
            {
                PersonnelManager.DeleteAllItemsAsync();
            }
            if (CurrentTeamMember == null && TeamMemberManager.GetCurrentTeamMember() != null)
            {
                CurrentTeamMember = (TeamMemberManager.GetCurrentTeamMember()).PersonnelFromTeamMember();
                PersonnelManager.UpsertItemAsync(CurrentTeamMember);
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

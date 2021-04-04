using MySARAssist.Models;

using MySARAssist.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySARAssist
{
    public partial class App : Application
    {
        public static TeamMember CurrentTeamMember { get; set; }
        public static Services.TeamMember_ManagerService TeamMemberManager { get; private set; }


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
            InitializeComponent();
            MainPage = new AppShell();
            CurrentTeamMember = TeamMemberManager.GetCurrentTeamMember();
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

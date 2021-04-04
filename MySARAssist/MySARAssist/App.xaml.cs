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

        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        public App(string databaseLocation)
        {
            DatabaseLocation = databaseLocation;
            InitializeComponent();
            MainPage = new AppShell();
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

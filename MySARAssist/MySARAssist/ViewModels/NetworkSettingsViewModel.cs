using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MySARAssist.ResourceClasses;

namespace MySARAssist.ViewModels
{
    class NetworkSettingsViewModel : BaseViewModel
    {
        public NetworkSettingsViewModel()
        {
            TestNetworkCommand = new Command(OnTestNetworkCommand);
            ServerIP = "10.0.0.107";
            PortNumber = 42999;
        }

        private string _ServerIP;
        private int _PortNumber;

        public string ServerIP
        {
            get => _ServerIP;
            set { _ServerIP = value; OnPropertyChanged(nameof(ServerIP)); }
        }
        public int PortNumber
        {
            get => _PortNumber;
            set { _PortNumber = value; OnPropertyChanged(nameof(PortNumber)); }
        }

        public Command TestNetworkCommand { get; }

        Guid NetworkTestGuidValue = Guid.Empty;
        bool silentNetworkTest = true;
        bool initialConnectionTest = false;

        private void OnTestNetworkCommand()
        {
            NetworkTestGuidValue = Guid.NewGuid();
            silentNetworkTest = false;
            
            Services.Network_Services.SendNetworkObject(NetworkTestGuidValue, "test", ServerIP, PortNumber.ToString());
        }
    }
}

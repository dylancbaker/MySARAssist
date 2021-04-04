using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.ViewModels
{
    class SignInQRViewModel : BaseViewModel
    {
        public SignInQRViewModel()
        {

        }
        private DateTime _SignInTime;
        private DateTime _MustBeOutTime;
        private bool _UseMustBeOutTime;

        public DateTime SignInTime
        {
            get => _SignInTime; set
            {
                _SignInTime = value;
                OnPropertyChanged(nameof(SignInTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }

        public DateTime MustBeOutTime
        {
            get => _SignInTime; set
            {
                _MustBeOutTime = value;
                OnPropertyChanged(nameof(MustBeOutTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }
        public bool UseMustBeOutTime
        {
            get => _UseMustBeOutTime;
            set
            {
                _UseMustBeOutTime = value;
                OnPropertyChanged(nameof(UseMustBeOutTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }

        public string FullQRString
        {
            get
            {
                string qrString = "~" + App.CurrentTeamMember.StringForQR;
                qrString += SignInTime + ";";
                if (UseMustBeOutTime)
                {
                    qrString += MustBeOutTime + ";";
                }
                qrString += "~";
                return qrString;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.ViewModels
{
    class SignInQRViewModel : BaseViewModel
    {
        public SignInQRViewModel()
        {
            SignInTime = DateTime.Now.TimeOfDay;
            MustBeOutTime = DateTime.Now.TimeOfDay;
        }
        private TimeSpan _SignInTime;
        private TimeSpan _MustBeOutTime;
        private bool _UseMustBeOutTime;

        public string CurrentMemberName
        {
            get
            {
                if (App.CurrentTeamMember != null) { return App.CurrentTeamMember.NameWithGroup; }
                else { return "-No member selected-"; }
            }
        }

        public TimeSpan SignInTime
        {
            get => _SignInTime; set
            {
                _SignInTime = value;
                OnPropertyChanged(nameof(SignInTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }

        public TimeSpan MustBeOutTime
        {
            get => _MustBeOutTime; set
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
                try
                {
                    string qrString = "~" + App.CurrentTeamMember.StringForQR;
                    qrString += convertTimespanToDate(SignInTime) + ";";
                    if (UseMustBeOutTime)
                    {
                        qrString += convertTimespanToDate(MustBeOutTime) + ";";
                    }
                    qrString += "~";
                    return qrString;
                } catch { return string.Empty; }
            }
        }

        private DateTime convertTimespanToDate(TimeSpan ts)
        {
            DateTime today = DateTime.Now;

            DateTime dt = new DateTime(today.Year, today.Month, today.Day);
            dt = dt + ts;
            return dt;
        }
    }
}

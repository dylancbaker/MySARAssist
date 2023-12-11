using MySARAssist.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.ViewModels
{
    class SignOutQRViewModel : BaseViewModel
    {
        public SignOutQRViewModel()
        {
            SignOutTime = DateTime.Now.TimeOfDay;
            KMs = 0;
        }

        private TimeSpan _SignOutTime;
        private int _KMs;

        public string CurrentMemberName
        {
            get
            {
                if (App.CurrentTeamMember != null) { return App.CurrentTeamMember.NameWithGroup; }
                else { return "-No member selected-"; }
            }
        }

        public TimeSpan SignOutTime
        {
            get => _SignOutTime; set
            {
                _SignOutTime = value;
                OnPropertyChanged(nameof(SignOutTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }
        public int KMs
        {
            get => _KMs;
            set
            {
                _KMs = value;
                OnPropertyChanged(nameof(KMs));
                OnPropertyChanged(nameof(FullQRString));
            }
        }

        public string FullQRString
        {
            get
            {
                try
                {
                    string qrString = "^" + App.CurrentTeamMember.StringForQR();
                    qrString += convertTimespanToDate(SignOutTime).ToString("HH:mm:ss") + ";";
                    qrString += KMs + ";";
                    qrString += "^";
                    return qrString;
                } catch
                {
                    return string.Empty;
                }
            }
        }

        /*
        public string FullQRString
        {
            get
            {
                string qrString = "~" + App.CurrentTeamMember.StringForQR;
                qrString += convertTimespanToDate(SignInTime) + ";";
                if (UseMustBeOutTime)
                {
                    qrString += convertTimespanToDate(MustBeOutTime) + ";";
                }
                qrString += "~";
                return qrString;
            }
        }
        */


        private DateTime convertTimespanToDate(TimeSpan ts)
        {
            DateTime today = DateTime.Now;

            DateTime dt = new DateTime(today.Year, today.Month, today.Day);
            dt = dt + ts;
            return dt;
        }
    }
}

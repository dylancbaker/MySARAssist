using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MySARAssist.ViewModels
{
    public class GridWorkEstimationViewModel : BaseViewModel
    {
        public GridWorkEstimationViewModel()
        {
            CalculateCommand = new Command(() =>
            {
                if (Area > 0 && Spacing > 0 && teamMembers > 0 && SearcherSpeed > 0)
                {
                    double tracklengtheffort = (Area * 1000) / Spacing;

                    estimatedDuration = tracklengtheffort / SearcherSpeed / teamMembers;
                }
                else { estimatedDuration = 0; }
                OnPropertyChanged(nameof(EstimatedDuration));
                //var argsts = new PropertyChangedEventArgs(nameof(EstimatedDuration));
                // PropertyChanged?.Invoke(this, argsts);

            });

            EraseCommand = new Command(() =>
            {
                Area = 0;
                SearcherSpeed = 1.6;
                TeamMembers = "2";
                Spacing = 0;
                EstimatedDuration = "0";
            });

            SpeedUpCommand = new Command(() =>
            {
                SearcherSpeed += 0.1;
                OnPropertyChanged(nameof(SearcherSpeed));
            });
            SpeedDownCommand = new Command(() =>
            {
                SearcherSpeed -= 0.1;
                OnPropertyChanged(nameof(SearcherSpeed));
            });

            MembersUpCommand = new Command(() =>
            {
                teamMembers += 1;
                OnPropertyChanged(nameof(TeamMembers));
            });
            MembersDownCommand = new Command(() =>
            {
                teamMembers -= 1;
                OnPropertyChanged(nameof(TeamMembers));
            });

            AreaUpCommand = new Command(() =>
            {
                Area += 0.01;
                OnPropertyChanged(nameof(Area));
            });
            AreaDownCommand = new Command(() =>
            {
                Area -= 0.01;
                OnPropertyChanged(nameof(Area));
            });

            SpacingUpCommand = new Command(() =>
            {
                Spacing += 1;
                OnPropertyChanged(nameof(Spacing));
            });
            SpacingDownCommand = new Command(() =>
            {
                Spacing -= 1;
                OnPropertyChanged(nameof(Spacing));
            });
        }

        public Command CalculateCommand { get; }
        public Command EraseCommand { get; }
        public Command SpeedUpCommand { get; }
        public Command SpeedDownCommand { get; }
        public Command MembersUpCommand { get; }
        public Command MembersDownCommand { get; }
        public Command AreaUpCommand { get; }
        public Command AreaDownCommand { get; }
        public Command SpacingUpCommand { get; }
        public Command SpacingDownCommand { get; }



        double estimatedDuration = 0;
        public string EstimatedDuration
        {
            get => string.Format("{0:#,##0.0}", estimatedDuration); set
            {
                double.TryParse(value, out estimatedDuration);
                OnPropertyChanged(nameof(EstimatedDuration));

            }
        }

        double _searcherSpeed = 1.6;
        public double SearcherSpeed { get => _searcherSpeed; set => _searcherSpeed = value; }

        int teamMembers = 2;
        public string TeamMembers
        {
            get => string.Format("{0:#,##0}", teamMembers);
            set
            {
                int.TryParse(value, out teamMembers);
                OnPropertyChanged(nameof(TeamMembers));
            }
        }

        double _area = 0;
        public double Area { get => _area; set => _area = value; }

        /* public string Area
         {
             get => string.Format("{0:#,##0.00}", area);

             set
             {
                 double.TryParse(value, out area);
                 var args = new PropertyChangedEventArgs(nameof(Area));
                 PropertyChanged?.Invoke(this, args);
             }
         }
        */


        double _spacing = 0;
        public double Spacing { get => _spacing; set => _spacing = value; }
    }
}

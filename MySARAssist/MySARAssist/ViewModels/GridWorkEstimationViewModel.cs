﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MySARAssist.ViewModels
{
    public class GridWorkEstimationViewModel : BaseViewModel
    {
        private void CalculateTimeEstimate()
        {
            if (Area > 0 && Spacing > 0 && teamMembers > 0 && SearcherSpeed > 0)
            {
                double tracklengtheffort = (Area * 1000) / Spacing;

                estimatedDuration = tracklengtheffort / SearcherSpeed / teamMembers;
            }
            else { estimatedDuration = 0; }
            OnPropertyChanged(nameof(EstimatedDuration));
        }

        public GridWorkEstimationViewModel()
        {
            CalculateCommand = new Command(() =>
            {
                CalculateTimeEstimate();   
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
                if (SearcherSpeed > 0.0) { SearcherSpeed -= 0.1; }
              
                OnPropertyChanged(nameof(SearcherSpeed));
            });

            MembersUpCommand = new Command(() =>
            {
                 teamMembers += 1;
               
                OnPropertyChanged(nameof(TeamMembers));
            });
            MembersDownCommand = new Command(() =>
            {
                if (teamMembers > 0) { teamMembers -= 1; }
               
                OnPropertyChanged(nameof(TeamMembers));
            });

            AreaUpCommand = new Command(() =>
            {
                Area += 0.01;
                OnPropertyChanged(nameof(AreaStr));
                OnPropertyChanged(nameof(Area));
            });
            AreaDownCommand = new Command(() =>
            {
                if (Area > 0) { Area -= 0.01; }
                OnPropertyChanged(nameof(AreaStr));
                OnPropertyChanged(nameof(Area));
            });

            SpacingUpCommand = new Command(() =>
            {
                Spacing += 1;
               
                OnPropertyChanged(nameof(Spacing));
            });
            SpacingDownCommand = new Command(() =>
            {
                if (Spacing > 0) { Spacing -= 1; }
               
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
            get
            {
                if (estimatedDuration > 0) { return string.Format("{0:#,##0.0}", estimatedDuration); }
                return null;
            }
            set
            {
                double.TryParse(value, out estimatedDuration);
                CalculateTimeEstimate();
                OnPropertyChanged(nameof(EstimatedDuration));
            }
        }

        double _searcherSpeed = 1.6;
        public double SearcherSpeed { get => _searcherSpeed; set { _searcherSpeed = value; CalculateTimeEstimate(); OnPropertyChanged(nameof(SearcherSpeedStr)); } }
        public string SearcherSpeedStr
        {
            get { if (_searcherSpeed > 0) { return _searcherSpeed.ToString(); } return null; }
            set { double.TryParse(value, out _searcherSpeed); CalculateTimeEstimate(); OnPropertyChanged(nameof(SearcherSpeedStr)); }
        }

        int teamMembers = 2;
        public string TeamMembers
        {
            get { if (teamMembers > 0) { return teamMembers.ToString(); } return null; }
            set { int.TryParse(value, out teamMembers); CalculateTimeEstimate(); OnPropertyChanged(nameof(TeamMembers)); }
        }

        double _area = 0.01;
        public double Area { get => _area; set { _area = value; CalculateTimeEstimate(); OnPropertyChanged(nameof(Area)); } }
        public string AreaStr
        {
            get { if(Area > 0) { return Area.ToString(); } return null; }
            set { if (!string.IsNullOrEmpty(value)) { double.TryParse(value, out double temp); Area = temp; } else { Area = 0; } }
        }

       
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


        double _spacing = 1;
        public double Spacing { get => _spacing; set { _spacing = value; CalculateTimeEstimate(); OnPropertyChanged(nameof(SpacingStr)); } }
        public string SpacingStr
        {
            get { if (_spacing > 0) { return _spacing.ToString(); } return null; }
            set { double.TryParse(value, out _spacing); CalculateTimeEstimate(); OnPropertyChanged(nameof(SpacingStr)); }
        }
    }
}

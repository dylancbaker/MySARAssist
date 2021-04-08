using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.ViewModels
{
    class PacingCalculatorViewModel : BaseViewModel
    {
        public PacingCalculatorViewModel()
        {

        }

       
        double _PacesPer100m = 0;
        public double PacesPer100m
        {
            get => _PacesPer100m; set { _PacesPer100m = value; OnPropertyChanged(nameof(PacesFromDistance)); OnPropertyChanged(nameof(DistanceFromPaces)); }
        }
        double _PacesTaken = 0;
        public double PacesTaken
        {
            get => _PacesTaken; set { _PacesTaken = value; OnPropertyChanged(nameof(PacesTaken)); OnPropertyChanged(nameof(DistanceFromPaces)); }
        }
        double _DistanceToTravel = 0;
        public double DistanceToTravel
        {
            get => _DistanceToTravel; set { _DistanceToTravel = value; OnPropertyChanged(nameof(DistanceToTravel)); OnPropertyChanged(nameof(PacesFromDistance)); }
        }

        double PacesPerMeter
        {
            get { return PacesPer100m / 100; }
        }

        public double PacesFromDistance
        {
            get
            {
                return Math.Round(PacesPerMeter * DistanceToTravel,2);
            }
        }

        public double DistanceFromPaces
        {
            get
            {
                return Math.Round(PacesTaken / PacesPerMeter,2);
            }
        }

    }
}

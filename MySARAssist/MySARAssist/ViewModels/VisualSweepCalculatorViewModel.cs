using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace MySARAssist.ViewModels
{
    class VisualSweepCalculatorViewModel : BaseViewModel
    {
        public VisualSweepCalculatorViewModel()
        {
            CalculateCommand = new Command(() =>
            {
                if (targetPOD <= 0)
                {
                    teamSpacing = rangeOfDetection * visibilityModifier;
                    pod = StatisticalTools.calculatePOD(rangeOfDetection, visibilityModifier, teamSpacing);
                }
                else
                {
                    //if (targetPOD > 1) { targetPOD = targetPOD / 100; }
                    teamSpacing = StatisticalTools.calculateSpacing(rangeOfDetection, visibilityModifier, targetPOD);
                    pod = StatisticalTools.calculatePOD(rangeOfDetection, visibilityModifier, teamSpacing);
                }



                OnPropertyChanged(nameof(TeamSpacing));
                OnPropertyChanged(nameof(POD));

            });

            EraseCommand = new Command(() =>
            {
                SelectedVisibilityIndex = 0;
                RangeOfDetection = "0";
                TargetPOD = "0.63";
                POD = "0";
                TeamSpacing = "0";
            });

            RDUpCommand = new Command(() =>
            {
                rangeOfDetection += 1;
                OnPropertyChanged(nameof(RangeOfDetection));

            });
            RDDownCommand = new Command(() =>
            {
                rangeOfDetection -= 1;
                OnPropertyChanged(nameof(RangeOfDetection));

            });

            IdealPODUpCommand = new Command(() =>
            {
                targetPOD += 0.01;
                OnPropertyChanged(nameof(TargetPODAsPercent));

            });
            IdealPODDownCommand = new Command(() =>
            {
                targetPOD -= 0.01;
                OnPropertyChanged(nameof(TargetPODAsPercent));

            });

            HowToRDCommand = new Command(OnHowToRD);
        }

        private async void OnHowToRD()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.HowToRangeOfDetectionPage)}");
        }


        public Command CalculateCommand { get; }
        public Command EraseCommand { get; }
        public Command RDUpCommand { get; }
        public Command RDDownCommand { get; }
        public Command IdealPODUpCommand { get; }
        public Command IdealPODDownCommand { get; }
        public Command HowToRDCommand { get; }


        double rangeOfDetection;
        public string RangeOfDetection
        {
            get => string.Format("{0:#,##0}", rangeOfDetection); set
            {
                double.TryParse(value, out rangeOfDetection);
                OnPropertyChanged(nameof(RangeOfDetection));

            }
        }


        int selectedVisibilityIndex;
        public int SelectedVisibilityIndex
        {
            get => selectedVisibilityIndex; set
            {
                selectedVisibilityIndex = value;
                OnPropertyChanged(nameof(SelectedVisibilityIndex));

            }
        }
        double visibilityModifier
        {
            get
            {
                switch (selectedVisibilityIndex)
                {
                    case 0:
                        return 1.8;
                    case 1:
                        return 1.6;
                    case 2:
                        return 1.1;
                    default:
                        return 1.8;
                }
            }
        }
        double targetPOD = 0.63;
        public string TargetPOD
        {
            get => targetPOD.ToString();
            set
            {
                double.TryParse(value, out targetPOD);
                //if(targetPOD > 1) { targetPOD = targetPOD / 100; }
                OnPropertyChanged(nameof(TargetPOD));

            }
        }

        public double TargetPODAsPercent
        {
            get { return targetPOD * 100; }
            set
            {
                if (value <= 100)
                {
                    targetPOD = value / 100;

                }
                OnPropertyChanged(nameof(TargetPODAsPercent));

            }
        }


        double teamSpacing = 0;
        double pod = 0;
        public string TeamSpacing
        {
            get => teamSpacing.ToString();
            set
            {
                double.TryParse(value, out teamSpacing);
                OnPropertyChanged(nameof(TeamSpacing));

            }
        }
        public string POD
        {
            get => string.Format("{0:P1}", pod);
            set
            {
                double.TryParse(value, out pod);
                OnPropertyChanged(nameof(POD));


            }
        }




    }
}

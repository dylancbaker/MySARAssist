using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MySARAssist.ViewModels
{
    class CalculatorsViewModel : BaseViewModel
    {
        public CalculatorsViewModel()
        {
            GridCommand = new Command(OnGridCommand);
            LineCommand = new Command(OnLineCommand);
            VisualSpacingCommand = new Command(OnVisualSpacingCommand);
            PacingCommand = new Command(OnPacingCommand);
        }

        public Command GridCommand { get; }
        public Command LineCommand { get; }
        public Command VisualSpacingCommand { get; }
        public Command PacingCommand { get;}

        private async void OnGridCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CalculatorsPage) + "/" + nameof(Views.GridWorkEstimationPage));
        }

        private async void OnLineCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CalculatorsPage) + "/" + nameof(Views.LinearWorkEstimationPage));
        }
        private async void OnVisualSpacingCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CalculatorsPage) + "/" + nameof(Views.VisualSweepCalculatorPage));
        }
        private async void OnPacingCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CalculatorsPage) + "/" + nameof(Views.PacingCalculatorPage));
        }
    }
}

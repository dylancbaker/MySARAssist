using MySARAssist.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.ViewModels
{
    internal class RademsTypeSelectViewModel : BaseViewModel
    {

        public List<RADeMSCategory> AllCagegories { get => RADeMSTools.GetCategories(); }
    }
}

using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using MySARAssist.Interfaces;
using MySARAssist.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSBrightnessService))]
namespace MySARAssist.iOS
{


    public class iOSBrightnessService : IBrightnessService
    {
        public float GetBrightness()
        {
            return (float)UIScreen.MainScreen.Brightness;
        }

        public void SetBrightness(float brightness)
        {
            UIScreen.MainScreen.Brightness = brightness;
        }


    }
}
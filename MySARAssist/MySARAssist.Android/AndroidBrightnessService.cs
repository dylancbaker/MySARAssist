using System;
using Android.Views;
using MySARAssist.Droid;
using MySARAssist.Interfaces;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidBrightnessService))]
namespace MySARAssist.Droid
{
	

public class AndroidBrightnessService : IBrightnessService
    {
        public float GetBrightness()
        {
            var window = CrossCurrentActivity.Current.Activity.Window;
            var attributesWindow = new WindowManagerLayoutParams();

            attributesWindow.CopyFrom(window.Attributes);
            return attributesWindow.ScreenBrightness;
            
        }

        public void SetBrightness(float brightness)
        {
            var window = CrossCurrentActivity.Current.Activity.Window;
            var attributesWindow = new WindowManagerLayoutParams();

            attributesWindow.CopyFrom(window.Attributes);
            attributesWindow.ScreenBrightness = brightness;

            window.Attributes = attributesWindow;
        }
    }
}


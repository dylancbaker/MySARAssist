using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySARAssist.Interfaces;
using MySARAssist.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]

namespace MySARAssist.Droid
{
    public class Toast_Android : Interfaces.Toast
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}
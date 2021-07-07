using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MySARAssist.ResourceClasses
{
    public static class ResourceHelper
    {
        public static void SetDynamicResource(string targetResourceName, string sourceResourceName)
        {
            if (!Application.Current.Resources.TryGetValue(sourceResourceName, out var value))
            {
                throw new InvalidOperationException($"key {sourceResourceName} not found in the resource dictionary");
            }
            try
            {
                Application.Current.Resources[targetResourceName] = value;
            }
            catch (Exception)
            {

            }
        }

        public static void setThemeColor()
        {
            string style = Xamarin.Forms.Application.Current.RequestedTheme.ToString();
            if (style.Equals("dark", StringComparison.InvariantCultureIgnoreCase))
            {
                SetDynamicResource("backgroundStyle", "backgroundStyleDark");
                SetDynamicResource("labelStyle", "labelStyleDarkTheme");
                SetDynamicResource("titleLabelStyle", "titleLabelStyleDarkTheme");
                SetDynamicResource("infoLabelStyle", "infoLabelStyleDarkTheme");
                SetDynamicResource("subtitleLabelStyle", "subtitleLabelStyleDarkTheme");
                SetDynamicResource("entryStyle", "entryStyleDarkTheme");
                SetDynamicResource("editorStyle", "editorStyleDarkTheme");
                SetDynamicResource("pickerStyle", "pickerStyleDarkTheme");
                SetDynamicResource("flyoutItemLayoutStyle", "flyoutItemLayoutStyleDark");
                SetDynamicResource("listFrameStyle", "listFrameStyleDarkTheme");
                SetDynamicResource("listViewStyle", "listViewStyleDark");
                SetDynamicResource("buttonStyle", "buttonStyleDarkTheme");
            }
            else
            {
                SetDynamicResource("backgroundStyle", "backgroundStyleLight");
                SetDynamicResource("labelStyle", "labelStyleLightTheme");
                SetDynamicResource("titleLabelStyle", "titleLabelStyleLightTheme");
                SetDynamicResource("infoLabelStyle", "infoLabelStyleDarkTheme");
                SetDynamicResource("subtitleLabelStyle", "subtitleLabelStyleLightTheme");
                SetDynamicResource("entryStyle", "entryStyleLightTheme");
                SetDynamicResource("editorStyle", "editorStyleLightTheme");
                SetDynamicResource("pickerStyle", "pickerStyleLightTheme");
                SetDynamicResource("flyoutItemLayoutStyle", "flyoutItemLayoutStyleLight");
                SetDynamicResource("listFrameStyle", "listFrameStyleLightTheme");
                SetDynamicResource("listViewStyle", "listViewStyleLight");
                SetDynamicResource("buttonStyle", "buttonStyleLightTheme");
            }

        }
    }
}

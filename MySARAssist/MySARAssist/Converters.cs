using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace MySARAssist
{
    public class DoubleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0";
            double thedecimal = (double)value;
            return thedecimal.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                strValue = "0";
            double resultdecimal;
            if (double.TryParse(strValue, out resultdecimal))
            {
                return resultdecimal;
            }
            return 0;
        }

    }
}

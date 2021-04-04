using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.ResourceClasses
{
    class Utilities
    {
    }

    public static class StringExt
    {
        public static string removeBadChrsForQR(this string str)
        {
            string bad = "~^;";
            if (!string.IsNullOrEmpty(str))
            {
                StringBuilder output = new StringBuilder(str.Length);

                foreach (char c in str)
                {
                    bool badchr = false;
                    foreach (char bc in bad)
                    {
                        if (c == bc) { badchr = true; }
                    }
                    if (!badchr) { output.Append(c); }

                }

                return output.ToString();
            }
            else { return null; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;

namespace NewProject
{
    public class FilterPattern
    {
        public const string Display = @"display[\s]*:[\s]*([\w]+)[\s]*;";

        public static string GetToPattern(string input)
        {
            string[] tmp = input.Split(' ');
            string st = "";
            for (int i = 0; i < tmp.Length; i++)
            {
                if (!string.IsNullOrEmpty(tmp[i].Trim()))
                {
                    string lower = tmp[i].Trim().ToLower();
                    string upper = tmp[i].Trim().ToUpper();

                    string f = "";
                    string format = "[{0}{1}{2}{3}]*";
                    for (int j = 0; j < lower.Length; j++)
                    {
                        f += string.Format(format, lower[j], upper[j], Filter.UnicodeToUnicode(lower[j].ToString()), Filter.UnicodeToUnicode(upper[j].ToString()));
                    }

                    st += f + @"[\s_\-]*";
                }
            }

            if (!string.IsNullOrEmpty(st))
                st = @"[\s]*" + st.Substring(0, st.Length - 8) + @"[\s]*";
            return st;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;

namespace WorkLibrary
{
    public class FilterPattern
    {
        public const string Display = @"display[\s]*:[\s]*([\w]+)[\s]*;";
        public static string BoDau(string accented)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = accented.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };



        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }
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

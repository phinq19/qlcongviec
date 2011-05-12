using System;
using System.Collections.Generic;
using System.Text;

namespace PostForum.Modules
{
    public class FilterHtml
    {
        public static FilterHtml Instant()
        {
            return new FilterHtml();
        }

        string[] PatternRemoveSpecChar
        {
            get
            {
                return new string[] { "&nbsp;","\r\n", "\n" };
            }
        }

        public string RemoveSpecialCharacter(string _input)
        {
            string tmp = _input;
            for (int i = 0; i < PatternRemoveSpecChar.Length; i++)
                tmp = tmp.Replace(PatternRemoveSpecChar[i], "");
            return tmp.Trim();
        }
    }
}

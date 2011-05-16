using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkLibrary
{
    public class Filter
    {
        public static string GetTextByRegex(string __RegexPattern, string __InputText)
        {
            return GetTextByRegex(__RegexPattern, __InputText, true);
        }

        public static string GetTextByRegex(string __RegexPattern, string __InputText, bool __RegexIgnoreCase)
        {
            return GetTextByRegex(__RegexPattern, __InputText, __RegexIgnoreCase, -1, -1);
        }

        public static string GetTextByRegex(string __RegexPattern, string __InputText, bool __RegexIgnoreCase, int __MatchIndex, int __GroupIndex)
        {
            string toReturn = string.Empty;

            if (!string.IsNullOrEmpty(__InputText))
            {
                Regex objRegex = new Regex(__RegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                MatchCollection _MatchCollection = objRegex.Matches(__InputText);

                if (__MatchIndex == -1 && __GroupIndex == -1)
                {
                    foreach (Match match in _MatchCollection)
                    {
                        foreach (Group group in match.Groups)
                        {
                            //toReturn += Server.HtmlEncode(group.Value);
                            toReturn += group.Value;
                        }
                    }
                }
                else
                {
                    if (_MatchCollection.Count > __MatchIndex)
                    {
                        if (_MatchCollection[__MatchIndex].Groups.Count > __GroupIndex)
                        {
                            toReturn = _MatchCollection[__MatchIndex].Groups[__GroupIndex].Value;
                        }
                    }
                }
            }

            return toReturn.Trim();
        }


        //----


        public static List<string> GetTextByRegex1(string __RegexPattern, string __InputText)
        {
            return GetTextByRegex1(__RegexPattern, __InputText, true);
        }

        public static List<string> GetTextByRegex1(string __RegexPattern, string __InputText, bool __RegexIgnoreCase)
        {
            return GetTextByRegex1(__RegexPattern, __InputText, __RegexIgnoreCase, -1, -1);
        }

        public static List<string> GetTextByRegex1(string __RegexPattern, string __InputText, bool __RegexIgnoreCase, int __MatchIndex, int __GroupIndex)
        {
            List<string> toReturn = new List<string>();

            if (!string.IsNullOrEmpty(__InputText))
            {
                Regex objRegex = new Regex(__RegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                MatchCollection _MatchCollection = objRegex.Matches(__InputText);

                if (__MatchIndex == -1 && __GroupIndex == -1)
                {
                    foreach (Match match in _MatchCollection)
                    {
                        //foreach (Group group in match.Groups)
                        //{
                        //    //toReturn += Server.HtmlEncode(group.Value);
                        //    toReturn.Add(group.Value);
                        //}
                        toReturn.Add(match.Groups[1].Value);
                    }
                }
                else
                {
                    if (_MatchCollection.Count > __MatchIndex)
                    {
                        if (_MatchCollection[__MatchIndex].Groups.Count > __GroupIndex)
                        {
                            toReturn.Add(_MatchCollection[__MatchIndex].Groups[__GroupIndex].Value);
                        }
                    }
                }
            }

            return toReturn;
        }

        public static List<string> GetTextByRegex1(string __RegexPattern, string __InputText, int __GroupIndex)
        {
            List<string> toReturn = new List<string>();

            if (!string.IsNullOrEmpty(__InputText))
            {
                Regex objRegex = new Regex(__RegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                MatchCollection _MatchCollection = objRegex.Matches(__InputText);

                
                    foreach (Match match in _MatchCollection)
                    {
                        if (__GroupIndex == -1)
                            foreach (Group group in match.Groups)
                            {
                                //toReturn += Server.HtmlEncode(group.Value);
                                toReturn.Add(group.Value);
                            }
                        else if (__GroupIndex > -1)
                            toReturn.Add(match.Groups[__GroupIndex].Value);
                    }
                
            }

            return toReturn;
        }

        public static string RegularReplace(string _input, string _regreplace, string _regnewstring)
        {
            return Regex.Replace(_input, _regreplace, _regnewstring);
        }

        public static String UnicodeToUnicode(String strTextIn)
        {
            StringBuilder strB = new StringBuilder(strTextIn);
            string[] Unicode_Char = { "\u00E1", "\u00E0", "\u1EA3", "\u00E3", "\u1EA1", "\u0103", "\u1EAF", "\u1EB1", "\u1EB3", "\u1EB5", "\u1EB7", "\u00E2", "\u1EA5", "\u1EA7", "\u1EA9", "\u1EAB", "\u1EAD", "\u00E9", "\u00E8", "\u1EBB", "\u1EBD", "\u1EB9", "\u00EA", "\u1EBF", "\u1EC1", "\u1EC3", "\u1EC5", "\u1EC7", "\u00ED", "\u00EC", "\u1EC9", "\u0129", "\u1ECB", "\u00F3", "\u00F2", "\u1ECF", "\u00F5", "\u1ECD", "\u00F4", "\u1ED1", "\u1ED3", "\u1ED5", "\u1ED7", "\u1ED9", "\u01A1", "\u1EDB", "\u1EDD", "\u1EDF", "\u1EE1", "\u1EE3", "\u00FA", "\u00F9", "\u1EE7", "\u0169", "\u1EE5", "\u01B0", "\u1EE9", "\u1EEB", "\u1EED", "\u1EEF", "\u1EF1", "\u00FD", "\u1EF3", "\u1EF7", "\u1EF9", "\u1EF5", "\u0111" };
            string[] Unicode_Cap = { "\u00C1", "\u00C0", "\u1EA2", "\u00C3", "\u1EA0", "\u0102", "\u1EAE", "\u1EB0", "\u1EB2", "\u1EB4", "\u1EB6", "\u00C2", "\u1EA4", "\u1EA6", "\u1EA8", "\u1EAA", "\u1EAC", "\u00C9", "\u00C8", "\u1EBA", "\u1EBC", "\u1EB8", "\u00CA", "\u1EBE", "\u1EC0", "\u1EC2", "\u1EC4", "\u1EC6", "\u00CD", "\u00CC", "\u1EC8", "\u0128", "\u1ECA", "\u00D3", "\u00D2", "\u1ECE", "\u00D5", "\u1ECC", "\u00D4", "\u1ED0", "\u1ED2", "\u1ED4", "\u1ED6", "\u1ED8", "\u01A0", "\u1EDA", "\u1EDC", "\u1EDE", "\u1EE0", "\u1EE2", "\u00DA", "\u00D9", "\u1EE6", "\u0168", "\u1EE4", "\u01AF", "\u1EE8", "\u1EEA", "\u1EEC", "\u1EEE", "\u1EF0", "\u00DD", "\u1EF2", "\u1EF6", "\u1EF8", "\u1EF4", "\u0110" };
            string[] Unicode_Char_Yahoo = { "&#225", "&#224", "&#7843", "&#227", "&#7841", "&#259", "&#7855", "&#7857", "&#7859", "&#7861", "&#7863", "&#226", "&#7845", "&#7847", "&#7849", "&#7851", "&#7853", "&#233", "&#232", "&#7867", "&#7869", "&#7865", "&#234", "&#7871", "&#7873", "&#7875", "&#7877", "&#7879", "&#237", "&#236", "&#7881", "&#297", "&#7883", "&#243", "&#242", "&#7887", "&#245", "&#7885", "&#244", "&#7889", "&#7891", "&#7893", "&#7895", "&#7897", "&#417", "&#7899", "&#7901", "&#7903", "&#7905", "&#7907", "&#250", "&#249", "&#7911", "&#361", "&#7909", "&#432", "&#7913", "&#7915", "&#7917", "&#7919", "&#7921", "&#253", "&#7923", "&#7927", "&#7929", "&#7925", "&#273" };
            string[] Unicode_Cap_Yahoo = { "&#193", "&#192", "&#7842", "&#195", "&#7840", "&#258", "&#7854", "&#7856", "&#7858", "&#7860", "&#7862", "&#194", "&#7844", "&#7846", "&#7848", "&#7850", "&#7852", "&#201", "&#200", "&#7866", "&#7868", "&#7864", "&#202", "&#7870", "&#7872", "&#7874", "&#7876", "&#7878", "&#205", "&#204", "&#7880", "&#296", "&#7882", "&#211", "&#210", "&#7886", "&#213", "&#7884", "&#212", "&#7888", "&#7890", "&#7892", "&#7894", "&#7896", "&#416", "&#7898", "&#7900", "&#7902", "&#7904", "&#7906", "&#218", "&#217", "&#7910", "&#360", "&#7908", "&#431", "&#7912", "&#7914", "&#7916", "&#7918", "&#7920", "&#221", "&#7922", "&#7926", "&#7928", "&#7924", "&#272" };
            for (int i = 0; i < Unicode_Char.Length; i++)
            {
                strB.Replace(Unicode_Char[i], Unicode_Char_Yahoo[i] + ";");
            }
            for (int i = 0; i < Unicode_Char.Length; i++)
            {
                strB.Replace(Unicode_Cap[i], Unicode_Cap_Yahoo[i] + ";");
            }

            return strB.ToString();
        }

        public static String UnicodeToUnicode2(String strTextIn)
        {
            StringBuilder strB = new StringBuilder(strTextIn);
            string[] Unicode_Char = { "\u00E1", "\u00E0", "\u1EA3", "\u00E3", "\u1EA1", "\u0103", "\u1EAF", "\u1EB1", "\u1EB3", "\u1EB5", "\u1EB7", "\u00E2", "\u1EA5", "\u1EA7", "\u1EA9", "\u1EAB", "\u1EAD", "\u00E9", "\u00E8", "\u1EBB", "\u1EBD", "\u1EB9", "\u00EA", "\u1EBF", "\u1EC1", "\u1EC3", "\u1EC5", "\u1EC7", "\u00ED", "\u00EC", "\u1EC9", "\u0129", "\u1ECB", "\u00F3", "\u00F2", "\u1ECF", "\u00F5", "\u1ECD", "\u00F4", "\u1ED1", "\u1ED3", "\u1ED5", "\u1ED7", "\u1ED9", "\u01A1", "\u1EDB", "\u1EDD", "\u1EDF", "\u1EE1", "\u1EE3", "\u00FA", "\u00F9", "\u1EE7", "\u0169", "\u1EE5", "\u01B0", "\u1EE9", "\u1EEB", "\u1EED", "\u1EEF", "\u1EF1", "\u00FD", "\u1EF3", "\u1EF7", "\u1EF9", "\u1EF5", "\u0111" };
            string[] Unicode_Cap = { "\u00C1", "\u00C0", "\u1EA2", "\u00C3", "\u1EA0", "\u0102", "\u1EAE", "\u1EB0", "\u1EB2", "\u1EB4", "\u1EB6", "\u00C2", "\u1EA4", "\u1EA6", "\u1EA8", "\u1EAA", "\u1EAC", "\u00C9", "\u00C8", "\u1EBA", "\u1EBC", "\u1EB8", "\u00CA", "\u1EBE", "\u1EC0", "\u1EC2", "\u1EC4", "\u1EC6", "\u00CD", "\u00CC", "\u1EC8", "\u0128", "\u1ECA", "\u00D3", "\u00D2", "\u1ECE", "\u00D5", "\u1ECC", "\u00D4", "\u1ED0", "\u1ED2", "\u1ED4", "\u1ED6", "\u1ED8", "\u01A0", "\u1EDA", "\u1EDC", "\u1EDE", "\u1EE0", "\u1EE2", "\u00DA", "\u00D9", "\u1EE6", "\u0168", "\u1EE4", "\u01AF", "\u1EE8", "\u1EEA", "\u1EEC", "\u1EEE", "\u1EF0", "\u00DD", "\u1EF2", "\u1EF6", "\u1EF8", "\u1EF4", "\u0110" };
            string[] Unicode_Char_Yahoo = { "&#225", "&#224", "&#7843", "&#227", "&#7841", "&#259", "&#7855", "&#7857", "&#7859", "&#7861", "&#7863", "&#226", "&#7845", "&#7847", "&#7849", "&#7851", "&#7853", "&#233", "&#232", "&#7867", "&#7869", "&#7865", "&#234", "&#7871", "&#7873", "&#7875", "&#7877", "&#7879", "&#237", "&#236", "&#7881", "&#297", "&#7883", "&#243", "&#242", "&#7887", "&#245", "&#7885", "&#244", "&#7889", "&#7891", "&#7893", "&#7895", "&#7897", "&#417", "&#7899", "&#7901", "&#7903", "&#7905", "&#7907", "&#250", "&#249", "&#7911", "&#361", "&#7909", "&#432", "&#7913", "&#7915", "&#7917", "&#7919", "&#7921", "&#253", "&#7923", "&#7927", "&#7929", "&#7925", "&#273" };
            string[] Unicode_Cap_Yahoo = { "&#193", "&#192", "&#7842", "&#195", "&#7840", "&#258", "&#7854", "&#7856", "&#7858", "&#7860", "&#7862", "&#194", "&#7844", "&#7846", "&#7848", "&#7850", "&#7852", "&#201", "&#200", "&#7866", "&#7868", "&#7864", "&#202", "&#7870", "&#7872", "&#7874", "&#7876", "&#7878", "&#205", "&#204", "&#7880", "&#296", "&#7882", "&#211", "&#210", "&#7886", "&#213", "&#7884", "&#212", "&#7888", "&#7890", "&#7892", "&#7894", "&#7896", "&#416", "&#7898", "&#7900", "&#7902", "&#7904", "&#7906", "&#218", "&#217", "&#7910", "&#360", "&#7908", "&#431", "&#7912", "&#7914", "&#7916", "&#7918", "&#7920", "&#221", "&#7922", "&#7926", "&#7928", "&#7924", "&#272" };
            for (int i = 0; i < Unicode_Char.Length; i++)
            {
                strB.Replace(Unicode_Char_Yahoo[i] + ";", Unicode_Char[i]);
            }
            for (int i = 0; i < Unicode_Char.Length; i++)
            {
                strB.Replace(Unicode_Cap_Yahoo[i] + ";" ,Unicode_Cap[i]);
            }

            return strB.ToString();
        }
    }
}

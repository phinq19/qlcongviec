using System;
using System.Collections.Generic;
using System.Text;

using WatiN.Core;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Threading;
using WatiN.Core.Constraints;

namespace WorkLibrary
{
    public class MyWatiN
    {
        public static bool IsExist(IE ie, HControl control)
        {
            try
            {
                bool existed = true;
                Regex regex = new Regex(FilterPattern.GetToPattern(control.Value));
                switch (control.Control.ToLower())
                {
                    case ControlType.AHref:
                        existed = ie.Link(Find.ByText(regex)).Exists;
                        break;
                    case ControlType.AHrefNoText:
                        try
                        {

                        }
                        catch (Exception ex)
                        {
                            existed = false;
                        }
                        break;
                    case ControlType.Button:
                        existed = ie.Button(GetControl(ie, control)).Exists;
                        break;
                    case ControlType.Div:
                        existed = ie.Div(GetControl(ie, control)).Exists;
                        break;
                    case ControlType.TextArea:
                        existed = ie.TextField(GetControl(ie, control)).Exists;
                        break;
                    case ControlType.TextBox:
                        existed = ie.TextField(GetControl(ie, control)).Exists;
                        break;
                }
                return existed;// 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static AttributeConstraint GetControl(IE ie, HControl control)
        {
            //Regex regex = new Regex(FilterPattern.GetToPattern(control.Value));
            String regex = control.Value;
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    return Find.ById(regex);
                case AttributeType.Name:
                    return Find.ByName(regex);
                case AttributeType.Class:
                    return Find.ByClass(regex);
                case AttributeType.Text:
                    {
                        Regex regexs = new Regex(FilterPattern.GetToPattern(control.Value));
                        return Find.ByText(regexs);
                    }
                case AttributeType.Value:
                    {
                        Regex regexs = new Regex(FilterPattern.GetToPattern(control.Value));
                        return Find.ByValue(regexs);
                    }
                default:
                    return Find.ByName(regex);
            }
        }

        public static string FillData(IE ie, HControl control)
        {
            return FillData(ie, control, "");
        }

        public static string FillData(IE ie, HControl control, string data)
        {
            int i = 0;
            while (i < Loop)
            {
                i++;
                try
                {
                    switch (control.Control.ToLower())
                    {
                        case ControlType.AHref:
                            ie.Link(GetControl(ie, control)).Click();
                            break;
                        case ControlType.AHrefNoText:
                            ie.GoTo(control.Value);
                            break;
                        case ControlType.Button:
                            ie.Button(GetControl(ie, control)).Click();
                            break;
                        case ControlType.Div:
                            ie.Div(GetControl(ie, control)).Click();
                            break;
                        case ControlType.TextArea:
                            ie.TextField(GetControl(ie, control)).Value = data;
                            break;
                        case ControlType.TextBox:
                            ie.TextField(GetControl(ie, control)).Value = data;
                            break;
                    }
                    ie.WaitForComplete();

                    return string.Empty;
                }
                catch (Exception ex)
                {
                    if (i == Loop)
                    {
                        return ex.Message;
                    }
                    Thread.Sleep(60000);
                }
            }
            return string.Empty;
        }

        public static string Goto(string Url, IE ie)
        {
            int i = 0;
            while (i < Loop)
            {
                i++;
                try
                {
                    ie.GoTo(Url);
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    if (i == Loop)
                    {
                        return ex.Message;
                    }
                    ie.Close();
                    Thread.Sleep(60000);
                    ie.Reopen();
                }
            }
            return string.Empty;
        }

        public static void Visible(bool visible)
        {
            Settings.MakeNewIeInstanceVisible = visible;
        }

        public static void Init()
        {
           
            
        }

        public static int Loop
        {
            get
            {
                return 5;
            }
        }
        public static WatiN.Core.Button GetButton(IE ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        foreach (WatiN.Core.Button bt in ie.Buttons)
                        {
                            if(bt.Id==control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Name:
                    {
                        foreach (WatiN.Core.Button bt in ie.Buttons)
                        {
                            if (bt.Name == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Class:
                     {
                        foreach (WatiN.Core.Button bt in ie.Buttons)
                        {
                            if (bt.ClassName == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Text:
                     {
                         foreach (WatiN.Core.Button bt in ie.Buttons)
                         {
                             string s = bt.Text.ToLowerInvariant();
                             s = s.Replace("ð", "đ");
                             if (s == control.Value.ToLower())
                                 return bt;
                         }
                         return null;
                     }
                case AttributeType.Value:
                     {
                         foreach (WatiN.Core.Button bt in ie.Buttons)
                         {
                             string s = bt.Value.ToLowerInvariant();
                             s = s.Replace("ð", "đ");
                             if(s==control.Value.ToLower())
                                 return bt;
                         }
                         return null;
                     }
                default:
                     {
                         foreach (WatiN.Core.Button bt in ie.Buttons)
                         {
                             if (bt.Name == control.Value)
                                 return bt;
                         }
                         return null;
                     }
            }
        }
        public static WatiN.Core.TextField GetTextField(IE ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        foreach (WatiN.Core.TextField bt in ie.TextFields)
                        {
                            if (bt.Id == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Name:
                    {
                        foreach (WatiN.Core.TextField bt in ie.TextFields)
                        {
                            if (bt.Name == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Class:
                    {
                        foreach (WatiN.Core.TextField bt in ie.TextFields)
                        {
                            if (bt.ClassName == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Text:
                    {
                        foreach (WatiN.Core.TextField bt in ie.TextFields)
                        {
                            if (bt.Text == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Value:
                    {
                        foreach (WatiN.Core.TextField bt in ie.TextFields)
                        {
                            if (bt.Value == control.Value)
                                return bt;
                        }
                        return null;
                    }
                default:
                    {
                        foreach (WatiN.Core.TextField bt in ie.TextFields)
                        {
                            if (bt.Name == control.Value)
                                return bt;
                        }
                        return null;
                    }
            }
        }
        public static WatiN.Core.Div GetDiv(IE ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        Div div= ie.Div(Find.ById(control.Value));
                        if (div.Exists)
                            return div;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        Div div = ie.Div(Find.ByName(control.Value));
                        if (div.Exists)
                            return div;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        Div div = ie.Div(Find.ByClass(control.Value));
                        if (div.Exists)
                            return div;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        foreach (WatiN.Core.Div bt in ie.Divs)
                        {
                            if (bt.Text == control.Value)
                                return bt;
                        }
                        return null;
                    }
                
                default:
                    {
                        foreach (WatiN.Core.Div bt in ie.Divs)
                        {
                            if (bt.Name == control.Value)
                                return bt;
                        }
                        return null;
                    }
            }
        }
        public static WatiN.Core.Link GetLink(IE ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        foreach (WatiN.Core.Link bt in ie.Links)
                        {
                            if (bt.Id == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Name:
                    {
                        foreach (WatiN.Core.Link bt in ie.Links)
                        {
                            if (bt.Name == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Class:
                    {
                        foreach (WatiN.Core.Link bt in ie.Links)
                        {
                            if (bt.ClassName == control.Value)
                                return bt;
                        }
                        return null;
                    }
                case AttributeType.Text:
                    {
                        foreach (WatiN.Core.Link bt in ie.Links)
                        {
                            if (bt.Text == control.Value)
                                return bt;
                        }
                        return null;
                    }
                
                default:
                    {
                        foreach (WatiN.Core.Link bt in ie.Links)
                        {
                            if (bt.Name == control.Value)
                                return bt;
                        }
                        return null;
                    }
            }
        }
    }
}

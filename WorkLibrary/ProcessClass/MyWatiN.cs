﻿using System;
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
                    ie.WaitForComplete();
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
                        Button bt = ie.Button(Find.ById(control.Value));
                        if (bt.Exists)
                            return bt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        Button bt = ie.Button(Find.ByName(control.Value));
                        if (bt.Exists)
                            return bt;
                        return null;
                    }
                case AttributeType.Class:
                     {
                         Button bt = ie.Button(Find.ByClass(control.Value));
                         if (bt.Exists)
                             return bt;
                         return null;
                    }
                case AttributeType.Text:
                     {
                         Button bt = ie.Button(Find.ByText(control.Value));
                         if (bt.Exists)
                             return bt;
                         return null;
                     }
                case AttributeType.Value:
                     {
                         foreach (WatiN.Core.Button bt in ie.Buttons)
                         {
                             string s = "234234fsfasrq3453rdwedawewr4";
                             try
                             {
                                 s = bt.Value.ToLowerInvariant();
                                 s = s.Replace("ð", "đ");
                             }
                             catch { }
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
        public static WatiN.Core.Button GetButton(Div ie, HControl control)
        {

            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        Button bt = ie.Button(Find.ById(control.Value));
                        if (bt.Exists)
                            return bt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        Button bt = ie.Button(Find.ByName(control.Value));
                        if (bt.Exists)
                            return bt;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        Button bt = ie.Button(Find.ByClass(control.Value));
                        if (bt.Exists)
                            return bt;
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
                            if (s == control.Value.ToLower())
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
                        TextField txt = ie.TextField(Find.ById(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        TextField txt = ie.TextField(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        TextField txt = ie.TextField(Find.ByClass(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        TextField txt = ie.TextField(Find.ByText(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Value:
                    {
                        TextField txt = ie.TextField(Find.ByValue(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                default:
                    {
                        TextField txt = ie.TextField(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
            }
        }
        public static WatiN.Core.TextField GetTextField(Div ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        TextField txt = ie.TextField(Find.ById(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        TextField txt = ie.TextField(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        TextField txt = ie.TextField(Find.ByClass(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        TextField txt = ie.TextField(Find.ByText(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Value:
                    {
                        TextField txt = ie.TextField(Find.ByValue(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                default:
                    {
                        TextField txt = ie.TextField(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
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
                        Div div = ie.Div(Find.ById(control.Value));
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
                        Link link = ie.Link(Find.ById(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        Link link = ie.Link(Find.ByName(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        Link link = ie.Link(Find.ByClass(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        Link link = ie.Link(Find.ByText(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                default:
                    {
                        Link link = ie.Link(Find.ByText(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
            }
        }
        public static WatiN.Core.Link GetLink(Div div, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        Link link = div.Link(Find.ById(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        Link link = div.Link(Find.ByName(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        Link link = div.Link(Find.ByClass(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        Link link = div.Link(Find.ByText(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
                default:
                    {
                        Link link = div.Link(Find.ByText(control.Value));
                        if (link.Exists)
                            return link;
                        return null;
                    }
            }
        }
        public static WatiN.Core.CheckBox GetCheckBox(IE ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        CheckBox txt = ie.CheckBox(Find.ById(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByClass(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByText(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Value:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByValue(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                default:
                    {
                        CheckBox txt = ie.CheckBox(Find.ById(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
            }
        }
        public static WatiN.Core.CheckBox GetCheckBox(Div ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        CheckBox txt = ie.CheckBox(Find.ById(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByClass(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByText(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Value:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByValue(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                default:
                    {
                        CheckBox txt = ie.CheckBox(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
            }
        }
        public static WatiN.Core.Image GetImage(IE ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        Image txt = ie.Image(Find.ById(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        Image txt = ie.Image(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        Image txt = ie.Image(Find.ByClass(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        Image txt = ie.Image(Find.ByText(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Value:
                    {
                        Image txt = ie.Image(Find.ByValue(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                default:
                    {
                        Image txt = ie.Image(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
            }
        }
        public static WatiN.Core.Image GetImage(Div ie, HControl control)
        {
            switch (control.Attribute.ToLower())
            {
                case AttributeType.Id:
                    {
                        Image txt = ie.Image(Find.ById(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Name:
                    {
                        Image txt = ie.Image(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Class:
                    {
                        Image txt = ie.Image(Find.ByClass(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Text:
                    {
                        Image txt = ie.Image(Find.ByText(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                case AttributeType.Value:
                    {
                        Image txt = ie.Image(Find.ByValue(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
                default:
                    {
                        Image txt = ie.Image(Find.ByName(control.Value));
                        if (txt.Exists)
                            return txt;
                        return null;
                    }
            }
        }
    }
}

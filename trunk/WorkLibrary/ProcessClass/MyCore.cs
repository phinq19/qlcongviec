using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using System.Threading;
using System.Text.RegularExpressions;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Native.Windows;

namespace WorkLibrary
{
    public class MyCore
    {
        static int Loop = 4;
        public static string ProcessStep(String strstep, IE ie)
        {
            try
            {
                string[] a = strstep.Split('(');
                string processType = a[0].Trim();
                string processText = a[1].Trim(')');
                switch (processType.ToLower())
                {
                    case ProcessType.Click:
                        {
                            return Click(processText, ie);
                            break;
                        }
                    case ProcessType.DivClick:
                        {
                            return DivClick(processText, ie);
                            break;
                        }
                    case ProcessType.Fill:
                        {
                            return Fill(processText, ie);
                            break;
                        }
                    case ProcessType.Goto:
                        {
                            return Goto(processText, ie);
                            break;
                        }
                    case ProcessType.Wait:
                        {
                            return Wait(processText);
                            break;
                        }
                    case ProcessType.ClickConfirm:
                        {
                            return ClickConfirm(processText, ie);
                            break;
                        }
                    case ProcessType.Check:
                        {
                            return Check(processText, ie);
                            break;
                        }
                    case ProcessType.Submit:
                        {
                            return Submit(processText, ie);
                            break;
                        }
                    case ProcessType.ClickMode:
                        {
                            return ClickMode(processText, ie);
                            break;
                        }
                }
                return "Error Type";
            }
            catch
            {
                return "Error Exception";
            }

        }
        public static string Goto(String text, IE ie)
        {
            int i = 0;
            while (i < Loop)
            {
                i++;
                try
                {
                    ie.GoTo(text);
                    ie.WaitForComplete();
                    //ie.WaitUntilContainsText("message");
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
        private static string Fill(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();
           
            string[] a = text.Trim().Split('|');
            string data = a[1].Trim();

            string[] k = a[0].Split(',');
            foreach (string l in k)
            {
                string[] m = l.Trim().Split('[');
                string n = m[1].Trim(']');
                string[] o = n.Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = m[0].Trim();
                hcontrol.Attribute = o[0].Trim();
                hcontrol.Value = o[1].Trim();
                controls.Add(hcontrol);
            }
            return RunControl(controls,data,ie);
        }
        private static string ClickMode(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();

            string[] a = text.Trim().Split('|');
            string g = a[1].Trim();
            string[] h = g.Split('[');
            string[] j = h[1].Trim(']').Split(':');
            HControl hcontroldiv = new HControl();
            hcontroldiv.Control = h[0].Trim();
            hcontroldiv.Attribute = j[0].Trim();
            hcontroldiv.Value = j[1].Trim();
            TextField div = MyWatiN.GetTextField(ie, hcontroldiv);
            string style = div.GetAttributeValue("style");
            string display = Filter.GetTextByRegex(FilterPattern.Display, style, true, 0, 1);
            if (!string.IsNullOrEmpty(display))
            {
                string[] k = a[0].Split(',');
                foreach (string l in k)
                {
                    string[] m = l.Trim().Split('[');
                    string n = m[1].Trim(']');
                    string[] o = n.Split(':');
                    HControl hcontrol = new HControl();
                    hcontrol.Control = m[0].Trim();
                    hcontrol.Attribute = o[0].Trim();
                    hcontrol.Value = o[1].Trim();
                    controls.Add(hcontrol);
                }
                return RunControl(controls, "", ie);
            }
            return String.Empty;
           
        }
        private static string Check(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();

            string[] a = text.Trim().Split('|');
            string data = a[1].Trim();
            string[] k = a[0].Split(',');
            foreach (string l in k)
            {
                string[] m = l.Trim().Split('[');
                string n = m[1].Trim(']');
                string[] o = n.Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = m[0].Trim();
                hcontrol.Attribute = o[0].Trim();
                hcontrol.Value = o[1].Trim();
                controls.Add(hcontrol);
            }
            return RunControl(controls, data, ie);
        }
        private static string DivClick(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();

            string[] a = text.Trim().Split('|');
            string g = a[0].Trim();
            string[] h = g.Split('[');
            string[] j = h[1].Trim(']').Split(':');
            HControl hcontroldiv = new HControl();
            hcontroldiv.Control = h[0].Trim();
            hcontroldiv.Attribute = j[0].Trim();
            hcontroldiv.Value = j[1].Trim();
            Div div = MyWatiN.GetDiv(ie, hcontroldiv);
            if (div != null)
            {
                string[] k = text.Split(',');
                foreach (string l in k)
                {
                    string[] m = l.Trim().Split('[');
                    string n = m[1].Trim(']');
                    string[] o = n.Split(':');
                    HControl hcontrol = new HControl();
                    hcontrol.Control = m[0].Trim();
                    hcontrol.Attribute = o[0].Trim();
                    hcontrol.Value = o[1].Trim();
                    controls.Add(hcontrol);
                }
                return RunControl(controls, div,ie);
            }
            return "Error";
        }
        private static string Click(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();
            string[] k = text.Split(',');
            foreach (string l in k)
            {
                string[] m = l.Trim().Split('[');
                string n = m[1].Trim(']');
                string[] o = n.Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = m[0].Trim();
                hcontrol.Attribute = o[0].Trim();
                hcontrol.Value = o[1].Trim();
                controls.Add(hcontrol);
            }
            return RunControl(controls,ie);
        }
        private static string Submit(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();
            string[] k = text.Split(',');
            foreach (string l in k)
            {
                string[] m = l.Trim().Split('[');
                string n = m[1].Trim(']');
                string[] o = n.Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = m[0].Trim();
                hcontrol.Attribute = o[0].Trim();
                hcontrol.Value = o[1].Trim();
                controls.Add(hcontrol);
            }
            foreach (WatiN.Core.Form frm in ie.Forms)
            {
                foreach (HControl ctr in controls)
                {
                    if (frm.GetAttributeValue(ctr.Attribute).IndexOf(ctr.Value) >= 0)
                    {
                        int timeout = Settings.WaitForCompleteTimeOut;
                        Settings.WaitForCompleteTimeOut = 1;
                        try
                        {
                            frm.Submit();
                        }
                        catch
                        {
                        }
                        ie.WaitForComplete();
                        Settings.WaitForCompleteTimeOut = timeout;
                        return String.Empty;
                    }
                }
            }
          
            return "Error";
        }
        public static bool Exist(String text, IE ie)
        {
            try
            {
                List<HControl> controls = new List<HControl>();
                string[] k = text.Split(',');
                foreach (string l in k)
                {
                    string[] m = l.Trim().Split('[');
                    string n = m[1].Trim(']');
                    string[] o = n.Split(':');
                    HControl hcontrol = new HControl();
                    hcontrol.Control = m[0].Trim();
                    hcontrol.Attribute = o[0].Trim();
                    hcontrol.Value = o[1].Trim();
                    controls.Add(hcontrol);
                }
                return CheckExist(controls, ie);
            }
            catch
            {
                return false;
            }
        }
        private static string ClickConfirm(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();
            string[] k = text.Split(',');
            foreach (string l in k)
            {
                string[] m = l.Trim().Split('[');
                string n = m[1].Trim(']');
                string[] o = n.Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = m[0].Trim();
                hcontrol.Attribute = o[0].Trim();
                hcontrol.Value = o[1].Trim();
                controls.Add(hcontrol);
            }
            return RunControlConfirm(controls, ie);
        }
        private static string Wait(String text)
        {
            int w = int.Parse(text)*1000;
            Thread.Sleep(w);
            return String.Empty;
        }

        public static string RunControl(List<HControl> controls, IE ie)
        {
            return RunControl(controls, "",ie);

        }
        public static string RunControl(List<HControl> controls, Div div, IE ie)
        {
            return RunControl(controls, "",div, ie);

        }
        public static bool CheckExist(List<HControl> controls, IE ie)
        {
            int i = 0;
            bool status = false;
            while (i < 2)
            {
                i++;
                foreach (HControl control in controls)
                {
                    switch (control.Control.ToLower())
                    {
                        case ControlType.AHref:
                            {
                                WatiN.Core.Link obj = MyWatiN.GetLink(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                        case ControlType.Link:
                            {
                                WatiN.Core.Link obj = MyWatiN.GetLink(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                        case ControlType.Button:
                            {
                                WatiN.Core.Button obj = MyWatiN.GetButton(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                        case ControlType.Div:
                            {
                                WatiN.Core.Div obj = MyWatiN.GetDiv(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                        case ControlType.Image:
                            {
                                WatiN.Core.Image obj = MyWatiN.GetImage(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                        case ControlType.TextArea:
                            {
                                WatiN.Core.TextField obj = MyWatiN.GetTextField(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                        case ControlType.TextBox:
                            {
                                WatiN.Core.TextField obj = MyWatiN.GetTextField(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                        case ControlType.CheckBox:
                            {
                                WatiN.Core.CheckBox obj = MyWatiN.GetCheckBox(ie, control);
                                if (obj != null)
                                {
                                    return true;
                                }
                                break;
                            }
                    }

                }
                if (i < 1)
                {
                    Thread.Sleep(1000);
                }
            }
            return status;
        }
        public static string RunControl(List<HControl> controls, string data, Div div, IE ie)
        {
            int i = 0;
            string status = "Error";
            while (i < Loop)
            {
                i++;
                foreach (HControl control in controls)
                {
                    switch (control.Control.ToLower())
                    {
                        
                        case ControlType.Link:
                            {
                                WatiN.Core.Link obj = MyWatiN.GetLink(div, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.Button:
                            {
                                WatiN.Core.Button obj = MyWatiN.GetButton(div, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.Image:
                            {
                                WatiN.Core.Image obj = MyWatiN.GetImage(div, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.TextArea:
                            {
                                WatiN.Core.TextField obj = MyWatiN.GetTextField(div, control);
                                if (obj != null)
                                {
                                    obj.Value = data;
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.TextBox:
                            {
                                WatiN.Core.TextField obj = MyWatiN.GetTextField(div, control);
                                if (obj != null)
                                {
                                    obj.Value = data;
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.CheckBox:
                            {
                                WatiN.Core.CheckBox obj = MyWatiN.GetCheckBox(div, control);
                                if (obj != null)
                                {
                                    obj.Checked = true;
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                    }

                }
                if (i < Loop - 1)
                {
                    Thread.Sleep(1000);
                }
            }
            return status;
        }
        public static string RunControl(List<HControl> controls, string data,IE ie)
        {
            int i = 0;
            string status = "Error";
            while (i < Loop)
            {
                i++;
                foreach (HControl control in controls)
                {
                    switch (control.Control.ToLower())
                    {
                        case ControlType.AHref:
                            {
                                WatiN.Core.Link obj = MyWatiN.GetLink(ie, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.AHrefNoText:
                            {
                                ie.GoTo(control.Value);
                                ie.WaitForComplete();
                                return String.Empty;
                                break;
                            }
                        case ControlType.Link:
                            {
                                WatiN.Core.Link obj = MyWatiN.GetLink(ie, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.Button:
                            {
                                WatiN.Core.Button obj = MyWatiN.GetButton(ie, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.Div:
                            {
                                WatiN.Core.Div obj = MyWatiN.GetDiv(ie, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.Image:
                            {
                                WatiN.Core.Image obj = MyWatiN.GetImage(ie, control);
                                if (obj != null)
                                {
                                    obj.Click();
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.TextArea:
                            {
                                WatiN.Core.TextField obj = MyWatiN.GetTextField(ie, control);
                                if (obj != null)
                                {
                                    obj.Value = data;
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.TextBox:
                            {
                                WatiN.Core.TextField obj = MyWatiN.GetTextField(ie, control);
                                if (obj != null)
                                {

                                    
                                    obj.Value = data;
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                        case ControlType.CheckBox:
                            {
                                WatiN.Core.CheckBox obj = MyWatiN.GetCheckBox(ie, control);
                                if (obj != null)
                                {
                                    obj.Checked = bool.Parse(data);
                                    ie.WaitForComplete();
                                    return String.Empty;

                                }
                                break;
                            }
                    }

                }
                Thread.Sleep(1000);
            }
            return status;
        }
        private static string RunControlConfirm(List<HControl> controls, IE ie)
        {
            int i = 0;
            string status = "Error";
            while (i < Loop)
            {
                i++;
                foreach (HControl control in controls)
                {
                    switch (control.Control.ToLower())
                    {
                        case ControlType.AHref:
                            {
                                WatiN.Core.Link obj = MyWatiN.GetLink(ie, control);
                                if (obj != null)
                                {
                                    ConfirmDialogHandler approveConfirmDialog = new ConfirmDialogHandler();
                                    using (new UseDialogOnce(ie.DialogWatcher, approveConfirmDialog))
                                    {
                                        obj.ClickNoWait();
                                        approveConfirmDialog.WaitUntilExists();
                                        approveConfirmDialog.OKButton.Click();
                                        ie.WaitForComplete();
                                        return String.Empty;
                                    }


                                }
                                break;
                            }
                        case ControlType.Link:
                            {
                                WatiN.Core.Link obj = MyWatiN.GetLink(ie, control);
                                if (obj != null)
                                {
                                    ConfirmDialogHandler approveConfirmDialog = new ConfirmDialogHandler();
                                    using (new UseDialogOnce(ie.DialogWatcher, approveConfirmDialog))
                                    {
                                        
                                        obj.ClickNoWait();
                                        approveConfirmDialog.WaitUntilExists();
                                        approveConfirmDialog.OKButton.Click();
                                        ie.WaitForComplete();
                                        return String.Empty;
                                    }
                                    

                                }
                                break;
                            }
                        case ControlType.Button:
                            {
                                WatiN.Core.Button obj = MyWatiN.GetButton(ie, control);
                                if (obj != null)
                                {
                                    ConfirmDialogHandler approveConfirmDialog = new ConfirmDialogHandler();
                                    using (new UseDialogOnce(ie.DialogWatcher, approveConfirmDialog))
                                    {
                                        obj.ClickNoWait();
                                        approveConfirmDialog.WaitUntilExists();
                                        approveConfirmDialog.OKButton.Click();
                                        ie.WaitForComplete();
                                        return String.Empty;
                                    }
                                }
                                break;
                            }
                        case ControlType.Div:
                            {
                                WatiN.Core.Div obj = MyWatiN.GetDiv(ie, control);
                                if (obj != null)
                                {
                                    ConfirmDialogHandler approveConfirmDialog = new ConfirmDialogHandler();

                                    using (new UseDialogOnce(ie.DialogWatcher, approveConfirmDialog))
                                    {
                                        obj.ClickNoWait();
                                        approveConfirmDialog.WaitUntilExists();
                                        approveConfirmDialog.OKButton.Click();
                                        ie.WaitForComplete();
                                        return String.Empty;
                                    }


                                }
                                break;
                            }
                    }

                }
                Thread.Sleep(1000);
            }
            return status;
        }
    }
}

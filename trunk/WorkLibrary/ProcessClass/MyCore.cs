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
        static int Loop = 5;
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
                }
                return "Error Type";
            }
            catch
            {
                return "Error Exception";
            }

        }
        private static string Goto(String text, IE ie)
        {
            int i = 0;
            while (i < Loop)
            {
                i++;
                try
                {
                    ie.GoTo(text);
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
        private static string Fill(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();
           
            string[] a = text.Trim().Split('|');
            string data = a[1].Trim();
            string b = a[0].Trim();
            string[] c = b.Split('[');
            string[] d = c[1].Trim(']').Split(',');
            foreach (string e in d)
            {
                string[] f = e.Trim().Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = c[0].Trim();
                hcontrol.Attribute = f[0].Trim();
                hcontrol.Value = f[1].Trim();
                controls.Add(hcontrol);
            }
            return RunControl(controls,data,ie);
        }
        private static string Click(String text, IE ie)
        {
            List<HControl> controls = new List<HControl>();
            string[] c = text.Trim().Split('[');
            string[] d = c[1].Trim(']').Split(',');
            foreach (string e in d)
            {
                string[] f = e.Trim().Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = c[0].Trim();
                hcontrol.Attribute = f[0].Trim();
                hcontrol.Value = f[1].Trim();
                controls.Add(hcontrol);
            }
            return RunControl(controls,ie);
        }
        public static bool Exist(String text, IE ie)
        {
            try
            {
                List<HControl> controls = new List<HControl>();
                string[] c = text.Trim().Split('[');
                string[] d = c[1].Trim(']').Split(',');
                foreach (string e in d)
                {
                    string[] f = e.Trim().Split(':');
                    HControl hcontrol = new HControl();
                    hcontrol.Control = c[0].Trim();
                    hcontrol.Attribute = f[0].Trim();
                    hcontrol.Value = f[1].Trim();
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
            string[] c = text.Trim().Split('[');
            string[] d = c[1].Trim(']').Split(',');
            foreach (string e in d)
            {
                string[] f = e.Trim().Split(':');
                HControl hcontrol = new HControl();
                hcontrol.Control = c[0].Trim();
                hcontrol.Attribute = f[0].Trim();
                hcontrol.Value = f[1].Trim();
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
                    }

                }
                Thread.Sleep(1000);
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

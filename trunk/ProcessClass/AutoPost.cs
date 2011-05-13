using System;
using System.Collections.Generic;
using System.Text;


using System.Threading;
using WatiN.Core;
using System.Windows.Forms;

namespace NewProject
{
    public class AutoPost
    {
        #region "Variable"

        WebLink forum;
        MultiForum multiforum;
        String _Status;
        String _Subject;
        String _Content;
        String _Tag;
        IE ie;
        #endregion

        public AutoPost(WebLink weblink, String Subject, String Content, String Tag)
        {
            //IE.Settings.AttachToIETimeOut = 100000;
            IE.Settings.BrowserType = BrowserType.FireFox;
            IE.Settings.WaitForCompleteTimeOut = 60000;
            IE.Settings.AttachToIETimeOut = 60000;
            forum = weblink;
            _Subject = Subject;
            _Content = Content;
            _Tag = Tag;
            multiforum = new MultiForum();
          

        }

        #region "Main Function"
      
        public string PostTopic()
        {
            
            if (forum == null)
                return "Object null";
            if (string.IsNullOrEmpty(forum.UrlPost))
            {
                return "Không có link post bài";
            }
            try
            {

                // Start WatiN
                if (Open() == false)
                {
                    return "Không mở được trình duyệt";
                }
                //ie.WaitForComplete();
                // Start Forum
                if (MyWatiN.Goto(forum.UrlPost, ie)!=String.Empty)
                {
                    Close();
                    return "Không vào được link post bài";
                }
                if (RunControl(multiforum.UserName, forum.UserName) != string.Empty)
                {
                    Close();
                    return "Không tìm thấy textbox user name";
                }
                if (RunControl(multiforum.PassWord, forum.Password) != string.Empty)
                {
                    Close();
                    return "Không tìm thấy textbox passworld";
                }
                if ( RunControl(multiforum.Login) != string.Empty)
                {
                    Close();
                    return "Không tìm thấy button Login";
                }
                if (MyWatiN.Goto(forum.UrlPost, ie)!=String.Empty)
                {
                    Close();
                    return "Lỗi do trình duyệt";
                    
                }
                bool isTopic = false;
               
                string href = "";
                HControl link = new HControl();
                link.Control = ControlType.AHref;
                link.Attribute = AttributeType.Text;
               
                for (int j = 0; j < multiforum.NewThread.Count; j++)
                {
                    href = GetLink(ie.Links, multiforum.NewThread[j].Value);
                    if (!string.IsNullOrEmpty(href))
                    {
                        // New Thread
                        multiforum.NewThread[j].Value = href;
                        MyWatiN.FillData(ie, multiforum.NewThread[j]);
                        isTopic = true;
                        break;
                    }
                }
                if (!isTopic)
                {
                    Close();
                    return "Không tìm thấy link tạo bài viết mới";
                }
                if (RunControl(multiforum.Subject, _Subject) != string.Empty)
                {
                    Close();
                    return "Không tìm thấy textbox subject";
                }

                // Control Mode
                bool b = false;
                for (int i = 0; i < multiforum.Mode.Count; i++)
                {
                    if (MyWatiN.IsExist(ie, multiforum.Mode[i]))
                    {
                        for (int j = 0; j < multiforum.Message.Count; j++)
                        {
                            string style = ie.TextField(MyWatiN.GetControl(ie, multiforum.Message[j])).GetAttributeValue("style");
                            string display = Filter.GetTextByRegex(FilterPattern.Display, style, true, 0, 1);
                            if (!string.IsNullOrEmpty(display))
                            {
                                MyWatiN.FillData(ie, multiforum.Mode[i]);
                                b = true;
                                break;
                            }
                        }
                        if (b == true) break;
                    }
                }
                if (RunControl(multiforum.Message, _Content) != string.Empty)
                {
                    Close();
                    return "Không tìm thấy textarea message";
                }
                RunControl(multiforum.Tags, _Tag);
                if (RunControl(multiforum.Submit) != string.Empty)
                {
                    Close();
                    return "Không tìm thấy nút gửi bài viết";
                }
                Close();
                return "Successful";
            }
            catch (Exception ex)
            {
                if (ie != null)
                {
                    
                    Close();
                    return "Lỗi hệ thống ";
                }
            }
            return "";
        }

        #endregion

        #region "Sub Function"

        string RunControl(List<HControl> controls)
        {
            string status = "Error";
            foreach (HControl ctr in controls)
            {
                if (MyWatiN.IsExist(ie, ctr))
                {
                    status = MyWatiN.FillData(ie, ctr);
                    break;
                }
            }
            return status;
            
        }

        string RunControl(List<HControl> controls, string data)
        {
            string status = string.Empty;
            foreach (HControl ctr in controls)
            {
                if (MyWatiN.IsExist(ie, ctr))
                {
                    status = MyWatiN.FillData(ie, ctr, data);
                    break;
                }
            }
            return status;
        }

        private bool Open()
        {
            try
            {
                

                ie = new IE(false);
                //ie.ShowWindow(NativeMethods.WindowShowStyle.Hide);
            }
            catch { return false; }
            try
            {
                ie.ClearCache();
            }
            catch { }
            try
            {
                ie.ClearCookies();
            }
            catch { }
            return true;
            //ie.ShowWindow(NativeMethods.WindowShowStyle.Hide);
        }

        void Close()
        {
            try
            {
                ie.ClearCache();
            }
            catch { }
            try
            {
                ie.ClearCookies();
            }
            catch { }
            try
            {
                ie.Close();
            }
            catch { }
            try
            {
                ie.Dispose();
            }
            catch { }
            ie.Close();
            ie.Dispose();

        }

        string GetLink(LinkCollection links, string link)
        {
            string href = "";
            if (link.IndexOf("./") != -1)
                link = link.Substring(2);
            for (int i = 0; i < links.Length; i++)
                if (!string.IsNullOrEmpty(links[i].Url) && links[i].Url.Contains(link))
                {
                    href = links[i].Url;
                    break;
                }

            return href;
        }

        #endregion
    }
}

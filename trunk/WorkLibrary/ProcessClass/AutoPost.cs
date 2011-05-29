using System;
using System.Collections.Generic;
using System.Text;


using System.Threading;
using WatiN.Core;
using System.Windows.Forms;
using WatiN.Core.Native.Windows;

namespace WorkLibrary
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

        private WebBrowser webBrowse;
        public AutoPost(WebBrowser webBrowse1, WebLink weblink, String Subject, String Content, String Tag)
        {
            //IE.Settings.AttachToIETimeOut = 100000;
            //IE.Settings.BrowserType = BrowserType.FireFox;
            Settings.WaitForCompleteTimeOut = 120000;
            Settings.AttachToBrowserTimeOut = 120000;
           
            webBrowse = webBrowse1;
            forum = weblink;
            _Subject = Subject;
            _Content = Content;
            _Tag = Tag;
            
          

        }

        #region "Main Function"

        public StatusObj PostTopic()
        {
            multiforum = new MultiForum(NumCode.POS);
            StatusObj statusObj=new StatusObj();

            if (forum == null)
            {
                statusObj.Message = "Object null";
                statusObj.Status = "Error";
                return statusObj;
            }
            if (string.IsNullOrEmpty(forum.UrlPost))
            {
                statusObj.Message = "Không có link post bài";
                statusObj.Status = "Error";
                return statusObj;
               
            }
            try
            {

                // Start WatiN
                if (Open() == false)
                {
                    statusObj.Message = "Không mở được trình duyệt";
                    statusObj.Status = "Error";
                    return statusObj;
                   
                }
                if (MyCore.Goto(forum.UrlPost, ie)!=String.Empty)
                {
                    Close();
                    statusObj.Message = "Không vào được link post bài";
                    statusObj.Status = "Error";
                    return statusObj;
                    
                }

                if (MyCore.RunControl(multiforum.UserName, forum.UserName, ie) != string.Empty)
                {
                    goto HadLogin;
                    Close();
                   
                    statusObj.Message = "Không tìm thấy textbox user name";
                    statusObj.Status = "Error";
                    return statusObj;
                }
                if (MyCore.RunControl(multiforum.PassWord, forum.Password,ie) != string.Empty)
                {
                    Close();
                    statusObj.Message = "Không tìm thấy textbox passworld";
                    statusObj.Status = "Error";
                    return statusObj;
                }
                if (MyCore.RunControl(multiforum.Login,ie) != string.Empty)
                {
                    Close();
                   
                    statusObj.Message = "Không tìm thấy button Login";
                    statusObj.Status = "Error";
                    return statusObj;

                }
                Thread.Sleep(3000);
                if (MyCore.Goto(forum.UrlPost, ie)!=String.Empty)
                {
                    Close();
                    
                    statusObj.Message = "Lỗi do trình duyệt";
                    statusObj.Status = "Error";
                    return statusObj;
                    
                }
                Thread.Sleep(1000);
            HadLogin:
                for (int j = 0; j < multiforum.NewThread.Count; j++)
                {
                    Link href = GetLink(ie.Links, multiforum.NewThread[j].Value);
                    if (href==null)
                    {
                        Close();
                        statusObj.Message = "Không tìm thấy link tạo bài viết mới";
                        statusObj.Status = "Error";
                        return statusObj;
                    }
                    else
                    {
                        href.Click();
                        break;
                    }
                    
                }
                if (MyCore.RunControl(multiforum.Subject, _Subject,ie) != string.Empty)
                {
                    Close();
                   
                    statusObj.Message = "Không tìm thấy textbox subject";
                    statusObj.Status = "Error";
                    return statusObj;
                }
               // RunControl(multiforum.Mode);
                if (MyCore.RunControl(multiforum.Mode,ie) != string.Empty)
                {
                    
                }
                if (MyCore.RunControl(multiforum.Message, _Content,ie) != string.Empty)
                {
                    Close();
                   
                    statusObj.Message = "Không tìm thấy textarea message";
                    statusObj.Status = "Error";
                    return statusObj;
                }
                MyCore.RunControl(multiforum.Tags, _Tag,ie);
                //if (MyCore.RunControl(multiforum.Submit) != string.Empty)
                //{
                //    Close();
                //    statusObj.Message = "Không tìm thấy nút gửi bài viết";
                //    statusObj.Status = "Error";
                //    return statusObj;
                   
                //}
                Thread.Sleep(1000);
                statusObj.Message = "Successful";
                statusObj.Status = "Successful";
                statusObj.Value = ie.Url;
                
                Close();
                return statusObj;
                
            }
            catch (Exception ex)
            {
                if (ie != null)
                {
                    
                    Close();
                    
                    statusObj.Message = "Lỗi hệ thống ";
                    statusObj.Status = "Error";
                    return statusObj;
                }
            }
            return statusObj;
        }
        public StatusObj UpTopic()
        {
            multiforum = new MultiForum("UP");
            StatusObj statusObj = new StatusObj();

            if (forum == null)
            {
                statusObj.Message = "Object null";
                statusObj.Status = "Error";
                return statusObj;
            }
            if (string.IsNullOrEmpty(forum.UrlPost))
            {
                statusObj.Message = "Không có link up bài";
                statusObj.Status = "Error";
                return statusObj;

            }
            try
            {

                // Start WatiN
                if (Open() == false)
                {
                    statusObj.Message = "Không mở được trình duyệt";
                    statusObj.Status = "Error";
                    return statusObj;

                }
                //ie.WaitForComplete();
                // Start Forum
                if (MyCore.Goto(forum.UrlPost, ie) != String.Empty)
                {
                    Close();
                    statusObj.Message = "Không vào được link up bài";
                    statusObj.Status = "Error";
                    return statusObj;

                }
                Thread.Sleep(1000);
                if (MyCore.CheckExist(multiforum.UserName, ie) ==false)
                {
                    goto HadLogin;
                    
                }
                if (MyCore.RunControl(multiforum.UserName, forum.UserName,ie) != string.Empty)
                {
                    Close();
                    statusObj.Message = "Không tìm thấy textbox user name";
                    statusObj.Status = "Error";
                    return statusObj;
                }
                if (MyCore.RunControl(multiforum.UserName, forum.UserName,ie) != string.Empty)
                {
                    goto HadLogin;
                    Close();
                    statusObj.Message = "Không tìm thấy textbox user name";
                    statusObj.Status = "Error";
                    return statusObj;
                }
             NoLogin:
                if (MyCore.RunControl(multiforum.PassWord, forum.Password,ie) != string.Empty)
                {
                    
                    Close();
                    statusObj.Message = "Không tìm thấy textbox passworld";
                    statusObj.Status = "Error";
                    return statusObj;
                }
                if (MyCore.RunControl(multiforum.Login,ie) != string.Empty)
                {
                    Close();
                    statusObj.Message = "Không tìm thấy button Login";
                    statusObj.Status = "Error";
                    return statusObj;

                }
                Thread.Sleep(3000);
                if (MyCore.Goto(forum.UrlPost, ie) != String.Empty)
                {
                    Close();

                    statusObj.Message = "Lỗi do trình duyệt";
                    statusObj.Status = "Error";
                    return statusObj;

                }
            HadLogin:
                Thread.Sleep(1000);
                //RunControl(multiforum.Mode) ;
                if (MyCore.RunControl(multiforum.Mode,ie) != string.Empty)
                {
                    
                }
                if (MyCore.RunControl(multiforum.Message, _Content,ie) != string.Empty)
                {
                    Close();
                    statusObj.Message = "Không tìm thấy textarea message";
                    statusObj.Status = "Error";
                    return statusObj;
                }
                //if (RunControl(multiforum.Submit) != string.Empty)
                //{
                //    Close();
                //    statusObj.Message = "Không tìm thấy nút gửi bài viết";
                //    statusObj.Status = "Error";
                //    return statusObj;

                //}
                statusObj.Message = "Successful";
                statusObj.Status = "Successful";
                statusObj.Value = ie.Url;

                Close();
                return statusObj;

            }
            
            catch (Exception ex)
            {
                if (ie != null)
                {

                    Close();

                    statusObj.Message = "Lỗi hệ thống ";
                    statusObj.Status = "Error";
                    return statusObj;
                }
            }
            return statusObj;
        }
        #endregion

        #region "Sub Function"
        private bool Open()
        {
            try
            {
                
                WatiN.Core.Settings.AutoStartDialogWatcher = false;
                ie = new IE(webBrowse.ActiveXInstance);
                
                ie = new IE(true);
                //ie.ShowWindow(NativeMethods.WindowShowStyle.Hide);
            }
            catch { return false; }
            try
            {
                //ie.ClearCache();
            }
            catch { }
            try
            {
                //ie.ClearCookies();
                
            }
            catch { }
            return true;
            //ie.ShowWindow(NativeMethods.WindowShowStyle.Hide);
        }

        void Close()
        {
            try
            {
                //ie.ClearCache();
                
            }
            catch { }
            try
            {
                //ie.ClearCookies();
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

        }

        Link GetLink(LinkCollection links, string link)
        {
            string href = "";
            if (link.IndexOf("./") != -1)
                link = link.Substring(2);
            for (int i = 0; i < links.Length; i++)
                if (!string.IsNullOrEmpty(links[i].Url) && links[i].Url.Contains(link))
                {
                    return links[i];
                  
                }

            return null;
        }

        #endregion
    }
}

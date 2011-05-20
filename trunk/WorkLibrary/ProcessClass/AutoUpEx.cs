using System;
using System.Collections.Generic;
using System.Text;


using System.Threading;
using WatiN.Core;
using System.Windows.Forms;
using WatiN.Core.Native.Windows;
using System.Data;

namespace WorkLibrary
{
    public class AutoUpEx
    {
        #region "Variable"

        WebLink forum;
        MultiForum multiforum;
        String _Status;
        IE ie;
        private long IDWeb;
        #endregion

        private WebBrowser webBrowse;
        public AutoUpEx(WebBrowser webBrowse1, WebLink weblink)
        {
            //IE.Settings.AttachToIETimeOut = 100000;
            //IE.Settings.BrowserType = BrowserType.FireFox;
            Settings.WaitForCompleteTimeOut = 120000;
            Settings.AttachToBrowserTimeOut = 120000;
            webBrowse = webBrowse1;
            forum = weblink;
            
          

        }

        #region "Main Function"
        public StatusObj UpTopic()
        {
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
                WebPage wp = WebPage.GetByPage(forum.Url.Trim().ToLower());
                if(wp==null)
                {
                    statusObj.Message = "Trang Web này chưa được đăng ký";
                    statusObj.Status = "Error";
                    return statusObj;
                }
                IDWeb = wp.ID;
                DataTable dtTable1 = WebStep.GetByIDWeb(IDWeb);
            foreach (DataRow dtRow in dtTable1.Rows)
            {
                string processStep = dtRow["Action"].ToString();
                processStep = processStep.Replace("{UserName}", forum.UserName);
                processStep = processStep.Replace("{Password}", forum.Password);
                processStep = processStep.Replace("{Url}", forum.UrlPost);
                processStep = processStep.Replace("{IDTopic}", forum.IDTopic);
                string s = MyCore.ProcessStep(processStep, ie);
                if(s!=String.Empty)
                {
                    Close();
                    statusObj.Message = dtRow["Message"].ToString();
                    statusObj.Status = "Error";
                    return statusObj;
                }
            }

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


        private bool Open()
        {
            try
            {
                
                WatiN.Core.Settings.AutoStartDialogWatcher = false;
                ie = new IE(webBrowse.ActiveXInstance);
                WatiN.Core.Settings.AutoCloseDialogs = true;
                //ie = new IE(true);
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
    }
}

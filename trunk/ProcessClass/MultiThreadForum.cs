using System;
using System.Collections.Generic;
using System.Text;


using System.Threading;
using WatiN.Core;
using System.Windows.Forms;

namespace NewProject
{
    public class MultiThreadForum
    {
        #region "Variable"

        List<WebLink> listforum;
        MultiForum multiforum;
        List<StatusEntity> status;
        Entry entry;

        bool isRun;

        public bool IsRun
        {
            get { return isRun; }
            set { isRun = value; }
        }

        IE ie;
        #endregion

        #region "Delegate"

        public delegate void ProccessingCallback(int val);
        public ProccessingCallback proccess;

        public delegate void CompleteCallback();
        public CompleteCallback complete;

        #endregion

        public MultiThreadForum()
        {
            IE.Settings.AttachToIETimeOut = 100000;
        }

        #region "Main Function"

        public void Init(List<WebLink> _forum, MultiForum _multiforum, List<StatusEntity> _status,  Entry _entry)
        {
            listforum = _forum;
            multiforum = _multiforum;
            status = _status;
            entry = _entry;

        }

        public WebBrowser webBrowser;
        public void Running()
        {
            //bRun = true;
            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Priority = ThreadPriority.Highest;
            //thread.IsBackground = true;
            //thread.Start();

            Run();
        }

        public void Run()
        {
            if (listforum == null)
                return;
            string error = "";
            while (listforum.Count != 0)
            {
                error = "";
                try
                {
                    WebLink forum = new WebLink();
                    lock (listforum)
                    {
                        if (listforum.Count > 0)
                        {
                            forum = listforum[0];
                            listforum.RemoveAt(0);
                        }
                    }
                    if (string.IsNullOrEmpty(forum.UrlPost))
                        break;
                    // Start WatiN
                    Open();
                    
                    // Start Forum
                    error = MyWatiN.Goto(forum.UrlPost, ie);
                    if (!string.IsNullOrEmpty(error))
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrTimeOut, error);
                        Close();
                        continue;
                    }

                    // === Step 1 === //
                    //NextStep();

                    // Control User name
                    error = "";
                    if ((error = RunControl(multiforum.UserName, forum.UserName)) != string.Empty)
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrUserName, error);
                        Close();
                        continue;
                    }

                    // Control Pass word
                    error = "";
                    if ((error = RunControl(multiforum.PassWord, forum.Password)) != string.Empty)
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrPassword, error);
                        Close();
                        continue;
                    }

                    // Control Login
                    error = "";
                    if ((error = RunControl(multiforum.Login)) != string.Empty)
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrLogin, error);
                        Close();
                        continue;
                    }

                    error = MyWatiN.Goto(forum.UrlPost, ie);
                    if (!string.IsNullOrEmpty(error))
                    {
                        
                        AddStatus(forum.UrlPost, RunStatus.ErrTimeOut, error);
                        Close();
                        continue;
                    }

                    //Wait(10000);

                    // === Step 2 === //
                    //NextStep();

                    // Filter topic
                    bool isTopic = false;
                    bool isNewTopic = false;
                    string href = "";

                    error = "";
                    string tmp = "";

                    HControl link = new HControl();
                    link.Control = ControlType.AHref;
                    link.Attribute = AttributeType.Text;
                   
                    for (int j = 0; j < multiforum.NewThread.Count; j++)
                    {
                        href = GetLink(ie.Links, multiforum.NewThread[j].Value);
                        //forum.NewThread.Field = FilterUrl.AbsoluteUrl(ie.Url, href);
                        if (!string.IsNullOrEmpty(href))
                        {
                            // New Thread
                            multiforum.NewThread[j].Value = href;
                            error = MyWatiN.FillData(ie, multiforum.NewThread[j]);
                            //Wait(3000);

                            isNewTopic = true;
                            isTopic = true;
                            break;
                        }
                    }
                    // === Step 3 === //
                    //NextStep();
                    if (!isNewTopic)
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrNewThread, error);
                        Close();
                        continue;
                    }

                    // Fill entry
                    // Control Subject
                    error = "";
                    if ((error = RunControl(multiforum.Subject, entry.Subject)) != string.Empty)
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrSubject, error);
                        Close();
                        continue;
                    }

                    // Control Mode
                    error = "";
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

                    // Control Message
                    error = "";
                    if ((error = RunControl(multiforum.Message, entry.Message)) != string.Empty)
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrMessage, error);
                        Close();
                        continue;
                    }
                    // === Step 4 === //
                   //NextStep();
                    // Control Tag
                    RunControl(multiforum.Tags, entry.Tags);


                    AddStatus(forum.UrlPost, RunStatus.Success);

                    // === Step 5 === //
                    error = "";
                    if ((error = RunControl(multiforum.Submit)) != string.Empty)
                    {
                        AddStatus(forum.UrlPost, RunStatus.ErrSubmit, error);
                        Close();
                        continue;
                    }

                   // NextStep();

                    // End WatiN
                    Close();
                    Wait(6000);
                }
                catch (Exception ex)
                {
                    if (ie != null)
                    {
                        AddStatus("", RunStatus.Error, ex.Message);
                        Close();
                    }
                }
            }
        }

        #endregion

        #region "Sub Function"
        void NextStep()
        {
            proccess(1);
        }

        void Wait(int time)
        {
            Thread.Sleep(time);
        }

        string RunControl(List<HControl> controls)
        {
            string status = string.Empty;
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

        void Open()
        {
            try
            {
                ie = new IE(false);
                ie.ShowWindow(NativeMethods.WindowShowStyle.Hide);
                ie.ClearCache();
                ie.ClearCookies();
            }
            catch { }
            //ie.ShowWindow(NativeMethods.WindowShowStyle.Hide);
        }

        void Close()
        {
            ie.ClearCache();
            ie.ClearCookies();
            ie.Close();
            ie.Dispose();

            complete();
        }

        void AddStatus(string _form, RunStatus _status)
        {
            status.Add(new StatusEntity(_form, _status));
        }

        void AddStatus(string _form, RunStatus _status, string _message)
        {
            status.Add(new StatusEntity(_form, _status, _message));
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

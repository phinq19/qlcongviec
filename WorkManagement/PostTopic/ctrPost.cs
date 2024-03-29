using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Collections;
using WorkLibrary;

namespace NewProject
{
    public partial class ctrPost : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrPost()
        {
            InitializeComponent();
        }
        MultiForum multiforum;
        public string _Type = NumCode.POS;
        private DataTable dtLogEntries;
 
        public void _InitData(string type)
        {
            _Type = type;
            dtLogEntries = new DataTable();
            dtLogEntries.Columns.Add("ID", typeof(long));
            dtLogEntries.Columns.Add("DateTime", typeof(string));
            dtLogEntries.Columns.Add("LogEntries", typeof(string));
            dtLogEntries.Columns.Add("LinkUp", typeof(string));
            dtLogEntries.Columns.Add("Status", typeof(string));
            gridControl1.DataSource = dtLogEntries;
            if (_Type == NumCode.UPFORUM)
            {
                colLinkUp.Visible = false;
                btnSend.Text = "Up";
                btnGetLinkUp.Visible = false;
                multiforum = new MultiForum("UP");
            }
            else
            {
                multiforum = new MultiForum("POS");
            }
        }

        private void ctrSend_Load(object sender, EventArgs e)
        {
            btnAbort.Enabled = false;
            btnSend.Enabled = true;
        }
        private string Content;
        private string Subject;
        private string Tag;
       
        private DataTable _WebLink;
        public void SetContent(string str)
        {
            Content = str;
        }
        public void SetTag(string str)
        {
            Tag = str;
        }
        public void SetSubject(string str)
        {
            Subject = str;
          
        }
        public void SetWebLink(DataTable dtTable)
        {
            _WebLink = dtTable;
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Maximum = dtTable.Rows.Count;
            progressBarControl1.Properties.Step = 1;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dtTable.Rows.Count;
            progressBar1.Step = 1;
           
        }
        private bool isAbort = false;
        private void btnAbort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Const.ThreadNumber; i++)
            {
                try
                {
                    int j = 0;
                    while (thread[i].IsAlive && j < 5)
                    {
                        j++;
                        thread[i].Abort();
                        thread[i].Interrupt();
                    }
                }
                catch { }
            }
            isAbort = true;
            btnAbort.Enabled = false;
            btnSend.Enabled = true;
        }

        private bool isFinish = false;
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_Type == NumCode.POS)
            {
                if (Subject == "")
                {
                    MessageBox.Show("Chưa có nội dung Subject.", "Thông báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }
            }
            if (Content == "")
            {
                MessageBox.Show("Chưa có nội dung Content.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (_WebLink.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chọn danh sách web cần đăng tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            ThreadPostTopic();
          
            
        }

        private bool haveSuccessful = false;
        private DataTable dtTable;
        Thread[] thread;
        private void ThreadPostTopic()
        {
            isComplete = false;
            haveSuccessful = false;
            progressBar1.Value = 0;
            btnAbort.Enabled = true;
            btnSend.Enabled = false;
            dtLogEntries.Rows.Clear();
            dtTable = _WebLink.Copy();
            hashTable.Clear();
            panelControl1.Controls.Clear();
           
            
            foreach(DataRow dtRow in dtTable.Rows)
            {
                System.Windows.Forms.WebBrowser webBrowser = new System.Windows.Forms.WebBrowser();
                webBrowser.Name = dtRow["ID"].ToString();
                webBrowser.ScriptErrorsSuppressed = true;
                panelControl1.Controls.Add(webBrowser);
            }
            thread = new Thread[Const.ThreadNumber];
            for (int i = 0; i < Const.ThreadNumber; i++)
            {
                thread[i] = new Thread(PostTopic);
                thread[i].SetApartmentState(ApartmentState.STA);

                thread[i].IsBackground = true;
                thread[i].Start();
                Thread.Sleep(1000);
            }
            backgroundWorker1.RunWorkerAsync();
            timer1.Start();


        }
        private void IsFinish()
        {
            while (1==1)
            {
                if (isAbort == true)
                    break;
                if (dtTable.Rows.Count == 0 && _WebLink.Rows.Count == hashTable.Count)
                {
                    bool flag=true;

                    foreach (DataRow dtRow in _WebLink.Rows)
                    {
                        long id = long.Parse(dtRow["ID"].ToString());
                        if ((bool)hashTable[id] == false)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag == true)
                        break;
                }

            }
            Thread.Sleep(1000);
        }
        private Hashtable hashTable = new Hashtable();
        private bool isLock = false;
        private void PostTopic()
        {
            while (dtTable.Rows.Count > 0 )
            {
                if (isAbort == false)
                {
                    if (isLock == false)
                    {
                        if (dtTable.Rows.Count > 0)
                        {
                            try
                            {
                                isLock = true;
                                string strErr = "";
                                long id = long.Parse(dtTable.Rows[0]["ID"].ToString());
                                dtTable.Rows.RemoveAt(0);
                                hashTable.Add(id, false);
                                WebLink weblink = WebLink.Get(id);
                                isLock = false;
                                StatusObj statusObj=new StatusObj();
                                if (weblink != null)
                                {
                                    DataRow dtRow = dtLogEntries.NewRow();
                                    dtRow["ID"] = id;
                                    dtRow["DateTime"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                    dtLogEntries.Rows.Add(dtRow);
                                    if (_Type == NumCode.POS)
                                    {
                                        strErr = "Post to " + weblink.Url + " ....................";
                                        dtRow["LogEntries"] = strErr;
                                        try
                                        {


                                            WebBrowser webBrowser = (WebBrowser) panelControl1.Controls[id.ToString()];
                                            AutoPost post = new AutoPost(webBrowser,multiforum, weblink, Subject, Content, Tag);
                                            statusObj = post.PostTopic();
                                            strErr = strErr + statusObj.Message;
                                        }
                                        catch
                                        {
                                            strErr = strErr + "Error.";
                                        }
                                    }
                                    else
                                    {
                                        strErr = "Up to " + weblink.Url +" [ "+weblink.Topic+" ] "+ "....................";
                                        dtRow["LogEntries"] = strErr;
                                        try
                                        {


                                            WebBrowser webBrowser = (WebBrowser)panelControl1.Controls[id.ToString()];
                                            AutoPost post = new AutoPost(webBrowser,multiforum, weblink, Subject, Content, Tag);
                                            statusObj = post.UpTopic();
                                            strErr = strErr + statusObj.Message;
                                        }
                                        catch
                                        {
                                            strErr = strErr + "Error.";
                                        }
                                    }
                                    dtRow["LogEntries"] = strErr;
                                    dtRow["LinkUp"] = statusObj.Value;
                                    dtRow["Status"] = statusObj.Status;
                                    if (statusObj.Status == "Successful")
                                    {
                                        haveSuccessful = true;
                                    }
                                    hashTable[id] = true;

                                }
                            }
                            catch
                            { isLock = false; }
                        }
                    }
                }
                
            }
            try
            {
                if (Thread.CurrentThread.IsAlive)
                    Thread.CurrentThread.Abort();
            }
            catch { }
            

        }
        private bool isComplete = false;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            IsFinish();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            if (isAbort == true)
            {
                MessageBox.Show("Quá trình Post tin đã được dừng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Đã hoàn thành Post tin bài hết danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            for (int i = 0; i < Const.ThreadNumber; i++)
            {
                try
                {
                    int j = 0;
                    while (thread[i].IsAlive&&j<5)
                    {
                        j++;
                        thread[i].Abort();
                        thread[i].Interrupt();
                    }
                }
                catch { }
            }
            isComplete = true;
            isAbort = false;
            btnAbort.Enabled = false;
            btnSend.Enabled = true;
            btnGetLinkUp.Enabled = false;
            if (haveSuccessful)
            {
                btnGetLinkUp.Enabled = true;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isComplete == true)
            {
                timer1.Stop();
                progressBar1.Value = _WebLink.Rows.Count;
            }
            else
            {
                progressBar1.Value = _WebLink.Rows.Count - CountFinish();
            }
        }
        private int CountFinish()
        {
            int count=0;
            foreach (DataRow dtRow in _WebLink.Rows)
            {
                long id = long.Parse(dtRow["ID"].ToString());
                if ((bool)hashTable[id] == false)
                {
                    count++;
                }
            }
            return count;
        }

        private void btnGetLinkUp_Click(object sender, EventArgs e)
        {
            frmGetLinkUp frm=new frmGetLinkUp(Subject,dtLogEntries);
            frm.ShowDialog();
        }


    }
}

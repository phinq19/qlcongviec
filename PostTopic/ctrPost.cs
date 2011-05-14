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

namespace NewProject
{
    public partial class ctrPost : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrPost()
        {
            InitializeComponent();
            _InitData();

        }
        private DataTable dtLogEntries;
 
        private void _InitData()
        {

            dtLogEntries = new DataTable();
            dtLogEntries.Columns.Add("DateTime", typeof(string));
            dtLogEntries.Columns.Add("LogEntries", typeof(string));
            gridControl1.DataSource = dtLogEntries;
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
           
        }
        private bool isAbort = false;
        private void btnAbort_Click(object sender, EventArgs e)
        {
            isAbort = true;
            btnAbort.Enabled = false;
            btnSend.Enabled = true;
        }

        private bool isFinish = false;
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (Subject == "")
            {
                MessageBox.Show("Chưa có nội dung Subject.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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

        private DataTable dtTable;
        private void ThreadPostTopic()
        {
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
            for (int i = 0; i < Const.ThreadNumber; i++)
            {
                Thread thread = new Thread(PostTopic);
                thread.SetApartmentState(ApartmentState.STA);
                //thread.Priority = ThreadPriority.Highest;
                thread.IsBackground = true;
                thread.Start();
            }
            //Thread threadFinish = new Thread(IsFinish);
            //threadFinish.SetApartmentState(ApartmentState.STA);
            ////threadFinish.Priority = ThreadPriority.Highest;
            //threadFinish.IsBackground = true;
            //threadFinish.Start();
            backgroundWorker1.RunWorkerAsync();


        }
        private void IsFinish()
        {
            while (1==1)
            {
                if (isAbort == true)
                    break;
                if (dtTable.Rows.Count == 0)
                {
                    bool flag=true;
                    IDictionaryEnumerator collec = hashTable.GetEnumerator();
                    while (collec.MoveNext())
                    {
                        if ((bool)hashTable[collec.Key] == false)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag == true)
                        break;
                }

            }
            Thread.Sleep(2000);
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
                        string strErr = "";
                        isLock = true;
                        long id = long.Parse(dtTable.Rows[0]["ID"].ToString());
                        hashTable.Add(id, false);
                        dtTable.Rows.RemoveAt(0);
                        WebLink weblink = WebLink.Get(id);
                        isLock = false;
                        if (weblink != null)
                        {

                            strErr = "Post to " + weblink.Url + ">....................";
                            try
                            {

                                
                                WebBrowser webBrowser = (WebBrowser)panelControl1.Controls[id.ToString()];
                                AutoPost post = new AutoPost(webBrowser, weblink, Subject, Content, Tag);
                                string s = post.PostTopic();
                                strErr = strErr + s;
                            }
                            catch
                            {
                                strErr = strErr + "Error.";
                            }
                            DataRow dtRow = dtLogEntries.NewRow();
                            dtRow["LogEntries"] = strErr;
                            dtRow["DateTime"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            dtLogEntries.Rows.Add(dtRow);
                            hashTable[id] = true;
                        }
                    }
                }
            }
            

        }

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
            isAbort = false;
            btnAbort.Enabled = false;
            btnSend.Enabled = true;
        }


    }
}

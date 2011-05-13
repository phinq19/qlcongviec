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
        Thread workerThread;
        private void btnAbort_Click(object sender, EventArgs e)
        {
            isAbort = true;
            btnAbort.Enabled = false;
            btnSend.Enabled = true;
        }

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
            btnAbort.Enabled = true;
            btnSend.Enabled = true;
            dtLogEntries.Rows.Clear();
            
            workerThread = new Thread(new ThreadStart(this.PostTopic));
            workerThread.Start();
          
            
        }
        private void PostTopic()
        {
            bool flag = true;
            for (int i = 0; i < _WebLink.Rows.Count; i++)
            {
                if (isAbort == false)
                {
                    string strErr = "";
                    long id = long.Parse(_WebLink.Rows[i]["ID"].ToString());
                    WebLink cus = WebLink.Get(id);
                    if (cus != null)
                    {
                        strErr = "Send to " + cus.Url + ">....................";
                        try{
                            Entry entry = new Entry();
                            entry.Subject = Subject;
                            entry.Message = Content;
                            entry.Tags = Tag;

                            MultiForum multiForum = new MultiForum();

                            List<WebLink> forumlist = new List<WebLink>();
                            WebLink weblink = new WebLink();
                            weblink.UserName = "sonituns";
                            weblink.Password = "264286313";
                            //weblink.Url = "http://symbianvn.net/forumdisplay.php?f=41";
                            weblink.Url = "http://www.5giay.vn/forumdisplay.php?f=30";
                            forumlist.Add(weblink);
                            List<StatusEntity> status = new List<StatusEntity>();



                            //MyWatiN.Visible(false);
                            MyWatiN.Init();

                            MultiThreadForum autopost = new MultiThreadForum();
                            //autopost.proccess



                            autopost.Init(forumlist, multiForum, status, entry);

                            autopost.Running();
     
                            
                        }
                        catch
                        {
                            strErr = strErr + "Error.";
                        }
                        //progressBarControl1.Increment(1);
                        DataRow dtRow = dtLogEntries.NewRow();
                        dtRow["LogEntries"] = strErr;
                        dtRow["DateTime"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        dtLogEntries.Rows.Add(dtRow);
                    }
                }
                else
                {

                    //workerThread.Abort();
                    flag = false;
                    break;
                }
            }
            if (flag == false)
            {
                MessageBox.Show("Quá trình Send Mail đã được dừng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("Send Mail hết danh sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            //Thread.Sleep(2000);

            isAbort = false;
           
        }

    }
}

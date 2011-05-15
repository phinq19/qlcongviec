using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using WorkLibrary;

namespace AutoUp
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
        private DataTable dtLogEntries;
        private string Content;
        private DataTable _WebLink;
        public void _InitData(string type)
        {
           
            dtLogEntries = new DataTable();
            dtLogEntries.Columns.Add("ID", typeof(long));
            dtLogEntries.Columns.Add("DateTime", typeof(string));
            dtLogEntries.Columns.Add("LogEntries", typeof(string));
            dtLogEntries.Columns.Add("LinkUp", typeof(string));
            dtLogEntries.Columns.Add("Status", typeof(string));
            gridControl1.DataSource = dtLogEntries;
           
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Content == ""||Content==null)
            {
                MessageBox.Show("Chưa có nội dung Content.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_WebLink==null||_WebLink.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chọn danh sách link cần up bài.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ThreadPostTopic();
        }
        private DataTable dtTable;
        private int ThreadNumber=5;
        private void ThreadPostTopic()
        {
            dtTable = _WebLink.Copy();
            panelControl1.Controls.Clear();
            foreach (DataRow dtRow in dtTable.Rows)
            {
                System.Windows.Forms.WebBrowser webBrowser = new System.Windows.Forms.WebBrowser();
                webBrowser.Name = dtRow["ID"].ToString();
                webBrowser.ScriptErrorsSuppressed = true;
                panelControl1.Controls.Add(webBrowser);
            }
            for (int i = 0; i < ThreadNumber; i++)
            {
                Thread thread = new Thread(PostTopic);
                thread.SetApartmentState(ApartmentState.STA);

                thread.IsBackground = true;
                thread.Start();
                Thread.Sleep(1000);
            }
        }
        private bool isLock = false;
        private bool isAbort = false;
        private void PostTopic()
        {
            while (dtTable.Rows.Count > 0)
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
                                WebLink weblink = WebLink.Get(id);
                                isLock = false;
                                StatusObj statusObj = new StatusObj();
                                if (weblink != null)
                                {
                                    strErr = "Up to " + weblink.Url + " [ " + weblink.Topic + " ] " + "....................";
                                    DataRow dtRow = dtLogEntries.NewRow();
                                    dtRow["ID"] = id;
                                    dtRow["LogEntries"] = strErr;
                                    dtRow["DateTime"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                    dtLogEntries.Rows.Add(dtRow);
                                    try
                                    {
                                        WebBrowser webBrowser = (WebBrowser)panelControl1.Controls[id.ToString()];
                                        AutoPost post = new AutoPost(webBrowser, weblink, "", Content, "");
                                        statusObj = post.UpTopic();
                                        strErr = strErr + statusObj.Message;
                                    }
                                    catch
                                    {
                                        strErr = strErr + "Error.";
                                    }
                                    dtRow["LogEntries"] = strErr;
                                    dtRow["LinkUp"] = statusObj.Value;
                                    dtRow["Status"] = statusObj.Status;
                                  
                                }
                            }
                            catch
                            { }
                        }
                    }
                }
            }


        }

    }
}
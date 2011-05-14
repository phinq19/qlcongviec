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
    public partial class ctrSend : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrSend()
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
        private MailConfig Sender;
        private DataTable Recipients;
        public void SetContent(string str)
        {
            Content = str;
        }
        public void SetSubject(string str)
        {
            Subject = str;
          
        }
        public void SetSender(MailConfig mailConfig)
        {
            Sender = mailConfig;
        }
        public void SetRecipients(DataTable dtTable)
        {
            Recipients = dtTable;
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
            if (Sender == null)
            {
                MessageBox.Show("Chưa có cấu hình Mail.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Recipients.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chọn danh sách khách hàng cần gởi Mail.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            ThreadSendMail();
            
        }

        private DataTable dtTable;
        /*private void SendMail()
        {
            bool flag = true;
            for (int i = 0; i < Recipients.Rows.Count; i++)
            {
                if (isAbort == false)
                {
                    string strErr = "";
                    long id = long.Parse(Recipients.Rows[i]["ID"].ToString());
                    Customers cus = Customers.Get(id);
                    if (cus != null)
                    {
                        strErr = "Send to " + cus.LastName + " " + cus.FirstName + "<" + cus.Email + ">....................";
                        try{
                            string strCt = Content;
                            strCt = strCt.Replace("[[CallName]]", cus.CallName);
                            strCt = strCt.Replace("[[LastName]]", cus.LastName);
                            strCt = strCt.Replace("[[FirstName]]", cus.FirstName);
                            strCt = strCt.Replace("[[Address]]", cus.Address);
                            strCt = strCt.Replace("[[Phone]]", cus.Phone);
                            strCt = strCt.Replace("[[Fax]]", cus.Fax);
                            strCt = strCt.Replace("[[Email]]", cus.Email);
                            strCt = strCt.Replace("[[Note]]", cus.Note);
                            SendMail sm = new SendMail(Sender.Spmt, Sender.Port, Sender.User, Sender.Pass, Sender.EnableSsl, Sender.Timeout);
                            if (sm.BeginSendMail(Sender.MailFrom, Sender.DislayName, cus.Email, Subject,strCt))
                            {
                                //MessageBox.Show("Gởi mail test thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                strErr=strErr+"Successful.";
                            }
                            else
                            {
                                 strErr=strErr+"Error.";
                                //MessageBox.Show("Gởi mail test lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            
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
        */
        private void ThreadSendMail()
        {
            btnAbort.Enabled = true;
            btnSend.Enabled = false;
            dtLogEntries.Rows.Clear();
            dtTable = Recipients.Copy();
            
            for (int i = 0; i < Const.ThreadNumber;i++ )
            {
                Thread thread = new Thread(SendMail);
                thread.IsBackground = true;
                thread.Start();
                Thread.Sleep(1000);
            }
            isFinish = true;
            backgroundWorker1.RunWorkerAsync();
            //Thread threadFinish = new Thread(IsFinish);
            //threadFinish.SetApartmentState(ApartmentState.STA);
            //threadFinish.Priority = ThreadPriority.Highest;
            //threadFinish.IsBackground = true;
            //threadFinish.Start();
            
            
        }
        private bool isFinish = false;
        private void IsFinish()
        {
            while (1==1)
            {
                if (isAbort == true)
                    break;
                if (dtTable.Rows.Count == 0 && Recipients.Rows.Count == hashTable.Count)
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
        private void SendMail()
        {
            while (dtTable.Rows.Count > 0)
            {
                if (isAbort == false)
                {
                    string strErr = "";
                    if (isLock == false)
                    {
                        if (dtTable.Rows.Count > 0)
                        {
                            try
                            {
                                isLock = true;
                                long id = long.Parse(dtTable.Rows[0]["ID"].ToString());
                                dtTable.Rows.RemoveAt(0);
                                hashTable.Add(id, false);
                                Customers cus = Customers.Get(id);

                                isLock = false;
                                if (cus != null)
                                {
                                    strErr = "Send to " + cus.LastName + " " + cus.FirstName + "<" + cus.Email +
                                             ">....................";
                                    try
                                    {
                                        string strCt = Content;
                                        strCt = strCt.Replace("[[CallName]]", cus.CallName);
                                        strCt = strCt.Replace("[[LastName]]", cus.LastName);
                                        strCt = strCt.Replace("[[FirstName]]", cus.FirstName);
                                        strCt = strCt.Replace("[[Address]]", cus.Address);
                                        strCt = strCt.Replace("[[Phone]]", cus.Phone);
                                        strCt = strCt.Replace("[[Fax]]", cus.Fax);
                                        strCt = strCt.Replace("[[Email]]", cus.Email);
                                        strCt = strCt.Replace("[[Note]]", cus.Note);
                                        SendMail sm = new SendMail(Sender.Spmt, Sender.Port, Sender.User, Sender.Pass,
                                                                   Sender.EnableSsl,
                                                                   Sender.Timeout);
                                        if (sm.BeginSendMail(Sender.MailFrom, Sender.DislayName, cus.Email, Subject, strCt))
                                        {
                                            //MessageBox.Show("Gởi mail test thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            strErr = strErr + "Successful.";
                                        }
                                        else
                                        {
                                            strErr = strErr + "Error.";
                                            //MessageBox.Show("Gởi mail test lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }

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
                                    hashTable[id] = true;
                                }
                            }
                            catch
                            {
                            }
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

            if (isAbort == false)
            {
                MessageBox.Show("Quá trình Send Mail đã được dừng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Đã gởi Mail toàn bộ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            isAbort = false;
            btnAbort.Enabled = false;
            btnSend.Enabled = true;
        }
     }
 }

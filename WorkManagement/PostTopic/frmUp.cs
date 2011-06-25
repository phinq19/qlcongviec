using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using WatiN.Core.Native.Windows;
using WorkLibrary;
using NewProject;
using WatiN.Core.DialogHandlers;
using System.Collections;

namespace NewProject
{
    public partial class frmUp : DevExpress.XtraEditors.XtraUserControl
    {
        MultiForum multiforum;
        public frmUp()
        {
            InitializeComponent();
            foreach (System.Windows.Forms.Control ctr in htmlEditor1.Controls)
            {
                if (ctr.Name == "lnkLabelPurchaseLink")
                {
                    ctr.Visible = false;
                    break;
                }
            }
            multiforum = new MultiForum("UP");
        }
        private bool isStop = false;
        private void btnGet_Click(object sender, EventArgs e)
        {
            int[] rows = gridView2.GetSelectedRows();
            for (int i = 0; i < rows.Length; i++)
            {
                object obj = gridView2.GetRow(rows[i]);

                if (gridView2.IsRowVisible(rows[i]) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible && obj != null)
                {
                    AddRowGridView(rows[i]);
                }
            }
            //gridView2.DeleteSelectedRows();
            _LoadDSWebLink();
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                object obj = gridView2.GetRow(i);
                if (gridView2.IsRowVisible(i) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible && obj != null)
                {
                    AddRowGridView(i);
                }
            }
            //dtWebLink.Rows.Clear();
            _LoadDSWebLink();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
            
            _LoadDSWebLink();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            DTbCTPN.Rows.Clear();
            _LoadDSWebLink();
        }
        private DataTable DTbCTPN;
        private DataTable dtTime;
        private DataTable dtWebLink;
        private DataTable dtContent;
        private DataTable dtLogEntries;
        
        public void _InitData()
        {
            DTbCTPN = new DataTable();
            DTbCTPN.Columns.Add("ID", typeof(long));
            DTbCTPN.Columns.Add("Url", typeof(string));
            DTbCTPN.Columns.Add("UrlPost", typeof(string));
            DTbCTPN.Columns.Add("UserName", typeof(string));
            DTbCTPN.Columns.Add("Passwold", typeof(string));
            DTbCTPN.Columns.Add("Topic", typeof(string));
            DTbCTPN.Columns.Add("Note", typeof(string));
            DTbCTPN.Columns.Add("Group", typeof(long));
            gridControl1.DataSource = DTbCTPN;
            _LoadNhomWebLink();
            _LoadWebLinkSettingUp();
            _LoadDSWebLink();
            dtTime = new DataTable();
            dtTime.Columns.Add("Value",typeof(string));
            gridControl3.DataSource = dtTime;
            dateEdit1.DateTime = DateTime.Now;
            _LoadSettingTime();
            dtContent = new DataTable();
            dtContent.Columns.Add("Value", typeof(string));
            _LoadSettingContent();
            dtLogEntries = new DataTable();
            dtLogEntries.Columns.Add("ID", typeof(long));
            dtLogEntries.Columns.Add("DateTime", typeof(string));
            dtLogEntries.Columns.Add("LogEntries", typeof(string));
            dtLogEntries.Columns.Add("LinkUp", typeof(string));
            dtLogEntries.Columns.Add("Status", typeof(string));
            gridControl5.DataSource = dtLogEntries;
            dtTableUp = new DataTable();
            dtTableUp.Columns.Add("ID", typeof(long));
            dtTimeUp = new DataTable();
            dtTimeUp.Columns.Add("Value", typeof(string));
            dtContentUp = new DataTable();
            dtContentUp.Columns.Add("Value", typeof(string));

        }
        private void _LoadSettingContent()
        {
            
            dtContent = UpSetting.GetByGroup("MESS");
            gridControl4.DataSource = dtContent;
        }
        private void _LoadSettingTime()
        {
            DataTable dt;
            dt= UpSetting.GetByGroup("OPS");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Value"].ToString() == "OPS2")
                {
                    checkEdit2.Checked = true;
                }
                else
                {
                    checkEdit1.Checked = true;
                }
            }
            else
            {
                checkEdit1.Checked = true;

            }
            dt = UpSetting.GetByGroup("OPS1");
            if (dt.Rows.Count > 0)
            {
                calcEditOption1.EditValue = dt.Rows[0]["Value"].ToString();
            }
            dtTime = UpSetting.GetByGroup("OPS2");
            gridControl3.DataSource = dtTime;
            dt = UpSetting.GetByGroup("NUMTH");
            if (dt.Rows.Count > 0)
            {
                calcEditNumThread.EditValue = dt.Rows[0]["Value"].ToString();
            }
            dt = UpSetting.GetByGroup("SLEEP");
            if (dt.Rows.Count > 0)
            {
                calcEditSleep.EditValue = dt.Rows[0]["Value"].ToString();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            UpSetting.Delete("LINK");
            foreach (DataRow dtRow in DTbCTPN.Rows)
            {
                try
                {
                    UpSetting up = new UpSetting();
                    up.Value = dtRow["ID"].ToString();
                    up.Group = "LINK";
                    UpSetting.Insert(up);
                }
                catch { }
            }
            UpSetting.Delete("OPS");
            UpSetting up0 = new UpSetting();
            if (checkEdit1.Checked)
                up0.Value = "OPS1";
            else
                up0.Value = "OPS2";
            up0.Group = "OPS";
            UpSetting.Insert(up0);

            UpSetting.Delete("OPS1");
            UpSetting up1 = new UpSetting();
            up1.Value = calcEditOption1.Value.ToString();
            up1.Group = "OPS1";
            UpSetting.Insert(up1);

            UpSetting.Delete("OPS2");
            foreach (DataRow dtRow in dtTime.Rows)
            {
                try
                {
                    UpSetting up2 = new UpSetting();
                    up2.Value = dtRow["value"].ToString();
                    up2.Group = "OPS2";
                    UpSetting.Insert(up2);
                }
                catch { }
            }
            UpSetting.Delete("MESS");
            foreach (DataRow dtRow in dtContent.Rows)
            {
                try
                {
                    UpSetting upm = new UpSetting();
                    upm.Value = dtRow["value"].ToString();
                    upm.Group = "MESS";
                    UpSetting.Insert(upm);
                }
                catch { }
            }
            UpSetting.Delete("NUMTH");
            UpSetting num = new UpSetting();
            num.Value = calcEditNumThread.Value.ToString();
            num.Group = "NUMTH";
            UpSetting.Delete("SLEEP");
            UpSetting.Insert(num);
            UpSetting sleep = new UpSetting();
            sleep.Value = calcEditSleep.Value.ToString();
            sleep.Group = "SLEEP";
            UpSetting.Insert(sleep);
            MessageBox.Show("Đã lưu thông tin thiết lập up tự động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
        private void _LoadWebLinkSettingUp()
        {
            DataTable dt = UpSetting.GetByGroup("LINK");
            string str = "0,";
            foreach (DataRow dtRow in dt.Rows)
            {
                str = str + dtRow["Value"] + ",";
            }
            
                str = "(" + str.Trim(',') + ")";

                DTbCTPN = WebLink.GetIn(str, NumCode.UPFORUM);
            gridControl1.DataSource = DTbCTPN;

        }
        private void _LoadDSWebLink()
        {
            int i = gridView2.TopRowIndex;
            int k = gridView2.FocusedRowHandle;
            string str = "";
            for (int j = 0; j < gridView1.RowCount; j++)
            {
                if (gridView1.GetRow(j) != null)
                {
                    str = str + gridView1.GetRowCellValue(j, colID1).ToString() + ",";
                }
            }
            if (str != "")
            {
                str = "(" + str.Trim(',') + ")";
            }
            dtWebLink = WebLink.GetNotIn(str, NumCode.UPFORUM);
            gridControl2.DataSource = dtWebLink;


            gridView2.FocusedRowHandle = k;
            gridView2.TopRowIndex = i;

        }

        private void _LoadNhomWebLink()
        {
            //DataTable NhomDT = CustomersType.GetAll();
            DataTable dtTable = CodeType.GetByGroup(NumCode.WEB);
            repositoryItemLookUpEdit1.DisplayMember = "Name";
            repositoryItemLookUpEdit1.ValueMember = "ID";
            repositoryItemLookUpEdit1.DataSource = dtTable;
            repositoryItemLookUpEdit3.DisplayMember = "Name";
            repositoryItemLookUpEdit3.ValueMember = "ID";
            repositoryItemLookUpEdit3.DataSource = dtTable;

        }
        private void AddRowGridView(int rowHandle)
        {
            if (rowHandle >= 0)
            {

                long id = long.Parse(gridView2.GetRowCellValue(rowHandle, colID).ToString());
                DataRow[] r = DTbCTPN.Select("ID=" + id.ToString());
                if (r.Length == 0)
                {
                    DataRow dtRow = DTbCTPN.NewRow();
                    dtRow["ID"] = id;
                    dtRow["Url"] = gridView2.GetRowCellValue(rowHandle, colUrl);
                    dtRow["UrlPost"] = gridView2.GetRowCellValue(rowHandle, colUrlPost);
                    dtRow["UserName"] = gridView2.GetRowCellValue(rowHandle, colUsername);
                    dtRow["Note"] = gridView2.GetRowCellValue(rowHandle, colNote);
                    dtRow["Group"] = gridView2.GetRowCellValue(rowHandle, colNhom);
                    dtRow["Topic"] = gridView2.GetRowCellValue(rowHandle, colTopic);
                    DTbCTPN.Rows.Add(dtRow);
                }
            }
            else
            {
                long type = long.Parse(gridView2.GetGroupRowValue(rowHandle).ToString());
                DataRow[] rows = dtWebLink.Select("Group=" + type.ToString());
                foreach (DataRow dtR in rows)
                {
                    long id = long.Parse(dtR["ID"].ToString());
                    DataRow[] r = DTbCTPN.Select("ID=" + id.ToString());
                    if (r.Length == 0)
                    {
                        DataRow dtRow = DTbCTPN.NewRow();
                        dtRow["ID"] = id;
                        dtRow["Url"] = dtR["Url"];
                        dtRow["UrlPost"] = dtR["UrlPost"];
                        dtRow["UserName"] = dtR["UserName"];
                        dtRow["Topic"] = dtR["Topic"];
                        dtRow["Note"] = dtR["Note"];
                        dtRow["Group"] = dtR["Group"];
                        DTbCTPN.Rows.Add(dtRow);
                    }
                }


            }
            //gridView2.DeleteRow(rowHandle);
        }
        private FormDialogWatcher dialogWatcher;
        private void frmOption_Load(object sender, EventArgs e)
        {
            _InitData();
            //WatiN.Core.Settings.AutoStartDialogWatcher = false;
           
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmWebLink frm = new frmWebLink(NumCode.UPFORUM,NumCode.FORUM);
            frm.ShowDialog();
            _LoadDSWebLink();
        }

        private void btnAddOp_Click(object sender, EventArgs e)
        {
            DataRow dtRow = dtTime.NewRow();
            dtRow["Value"] = dateEdit1.DateTime.ToString("HH:mm");
            dtTime.Rows.Add(dtRow);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            gridView3.DeleteSelectedRows();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                groupBox4.Enabled = true;
                groupBox5.Enabled = false;
                checkEdit2.Checked = false;
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                groupBox4.Enabled = false;
                groupBox5.Enabled = true;
                checkEdit1.Checked = false;
            }
        }

        private void btnAddMess_Click(object sender, EventArgs e)
        {
            
            DataRow dtRow = dtContent.NewRow();
            dtRow["Value"] = htmlEditor1.BodyHtml;
            dtContent.Rows.Add(dtRow);
        }

        private void btnDelMess_Click(object sender, EventArgs e)
        {
            gridView4.DeleteSelectedRows();
        }

        private void btnDelLink_Click(object sender, EventArgs e)
        {
            try
            {
                int k = gridView2.SelectedRowsCount;
                if (k > 0)
                {
                    if (
                        MessageBox.Show("Bạn muốn xóa những Topic này", "Thông tin", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        int[] arrSelect = gridView2.GetSelectedRows();
                        for (int i = 0; i < k; i++)
                        {
                            long MaDT = long.Parse(gridView2.GetRowCellValue(arrSelect[i], colID).ToString());
                            WebLink temp = WebLink.Get(MaDT);
                            if (temp != null)
                            {
                                if (WebLink.Delete(temp.ID))
                                {
                                    _LoadDSWebLink();
                                }
                                else
                                {
                                    MessageBox.Show("Không thể link đang được sử dụng");
                                }
                            }
                        }

                    }
                }
            }
            catch { }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                
                int k = gridView2.SelectedRowsCount;
                if (k > 0)
                {
                    int RowHandle = gridView2.GetSelectedRows()[k - 1];
                    if (RowHandle >= 0)
                    {
                        long iDDT = long.Parse(gridView2.GetRowCellValue(RowHandle, colID).ToString());
                        WebUp temp = WebUp.Get(iDDT);
                        if (temp != null)
                        {
                            frmWebLink frm = new frmWebLink(NumCode.UPFORUM,NumCode.FORUM);
                            frm._cus = temp;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                _LoadDSWebLink();
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private DataTable dtContentUp;
        private DataTable dtTimeUp;
        private DataTable dtTableUp;
         
        private void btnStart_Click(object sender, EventArgs e)
        {

            if (DTbCTPN == null || DTbCTPN.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chọn danh sách link cần up bài.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            if (dtContent == null || dtContent.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có nội dung up.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (calcEditNumThread.EditValue == null || calcEditNumThread.EditValue.ToString() == "0")
            {
                MessageBox.Show("Thiết lập số tiến trình thực hiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (calcEditSleep.EditValue == null)
            {
                MessageBox.Show("Thiết lập thời gian up cách nhau giữa mỗi trang.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            btnClose.Enabled = false;
            btnClearLog.Enabled = false;
            tabOption.PageEnabled = false;
            tabMessage.PageEnabled = false;
            tabSelectLink.PageEnabled = false;
            dtTimeUp.Rows.Clear();
            foreach (DataRow dtR in dtTime.Rows)
            {
                try
                {
                    DataRow newRow = dtTimeUp.NewRow();
                    newRow["Value"] = dtR["Value"];
                    dtTimeUp.Rows.Add(newRow);
                }
                catch { }
            }
            //dtTimeUp = dtTime.Copy();
            isStop = false;
            dialogWatcher = new FormDialogWatcher(this.Handle);
            dialogWatcher.CloseUnhandledDialogs = true;
            ThreadPostTopic();

        }
        private void btnStop_Click(object sender, EventArgs e)
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
            isStop = true;
            isAbort = true;
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            tabSelectLink.PageEnabled = true;
            tabMessage.PageEnabled = true;
            tabOption.PageEnabled = true;
            btnClearLog.Enabled = true;
            dialogWatcher.CloseUnhandledDialogs = false;
            try
            {
                dialogWatcher.Dispose();
            }
            catch { }
            
        }
        
        private void btnHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        public int NumberThread=0;
        private void ThreadPostTopic()
        {
            Content = dtContent.Rows[RandomNumber(0, dtContent.Rows.Count - 1)]["Value"].ToString();
            //dtTableUp = DTbCTPN.Copy();
            dtTableUp.Rows.Clear();
            dtLogEntries.Rows.Clear();
            hashTable.Clear();
            panelControl1.Controls.Clear();
            isComplete = false;
            haveSuccessful = false;
            progressBar1.Value = 0;
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            foreach (DataRow dtR in DTbCTPN.Rows)
            {
                try
                {
                    DataRow newRow = dtTableUp.NewRow();
                    newRow["ID"] = dtR["ID"];
                    dtTableUp.Rows.Add(newRow);
                }
                catch { }
            }
            panelControl1.Controls.Clear();
            panelControl1.Visible = true;
            NumberThread=int.Parse(calcEditNumThread.Value.ToString());
            foreach (DataRow dtRow in dtTableUp.Rows)
            {
                System.Windows.Forms.WebBrowser webBrowser = new System.Windows.Forms.WebBrowser();
                webBrowser.Name = dtRow["ID"].ToString();
                webBrowser.Dock = DockStyle.Fill;
                webBrowser.ScriptErrorsSuppressed = true;
                panelControl1.Controls.Add(webBrowser);
            }
            for (int i = 0; i < NumberThread; i++)
            {
                thread[i] = new Thread(PostTopic);
                thread[i].SetApartmentState(ApartmentState.STA);

                thread[i].IsBackground = true;
                thread[i].Start();
                Thread.Sleep(1000*int.Parse(calcEditSleep.Value.ToString()));
            }
            backgroundWorker1.RunWorkerAsync();
            timer1.Start();
        }
        private bool isLock = false;
        private bool isAbort = false;
        private string Content = "";
        
        private void PostTopic()
        {
            while (dtTableUp.Rows.Count > 0)
            {
                if (isAbort == false)
                {
                    if (isLock == false)
                    {
                        if (dtTableUp.Rows.Count > 0)
                        {
                            try
                            {
                                string strErr = "";
                                long id = -1;
                                WebLink weblink = null;
                                lock (this)
                                {
                                    isLock = true;
                                    
                                    id = long.Parse(dtTableUp.Rows[0]["ID"].ToString());
                                    dtTableUp.Rows.RemoveAt(0);
                                    weblink = WebLink.Get(id);
                                    hashTable.Add(id, false);
                                    isLock = false;
                                }
                                StatusObj statusObj = new StatusObj();
                                if (weblink != null)
                                {
                                    strErr = "Up page " + weblink.Url + " [ " + weblink.Topic + " ] " + "....................";
                                    DataRow dtRow = dtLogEntries.NewRow();
                                    dtRow["ID"] = id;
                                    dtRow["LogEntries"] = strErr;
                                    dtRow["DateTime"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                    dtLogEntries.Rows.Add(dtRow);
                                    try
                                    {
                                        WebBrowser webBrowser = (WebBrowser)panelControl1.Controls[id.ToString()];
                                        WorkLibrary.AutoUp post = new WorkLibrary.AutoUp(webBrowser, weblink, Content);
                                        statusObj = post.UpTopicForum();
                                        strErr = strErr + statusObj.Message;
                                        try { webBrowser.Dispose(); }
                                        catch { }
                                        try { panelControl1.Controls.Remove(webBrowser); }
                                        catch { }
                                    }
                                    catch
                                    {
                                        strErr = strErr + " Error.";
                                    }
                                    dtRow["LogEntries"] = strErr;
                                    dtRow["LinkUp"] = statusObj.Value;
                                    dtRow["Status"] = statusObj.Status;

                                }
                            }
                            catch
                            {
                                isLock = false;
                            }
                        }
                    }
                }
            }


        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private void xtabOption_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page==tabUp)
            {
                dtTimeUp.Rows.Clear();
                dtContentUp.Rows.Clear();
                foreach (DataRow dtR in dtTime.Rows)
                {
                    try
                    {
                        DataRow newRow = dtTime.NewRow();
                        newRow["Value"] = dtR["Value"];
                        dtTimeUp.Rows.Add(newRow);
                    }
                    catch { }
                }
                foreach (DataRow dtR in dtContent.Rows)
                {
                    try
                    {
                        DataRow newRow = dtContent.NewRow();
                        newRow["Value"] = dtR["Value"];
                        dtContentUp.Rows.Add(newRow);
                    }
                    catch { }
                }
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            dtLogEntries.Rows.Clear();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                int k = gridView5.SelectedRowsCount;
                if (k > 0)
                {
                    int RowHandle = gridView5.GetSelectedRows()[k - 1];
                    if (RowHandle >= 0)
                    {
                        long iDDT = long.Parse(gridView5.GetRowCellValue(RowHandle, colLogID).ToString());
                        WebLink temp = WebLink.Get(iDDT);
                        if (temp != null)
                        {
                            System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                            Proc.StartInfo.FileName = temp.UrlPost;
                            Proc.Start();
                        }
                    }
                }
            }
            catch { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {

                int k = gridView2.SelectedRowsCount;
                if (k > 0)
                {
                    int RowHandle = gridView2.GetSelectedRows()[k - 1];
                    if (RowHandle >= 0)
                    {
                        long iDDT = long.Parse(gridView2.GetRowCellValue(RowHandle, colID).ToString());
                        WebLink temp = WebLink.Get(iDDT);
                        if (temp != null)
                        {
                            System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                            Proc.StartInfo.FileName = temp.UrlPost;
                            Proc.Start();
                        }
                    }
                }
            }
            catch { }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            IsFinish();
        }
        private void IsFinish()
        {
            while (1 == 1)
            {
                if (isAbort == true)
                    break;
                if (dtTableUp.Rows.Count == 0 && DTbCTPN.Rows.Count == hashTable.Count)
                {
                    bool flag = true;

                    foreach (DataRow dtRow in DTbCTPN.Rows)
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
        private bool haveSuccessful = false;
        Thread[] thread;
        private bool isComplete = false;
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
            for (int i = 0; i < NumberThread; i++)
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
            isComplete = true;
            isAbort = false;
            btnStop.Enabled = false;
            btnStart.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isComplete == true)
            {
                timer1.Stop();
                progressBar1.Value = DTbCTPN.Rows.Count;
            }
            else
            {
                progressBar1.Value = DTbCTPN.Rows.Count - CountFinish();
            }
        }
        private int CountFinish()
        {
            int count = 0;
            foreach (DataRow dtRow in DTbCTPN.Rows)
            {
                long id = long.Parse(dtRow["ID"].ToString());
                if ((bool)hashTable[id] == false)
                {
                    count++;
                }
            }
            return count;
        }



    }
}
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

namespace AutoUpWeb
{
    public partial class frmAutoUpWeb : DevExpress.XtraEditors.XtraForm
    {
        public frmAutoUpWeb()
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
        }
        private bool isStop = false;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
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

        }
        private void _LoadSettingContent()
        {
            
            dtContent = UpSetting.GetByGroup("MESSEX");
            gridControl4.DataSource = dtContent;
        }
        private void _LoadSettingTime()
        {
            DataTable dt;
            dt= UpSetting.GetByGroup("OPSEX");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Value"].ToString() == "OPS2EX")
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
            dt = UpSetting.GetByGroup("OPS1EX");
            if (dt.Rows.Count > 0)
            {
                calcEditOption1.EditValue = dt.Rows[0]["Value"].ToString();
            }
            dtTime = UpSetting.GetByGroup("OPS2EX");
            gridControl3.DataSource = dtTime;
            dt = UpSetting.GetByGroup("NUMTHEX");
            if (dt.Rows.Count > 0)
            {
                calcEditNumThread.EditValue = dt.Rows[0]["Value"].ToString();
            }
            dt = UpSetting.GetByGroup("SLEEPEX");
            if (dt.Rows.Count > 0)
            {
                calcEditSleep.EditValue = dt.Rows[0]["Value"].ToString();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            UpSetting.Delete("LINKEX");
            foreach (DataRow dtRow in DTbCTPN.Rows)
            {
                try
                {
                    UpSetting up = new UpSetting();
                    up.Value = dtRow["ID"].ToString();
                    up.Group = "LINKEX";
                    UpSetting.Insert(up);
                }
                catch { }
            }
            UpSetting.Delete("OPSEX");
            UpSetting up0 = new UpSetting();
            if (checkEdit1.Checked)
                up0.Value = "OPS1EX";
            else
                up0.Value = "OPS2EX";
            up0.Group = "OPSEX";
            UpSetting.Insert(up0);

            UpSetting.Delete("OPS1EX");
            UpSetting up1 = new UpSetting();
            up1.Value = calcEditOption1.Value.ToString();
            up1.Group = "OPS1EX";
            UpSetting.Insert(up1);

            UpSetting.Delete("OPS2EX");
            foreach (DataRow dtRow in dtTime.Rows)
            {
                try
                {
                    UpSetting up2 = new UpSetting();
                    up2.Value = dtRow["value"].ToString();
                    up2.Group = "OPS2EX";
                    UpSetting.Insert(up2);
                }
                catch { }
            }
            UpSetting.Delete("MESSEX");
            foreach (DataRow dtRow in dtContent.Rows)
            {
                try
                {
                    UpSetting upm = new UpSetting();
                    upm.Value = dtRow["value"].ToString();
                    upm.Group = "MESSEX";
                    UpSetting.Insert(upm);
                }
                catch { }
            }
            UpSetting.Delete("NUMTHEX");
            UpSetting num = new UpSetting();
            num.Value = calcEditNumThread.Value.ToString();
            num.Group = "NUMTHEX";
            UpSetting.Delete("SLEEPEX");
            UpSetting.Insert(num);
            UpSetting sleep = new UpSetting();
            sleep.Value = calcEditSleep.Value.ToString();
            sleep.Group = "SLEEPEX";
            UpSetting.Insert(sleep);
            MessageBox.Show("Đã lưu thông tin thiết lập up tự động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
        private void _LoadWebLinkSettingUp()
        {
            DataTable dt = UpSetting.GetByGroup("LINKEX");
            string str = "0,";
            foreach (DataRow dtRow in dt.Rows)
            {
                str = str + dtRow["Value"] + ",";
            }
            
                str = "(" + str.Trim(',') + ")";

                DTbCTPN = WebLink.GetIn(str, NumCode.UPWEB);
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
            dtWebLink = WebLink.GetNotIn(str, NumCode.UPWEB);
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
            frmWebLink frm = new frmWebLink(NumCode.UPWEB,NumCode.WEB);
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
                            frmWebLink frm = new frmWebLink(NumCode.UPWEB,NumCode.WEB);
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
            if (checkEdit1.Checked)
            {
                if (calcEditOption1.EditValue == null || calcEditOption1.EditValue.ToString()=="0")
                {
                    MessageBox.Show("Thiết lập thời gian up bài.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (checkEdit2.Checked)
            {
                if (gridView3.RowCount<=0)
                {
                    MessageBox.Show("Thiết lập thời gian up bài.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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
            if (checkEdit1.Checked)
            {
                timer1.Interval = int.Parse(calcEditOption1.Value.ToString()) * 60000;
                timer1.Start();
            }
            else
            {
                foreach (DataRow dtRow in dtTimeUp.Rows)
                {
                    string[] time = dtRow["Value"].ToString().Split(':');
                    TimeSpan span1 = new TimeSpan(int.Parse(time[0]), int.Parse(time[1]), 0);
                    TimeSpan span2 = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    if (span1 <= span2)
                    {
                        dtRow["Value"] = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + dtRow["Value"].ToString();
                    }
                    else
                    {
                        dtRow["Value"] = DateTime.Now.ToString("yyyyMMdd") + dtRow["Value"].ToString();
                    }
                }
               
                timer2.Start();
            }

        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
            timer1.Stop();
            timer2.Stop();
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            btnClose.Enabled = true;
            btnClearLog.Enabled = true;
            tabSelectLink.PageEnabled = true;
            tabOption.PageEnabled = true;
            dialogWatcher.CloseUnhandledDialogs = false;
            try
            {
                dialogWatcher.Dispose();
            }
            catch { }
            
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
           ThreadPostTopic();
            //Thread thread = new Thread(Start);
            
            //thread.Start();

        }
       
        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (DataRow dtRow in dtTimeUp.Rows)
            {
                if (dtRow["Value"].ToString() == DateTime.Now.ToString("yyyyMMddHH:mm"))
                {
                    dtRow["Value"] = DateTime.Now.AddDays(1).ToString("yyyyMMddHH:mm");
                    ThreadPostTopic();
                }
            }

        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Minimized;
        }

        private void frmOption_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

       
        private void ThreadPostTopic()
        {
            //Content = dtContent.Rows[RandomNumber(0, dtContent.Rows.Count - 1)]["Value"].ToString();
            //dtTableUp = DTbCTPN.Copy();
            dtTableUp.Rows.Clear();
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
            
            foreach (DataRow dtRow in dtTableUp.Rows)
            {
                System.Windows.Forms.WebBrowser webBrowser = new System.Windows.Forms.WebBrowser();
                webBrowser.Name = dtRow["ID"].ToString();
                webBrowser.Dock = DockStyle.Fill;
                webBrowser.ScriptErrorsSuppressed = true;
                panelControl1.Controls.Add(webBrowser);
            }
            for (int i = 0; i < int.Parse(calcEditNumThread.Value.ToString()); i++)
            {
                Thread thread = new Thread(PostTopic);
                
                thread.SetApartmentState(ApartmentState.STA);

                thread.IsBackground = true;
                thread.Start();
                Thread.Sleep(1000*int.Parse(calcEditSleep.Value.ToString()));
            }
        }
        private bool isLock = false;
        private bool isAbort = false;
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
                                        WorkLibrary.AutoUp post = new WorkLibrary.AutoUp(webBrowser, weblink);
                                        statusObj = post.UpTopicWeb();
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

        private void xtabOption_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page==tabUp)
            {
                dtTimeUp = dtTime.Copy();
                dtTableUp = DTbCTPN.Copy();
                dtContentUp = dtContent.Copy();
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
                            this.Close();
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
    }
}
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
using NewProject;

namespace AutoUp
{
    public partial class frmOption : DevExpress.XtraEditors.XtraForm
    {
        public frmOption()
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

        }
        private void _LoadSettingContent()
        {
            
            dtContent = UpSetting.GetByGroup("MESS");
            gridControl4.DataSource = dtContent;
        }
        private void _LoadSettingTime()
        {
            DataTable dt = UpSetting.GetByGroup("OPS");
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
            DataTable dt1 = UpSetting.GetByGroup("OPS1");
            if (dt1.Rows.Count > 0)
            {
                calcEditOption1.EditValue = dt1.Rows[0]["Value"].ToString();
            }
            dtTime = UpSetting.GetByGroup("OPS2");
            gridControl3.DataSource = dtTime;
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

                DTbCTPN = WebLink.GetIn(str, NumCode.UP);
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
            dtWebLink = WebLink.GetNotIn(str, NumCode.UP);
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

        private void frmOption_Load(object sender, EventArgs e)
        {
            _InitData();
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
            up1.Value = calcEditOption1.EditValue.ToString();
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
           
            MessageBox.Show("Đã lưu thông tin up tự động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //frmWebLink frm = new frmWebLink(NumCode.UP);
            //frm.ShowDialog();
            //_LoadDSWebLink();
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

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace NewProject
{
    public partial class ctrSelectLink : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrSelectLink()
        {
            InitializeComponent();
            
            
        }
        private DataTable DTbCTPN;
        private DataTable dtWebLink;
        private string _Type;
        public void _InitData(string type)
        {
            _Type = type;
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
            _LoadDSWebLink();
            if(_Type==NumCode.POS)
            {
                colTopic.Visible = false;
                colTopic1.Visible = false;
            }
            else
            {
                colUrlPost.Visible = false;
                colUrlPost1.Visible = false;
            }
           
        }
        private void ctrRecipients_Load(object sender, EventArgs e)
        {
            
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
            dtWebLink = WebLink.GetNotIn(str, _Type);
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

        
        private void AddRowGridView( int rowHandle)
        {
            if (rowHandle >= 0)
            {

                long id = long.Parse(gridView2.GetRowCellValue(rowHandle, colID).ToString());
                DataRow[] r = DTbCTPN.Select("ID="+id.ToString());
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
        private void btnGet_Click(object sender, EventArgs e)
        {
           int[]rows= gridView2.GetSelectedRows();
            for(int i=0;i<rows.Length;i++)
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
                if (gridView2.IsRowVisible(i) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible&&obj!=null)
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
        public DataTable  GetWebLink()
        {
            return DTbCTPN;
        }
        
    }
}

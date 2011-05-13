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
            _InitData();
            
        }
        private DataTable DTbCTPN;
        private DataTable DTbCustomer;
        public void _InitData()
        {

            DTbCTPN = new DataTable();
            DTbCTPN.Columns.Add("ID", typeof(long));
            DTbCTPN.Columns.Add("Url", typeof(string));
            DTbCTPN.Columns.Add("UrlPost", typeof(string));
            DTbCTPN.Columns.Add("UserName", typeof(string));
            DTbCTPN.Columns.Add("Passwold", typeof(string));
            DTbCTPN.Columns.Add("Note", typeof(string));
            DTbCTPN.Columns.Add("Group", typeof(long));
            gridControl1.DataSource = DTbCTPN;
            _LoadNhomWebLink();
            _LoadDSWebLink();
           
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
            DTbCustomer = WebLink.GetNotIn(str);
            gridControl2.DataSource = DTbCustomer;


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
                    DTbCTPN.Rows.Add(dtRow);
                }
            }
            else
            {
                long type = long.Parse(gridView2.GetGroupRowValue(rowHandle).ToString());
                DataRow[] rows = DTbCustomer.Select("Group=" + type.ToString());
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
            //DTbCustomer.Rows.Clear();
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
            //gridControl1.DataSource = DTbCTPN;
            _LoadDSWebLink();
        }
        public DataTable  GetWebLink()
        {
            return DTbCTPN;
            //ArrayList arr = new ArrayList();
            //foreach (DataRow dtRow in DTbCTPN.Rows)
            //{
            //    Customers cus = new Customers();
            //    cus.ID = long.Parse(dtRow["ID"].ToString());
            //    cus.Email = dtRow["Email"].ToString();
            //    cus.LastName = dtRow["LastName"].ToString();
            //    cus.FirstName = dtRow["FirstName"].ToString();
            //    cus.CallName = dtRow["CallName"].ToString();
            //    arr.Add(cus);
            //}
            //return arr;
        }
        
    }
}

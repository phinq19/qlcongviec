using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using WorkLibrary;

namespace NewProject
{
    public partial class ctrRecipients : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrRecipients()
        {
            InitializeComponent();
            _InitData();
            
        }
        private DataTable DTbCTPN;
        private DataTable DTbCustomer;
        private void _InitData()
        {

            DTbCTPN = new DataTable();
            DTbCTPN.Columns.Add("ID", typeof(int));
            DTbCTPN.Columns.Add("LastName", typeof(string));
            DTbCTPN.Columns.Add("FirstName", typeof(string));
            DTbCTPN.Columns.Add("Type", typeof(int));
            DTbCTPN.Columns.Add("Email", typeof(string));
            DTbCTPN.Columns.Add("CallName", typeof(string));
            gridControl1.DataSource = DTbCTPN;
            _LoadNhomDoiTac();
            _LoadDSDoiTac();
        }
        private void ctrRecipients_Load(object sender, EventArgs e)
        {
           

        }
        private void _LoadDSDoiTac()
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
            DTbCustomer = Customers.GetNotIn(str);
            gridControl2.DataSource = DTbCustomer;


            gridView2.FocusedRowHandle = k;
            gridView2.TopRowIndex = i;

        }

        private void _LoadNhomDoiTac()
        {
            DataTable NhomDT = CustomersType.GetAll();
            ItemLookUp_Nhom.DisplayMember = "Name";
            ItemLookUp_Nhom.ValueMember = "Code";
            ItemLookUp_Nhom.DataSource = NhomDT;
            lkEditNhom.DisplayMember = "Name";
            lkEditNhom.ValueMember = "Code";
            lkEditNhom.DataSource = NhomDT;
            
        }

        
        private void AddRowGridView( int rowHandle)
        {
            if (rowHandle >= 0)
            {
                
                int id = int.Parse(gridView2.GetRowCellValue(rowHandle, colID).ToString());
                DataRow[] r = DTbCTPN.Select("ID="+id.ToString());
                if (r.Length == 0)
                {
                    DataRow dtRow = DTbCTPN.NewRow();
                    dtRow["ID"] = id;
                    dtRow["LastName"] = gridView2.GetRowCellValue(rowHandle, colHo);
                    dtRow["FirstName"] = gridView2.GetRowCellValue(rowHandle, colTen);
                    dtRow["CallName"] = gridView2.GetRowCellValue(rowHandle, colTenGoi);
                    dtRow["Email"] = gridView2.GetRowCellValue(rowHandle, colEmail);
                    dtRow["Type"] = gridView2.GetRowCellValue(rowHandle, colNhom);
                    DTbCTPN.Rows.Add(dtRow);
                }
            }
            else
            {
                int type = int.Parse(gridView2.GetGroupRowValue(rowHandle).ToString());
                DataRow[] rows = DTbCustomer.Select("Type=" + type.ToString());
                foreach (DataRow dtR in rows)
                {
                    int id = int.Parse(dtR["ID"].ToString());
                    DataRow[] r = DTbCTPN.Select("ID=" + id.ToString());
                    if (r.Length == 0)
                    {
                        DataRow dtRow = DTbCTPN.NewRow();
                        dtRow["ID"] = dtR["ID"];
                        dtRow["LastName"] = dtR["LastName"];
                        dtRow["FirstName"] = dtR["FirstName"];
                        dtRow["CallName"] = dtR["CallName"];
                        dtRow["Email"] = dtR["Email"];
                        dtRow["Type"] = dtR["Type"];
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
            _LoadDSDoiTac();
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
            _LoadDSDoiTac();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
            _LoadDSDoiTac();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            DTbCTPN.Rows.Clear();
            //gridControl1.DataSource = DTbCTPN;
            _LoadDSDoiTac();
        }
        public DataTable  GetRecipients()
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

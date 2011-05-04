using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

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
                str=str+gridView1.GetRowCellValue(j,colID1).ToString()+",";
            }
            if (str != "")
            {
                str = "(" + str.Trim(',') + ")";
            }
            gridControl2.DataSource = Customers.GetNotIn(str);


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
            gridView1.AddNewRow();
            gridView1.SetRowCellValue(gridView1.RowCount - 1, colID1, gridView2.GetRowCellValue(rowHandle, colID));
            gridView1.SetRowCellValue(gridView1.RowCount - 1, colHo1, gridView2.GetRowCellValue(rowHandle, colHo));
            gridView1.SetRowCellValue(gridView1.RowCount - 1, colTen1, gridView2.GetRowCellValue(rowHandle, colTen));
            gridView1.SetRowCellValue(gridView1.RowCount - 1, colTenGoi1, gridView2.GetRowCellValue(rowHandle, colTenGoi));
            gridView1.SetRowCellValue(gridView1.RowCount - 1, colEmail1, gridView2.GetRowCellValue(rowHandle, colEmail));
            gridView1.SetRowCellValue(gridView1.RowCount - 1, colNhom1, gridView2.GetRowCellValue(rowHandle, colNhom));
            gridView2.DeleteRow(rowHandle);
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
           int[]rows= gridView2.GetSelectedRows();
            for(int i=0;i<rows.Length;i++)
            {
                if (gridView2.IsRowVisible(rows[i]) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible)
                {
                    AddRowGridView(rows[i]);
                }
            }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                if (gridView2.IsRowVisible(i) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible)
                {
                    AddRowGridView(i);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
            _LoadDSDoiTac();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            _LoadDSDoiTac();
        }
        
    }
}

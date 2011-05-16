using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WorkLibrary;

namespace NewProject
{
    public partial class frmCustomersList : DevExpress.XtraEditors.XtraForm
    {
        public frmCustomersList()
        {
            InitializeComponent();
        }
       
        private void DSDoiTac_Load(object sender, EventArgs e)
        {
            
            _InitData();
        }
        private void _InitData()
        {
            _LoadNhomDoiTac();
            _LoadDSDoiTac();
                      
            //grid_KhachHang.DataSource = ds.Tables[0];
        }

        private void _LoadDSDoiTac()
        {
            int i = gridView1.TopRowIndex;
            int k = gridView1.FocusedRowHandle;


            grid_KhachHang.DataSource = Customers.GetAll();


            gridView1.FocusedRowHandle = k;
            gridView1.TopRowIndex = i;
            
        }

        private void _LoadNhomDoiTac()
        {
            DataTable NhomDT = CustomersType.GetAll();
            ItemLookUp_Nhom.DisplayMember = "Name";
            ItemLookUp_Nhom.ValueMember = "Code";
            ItemLookUp_Nhom.DataSource = NhomDT;
        }


        private void btnThemMoi_Click(object sender, EventArgs e)
        {

            frmCustomers frm = new frmCustomers();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _LoadDSDoiTac();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            _SelectUpdateDoiTac();
        }

        private void _SelectUpdateDoiTac()
        {
            try
            {
                int k = gridView1.SelectedRowsCount;
                if (k > 0)
                {
                    int RowHandle = gridView1.GetSelectedRows()[k - 1];
                    if (RowHandle >= 0)
                    {
                        long iDDT = long.Parse(gridView1.GetRowCellValue(RowHandle, colID).ToString());
                        Customers temp = Customers.Get(iDDT);
                        if (temp != null)
                        {
                            frmCustomers frm = new frmCustomers();
                            frm._cus = temp;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                _LoadDSDoiTac();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            _SelectUpdateDoiTac();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int k = gridView1.SelectedRowsCount;
                if (k > 0)
                {

                    int[] arrSelect = gridView1.GetSelectedRows();
                    for (int i = 0; i < k; i++)
                    {
                        long MaDT = long.Parse(gridView1.GetRowCellValue(arrSelect[i], colID).ToString());
                        Customers temp = Customers.Get(MaDT);
                        if (temp != null)
                        {
                            if (Customers.Delete(temp.ID))
                            {
                                _LoadDSDoiTac();
                            }
                            else
                            {
                                MessageBox.Show("Không thể xoá khách hàng đang được sử dụng");
                            }
                        }
                    }

                }
            }
            catch { }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            _LoadDSDoiTac();
        }
       





    }
}
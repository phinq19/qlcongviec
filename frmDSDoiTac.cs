using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLKD;

namespace PN_QLKD.DanhMuc
{
    public partial class frmDSDoiTac : DevExpress.XtraEditors.XtraForm
    {
        public frmDSDoiTac()
        {
            InitializeComponent();
        }
        public PhanQuyenClass _phanQuyen;
        private void DSDoiTac_Load(object sender, EventArgs e)
        {
            _phanQuyen = PhanQuyenClass.KiemTraPhanQuyen(MyFunction.NguoiDungHienTai.ID, "DoiTac");
            if (_phanQuyen == null)
            {
                MessageBox.Show("Không có quyền mở màn hình này.", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
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


            grid_KhachHang.DataSource = DoiTac.SelectAll();


            gridView1.FocusedRowHandle = k;
            gridView1.TopRowIndex = i;
            
        }

        private void _LoadNhomDoiTac()
        {
            DataTable NhomDT = NhomDoiTac.SelectAll_DataTable();
            ItemLookUp_Nhom.DisplayMember = "Ten";
            ItemLookUp_Nhom.ValueMember = "Ma";
            ItemLookUp_Nhom.DataSource = NhomDT;
        }


        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (_phanQuyen == null || _phanQuyen.Them == false)
            {
                MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmDoiTac frm = new frmDoiTac();
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
            int k = gridView1.SelectedRowsCount;
            if (k > 0)
            {
                if (_phanQuyen == null || _phanQuyen.Sua == false)
                {
                    MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int RowHandle = gridView1.GetSelectedRows()[k - 1];
                if(RowHandle>=0)
                {
                    long iDDT = (long)gridView1.GetRowCellValue(RowHandle, colID);
                    DoiTac temp = DoiTac.ExecuteSelect(iDDT);
                    if (temp != null)
                    {
                        frmDoiTac frm = new frmDoiTac();
                        frm._doiTac = temp;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            _LoadDSDoiTac();
                        }
                    }
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            _SelectUpdateDoiTac();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int k = gridView1.SelectedRowsCount;
            if(k>0)
                {
                    if (_phanQuyen == null || _phanQuyen.Xoa == false)
                    {
                        MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    int[] arrSelect = gridView1.GetSelectedRows();
                    for (int i = 0; i < k; i++)
                    {
                        long MaDT = (long)(gridView1.GetRowCellValue(arrSelect[i], colID));
                        DoiTac temp = DoiTac.ExecuteSelect(MaDT);
                        if (temp != null)
                        {
                            if (temp.ExecuteDelete())
                            {                        
                                
                            }
                            else
                            {
                                MessageBox.Show("Không thể xoá khách hàng đang được sử dụng");
                            }                    
                        }
                    }
                    _LoadDSDoiTac();
             }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            _LoadDSDoiTac();
        }
                                                                                                                          
        private void btnTim_Click(object sender, EventArgs e)
        {
            string maDT = txtMaKH.Text;
            string tenDT = txtTenKH.Text;
            _LoadDSDoiTac(maDT, tenDT);
        }

        private void _LoadDSDoiTac(string maDT, string tenDT)
        {
            grid_KhachHang.DataSource = DoiTac.SelectAllBySearchKey(maDT,tenDT);
        }





    }
}
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
    public partial class formLoaiKhachHang : DevExpress.XtraEditors.XtraForm
    {
        bool Saved;
        DonViTinhClass _loaiHH;
        string status = "NORMAL";
        public formLoaiKhachHang()
        {
            InitializeComponent();
            _loaiHH = null;
        }

        private void _SetFormInfo()
        {
            if (_loaiHH != null)
            {
                txtMa.Text = _loaiHH.Ma.ToString();
                txtTen.Text = _loaiHH.Ten;
                txtGhiChu.Text = _loaiHH.GhiChu;
            }
        }

        private void _ClearForm()
        {
            txtMa.Enabled = true;
            txtMa.Text = "";
            txtTen.Text = "";
            txtGhiChu.Text = "";
            txtMa.Text = "";
            _loaiHH = null;
        }

        private void _setFormStatus(string s)
        {
            status = s;
            if (s == "EDIT")
            {
              
                txtMa.Enabled = false;
                txtGhiChu.Enabled = true;
                txtTen.Enabled = true;
                btnXoa.Enabled = true;
               // btnLuu.Enabled = true;
                btnSua.Enabled = true;
                btnThemMoi.Enabled = false;
                btnXoa.Text = MyFunction.GetResourceName("btnHuy");
                btnSua.Text = MyFunction.GetResourceName("btnLuu");
                txtTen.Focus();

            }
            else if (s == "NEW")
            {
               
               
                txtMa.Enabled = false;
                txtGhiChu.Enabled = true;
              
                txtTen.Enabled = true;
                btnXoa.Enabled = true;
                //btnLuu.Enabled = true;
                btnThemMoi.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Text = MyFunction.GetResourceName("btnHuy");
                btnSua.Text = MyFunction.GetResourceName("btnLuu");
                txtTen.Focus();
            }
            else
            {
                txtGhiChu.Enabled = false;
                txtMa.Enabled = false;
                txtTen.Enabled = false;
               // btnLuu.Enabled = false;
                btnThemMoi.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnXoa.Text = MyFunction.GetResourceName("btnXoa");
                btnSua.Text = MyFunction.GetResourceName("btnSua");
                if (txtMa.Text == "")
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
                
            }
        }

        private DonViTinhClass _getFormInfo()
        {
            DonViTinhClass nhom = null;
           
                if (txtTen.Text != "")
                {
                    nhom = new DonViTinhClass();
                    //nhom.Ma = int.Parse(txtMa.Text);
                    nhom.Ten = txtTen.Text;
                    nhom.GhiChu = txtGhiChu.Text;
                }
                else
                {
                    MessageBox.Show("Nhập vào tên đơn vị tính");
                    txtTen.Focus();

                }
            
            return nhom;
        }

        private void _LoadDSNHom()
        {
            DataTable list = DonViTinhClass.GetAll();
            grid_Control.DataSource = list;
        }
        public void GetResource()
        {
            btnThemMoi.Text = MyFunction.GetResourceName("btnThem");
            btnThoat.Text = MyFunction.GetResourceName("btnThoat");
            btnXoa.Text = MyFunction.GetResourceName("btnXoa");
            btnSua.Text = MyFunction.GetResourceName("btnSua");
        }
        public PhanQuyenClass _phanQuyen;
        private void frmNhomMH_Load(object sender, EventArgs e)
        {
            _phanQuyen = PhanQuyenClass.KiemTraPhanQuyen(MyFunction.NguoiDungHienTai.ID, "DonViTinh");
            if (_phanQuyen == null)
            {
                MessageBox.Show("Không có quyền mở màn hình này.", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            GetResource();
            _LoadDSNHom();
            _ClearForm();
            _setFormStatus("NORMAL");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            //if (!Saved)
            //{
            //    if (MessageBox.Show("Thông tin chưa được lưu. Bạn có muốn đóng form?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        this.Close();
            //    }
            //}
            //else
            {
                this.Close();
            }
        }

        private void Luu()
        {
            DonViTinhClass temp = _getFormInfo();
            if (temp != null)
            {
                if (status == "NEW") //thêm mới
                {
                    try
                    {
                        txtMa.Text= DonViTinhClass.Insert(temp).ToString();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if(status=="EDIT")
                {
                    temp.Ma = int.Parse(txtMa.Text);
                    DonViTinhClass.Update(temp);
                }
                _setFormStatus("NORMAL");
                _LoadDSNHom();
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (_phanQuyen == null || _phanQuyen.Them == false)
            {
                MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ////if (!Saved)
            ////{
            ////    if (MessageBox.Show("Thông tin chưa được lưu. Bạn có muốn huỷ các thay đổi?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            ////    {
            ////        _ClearForm();
            ////    }
            ////}
            ////else
            {
                _ClearForm();
                _setFormStatus("NEW");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (status == "NORMAL")
            {
                if (_loaiHH != null)
                {
                    try
                    {
                        if (_phanQuyen == null || _phanQuyen.Xoa == false)
                        {
                            MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DonViTinhClass.Delete(txtMa.Text);

                        _ClearForm();

                        _LoadDSNHom();
                        _setFormStatus("NORMAL");
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xoá đơn vị tính đang sử dụng");
                    }
                }
                else
                {
                    MessageBox.Show("Chưa có đơn vị tính được chọn");
                }
            }
            else { _setFormStatus("NORMAL"); }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            int k = gridView1.SelectedRowsCount;
            if (k > 0)
            {
                int RowHandle = gridView1.GetSelectedRows()[k - 1];
                if (RowHandle >= 0)
                {
                    _loaiHH = new DonViTinhClass();
                    _loaiHH.Ma = int.Parse(gridView1.GetRowCellValue(RowHandle, colMa).ToString());
                    _loaiHH.Ten = gridView1.GetRowCellValue(RowHandle, colTen).ToString();
                    _loaiHH.GhiChu = gridView1.GetRowCellValue(RowHandle, colGhiChu).ToString();
                    _SetFormInfo();
                    _setFormStatus("NORMAL");
                }
            }
        }

        private void txtTen_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void txtGhiChu_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (status == "NORMAL")
            {
                if (_phanQuyen == null || _phanQuyen.Sua == false)
                {
                    MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _setFormStatus("EDIT");
            }
            else
            {
                Luu();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1_Click(null, null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NewProject
{
    public partial class frmCustomerType : DevExpress.XtraEditors.XtraForm
    {
        bool Saved;
        CustomersType _cusType;
        string status = "NORMAL";
        public frmCustomerType()
        {
            InitializeComponent();
            _cusType = null;
        }

        private void _SetFormInfo()
        {
            if (_cusType != null)
            {
                txtMa.Text = _cusType.Code.ToString();
                txtTen.Text = _cusType.Name;
                txtGhiChu.Text = _cusType.Note;
            }
        }

        private void _ClearForm()
        {
            txtMa.Enabled = true;
            txtMa.Text = "";
            txtTen.Text = "";
            txtGhiChu.Text = "";
            txtMa.Text = "";
            _cusType = null;
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
                btnXoa.Text = Common.GetResourceName("btnHuy");
                btnSua.Text = Common.GetResourceName("btnLuu");
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
                btnXoa.Text = Common.GetResourceName("btnHuy");
                btnSua.Text = Common.GetResourceName("btnLuu");
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
                btnXoa.Text = Common.GetResourceName("btnXoa");
                btnSua.Text = Common.GetResourceName("btnSua");
                if (txtMa.Text == "")
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
                
            }
        }

        private CustomersType _getFormInfo()
        {
            CustomersType nhom = null;
           
                if (txtTen.Text != "")
                {
                    nhom = new CustomersType();
                    //nhom.Ma = int.Parse(txtMa.Text);
                    nhom.Name = txtTen.Text;
                    nhom.Note = txtGhiChu.Text;
                }
                else
                {
                    MessageBox.Show("Nhập vào tên loại khách hàng");
                    txtTen.Focus();

                }
            
            return nhom;
        }

        private void _LoadDSNHom()
        {
            DataTable list = CustomersType.GetAll();
            gridControl1.DataSource = list;
        }
        public void GetResource()
        {
            btnThemMoi.Text = Common.GetResourceName("btnThem");
            btnThoat.Text = Common.GetResourceName("btnThoat");
            btnXoa.Text = Common.GetResourceName("btnXoa");
            btnSua.Text = Common.GetResourceName("btnSua");
        }
       
        private void frmNhomMH_Load(object sender, EventArgs e)
        {
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
            CustomersType temp = _getFormInfo();
            if (temp != null)
            {
                if (status == "NEW") //thêm mới
                {
                    try
                    {
                        txtMa.Text= CustomersType.Insert(temp).ToString();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if(status=="EDIT")
                {
                    temp.Code = long.Parse(txtMa.Text);
                    CustomersType.Update(temp);
                }
                _setFormStatus("NORMAL");
                _LoadDSNHom();
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            //if (_phanQuyen == null || _phanQuyen.Them == false)
            //{
            //    MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
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
                if (_cusType != null)
                {
                    try
                    {
                        //if (_phanQuyen == null || _phanQuyen.Xoa == false)
                        //{
                        //    MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        CustomersType.Delete(long.Parse(txtMa.Text));

                        _ClearForm();

                        _LoadDSNHom();
                        _setFormStatus("NORMAL");
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xoá loại khách hàng đang sử dụng");
                    }
                }
                else
                {
                    MessageBox.Show("Chưa có loại khách hàng được chọn");
                }
            }
            else { _setFormStatus("NORMAL"); }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                int k = gridView1.SelectedRowsCount;
                if (k > 0)
                {
                    int RowHandle = gridView1.FocusedRowHandle;
                    if (RowHandle >= 0)
                    {
                        _cusType = new CustomersType();
                        _cusType.Code = int.Parse(gridView1.GetRowCellValue(RowHandle, colMa).ToString());
                        _cusType.Name = gridView1.GetRowCellValue(RowHandle, colTen).ToString();
                        _cusType.Note = gridView1.GetRowCellValue(RowHandle, colGhiChu).ToString();
                        _SetFormInfo();
                        _setFormStatus("NORMAL");
                    }
                }
            }
            catch { }
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
                //if (_phanQuyen == null || _phanQuyen.Sua == false)
                //{
                //    MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                _setFormStatus("EDIT");
            }
            else
            {
                Luu();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                int k = gridView1.SelectedRowsCount;
                if (k > 0)
                {
                    int RowHandle = gridView1.FocusedRowHandle;
                    if (RowHandle >= 0)
                    {
                        _cusType = new CustomersType();
                        _cusType.Code = int.Parse(gridView1.GetRowCellValue(RowHandle, colMa).ToString());
                        _cusType.Name = gridView1.GetRowCellValue(RowHandle, colTen).ToString();
                        _cusType.Note = gridView1.GetRowCellValue(RowHandle, colGhiChu).ToString();
                        _SetFormInfo();
                        _setFormStatus("NORMAL");
                    }
                }
            }
            catch{}
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using WorkLibrary;

namespace NewProject
{
    public partial class frmCustomers : DevExpress.XtraEditors.XtraForm
    {

        public Customers _cus;
        bool Saved;

        public frmCustomers()
        {
            InitializeComponent();
            Saved = true;
        }

        private void _InitData()
        {
            _LoadCustomersType();
           
        }
        private void _LoadCustomersType()
        {
            DataTable NhomDT = CustomersType.GetAll();

            lookUpEdit_Nhom.Properties.DataSource = NhomDT;
            lookUpEdit_Nhom.Properties.DisplayMember = "Name";
            lookUpEdit_Nhom.Properties.ValueMember = "Code";
        }
        private void frmCustomers_Load(object sender, EventArgs e)
        {
            _InitData();

            if (_cus == null)
            {
                _ClearForm();
            }
            else
            {
                _SetFormInfo();
            }
            _setFormStatus(-1);

        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            //if (_phanQuyen == null || _phanQuyen.Them == false)
            //{
            //    MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            _ClearForm();
            _setFormStatus(-1);
            
        }

        private void _SetFormInfo()
        {
            txtMa.Text = _cus.Code;
            txtHo.Text = _cus.LastName;
            txtTen.Text = _cus.FirstName;
            txtTenGoi.Text = _cus.CallName;
            lookUpEdit_Nhom.EditValue =int.Parse( _cus.Type.ToString());
            //checkEdit_KhachHang.Checked = _cus.KhachHang;
            //checkEdit_NhaCungCap.Checked = _cus.NhaCungCap;

            txtDiaChi.Text = _cus.Address;
            txtDienThoai.Text = _cus.Phone;
            txtFax.Text = _cus.Fax;
            //txtMST.Text = _cus.MaSoThue;
            txtEmail.Text = _cus.Email;
            txtGhiChu.Text = _cus.Note;
        }

        private void _ClearForm()
        {
            txtMa.Text = "";
            lookUpEdit_Nhom.EditValue = null;
            txtHo.Text = "";
            txtTenGoi.Text = "";
            txtTen.Text = "";
           

            txtDiaChi.Text = "";
            txtDienThoai.Text = ""; 
            txtFax.Text = "";
           
            txtEmail.Text = "";
            txtGhiChu.Text = ""; 
           
            _cus = null;

            txtMa.Focus();
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (!Saved)
            {
                if (MessageBox.Show("Thông tin chưa được lưu. Bạn có muốn huỷ các thay đổi và đóng form?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Customers temp = _getFormInfo();
            if (temp != null)
            {
                if (_cus == null) //thêm mới
                {
                   
                    if (!Customers.CheckExits(temp.Code,0))
                    {
                        _cus = temp;
                        Customers.Insert( _cus);
                        DialogResult = DialogResult.OK;
                        if (chkCloseAlterSave.Checked)
                        {
                            
                            this.Close();
                            return;
                        }
                        
                        _setFormStatus(-1);
                    }
                    else
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại!");
                    }
                   

                }
                else //cập nhật
                {

                    if (!Customers.CheckExits(temp.Code, _cus.ID))
                    {
                        temp.ID = _cus.ID;
                        _cus = temp;
                        Customers.Update(_cus);
                        DialogResult = DialogResult.OK;
                        if (chkCloseAlterSave.Checked)
                        {

                            this.Close();
                            return;
                        }
                        _setFormStatus(-1);
                        
                    }
                    else
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại!");
                    }

                }
            }
           
        }

        private Customers _getFormInfo()
        {
            Customers temp = null ;

            if (txtMa.Text != "")
            {
                if (lookUpEdit_Nhom.EditValue != null)
                {
                    if (txtHo.Text != "")
                    {
                        temp = new Customers();
                        temp.Code = txtMa.Text;
                        temp.Type = (int)lookUpEdit_Nhom.EditValue;
                        temp.LastName = txtHo.Text;
                        temp.FirstName = txtTen.Text;
                        temp.CallName = txtTenGoi.Text;
                       

                        temp.Address = txtDiaChi.Text;
                        temp.Phone = txtDienThoai.Text;
                        temp.Fax = txtFax.Text;
                        temp.Type = long.Parse(lookUpEdit_Nhom.EditValue.ToString());
                        temp.Email = txtEmail.Text;
                        temp.Note = txtGhiChu.Text;

                    }
                    else
                    {
                        MessageBox.Show("Nhập tên đối tác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtHo.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Chọn nhóm đối tác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lookUpEdit_Nhom.Focus();
                }
            }
            else
            {
                MessageBox.Show("Nhập mã đối tác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMa.Focus();
            }
            return temp;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_cus != null)
            {
                
                if (Customers.Delete(_cus.ID))
                {
                    DialogResult = DialogResult.OK;
                    if (chkCloseAlterSave.Checked)
                    {

                        this.Close();
                        return;
                    }
                    _ClearForm();
                    _setFormStatus(-1);
                    
                }
                else
                {
                    MessageBox.Show("Không thể xoá khách hàng đang được sử dụng");
                }
            }
        }

        private void btnNhom_Click(object sender, EventArgs e)
        {
            frmCustomerType frm = new frmCustomerType();
            frm.ShowDialog();
            _LoadCustomersType();
            
        }

        private void _setFormStatus(int p)//-1 new -saved ; 0 edited new - not save ;
        {

            btnLuu.Enabled = true;             
            if (_cus == null)
            {
                btnXoa.Enabled = false;
            }
            else
            {
                btnXoa.Enabled = true;
            }
        }

        

        private void txtMa_Leave(object sender, EventArgs e)
        {
            if (txtMa.Text.Trim() != "")
            {
                if (_cus == null)
                {
                    if (Customers.CheckExits(txtMa.Text, 0))
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại");
                        txtMa.Focus();
                    }
                }
                else
                {
                    if (Customers.CheckExits(txtMa.Text, _cus.ID))
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại");
                        txtMa.Focus();
                    }
                }
            }
                else
                {
                    btnLuu.Enabled=false;
                    btnXoa.Enabled=false;
                }
            
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            _setFormStatus(0);
            
        }


    }
}
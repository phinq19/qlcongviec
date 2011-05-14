using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace NewProject
{
    public partial class frmWebLink : DevExpress.XtraEditors.XtraForm
    {

        public WebLink _cus;
        bool Saved;
        string _Type;
        public frmWebLink(string _type)
        {
            InitializeComponent();
            Saved = true;
            _Type = _type;
        }

        private void _InitData()
        {
            _LoadWebLinkType();
           
        }
        private void _LoadWebLinkType()
        {
            DataTable NhomDT = CodeType.GetByGroup(NumCode.WEB);

            lookUpEdit_Nhom.Properties.DataSource = NhomDT;
            lookUpEdit_Nhom.Properties.DisplayMember = "Name";
            lookUpEdit_Nhom.Properties.ValueMember = "ID";
        }
        private void frmWebLink_Load(object sender, EventArgs e)
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
            txtID.Text = _cus.ID.ToString();
            txtUsername.Text = _cus.UserName;
            txtPassword.Text = _cus.Password;
            txtUrl.Text = _cus.Url;
            txtTopic.Text = _cus.Topic;
            txtUrlPost.Text = _cus.UrlPost;
            txtNote.Text = _cus.Note;
            lookUpEdit_Nhom.EditValue =int.Parse( _cus.Group.ToString());

        }

        private void _ClearForm()
        {
            txtID.Text = "";
            lookUpEdit_Nhom.EditValue = null;
            txtUsername.Text = "";
            txtUrl.Text = "";
            txtUrlPost.Text = "";
            txtPassword.Text = "";
            txtNote.Text = "";
            txtTopic.Text = "";
            _cus = null;

            txtUsername.Focus();
            
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
            WebLink temp = _getFormInfo();
            if (temp != null)
            {
                if (_cus == null) //thêm mới
                {
                        _cus = temp;
                        txtID.Text=WebLink.Insert( _cus).ToString();
                        DialogResult = DialogResult.OK;
                        if (chkCloseAlterSave.Checked)
                        {
                            
                            this.Close();
                            return;
                        }
                        
                        _setFormStatus(-1);
                    
                   

                }
                else //cập nhật
                {

                   
                        temp.ID = _cus.ID;
                        _cus = temp;
                        WebLink.Update(_cus);
                        DialogResult = DialogResult.OK;
                        if (chkCloseAlterSave.Checked)
                        {

                            this.Close();
                            return;
                        }
                        _setFormStatus(-1);

                }
            }
           
        }

        private WebLink _getFormInfo()
        {
                WebLink temp = null ;

            
                if (lookUpEdit_Nhom.EditValue != null)
                {
                    if (txtUrl.Text != "")
                    {
                        if (txtUrl.Text != "")
                        {
                            if (txtUsername.Text != "")
                            {
                                if (txtPassword.Text != "")
                                {
                                    temp = new WebLink();

                                    temp.Group = (int)lookUpEdit_Nhom.EditValue;
                                    temp.Type = _Type;
                                    temp.UrlPost = txtUrlPost.Text;
                                    temp.Url = txtUrl.Text;
                                    temp.UserName = txtUsername.Text;
                                    temp.Password = txtPassword.Text;
                                    temp.Note = txtNote.Text;
                                    temp.Topic = txtTopic.Text;
                                }
                                else
                                {
                                    MessageBox.Show("Nhập password đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtPassword.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nhập user name đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtUsername.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nhập link topic cần post tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUrlPost.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Nhập Web page", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUrl.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Chọn nhóm link", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lookUpEdit_Nhom.Focus();
                }
            
            
            return temp;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_cus != null)
            {
                
                if (WebLink.Delete(_cus.ID))
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
            frmTypeSetup frm = new frmTypeSetup(NumCode.WEB,"Thiết lập nhóm web");
            frm.ShowDialog();
            _LoadWebLinkType();
            
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

    }
}
﻿using System;
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
    public partial class frmWebLink : DevExpress.XtraEditors.XtraForm
    {

        public WebUp _cus;
        bool Saved;
        string _Type;
        string _TypePage;
        public frmWebLink(string _type, string _typePage)
        {
            InitializeComponent();
            Saved = true;
            _Type = _type;
            _TypePage = _typePage;
        }

        private void _InitData()
        {
            _LoadWebUpType();
            _LoadWebPage();
           
        }
        private void _LoadWebUpType()
        {
            DataTable NhomDT = CodeType.GetByGroup(NumCode.WEB);

            lookUpEdit_Nhom.Properties.DataSource = NhomDT;
            lookUpEdit_Nhom.Properties.DisplayMember = "Name";
            lookUpEdit_Nhom.Properties.ValueMember = "ID";
        }
        private void _LoadWebPage()
        {
            DataTable NhomDT = WebReg.GetByType(_TypePage);

            lookUpEditPage.Properties.DataSource = NhomDT;
            lookUpEditPage.Properties.DisplayMember = "Page";
            lookUpEditPage.Properties.ValueMember = "ID";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo colun0 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            colun0.Caption = "Page";
            colun0.FieldName = "Page";
            lookUpEditPage.Properties.Columns.Add(colun0);

            DevExpress.XtraEditors.Controls.LookUpColumnInfo colun1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            colun1.Caption = "UserName";
            colun1.FieldName = "UserName";
            lookUpEditPage.Properties.Columns.Add(colun1);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo colun2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            colun2.Caption = "Password";
            colun2.FieldName = "Password";
            colun2.Width = 40;
            lookUpEditPage.Properties.Columns.Add(colun2);

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
            WebReg webReg = WebReg.Get(_cus.Page);
            txtUsername.Text = webReg.UserName;
            txtPassword.Text = webReg.Password;
            txtTopic.Text = _cus.Topic;
            txtIDTopic.Text = _cus.IDTopic;
            txtUrlPost.Text = _cus.UrlPost;
            txtNote.Text = _cus.Note;
            lookUpEdit_Nhom.EditValue =int.Parse( _cus.Group.ToString());
            lookUpEditPage.EditValue = int.Parse(_cus.Page.ToString());

        }

        private void _ClearForm()
        {
            txtID.Text = "";
            lookUpEdit_Nhom.EditValue = null;
            lookUpEditPage.EditValue = null;
            txtUsername.Text = "";
            txtUrlPost.Text = "";
            txtPassword.Text = "";
            txtNote.Text = "";
            txtTopic.Text = "";
            txtIDTopic.Text = "";
            _cus = null;

            lookUpEditPage.Focus();
            
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
            WebUp temp = _getFormInfo();
            if (temp != null)
            {
                if (_cus == null) //thêm mới
                {
                        _cus = temp;
                        txtID.Text=WebUp.Insert( _cus).ToString();
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
                        WebUp.Update(_cus);
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

        private WebUp _getFormInfo()
        {
                WebUp temp = null ;

                if (lookUpEditPage.EditValue != null)
                {
                    if (lookUpEdit_Nhom.EditValue != null)
                    {

                        if (txtUrlPost.Text != "")
                        {
                            
                                    temp = new WebUp();

                                    temp.Group = (int) lookUpEdit_Nhom.EditValue;
                                    temp.Page = long.Parse( lookUpEditPage.EditValue.ToString());
                                    temp.Type = _Type;
                                    temp.UrlPost = txtUrlPost.Text;
                                    temp.Note = txtNote.Text;
                                    temp.Topic = txtTopic.Text;
                                    temp.IDTopic = txtIDTopic.Text;
                                
                        }
                        else
                        {
                            MessageBox.Show("Nhập link topic cần post tin", "Lỗi", MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            txtUrlPost.Focus();
                        }


                    }
                    else
                    {
                        MessageBox.Show("Chọn nhóm link", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lookUpEdit_Nhom.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Chọn trang Web", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lookUpEditPage.Focus();
                }


            return temp;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_cus != null)
            {
                
                if (WebUp.Delete(_cus.ID))
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
                    MessageBox.Show("Không thể xoá weblink đang được sử dụng");
                }
            }
        }

        private void btnNhom_Click(object sender, EventArgs e)
        {
            frmTypeSetup frm = new frmTypeSetup(NumCode.WEB,"Thiết lập nhóm web");
            frm.ShowDialog();
            _LoadWebUpType();
            
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmWebReg frm=new frmWebReg(_TypePage);
            if(frm.ShowDialog()==DialogResult.OK)
                _LoadWebPage();
        }

        private void lookUpEditPage_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditPage.EditValue != null)
            {
                WebReg webReg = WebReg.Get(long.Parse(lookUpEditPage.EditValue.ToString()));
                txtUsername.Text = webReg.UserName;
                txtPassword.Text = webReg.Password;
            }
        }

    }
}
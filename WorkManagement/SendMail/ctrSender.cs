using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WorkLibrary;

namespace NewProject
{
    public partial class ctrSender : DevExpress.XtraEditors.XtraUserControl
    {
        private MailConfig mailConfig;
        private string status = "EDIT";
        public ctrSender()
        {
            InitializeComponent();
            foreach (System.Windows.Forms.Control ctr in htmlEditor1.Controls)
            {
                if (ctr.Name == "lnkLabelPurchaseLink")
                {
                    ctr.Visible = false;
                    break;
                }
            }
        }
        private void ctrContent_Load(object sender, EventArgs e)
        {
            LoadMailSetting();
        }
        private void LoadMailSetting()
        {
            DataTable dtTable = MailConfig.GetAll();

            lkProfile.Properties.DataSource = dtTable;
            lkProfile.Properties.DisplayMember = "Code";
            lkProfile.Properties.ValueMember = "ID";
            lkProfile.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo colun1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            colun1.Caption = "Code";
            colun1.FieldName = "Code";
            lkProfile.Properties.Columns.Add(colun1);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo colun2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            colun2.Caption = "Mail From";
            colun2.FieldName = "MailFrom";
            lkProfile.Properties.Columns.Add(colun2);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo colun3 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            colun3.Caption = "Dislay Name";
            colun3.FieldName = "DislayName";
            lkProfile.Properties.Columns.Add(colun3);
            if (dtTable.Rows.Count > 0)
            {
                MailConfig cf = MailConfig.GetByDefault();
                
                if (cf != null)
                {
                    lkProfile.EditValue = int.Parse(cf.ID.ToString());
                }
                else
                {
                    lkProfile.ItemIndex = 0;
                }
            }
            else
            {
                ClearFormInfo();
                SetFormStatus("NEW");
            }
        }

        private void lkProfile_EditValueChanged(object sender, EventArgs e)
        {
            if (lkProfile.EditValue != null)
            {
                long ID = long.Parse(lkProfile.EditValue.ToString());
                mailConfig = MailConfig.Get(ID);
                ClearFormInfo();
                SetFormInfo();
                SetFormStatus("EDIT");
                btnDelete.Enabled = true;
            }
        }
        private void SetFormInfo()
        {
            txtCode.Text = mailConfig.Code;
            txtFromEmail.Text = mailConfig.MailFrom;
            txtDislayName.Text = mailConfig.DislayName;
            txtSPMT.Text = mailConfig.Spmt;
            calPort.EditValue = mailConfig.Port;
            chkDefault.Checked = mailConfig.Default;
            chkEnableSsl.Checked = mailConfig.EnableSsl;
            calTimeout.EditValue = mailConfig.Timeout;
            txtUser.Text = mailConfig.User;
            txtPass.Text = mailConfig.Pass;
            htmlEditor1.BodyHtml = mailConfig.Signature;
        }
        private MailConfig GetFormInfo()
        {
            MailConfig cf = new MailConfig();
            cf.Code = txtCode.Text;
            cf.MailFrom = txtFromEmail.Text;
            cf.DislayName = txtDislayName.Text;
            cf.Spmt = txtSPMT.Text;
            cf.Port = int.Parse(calPort.EditValue.ToString());
            cf.Default = chkDefault.Checked;
            cf.EnableSsl = chkEnableSsl.Checked;
            cf.Timeout = int.Parse(calTimeout.EditValue.ToString());
            cf.User = txtUser.Text;
            cf.Pass = txtPass.Text;
            cf.Signature = htmlEditor1.BodyHtml;
            cf.UseCredentials = false;
            return cf;
        }
        private void ClearFormInfo()
        {
            txtCode.Text = "";
            txtFromEmail.Text = "";
            txtDislayName.Text = "";
            txtSPMT.Text = "";
            calPort.EditValue = 0;
            chkDefault.Checked = false;
            chkEnableSsl.Checked = true;
            calTimeout.EditValue = 15000;
            txtUser.Text = "";
            txtPass.Text = "";
            htmlEditor1.BodyHtml = "";
        }
        private void SetFormStatus(string s)
        {
            status = s;
            if (s == "NEW")
            {
                txtCode.Enabled = true;
                mailConfig = null;

            }
            else if (s == "EDIT")
            {

                txtCode.Enabled = false;

            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            
            lkProfile.EditValue = null;
            ClearFormInfo();
            mailConfig = null;
            SetFormStatus("NEW");
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lkProfile.EditValue != null)
            {
                long ID = long.Parse(lkProfile.EditValue.ToString());
                MailConfig.Delete(ID);
                ClearFormInfo();
                LoadMailSetting();
                btnDelete.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã profile","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (mailConfig == null && status == "NEW")
            {
                MailConfig cf = GetFormInfo();
                if (chkDefault.Checked)
                {
                    MailConfig.ResetDefault();

                }
                long ID=MailConfig.Insert(cf);
                MessageBox.Show("Thêm mới profile thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMailSetting();

            }
            else if(mailConfig!=null&&status=="EDIT")
            {
                MailConfig cf = GetFormInfo();
                if (chkDefault.Checked)
                {
                    MailConfig.ResetDefault();

                }
                cf.ID = mailConfig.ID;
                MailConfig.Update(cf);
                mailConfig = cf;
                MessageBox.Show("Cập nhập profile thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            SendMail sm = new SendMail(txtSPMT.Text, int.Parse(calPort.EditValue.ToString()), txtUser.Text, txtPass.Text, chkEnableSsl.Checked, int.Parse(calTimeout.EditValue.ToString()));
            if (sm.BeginSendMail(txtFromEmail.Text, txtDislayName.Text, txtFromEmail.Text, "Check Connection From QLCV", "Check Connection From QLCV"))
            {
                MessageBox.Show("Gởi mail test thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Gởi mail test lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public MailConfig GetSender()
        {
            return mailConfig;
        }
    }
}

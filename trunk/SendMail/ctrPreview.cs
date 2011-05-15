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
    public partial class ctrPreview : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrPreview()
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
        private string Content;
        public void SetContent(string str)
        {
             htmlEditor1.BodyHtml=str;
             Content = str;
        }
        public void SetSubject(string str)
        {
            lblSubject.Text = str;
        }
        public void SetSender(MailConfig mailConfig)
        {
            lblMailFrom.Text = mailConfig.DislayName+" <"+mailConfig.MailFrom+">";
        }
        public void SetRecipients(DataTable arr)
        {
            lkMailTo.Properties.DataSource = arr;
            lkMailTo.Properties.DisplayMember = "Email";
            lkMailTo.Properties.ValueMember = "ID";
            lkMailTo.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo colun1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            colun1.Caption = "Email";
            colun1.FieldName = "Email";
            lkMailTo.Properties.Columns.Add(colun1);
            lkMailTo.ItemIndex = 0;
            //lkMailTo_EditValueChanged(null, null);
        }
        private void ctrPreview_Load(object sender, EventArgs e)
        {

        }

        private void lkMailTo_EditValueChanged(object sender, EventArgs e)
        {
            if (lkMailTo.EditValue != null && lkMailTo.EditValue.ToString() != "")
            {
                long id = long.Parse(lkMailTo.EditValue.ToString());
                Customers cus = Customers.Get(id);
                if (cus != null)
                {
                    string strCt = Content;
                    strCt = strCt.Replace("[[CallName]]", cus.CallName);
                    strCt = strCt.Replace("[[LastName]]", cus.LastName);
                    strCt = strCt.Replace("[[FirstName]]", cus.FirstName);
                    strCt = strCt.Replace("[[Address]]", cus.Address);
                    strCt = strCt.Replace("[[Phone]]", cus.Phone);
                    strCt = strCt.Replace("[[Fax]]", cus.Fax);
                    strCt = strCt.Replace("[[Email]]", cus.Email);
                    strCt = strCt.Replace("[[Note]]", cus.Note);
                    htmlEditor1.BodyHtml = strCt;
                }
            }
        }
    }
}

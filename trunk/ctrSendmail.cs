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
    public partial class ctrSendmail : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrSendmail()
        {
            InitializeComponent();
        }
        private void ctrSendmail_Load(object sender, EventArgs e)
        {
            ctrContent1.Visible = false;
            ctrSender1.Visible = false;
            ctrRecipients1.Visible = false;
        }
        private void btnContent_Click(object sender, EventArgs e)
        {
            ctrContent1.Visible = true;
            ctrSender1.Visible = false;
            ctrRecipients1.Visible = false;
        }

        private void btnSender_Click(object sender, EventArgs e)
        {
            ctrSender1.Visible = true;
            ctrContent1.Visible = false;
            ctrRecipients1.Visible = false;
           
        }

        private void btnRecipients_Click(object sender, EventArgs e)
        {
            ctrSender1.Visible = false;
            ctrContent1.Visible = false;
            ctrRecipients1.Visible = true;
        }

        
    }
}

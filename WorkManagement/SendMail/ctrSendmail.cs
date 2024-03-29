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
            ctrSender1.Visible = true;
            ctrRecipients1.Visible = false;
            ctrPreview1.Visible = false;
            ctrSend1.Visible = false;
        }
        private void btnContent_Click(object sender, EventArgs e)
        {
            ctrContent1.Visible = true;
            ctrSender1.Visible = false;
            ctrRecipients1.Visible = false;
            ctrPreview1.Visible = false;
            ctrSend1.Visible = false;
        }

        private void btnSender_Click(object sender, EventArgs e)
        {
            ctrSender1.Visible = true;
            ctrContent1.Visible = false;
            ctrRecipients1.Visible = false;
            ctrPreview1.Visible = false;
            ctrSend1.Visible = false;
           
        }

        private void btnRecipients_Click(object sender, EventArgs e)
        {
            ctrSender1.Visible = false;
            ctrContent1.Visible = false;
            ctrRecipients1.Visible = true;
            ctrPreview1.Visible = false;
            ctrSend1.Visible = false;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            ctrSender1.Visible = false;
            ctrContent1.Visible = false;
            ctrRecipients1.Visible = false;
            ctrPreview1.Visible = true;
            ctrSend1.Visible = false;
            ctrPreview1.SetSender(ctrSender1.GetSender());
            ctrPreview1.SetSubject(ctrContent1.GetSubject());
            ctrPreview1.SetContent(ctrContent1.GetContent()  + ctrSender1.GetSender().Signature);
            ctrPreview1.SetRecipients(ctrRecipients1.GetRecipients());

        }

        private void btnSendmail_Click(object sender, EventArgs e)
        {
            ctrSender1.Visible = false;
            ctrContent1.Visible = false;
            ctrRecipients1.Visible = false;
            ctrPreview1.Visible = false;
            ctrSend1.Visible = true;
            ctrSend1.SetSender(ctrSender1.GetSender());
            ctrSend1.SetSubject(ctrContent1.GetSubject());
            ctrSend1.SetContent(ctrContent1.GetContent() + ctrSender1.GetSender().Signature);
            ctrSend1.SetRecipients(ctrRecipients1.GetRecipients());

        }

        
    }
}

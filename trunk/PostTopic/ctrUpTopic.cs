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
    public partial class ctrUpTopic : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrUpTopic()
        {
            InitializeComponent();
        }
        private void ctrSendmail_Load(object sender, EventArgs e)
        {
            ctrSelectLink1.Visible = true;
            ctrSelectLink1._InitData(NumCode.UP);
            ctrPost1.Visible = false;
            ctrPost1._InitData(NumCode.UP);
            ctrContentTopic1.Visible = false;
            ctrContentTopic1._InitData(NumCode.UP);

        }
        private void btnContent_Click(object sender, EventArgs e)
        {
           ctrSelectLink1.Visible = false;
            ctrPost1.Visible = false;
            ctrContentTopic1.Visible = true;
        }

        private void btnSender_Click(object sender, EventArgs e)
        {
            
           
        }

        private void btnRecipients_Click(object sender, EventArgs e)
        {
            ctrSelectLink1.Visible = true;
            ctrPost1.Visible = false;
            ctrContentTopic1.Visible = false;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            //ctrSender1.Visible = false;
            //ctrContent1.Visible = false;
            //ctrRecipients1.Visible = false;
            //ctrPreview1.Visible = true;
            //ctrSend1.Visible = false;
            //ctrPreview1.SetSender(ctrSender1.GetSender());
            //ctrPreview1.SetSubject(ctrContent1.GetSubject());
            //ctrPreview1.SetContent(ctrContent1.GetContent()  + ctrSender1.GetSender().Signature);
            //ctrPreview1.SetRecipients(ctrRecipients1.GetRecipients());

        }

        private void btnSendmail_Click(object sender, EventArgs e)
        {
            ctrSelectLink1.Visible = false;
            ctrPost1.Visible = true;
            ctrContentTopic1.Visible = false;
            ctrPost1.SetWebLink(ctrSelectLink1.GetWebLink());
            ctrPost1.SetSubject(ctrContentTopic1.GetSubject());
            ctrPost1.SetContent(ctrContentTopic1.GetContent());
            ctrPost1.SetTag(ctrContentTopic1.GetTag());
        }

        
    }
}

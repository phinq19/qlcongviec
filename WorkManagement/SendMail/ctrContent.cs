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
    public partial class ctrContent : DevExpress.XtraEditors.XtraUserControl
    {
        public ctrContent()
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
        public string GetSubject()
        {
            return txtSubject.Text.Trim();
        }
        public string GetContent()
        {
            return htmlEditor1.BodyHtml;
        }
        private void ctrContent_Load(object sender, EventArgs e)
        {

        }
    }
}

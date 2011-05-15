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
    public partial class ctrContentTopic : DevExpress.XtraEditors.XtraUserControl
    {
        private string _Type=NumCode.POS;
        public ctrContentTopic()
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
        public void _InitData(string _type)
        {
            _Type = _type;
            if(_Type==NumCode.UP)
            {
                txtSubject.Visible = false;
                txtTag.Visible = false;
                labelControl1.Visible = false;
                labelControl2.Visible = false;
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
        public string GetTag()
        {
            return txtTag.Text ;
        }
        private void ctrContent_Load(object sender, EventArgs e)
        {

        }
    }
}

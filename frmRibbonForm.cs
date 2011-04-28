using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace NewProject
{
    public partial class frmRibbonForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmRibbonForm()
        {
            InitializeComponent();
        }

        private void frmRibbonForm_Load(object sender, EventArgs e)
        {
            _showControl(new ctrSendmail());
        }
        public void _showControl(Control obj)
        {
            clientPanel.Visible = false;
            clientPanel.Controls.Clear();
            obj.Dock = DockStyle.Fill;
            clientPanel.Controls.Add(obj);
            clientPanel.Visible = true;
        }
    }
}
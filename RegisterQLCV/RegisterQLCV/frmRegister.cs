using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace RegiterQLCV
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Encryption enc = new Encryption();
            string strK = enc.DecryptData(txtKey.Text);
            string strKey = strK.Substring(2, 1) + strK.Substring(6, 1) + strK.Substring(4, 1) + strK.Substring(2, 1) + strK.Substring(8, 1) + strK.Substring(6, 1) + strK.Substring(3, 1) + strK.Substring(1, 1) + strK.Substring(3, 1);
            txtKeyRegister.Text = strKey;
            
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            
        }
    }
}

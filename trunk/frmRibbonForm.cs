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
            DevExpress.Skins.SkinManager.EnableFormSkins();
            InitializeComponent();
           // DevExpress.Skins.SkinManager.EnableFormSkins();

            
        }

        private void frmRibbonForm_Load(object sender, EventArgs e)
        {
           // _showControl(new ctrSendmail());
           
            Type myObjectType = typeof(Customers);
            System.Reflection.FieldInfo[] fieldInfo = myObjectType.GetFields();
            string str = "";
            foreach (System.Reflection.FieldInfo info in fieldInfo)
            {
                str = str + info.Name + ":" + info.FieldType.Name + "\n";
                    
            }

            MessageBox.Show(str);
        }
        public void _showControl(Control obj)
        {
            clientPanel.Visible = false;
            clientPanel.Controls.Clear();
            obj.Dock = DockStyle.Fill;
            clientPanel.Controls.Add(obj);
            clientPanel.Visible = true;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCustomerType frm = new frmCustomerType();
            frm.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCustomersList frm = new frmCustomersList();
            frm.ShowDialog();
        }
    }
}
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
using WorkLibrary;

namespace AutoUp
{
    public partial class frmRegister : Form
    {
        private string sGuiID = "";
        private string sSerial = "";
        public frmRegister()
        {
            InitializeComponent();
            sGuiID = Guid.NewGuid().ToString();
            sSerial = Common.GetStuff();

        }
        private bool CheckKey(string Key,string RegisterKey)
        {
            Encryption enc = new Encryption();
            string strK =enc.DecryptData(Key);
            string strKey = strK.Substring(2, 1) + strK.Substring(6, 1) + strK.Substring(4, 1) + strK.Substring(2, 1) + strK.Substring(8, 1) + strK.Substring(6, 1) + strK.Substring(3, 1) + strK.Substring(1, 1) + strK.Substring(3, 1);
            if (strKey == RegisterKey)
                return true;
            return false;
        }
        private bool SaveRegister(string RegisterKey)
        {
            try
            {
                
                Encryption enc = new Encryption();
                FileStream fs = new FileStream("reqlkd.dll", FileMode.Create);
                XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);

                // Khởi động tài liệu.
                w.WriteStartDocument();
                w.WriteStartElement("QLCV");

                // Ghi một product.
                w.WriteStartElement("Register");
                w.WriteAttributeString("GuiNumber", enc.EncryptData(sGuiID));
                w.WriteAttributeString("Serialnumber", enc.EncryptData(sSerial));
                w.WriteAttributeString("KeyRegister", enc.EncryptData(RegisterKey, sSerial + sGuiID));
                w.WriteEndElement();

                // Kết thúc tài liệu.
                w.WriteEndElement();
                w.WriteEndDocument();
                w.Flush();
                w.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (CheckKey(txtKey.Text, txtKeyRegister.Text) == true)
            {
                SaveRegister(txtKeyRegister.Text);
                MessageBox.Show("Đã đăng ký thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Thông tin số đăng ký không chính xác.Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKeyRegister.SelectAll();
                txtKeyRegister.Focus();
            }
            
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            Encryption enc = new Encryption();
            txtKey.Text = enc.EncryptData(sSerial + sGuiID);
        }
    }
}

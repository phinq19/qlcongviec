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
            
            
           
            //Type myObjectType = typeof(Customers);
            //System.Reflection.FieldInfo[] fieldInfo = myObjectType.GetFields();
            //string str = "";
            //foreach (System.Reflection.FieldInfo info in fieldInfo)
            //{
            //    str = str + info.Name + ":" + info.FieldType.Name + "\n";
                    
            //}

            //MessageBox.Show(str);
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

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            _showControl(new ctrSendmail());
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Chương trình sẽ đóng để thực hiện cập nhập.Bạn có muốn tiếp tục thực hiện", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                    Proc.StartInfo.FileName = "UpdateQLCV.exe";
                    Proc.Start();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Không thể thực hiện cập nhập phần mềm.");
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
           
                

                Entry entry = new Entry();
                entry.Subject = "Test";
                entry.Message = "Test";
                entry.Tags = "tag";

                MultiForum multiForum = new MultiForum();

                List<WebLink> forumlist = new List<WebLink>();
                WebLink weblink = new WebLink();
                weblink.UserName = "sonituns";
                weblink.Password = "264286313";
                //weblink.Url = "http://symbianvn.net/forumdisplay.php?f=41";
                weblink.Url = "http://www.5giay.vn/forumdisplay.php?f=30";
                forumlist.Add(weblink);
                List<StatusEntity>  status = new List<StatusEntity>();

                

                //MyWatiN.Visible(false);
                MyWatiN.Init();

                MultiThreadForum autopost = new MultiThreadForum();
                //autopost.proccess
             
              
               
                autopost.Init(forumlist, multiForum, status, entry);
               
                autopost.Running();
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using WorkLibrary;


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
            _showControl(new ucAbout());
            
           
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


            _showControl(new ctrPostTopic());
                //Entry entry = new Entry();
                //entry.Subject = "Test";
                //entry.Message = "Test";
                //entry.Tags = "tag";

                //MultiForum multiForum = new MultiForum();

                //List<WebLink> forumlist = new List<WebLink>();
                //WebLink weblink = new WebLink();
                //weblink.UserName = "sonituns";
                ////weblink.Url = "http://symbianvn.net/forumdisplay.php?f=41";
                //weblink.UrlPost = "http://www.5giay.vn/forumdisplay.php?f=30";
                //forumlist.Add(weblink);
                //List<StatusEntity>  status = new List<StatusEntity>();

                

                ////MyWatiN.Visible(false);
                //MyWatiN.Init();

                //MultiThreadForum autopost = new MultiThreadForum();
                ////autopost.proccess
             
              
               
                //autopost.Init(forumlist, multiForum, status, entry);
          
                //autopost.Running();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmFieldSetting frm = new frmFieldSetting(NumCode.POS);
            frm.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmWebLinkList frm = new frmWebLinkList(NumCode.POS,NumCode.FORUM);
            frm.ShowDialog();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                    Proc.StartInfo.FileName = "WebBrowser.exe";
                    Proc.Start();
            }
            catch
            {
                MessageBox.Show("Không mở được WebBrowser.");
            }
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmWebLinkList frm = new frmWebLinkList(NumCode.UPFORUM, NumCode.FORUM);
            frm.ShowDialog();
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            _showControl(new ctrUpTopic());
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmFieldSetting frm = new frmFieldSetting("UP");
            frm.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmWebReg frm=new frmWebReg(NumCode.WEB);
            frm.ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmWebLinkList frm=new frmWebLinkList(NumCode.UPWEB,NumCode.WEB);
            frm.ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                    Proc.StartInfo.FileName = "AutoUpWeb.exe";
                    Proc.Start();
            }
            catch
            {
                MessageBox.Show("Không mở được chương trình Up Bài Tự Động.");
            }
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmWebReg frm = new frmWebReg(NumCode.FORUM);
            frm.ShowDialog();
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                Proc.StartInfo.FileName = "AutoUpForum.exe";
                Proc.Start();
            }
            catch
            {
                MessageBox.Show("Không mở được chương trình Up Bài Tự Động.");
            }
        }
    }
}
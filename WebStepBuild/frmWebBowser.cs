using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WatiN.Core;
using Image=WatiN.Core.Image;
using WorkLibrary;
using NewProject;


namespace CreateWebStep
{
    public partial class frmWebBowser : DevExpress.XtraEditors.XtraForm
    {
        private string _Type;
        public frmWebBowser()
        {
            InitializeComponent();
        }

        private IE ie;
        private FireFox fire;
        WebPage webpage;
        public long pageID = -1;
        private void frmWebBowser_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            //IE.Settings.AutoStartDialogWatcher = false;
            WatiN.Core.Settings.AutoStartDialogWatcher = false;
            ie = new IE(webBrowser1.ActiveXInstance);
            webpage = new WebPage();
            lookUpEditPage.Properties.DataSource = webpage.GetAllToTable();
            lookUpEditPage.Properties.DisplayMember = "Page";
            lookUpEditPage.Properties.ValueMember = "ID";
            if (pageID > 0)
            {
                lookUpEditPage.EditValue = int.Parse(pageID.ToString());
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            txtLink.Text = webBrowser1.Url.ToString();
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(webBrowser1.Url.ToString());
            treeView1.Nodes.Add(root);
            TreeNode textBox = new TreeNode("TextBox");
            foreach (WatiN.Core.TextField obj in ie.TextFields)
            {
                TreeNode nodeA = new TreeNode(obj.Name);
                nodeA.Nodes.Add(obj.Id, "Id = " + obj.Id);
                nodeA.Nodes.Add(obj.Name,"Name = " + obj.Name);
                nodeA.Nodes.Add(obj.ReadOnly.ToString(), "ReadOnly = " + obj.ReadOnly.ToString());
                nodeA.Nodes.Add(obj.Text,"Text = " + obj.Text);
                nodeA.Nodes.Add(obj.Value,"Value = " + obj.Value);
                textBox.Nodes.Add(nodeA);
            }
            root.Nodes.Add(textBox);
            textBox.Expand();
            TreeNode button = new TreeNode("Button");
            foreach (WatiN.Core.Button obj in ie.Buttons)
            {
                TreeNode nodeB = new TreeNode(obj.Value);
                nodeB.Nodes.Add(obj.Id,"Id = " + obj.Id);
                nodeB.Nodes.Add(obj.Text,"ClassName = " + obj.ClassName);
                nodeB.Nodes.Add(obj.Text,"Text = " + obj.Text);
                nodeB.Nodes.Add(obj.Value,"Value = " + obj.Value);
                //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                button.Nodes.Add(nodeB);
            }
            root.Nodes.Add(button);
            button.Expand();
            root.Expand();
            TreeNode div = new TreeNode("Div");
            foreach (WatiN.Core.Div obj in ie.Divs)
            {
                if (obj.Id != null && obj.Id.ToString() != "")
                {
                    TreeNode nodeD = new TreeNode(obj.Id);
                    nodeD.Nodes.Add(obj.Id,"Id = " + obj.Id);
                    nodeD.Nodes.Add(obj.Name,"Name = " + obj.Name);
                    nodeD.Nodes.Add(obj.ClassName,"ClassName = " + obj.ClassName);
                    //nodeD.Nodes.Add("Text = " + obj.Text);

                    //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                    div.Nodes.Add(nodeD);
                }
            }
            root.Nodes.Add(div);
            div.Expand();
          

            TreeNode link = new TreeNode("Link");
            foreach (WatiN.Core.Link obj in ie.Links)
            {
                //if (obj.ClassName != null && obj.ClassName.ToString() != "")
                {
                    TreeNode nodeL = new TreeNode(obj.Text);
                    nodeL.Nodes.Add("Id = " + obj.Id);
                    nodeL.Nodes.Add("Value = " + obj.Name);
                    nodeL.Nodes.Add("Text = " + obj.Text);
                    nodeL.Nodes.Add("Link = " + obj.Url);
                    nodeL.Nodes.Add("Class = " + obj.ClassName);
                    //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                    link.Nodes.Add(nodeL);
                }
            }
            link.Expand();
            root.Nodes.Add(link);
            root.Expand();
            //TreeNode image = new TreeNode("Image");
            //foreach (WatiN.Core.Image obj in ie.Images)
            //{
            //    //if (obj.ClassName != null && obj.ClassName.ToString() != "")
            //    {
            //        TreeNode nodeL = new TreeNode(obj.Name);
            //        nodeL.Nodes.Add("Id = " + obj.Id);
            //        nodeL.Nodes.Add("Value = " + obj.Name);
            //        nodeL.Nodes.Add("Text = " + obj.Text);
            //        nodeL.Nodes.Add("Link = " + obj.Src);
            //        nodeL.Nodes.Add("Class = " + obj.ClassName);
            //        //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
            //        image.Nodes.Add(nodeL);
            //    }
            //}
            //image.Expand();
            //root.Nodes.Add(image);
            //root.Expand();
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            backgroundWorker1.RunWorkerAsync();
        }

        private void txtLink_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                
               backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                webBrowser1.Url = new Uri(txtLink.Text);
            }
            catch (Exception)
            {


            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //foreach (WatiN.Core.TextField obj in ie.TextFields)
            //{
            //    obj.Highlight(false);
            //}

            //foreach (WatiN.Core.Button obj in ie.Buttons)
            //{
            //    obj.Highlight(false);
            //}
            //foreach (WatiN.Core.Div obj in ie.Divs)
            //{
            //    obj.Highlight(false);
            //}\
            
            txtSelect.Text=e.Node.Text;
            string Value = e.Node.Text;
            if(Value.IndexOf(" = ")>=0)
            {
                Value = Value.Split('=')[1].Trim();
            }
            if (e.Node.FullPath.IndexOf("Div") >= 0)
            {
                if (txtSelect.Text.IndexOf("Name = ") >= 0)
                {
                    Div div = ie.Div(Find.ByName(Value));
                    if (div != null && div.Exists)
                    {
                        div.Highlight(true);
                    }
                }
                else
                {
                    Div div = ie.Div(Find.ById(Value));
                    if (div != null && div.Exists)
                    {
                        div.Highlight(true);
                    }
                }
            }
            else if (e.Node.FullPath.IndexOf("TextBox") >= 0)
            {
                TextField text = ie.TextField(Find.ByName(Value));
                if (text != null && text.Exists)
                {
                    text.Highlight(true);
                    text.Select();
                }
            }
            else if (e.Node.FullPath.IndexOf("Button") >= 0)
            {
                WatiN.Core.Button butt = ie.Button(Find.ByValue(Value));
                if (butt != null && butt.Exists)
                {
                    butt.Highlight(true);
                }
            }
            else if (e.Node.FullPath.IndexOf("Link") >= 0)
            {
                if (txtSelect.Text.IndexOf("Class = ") >= 0)
                {
                    WatiN.Core.Link link = ie.Link(Find.ByClass(Value));
                    if (link != null && link.Exists)
                    {
                        link.Highlight(true);
                    }
                }
                else
                {
                    WatiN.Core.Link link = ie.Link(Find.ByText(Value));
                    if (link != null && link.Exists)
                    {
                        link.Highlight(true);
                    }
                }
            }

        }

        private void hideContainerRight_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEditPage_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditPage.EditValue != null)
            {
                long ID = long.Parse(lookUpEditPage.EditValue.ToString());
                gridControl3.DataSource = WebStep.GetByIDWeb(ID);
                _LoadDSWebLink(lookUpEditPage.Text);
            }
        }
        int type = 0;
        private void bntProcess_Click(object sender, EventArgs e)
        {
            if (forum == null)
            {
                MessageBox.Show("Chưa chọn link Topic");
                return;
            }
            if (gridView3.FocusedRowHandle >= 0)
            {
                type = 0;
                proccessStep = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, colAction1).ToString();
                backgroundWorker2.RunWorkerAsync();
            }
        }
        WebLink forum;
        string proccessStep;
        string result;
        private void btnProcessAndNext_Click(object sender, EventArgs e)
        {
            if (forum == null)
            {
                MessageBox.Show("Chưa chọn link Topic");
                return;
            }
            if (gridView3.FocusedRowHandle >= 0)
            {
                type = 1;
                proccessStep = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, colAction1).ToString();
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            proccessStep = proccessStep.Replace("{UserName}", forum.UserName);
            proccessStep = proccessStep.Replace("{Password}", forum.Password);
            proccessStep = proccessStep.Replace("{Url}", forum.UrlPost);
            proccessStep = proccessStep.Replace("{IDTopic}", forum.IDTopic);
            result = MyCore.ProcessStep(proccessStep, ie);
           
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result != String.Empty)
            {
                MessageBox.Show(result, "Lỗi");
                return;
            }
            if (type == 1)
            {
                gridView3.ClearSelection();
                gridView3.FocusedRowHandle = gridView3.FocusedRowHandle + 1;
                gridView3.SelectRow(gridView3.FocusedRowHandle);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmWebLink frm = new frmWebLink(NumCode.UPEX);
            frm.ShowDialog();
            if (lookUpEditPage.EditValue!=null)
                _LoadDSWebLink(lookUpEditPage.Text);
        }
        private void _LoadDSWebLink(string Page)
        {
            int i = gridView1.TopRowIndex;
            int k = gridView1.FocusedRowHandle;
            gridControl1.DataSource = WebLink.GetByPage(Page);
            gridView1.FocusedRowHandle = k;
            gridView1.TopRowIndex = i;

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridView1.FocusedRowHandle >= 0)
                {
                    long ID = long.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colID).ToString());
                    forum = WebLink.Get(ID);
                }
            }
            catch (Exception)
            {

            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            gridView1_FocusedRowChanged(null, null);
        }

        private void btnClearCache_Click(object sender, EventArgs e)
        {
            try
            {
                ie.ClearCache();

            }
            catch { }
            try
            {
                ie.ClearCookies();
            }
            catch { }
        }

        private void hideContainerLeft_Click(object sender, EventArgs e)
        {

        }


       
       





    }
}
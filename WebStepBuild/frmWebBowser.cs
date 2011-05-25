using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WatiN.Core;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Native.Windows;
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
            dtSource = new DataTable();
            dtSource.Columns.Add("ID", typeof(long));
            dtSource.Columns.Add("Step", typeof(int));
            dtSource.Columns.Add("Action", typeof(string));
            dtSource.Columns.Add("Message", typeof(string));
            gridControl2.DataSource = dtSource;
        }
        private DataTable dtSource;
        private IE ie;
        private FireFox fire;
        WebPage webpage;
        WebPage webpageUp;
        public long pageID = -1;
        private DialogWatcher dialogWatcher;
        private void frmWebBowser_Load(object sender, EventArgs e)
        {
            Settings.WaitForCompleteTimeOut = 120000;
            Settings.AttachToBrowserTimeOut = 120000;
            webBrowser1.ScriptErrorsSuppressed = true;
            //IE.Settings.AutoStartDialogWatcher = false;
            WatiN.Core.Settings.AutoStartDialogWatcher = false;
            WatiN.Core.Settings.AutoCloseDialogs = true;
            ie = new IE(webBrowser1.ActiveXInstance);
            dialogWatcher = new DialogWatcher(new Window(this.Handle));
            dialogWatcher.CloseUnhandledDialogs = false;
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
            LoadControl();
            
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
                webpageUp = new WebPage();
                webpageUp.ID = ID;
                webpageUp = (WebPage)webpageUp.Get();
                if (webpageUp != null)
                {
                    txtUrl.Text = webpageUp.Page;
                    txtID.Text = webpageUp.ID.ToString();
                    dtSource = WebStep.GetByIDWeb(webpageUp.ID);
                    gridControl2.DataSource = dtSource;
                }
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
                btnProcessAndNext.Enabled = false;
                bntProcess.Enabled = false;
                type = 1;
                proccessStep = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, colAction1).ToString();
                backgroundWorker2.RunWorkerAsync();
            }
        }
        private int nextStep;
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                dialogWatcher.CloseUnhandledDialogs = true;
                if (proccessStep.IndexOf("Exists") < 0)
                {
                    proccessStep = proccessStep.Replace("{UserName}", forum.UserName);
                    proccessStep = proccessStep.Replace("{Password}", forum.Password);
                    proccessStep = proccessStep.Replace("{Url}", forum.UrlPost);
                    proccessStep = proccessStep.Replace("{IDTopic}", forum.IDTopic);

                    result = MyCore.ProcessStep(proccessStep, ie);
                }
                else
                {
                    try
                    {
                        type = 2;
                        result = String.Empty;
                        string[] a = proccessStep.Split('(');
                        string processType = a[0].Trim();
                        string processText = a[1].Trim(')');
                        string[] b = processText.Split('|');
                        string text = b[0].Trim();
                        int stepYes = int.Parse(b[1]);
                        int stepNo = int.Parse(b[2]);
                        if (MyCore.Exist(text, ie))
                        {
                            nextStep = stepYes - 1;
                        }
                        else
                        {
                            nextStep = stepNo - 1;
                        }
                    }
                    catch
                    {
                        type = 1;
                    }

                }
            }
            catch { }
           
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnProcessAndNext.Enabled = true;
            bntProcess.Enabled = true;
            dialogWatcher.CloseUnhandledDialogs = false;
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
            else if (type == 2)
            {
                gridView3.ClearSelection();
                gridView3.FocusedRowHandle = nextStep;
                gridView3.SelectRow(nextStep);
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
            gridView3.ClearSelection();
            gridView3.FocusedRowHandle = 0;
            gridView3.SelectRow(0);
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
            try
            {
                //webBrowser1.Dispose();
                //webBrowser1 = new WebBrowser();
                //webBrowser1.Name = "webBrowser1";
                //webBrowser1.Dock = DockStyle.Fill;
                //webBrowser1.ScriptErrorsSuppressed = true;
                //groupBox1.Controls.Add(webBrowser1);
                //ie = new IE(webBrowser1.ActiveXInstance);
            }
            catch { }
            try
            {
               
            }
            catch { }
        }

        private void hideContainerLeft_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Goto( {Url} )";
            txtMessage.Text = "Không mở được trang web";
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Wait( 1 )";
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Fill(TextBox[ Id : ]|{UserName})";
            txtMessage.Text = "Không tìm thấy text box Username";
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Fill(TextBox[ Id : ]|{Password})";
            txtMessage.Text = "Không tìm thấy text box Password";
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Click(Button[ Id : ])";
            txtMessage.Text = "Không tìm thấy button";
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Click(Link[ Id : ])";
            txtMessage.Text = "Không tìm thấy link";
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            txtAction.Text = "";
            txtMessage.Text = "";
        }

        private void bntAdd_Click(object sender, EventArgs e)
        {
            DataRow dtRow = dtSource.NewRow();
            dtRow["Action"] = txtAction.Text;
            dtRow["Message"] = txtMessage.Text;
            dtSource.Rows.Add(dtRow);
            CreateStep();
        }
        private void CreateStep()
        {
            int i = 1;
            foreach (DataRow dtRow in dtSource.Rows)
            {
                try
                {
                    dtRow["Step"] = i;
                    i++;
                }
                catch (Exception)
                {


                }

            }
        }

        private void bntInsert_Click(object sender, EventArgs e)
        {
            DataRow dtRow = dtSource.NewRow();
            dtRow["Action"] = txtAction.Text;
            dtRow["Message"] = txtMessage.Text;
            dtSource.Rows.InsertAt(dtRow, int.Parse(calcEditStep.Value.ToString()) - 1);
            CreateStep();
        }

        private void bntUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataRow dtRow in dtSource.Rows)
            {
                try
                {
                    if (dtRow["Step"].ToString() == txtStep.Text)
                    {
                        dtRow["Action"] = txtAction.Text;
                        dtRow["Message"] = txtMessage.Text;
                        break;
                    }
                }
                catch (Exception)
                {


                }
            }
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {
            gridView2.DeleteSelectedRows();
            CreateStep();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                txtAction.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, colAction).ToString();
                txtMessage.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, colMessage).ToString();
                txtStep.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, colStep).ToString();
            }
        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            gridView2_FocusedRowChanged(null, null);
        }

        private void bntNew_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtUrl.Text = "";
            txtAction.Text = "";
            txtStep.Text = "";
            txtMessage.Text = "";
            webpageUp = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUrl.Text == "")
            {
                MessageBox.Show("Nhập vào tên trang Web");
                return;
            }
            if (webpageUp == null)
            {
                webpageUp = new WebPage();
                webpageUp.Page = txtUrl.Text;
                long ID = webpageUp.Insert();
                txtID.Text = ID.ToString();
                foreach (DataRow dtRow in dtSource.Rows)
                {
                    try
                    {
                        WebStep webStep = new WebStep();
                        webStep.IDWeb = ID;
                        webStep.Step = long.Parse(dtRow["Step"].ToString());
                        webStep.Action = dtRow["Action"].ToString();
                        webStep.Message = dtRow["Message"].ToString();
                        webStep.Insert();

                    }
                    catch (Exception)
                    {


                    }
                }
                MessageBox.Show("Đã lưu thành công");
                lookUpEditPage_EditValueChanged(null, null);

            }
            else
            {

                webpageUp.Page = txtUrl.Text;
                webpageUp.Update();
                WebStep.DeleteByID(webpageUp.ID);
                foreach (DataRow dtRow in dtSource.Rows)
                {
                    try
                    {
                        WebStep webStep = new WebStep();
                        webStep.IDWeb = webpageUp.ID;
                        webStep.Step = long.Parse(dtRow["Step"].ToString());
                        webStep.Action = dtRow["Action"].ToString();
                        webStep.Message = dtRow["Message"].ToString();
                        webStep.Insert();

                    }
                    catch (Exception)
                    {


                    }
                }
                MessageBox.Show("Đã lưu thành công");
                lookUpEditPage_EditValueChanged(null, null);
               
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click_2(object sender, EventArgs e)
        {
            txtAction.Text = "Exists(TextBox[ Id : ]| | )";
        }
        private void CreateStepDefault()
        {
            dtSource.Rows.Clear();
            //1
            DataRow dtRow1 = dtSource.NewRow();
            dtRow1["Action"] = "Goto(  )";
            dtRow1["Message"] = "Không mở được trang web";
            dtSource.Rows.Add(dtRow1);
            //2
            DataRow dtRow2 = dtSource.NewRow();
            dtRow2["Action"] = "Wait( 1 )";
            dtRow2["Message"] = "";
            dtSource.Rows.Add(dtRow2);
            //3
            DataRow dtRow3 = dtSource.NewRow();
            dtRow3["Action"] = "Exists(TextBox[ Id : ]| 8 | 4 )";
            dtRow3["Message"] = "";
            dtSource.Rows.Add(dtRow3);
            //4
            DataRow dtRow4 = dtSource.NewRow();
            dtRow4["Action"] = "Fill(TextBox[ Id : ]|{UserName})";
            dtRow4["Message"] = "Không tìm thấy text box Username";
            dtSource.Rows.Add(dtRow4);
            //5
            DataRow dtRow5 = dtSource.NewRow();
            dtRow5["Action"] = "Fill(TextBox[ Id : ]|{Password})";
            dtRow5["Message"] = "Không tìm thấy text box Password";
            dtSource.Rows.Add(dtRow5);
            //6
            DataRow dtRow6 = dtSource.NewRow();
            dtRow6["Action"] = "Click(Button[ Id : ])";
            dtRow6["Message"] = "Không tìm thấy button Đăng nhập";
            dtSource.Rows.Add(dtRow6);
            //7
            DataRow dtRow7 = dtSource.NewRow();
            dtRow7["Action"] = "Wait( 1 )";
            dtRow7["Message"] = "";
            dtSource.Rows.Add(dtRow7);
            //8
            DataRow dtRow8 = dtSource.NewRow();
            dtRow8["Action"] = "Goto( {Url} )";
            dtRow8["Message"] = "Không tìm thấy link Up bài";
            dtSource.Rows.Add(dtRow8);
            //9
            DataRow dtRow9 = dtSource.NewRow();
            dtRow9["Action"] = "Wait( 1 )";
            dtRow9["Message"] = "";
            dtSource.Rows.Add(dtRow9);
            //10
            DataRow dtRow10 = dtSource.NewRow();
            dtRow10["Action"] = "Click(Button[ Id : ])";
            dtRow10["Message"] = "Không tìm thấy button Cập nhập";
            dtSource.Rows.Add(dtRow10);
            CreateStep();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            CreateStepDefault();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            LoadControl();
        }
        private void LoadControl()
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
                nodeA.Nodes.Add(obj.Name, "Name = " + obj.Name);
                nodeA.Nodes.Add(obj.ReadOnly.ToString(), "ReadOnly = " + obj.ReadOnly.ToString());
                nodeA.Nodes.Add(obj.Text, "Text = " + obj.Text);
                nodeA.Nodes.Add(obj.Value, "Value = " + obj.Value);
                textBox.Nodes.Add(nodeA);
            }
            root.Nodes.Add(textBox);
            textBox.Expand();
            TreeNode button = new TreeNode("Button");
            foreach (WatiN.Core.Button obj in ie.Buttons)
            {
                TreeNode nodeB = new TreeNode(obj.Value);
                nodeB.Nodes.Add(obj.Id, "Id = " + obj.Id);
                nodeB.Nodes.Add(obj.Text, "ClassName = " + obj.ClassName);
                nodeB.Nodes.Add(obj.Text, "Text = " + obj.Text);
                nodeB.Nodes.Add(obj.Value, "Value = " + obj.Value);
                //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                button.Nodes.Add(nodeB);
            }
            root.Nodes.Add(button);
            button.Expand();

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

            root.Nodes.Add(link);
            link.Expand();


            TreeNode check = new TreeNode("CheckBox");
            foreach (WatiN.Core.CheckBox obj in ie.CheckBoxes)
            {
                //if (obj.ClassName != null && obj.ClassName.ToString() != "")
                {
                    TreeNode nodeCB = new TreeNode(obj.Name);
                    nodeCB.Nodes.Add("Id = " + obj.Id);
                    nodeCB.Nodes.Add("Value = " + obj.Name);
                    nodeCB.Nodes.Add("Text = " + obj.Text);
                    nodeCB.Nodes.Add("Class = " + obj.ClassName);
                    //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                    check.Nodes.Add(nodeCB);
                }
            }
            root.Nodes.Add(check);
            check.Expand();

            //TreeNode lable = new TreeNode("Label");
            //foreach (WatiN.Core.Label obj in ie.Labels)
            //{
            //    //if (obj.ClassName != null && obj.ClassName.ToString() != "")
            //    {
            //        TreeNode nodeCB = new TreeNode(obj.Text);
            //        nodeCB.Nodes.Add("Id = " + obj.Id);
            //        nodeCB.Nodes.Add("Value = " + obj.Name);
            //        nodeCB.Nodes.Add("Text = " + obj.Text);
            //        nodeCB.Nodes.Add("Class = " + obj.ClassName);
            //        //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
            //        lable.Nodes.Add(nodeCB);
            //    }
            //}
            //root.Nodes.Add(lable);
            //lable.Expand();
            TreeNode div = new TreeNode("Div");
            foreach (WatiN.Core.Div obj in ie.Divs)
            {
                if (obj.Id != null && obj.Id.ToString() != "")
                {
                    TreeNode nodeD = new TreeNode(obj.Id);
                    nodeD.Nodes.Add(obj.Id, "Id = " + obj.Id);
                    nodeD.Nodes.Add(obj.Name, "Name = " + obj.Name);
                    nodeD.Nodes.Add(obj.ClassName, "ClassName = " + obj.ClassName);
                    //nodeD.Nodes.Add("Text = " + obj.Text);

                    //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                    div.Nodes.Add(nodeD);

                    TreeNode textBox1 = new TreeNode("TextBox");
                    foreach (WatiN.Core.TextField obj1 in obj.TextFields)
                    {
                        TreeNode nodeA1 = new TreeNode(obj1.Name);
                        nodeA1.Nodes.Add(obj1.Id, "Id = " + obj1.Id);
                        nodeA1.Nodes.Add(obj1.Name, "Name = " + obj1.Name);
                        nodeA1.Nodes.Add(obj1.ReadOnly.ToString(), "ReadOnly = " + obj1.ReadOnly.ToString());
                        nodeA1.Nodes.Add(obj1.Text, "Text = " + obj1.Text);
                        nodeA1.Nodes.Add(obj1.Value, "Value = " + obj1.Value);
                        textBox1.Nodes.Add(nodeA1);
                    }
                    nodeD.Nodes.Add(textBox1);
                    TreeNode button1 = new TreeNode("Button");
                    foreach (WatiN.Core.Button obj1 in obj.Buttons)
                    {
                        TreeNode nodeB1 = new TreeNode(obj1.Value);
                        nodeB1.Nodes.Add(obj1.Id, "Id = " + obj1.Id);
                        nodeB1.Nodes.Add(obj1.Text, "ClassName = " + obj1.ClassName);
                        nodeB1.Nodes.Add(obj1.Text, "Text = " + obj1.Text);
                        nodeB1.Nodes.Add(obj1.Value, "Value = " + obj1.Value);
                        //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                        button1.Nodes.Add(nodeB1);
                    }
                    nodeD.Nodes.Add(button1);
                    TreeNode link1 = new TreeNode("Link");
                    foreach (WatiN.Core.Link obj1 in obj.Links)
                    {
                        //if (obj.ClassName != null && obj.ClassName.ToString() != "")
                        {
                            TreeNode nodeL1 = new TreeNode(obj1.Text);
                            nodeL1.Nodes.Add("Id = " + obj1.Id);
                            nodeL1.Nodes.Add("Value = " + obj1.Name);
                            nodeL1.Nodes.Add("Text = " + obj1.Text);
                            nodeL1.Nodes.Add("Link = " + obj1.Url);
                            nodeL1.Nodes.Add("Class = " + obj1.ClassName);
                            //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                            link1.Nodes.Add(nodeL1);
                        }
                    }
                    nodeD.Nodes.Add(link1);


                }
            }
            root.Nodes.Add(div);
            div.Expand();

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

       
       





    }
}
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


namespace WebBrowser
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
        private void frmWebBowser_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            //IE.Settings.AutoStartDialogWatcher = false;
            WatiN.Core.Settings.AutoStartDialogWatcher = false;
            ie = new IE(webBrowser1.ActiveXInstance);
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
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
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

                txtSelect.Text = e.Node.Text;
                string Value = e.Node.Text;
                if (Value.IndexOf(" = ") >= 0)
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
                else if (e.Node.FullPath.IndexOf("Element") >= 0)
                {
                    if (txtSelect.Text.IndexOf("Id = ") >= 0)
                    {
                        WatiN.Core.Element link = ie.Element(Find.ById(Value));
                        if (link != null && link.Exists)
                        {
                            link.Highlight(true);
                        }
                    }
                    else
                    {
                        WatiN.Core.Element link = ie.Element(Find.ByText(Value));
                        if (link != null && link.Exists)
                        {
                            link.Highlight(true);
                        }
                    }
                }
            }
            catch { }

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Element elemen = ie.Element(Find.ByAlt("Submit"));
                //if (elemen.Exists)
                //{
                //    elemen.Click();
                //    ie.WaitForComplete();
                //}
                foreach (WatiN.Core.Form frm in ie.Forms)
                {
                    if (frm.GetAttributeValue("Action").IndexOf("do=login") > 0)
                    {
                        Settings.WaitForCompleteTimeOut = 1;
                        try
                        {
                            frm.Submit();
                        }
                        catch
                        { }
                        ie.WaitForComplete();
                        Settings.WaitForCompleteTimeOut = 30000;
                        break;
                    }
                }
                
            }
            catch { }
           // LoadControl();
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

            TreeNode textBoxBody = new TreeNode("Table");
            foreach (WatiN.Core.TableBody obj in ie.TableBodies)
            {
                TreeNode nodeA = new TreeNode(obj.Id);
                nodeA.Nodes.Add(obj.Id, "Id = " + obj.Id);
                nodeA.Nodes.Add(obj.Name, "Name = " + obj.Name);
                //nodeA.Nodes.Add(obj.ReadOnly.ToString(), "ReadOnly = " + obj.ReadOnly.ToString());
                nodeA.Nodes.Add(obj.Text, "Text = " + obj.Text);
                nodeA.Nodes.Add(obj.ClassName, "Value = " + obj.ClassName);
                textBoxBody.Nodes.Add(nodeA);
                TreeNode treerow = new TreeNode("Row");
                foreach (WatiN.Core.TableRow objrow in obj.TableRows)
                {
                    TreeNode row = new TreeNode(objrow.Id);
                    row.Nodes.Add(objrow.Id, "Id = " + objrow.Id);
                    row.Nodes.Add(objrow.Name, "Name = " + objrow.Name);
                    //nodeA.Nodes.Add(obj.ReadOnly.ToString(), "ReadOnly = " + obj.ReadOnly.ToString());
                    row.Nodes.Add(objrow.Text, "Text = " + objrow.Text);
                    row.Nodes.Add(objrow.ClassName, "Value = " + objrow.ClassName);
                    treerow.Nodes.Add(row);
                    TreeNode treecell = new TreeNode("Cell");
                    foreach (WatiN.Core.TableCell objcell in objrow.TableCells)
                    {
                        TreeNode cell = new TreeNode(objcell.Id);
                        cell.Nodes.Add(objcell.Id, "Id = " + objcell.Id);
                        cell.Nodes.Add(objcell.Name, "Name = " + objcell.Name);
                        //nodeA.Nodes.Add(obj.ReadOnly.ToString(), "ReadOnly = " + obj.ReadOnly.ToString());
                        cell.Nodes.Add(objcell.Text, "Text = " + objcell.Text);
                        cell.Nodes.Add(objcell.ClassName, "Value = " + objcell.ClassName);
                        treecell.Nodes.Add(cell);
                    }
                    row.Nodes.Add(treecell);
                }
                nodeA.Nodes.Add(treerow);
            }
            root.Nodes.Add(textBoxBody);
            textBoxBody.Expand();
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
                    //LoadDiv(obj, nodeD);


                }
            }
            root.Nodes.Add(div);
            div.Expand();

            TreeNode image = new TreeNode("Image");
            foreach (WatiN.Core.Image obj in ie.Images)
            {
                if (obj.Id != null && obj.Id.ToString() != "")
                {
                    TreeNode nodeL = new TreeNode(obj.Id);
                    nodeL.Nodes.Add("Id = " + obj.Id);
                    nodeL.Nodes.Add("Name = " + obj.Name);
                    nodeL.Nodes.Add("Text = " + obj.Text);
                    nodeL.Nodes.Add("Src = " + obj.Src);
                    nodeL.Nodes.Add("Class = " + obj.ClassName);
                    //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                    image.Nodes.Add(nodeL);
                }
            }
            image.Expand();
            root.Nodes.Add(image);
            root.Expand();
            TreeNode element = new TreeNode("Element");
            foreach (WatiN.Core.Element<Image> obj in ie.ElementsOfType<Image>())
            {
                //if (obj.ClassName != null && obj.ClassName.ToString() != "")
                {
                    TreeNode nodeL = new TreeNode(obj.GetValue("Alt"));
                    nodeL.Nodes.Add("Id = " + obj.Id);
                    nodeL.Nodes.Add("TagName = " + obj.TagName);
                    nodeL.Nodes.Add("Name = " + obj.Name);
                    nodeL.Nodes.Add("Text = " + obj.Text);
                    nodeL.Nodes.Add("Title = " + obj.Title);
                    nodeL.Nodes.Add("Tilte = " + obj.GetValue("Src"));
                    element.Nodes.Add(nodeL);
                }
            }
            element.Expand();
            root.Nodes.Add(element);
            TreeNode Form = new TreeNode("Form");
            foreach (WatiN.Core.Form obj in ie.Forms)
            {
                //if (obj.ClassName != null && obj.ClassName.ToString() != "")
                {
                    TreeNode nodeL = new TreeNode(obj.GetValue("Action"));
                    nodeL.Nodes.Add("Id = " + obj.Id);
                    nodeL.Nodes.Add("Name = " + obj.Name);
                    nodeL.Nodes.Add("Text = " + obj.Text);
                    nodeL.Nodes.Add("Title = " + obj.Title);
                    Form.Nodes.Add(nodeL);
                }
            }
            Form.Expand();
            root.Nodes.Add(Form);
            root.Expand();
        }
        private void LoadDiv(Div divobj,TreeNode node)
        {
            TreeNode div = new TreeNode("Div");
            foreach (WatiN.Core.Div obj in divobj.Divs)
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
                    LoadDiv(obj, nodeD);


                }
            }
            node.Nodes.Add(div);
        }
       





    }
}
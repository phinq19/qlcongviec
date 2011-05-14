using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WatiN.Core;


namespace NewProject
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
            txtLink.Text = webBrowser1.Url.ToString();
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(webBrowser1.Url.ToString());
            treeView1.Nodes.Add(root);
            TreeNode textBox = new TreeNode("TextBox");
            foreach (WatiN.Core.TextField obj in ie.TextFields)
            {
                TreeNode nodeA = new TreeNode(obj.Name);
                nodeA.Nodes.Add("Id = " + obj.Id);
                nodeA.Nodes.Add("Name = " + obj.Name);
                nodeA.Nodes.Add("ReadOnly = " + obj.ReadOnly.ToString());
                nodeA.Nodes.Add("Text = " + obj.Text);
                nodeA.Nodes.Add("Value = " + obj.Value);
                textBox.Nodes.Add(nodeA);
            }
            root.Nodes.Add(textBox);
            textBox.Expand();
            TreeNode button = new TreeNode("Button");
            foreach (WatiN.Core.Button obj in ie.Buttons)
            {
                TreeNode nodeB = new TreeNode(obj.Value);
                nodeB.Nodes.Add("Id = " + obj.Id);
                nodeB.Nodes.Add("ClassName = " + obj.ClassName);
                nodeB.Nodes.Add("Text = " + obj.Text);
                nodeB.Nodes.Add("Value = " + obj.Value);
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
                    nodeD.Nodes.Add("Id = " + obj.Id);
                    nodeD.Nodes.Add("Name = " + obj.Name);
                    nodeD.Nodes.Add("ClassName = " + obj.ClassName);
                    //nodeD.Nodes.Add("Text = " + obj.Text);

                    //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
                    div.Nodes.Add(nodeD);
                }
            }
            root.Nodes.Add(div);
            div.Expand();
            root.Expand();
            //TreeNode link = new TreeNode("Link");
            //foreach (WatiN.Core.Button obj in ie.d)
            //{
            //    TreeNode nodeB = new TreeNode(obj.Value);
            //    nodeB.Nodes.Add("Id = " + obj.Id);
            //    nodeB.Nodes.Add("ClassName = " + obj.ClassName);
            //    nodeB.Nodes.Add("Text = " + obj.Text);
            //    nodeB.Nodes.Add("Value = " + obj.Value);
            //    //nodeB.Nodes.Add("Tilte = " + obj.Title.ToString());
            //    button.Nodes.Add(nodeB);
            //}
            //root.Nodes.Add(button);
            
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

       
       





    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WorkLibrary;

namespace CreateWebStep
{
    public partial class frmCreateStep : DevExpress.XtraEditors.XtraForm
    {
        public frmCreateStep()
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
        public WebPage webPage;
        private void frmCreateStep_Load(object sender, EventArgs e)
        {
            if(webPage!=null)
            {
                txtUrl.Text = webPage.Page;
                txtID.Text = webPage.ID.ToString();
                dtSource= WebStep.GetByIDWeb(webPage.ID);
                gridControl2.DataSource = dtSource;
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(gridView2.FocusedRowHandle>=0)
            {
                txtAction.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, colAction).ToString();
                txtMessage.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, colMessage).ToString();
                txtStep.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, colStep).ToString();
            }
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {
            gridView2.DeleteSelectedRows();
            CreateStep();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAction.Text = "";
            txtMessage.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
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
                    if(dtRow["Step"].ToString()==txtStep.Text)
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

        private void gridView2_Click(object sender, EventArgs e)
        {
            gridView2_FocusedRowChanged(null, null);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtUrl.Text=="")
            {
                MessageBox.Show("Nhập vào tên trang Web");
                return;
            }
            if(webPage==null)
            {
                webPage=new WebPage();
                webPage.Page = txtUrl.Text;
                webPage.Type = NumCode.UPWEB;
                webPage.Active = 1;
                long ID = WebPage.Insert(webPage);
                txtID.Text = ID.ToString();
                foreach (DataRow dtRow in dtSource.Rows)
                {
                    try
                    {
                        WebStep webStep=new WebStep();
                        webStep.IDWeb = ID;
                        webStep.Step = long.Parse(dtRow["Step"].ToString());
                        webStep.Action=    dtRow["Action"].ToString();
                        webStep.Message=   dtRow["Message"].ToString();
                        WebStep.Insert(webStep);
                        
                    }
                    catch (Exception)
                    {


                    }
                }
                MessageBox.Show("Đã lưu thành công");
                DialogResult = DialogResult.OK;

            }
            else
            {
              
                webPage.Page = txtUrl.Text;
                WebPage.Update(webPage);
                WebStep.DeleteByID(webPage.ID);
                foreach (DataRow dtRow in dtSource.Rows)
                {
                    try
                    {
                        WebStep webStep = new WebStep();
                        webStep.IDWeb = webPage.ID;
                        webStep.Step = long.Parse(dtRow["Step"].ToString());
                        webStep.Action = dtRow["Action"].ToString();
                        webStep.Message = dtRow["Message"].ToString();
                        WebStep.Insert(webStep);

                    }
                    catch (Exception)
                    {


                    }
                }
                MessageBox.Show("Đã lưu thành công");
                DialogResult = DialogResult.OK;
            }
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
            dtRow3["Action"] = "Exists(TextBox[ Id : ]| 4 | 8 )";
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Goto( {Url} )";
            txtMessage.Text = "Không mở được trang web";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Wait( 1 )";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Fill(TextBox[ Id : ]|{UserName})";
            txtMessage.Text = "Không tìm thấy text box Username";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Fill(TextBox[ Id : ]|{Password})";
            txtMessage.Text = "Không tìm thấy text box Password";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Click(Button[ Id : ])";
            txtMessage.Text = "Không tìm thấy button";
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Click(Link[ Id : ])";
            txtMessage.Text = "Không tìm thấy link";
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            txtAction.Text = "Exists(TextBox[ Id : ]| | )";
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            CreateStepDefault();
        }
    }
}
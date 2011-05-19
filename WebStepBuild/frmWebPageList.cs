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
    public partial class frmWebPageList : DevExpress.XtraEditors.XtraForm
    {
        private string _Type;
        private WebPage webPage;
        public frmWebPageList()
        {
            InitializeComponent();
            webPage=new WebPage();
        }
       
        private void _InitData()
        {
            
            _LoadDSWebLink();
                      
            //grid_KhachHang.DataSource = ds.Tables[0];
        }

        private void _LoadDSWebLink()
        {
            int i = gridView1.TopRowIndex;
            int k = gridView1.FocusedRowHandle;
            gridControl1.DataSource = webPage.GetAllToTable();
            gridView1.FocusedRowHandle = k;
            gridView1.TopRowIndex = i;
            
        }


        private void btnThemMoi_Click(object sender, EventArgs e)
        {

            frmCreateStep frm = new frmCreateStep();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _LoadDSWebLink();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            _SelectUpdateWebLink();
        }

        private void _SelectUpdateWebLink()
        {
            try
            {
                int k = gridView1.SelectedRowsCount;
                if (k > 0)
                {
                    int RowHandle = gridView1.GetSelectedRows()[k - 1];
                    if (RowHandle >= 0)
                    {
                        long iDDT = long.Parse(gridView1.GetRowCellValue(RowHandle, colID).ToString());
                        webPage.ID = iDDT;
                        WebPage web = (WebPage)webPage.Get();
                        frmCreateStep frm = new frmCreateStep();
                        frm.webPage = web;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                _LoadDSWebLink();
                            }
                    }
                }
            }
            catch { }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            _SelectUpdateWebLink();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int k = gridView1.SelectedRowsCount;
                if (k > 0)
                {
                    if (MessageBox.Show("Bạn muốn xóa WebPage này?", "Thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int[] arrSelect = gridView1.GetSelectedRows();
                        for (int i = 0; i < k; i++)
                        {

                            long MaDT = long.Parse(gridView1.GetRowCellValue(arrSelect[i], colID).ToString());
                            webPage.ID = MaDT;
                            webPage.Delete();
                            WebStep.DeleteByID(MaDT);
                            MessageBox.Show("Đã xóa thành công");
                        }
                    }

                }
            }
            catch { }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
        }

        private void frmWebLinkList_Load(object sender, EventArgs e)
        {
           _InitData();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if(gridView1.FocusedRowHandle>=0)
            {
                long ID = long.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colID).ToString());
                gridControl2.DataSource = WebStep.GetByIDWeb(ID);
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
       





    }
}
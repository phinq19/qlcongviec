using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WorkLibrary;

namespace NewProject
{
    public partial class frmWebLinkList : DevExpress.XtraEditors.XtraForm
    {
        private string _Type;
        private string _TypePage;
        public frmWebLinkList(string type,string typepage)
        {
            InitializeComponent();
            _Type = type;
            _TypePage = typepage;
        }
       
        private void _InitData()
        {
            _LoadNhomWebLink();
            _LoadDSWebLink();
                      
            //grid_KhachHang.DataSource = ds.Tables[0];
        }

        private void _LoadDSWebLink()
        {
            int i = gridView1.TopRowIndex;
            int k = gridView1.FocusedRowHandle;
            grid_KhachHang.DataSource = WebLink.GetByType(_Type);
            gridView1.FocusedRowHandle = k;
            gridView1.TopRowIndex = i;
            
        }

        private void _LoadNhomWebLink()
        {
            DataTable NhomDT = CodeType.GetByGroup(NumCode.WEB);
            ItemLookUp_Nhom.DisplayMember = "Name";
            ItemLookUp_Nhom.ValueMember = "ID";
            ItemLookUp_Nhom.DataSource = NhomDT;
        }


        private void btnThemMoi_Click(object sender, EventArgs e)
        {

            frmWebLink frm = new frmWebLink(_Type,_TypePage);
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
                        WebUp temp = WebUp.Get(iDDT);
                        if (temp != null)
                        {
                            frmWebLink frm = new frmWebLink(_Type,_TypePage);
                            frm._cus = temp;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                _LoadDSWebLink();
                            }
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

                    int[] arrSelect = gridView1.GetSelectedRows();
                    for (int i = 0; i < k; i++)
                    {
                        long MaDT = long.Parse(gridView1.GetRowCellValue(arrSelect[i], colID).ToString());
                        WebUp temp = WebUp.Get(MaDT);
                        if (temp != null)
                        {
                            if (WebUp.Delete(temp.ID))
                            {
                                _LoadDSWebLink();
                            }
                            else
                            {
                                MessageBox.Show("Không thể link đang được sử dụng");
                            }
                        }
                    }

                }
            }
            catch { }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            _LoadDSWebLink();
        }

        private void frmWebLinkList_Load(object sender, EventArgs e)
        {
            _InitData();
            if(_Type=="POST")
            {
                colTopic.Visible = false;
            }
        }
       





    }
}
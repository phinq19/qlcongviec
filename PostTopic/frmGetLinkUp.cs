using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NewProject
{
    public partial class frmGetLinkUp : DevExpress.XtraEditors.XtraForm
    {
        private string _Subject;
        private DataTable _dtTable;
        private DataTable _dtLogEntry;
        public frmGetLinkUp(string Subject,DataTable dtTable)
        {
            InitializeComponent();
            _Subject = Subject;
            _dtLogEntry = dtTable;
            txtTopic.Text = _Subject;
        }
       
        private void _InitData()
        {
            _LoadNhomWebLink();
            _dtTable=new DataTable();
            _dtTable.Columns.Add("ID", typeof(long));
            _dtTable.Columns.Add("Url", typeof(string));
            _dtTable.Columns.Add("UrlPost", typeof(string));
            _dtTable.Columns.Add("UserName", typeof(string));
            _dtTable.Columns.Add("Password", typeof(string));
            _dtTable.Columns.Add("Topic", typeof(string));
            _dtTable.Columns.Add("Group", typeof(int));
            _dtTable.Columns.Add("Note", typeof(string));
            _dtTable.Columns.Add("Type", typeof(string));
            foreach (DataRow dtRow in _dtLogEntry.Rows)
            {
                WebLink webLink = WebLink.Get(long.Parse(dtRow["ID"].ToString()));
                if(webLink!=null)
                {
                    DataRow dataRow = _dtTable.NewRow();
                    dataRow["ID"] = webLink.ID;
                    dataRow["Url"] = webLink.Url;
                    dataRow["UrlPost"] = dtRow["LinkUp"];
                    dataRow["Topic"] = _Subject;
                    dataRow["UserName"] = webLink.UserName;
                    dataRow["Password"] = webLink.Password;
                    dataRow["Group"] = webLink.Group;
                    dataRow["Tupe"] = NumCode.UP;
                    _dtTable.Rows.Add(dataRow);
                }
            }
            grid_KhachHang.DataSource = _dtTable;
        }

        private void _LoadNhomWebLink()
        {
            DataTable NhomDT = CodeType.GetByGroup(NumCode.WEB);
            ItemLookUp_Nhom.DisplayMember = "Name";
            ItemLookUp_Nhom.ValueMember = "ID";
            ItemLookUp_Nhom.DataSource = NhomDT;
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in _dtTable.Rows)
            {
                WebLink webLink=new WebLink();
                webLink.Url = dataRow["Url"].ToString();
                webLink.UrlPost = dataRow["UrlPost"].ToString();
                webLink.Topic = dataRow["Topic"].ToString();
                webLink.Note = dataRow["Note"].ToString();
                webLink.UserName = dataRow["UserName"].ToString();
                webLink.Password = dataRow["Password"].ToString();
                webLink.Type = NumCode.UP;
                webLink.Group = int.Parse(dataRow["Group"].ToString());
                WebLink.Insert(webLink);
            }
            MessageBox.Show("Lưu link up thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
             this.Close();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                gridView1.DeleteSelectedRows();
                
            }
            catch { }
        }

       
        private void frmWebLinkList_Load(object sender, EventArgs e)
        {
            _InitData();
        }

        private void btnSetAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in _dtTable.Rows)
            {
                if(txtTopic.Text.Trim()!="")
                    dataRow["Topic"]=txtTopic.Text;
                if(txtNote.Text.Trim()!="")
                    dataRow["Note"]=txtNote.Text;
            }
        }
       





    }
}
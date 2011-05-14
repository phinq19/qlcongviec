using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace NewProject
{
    public partial class frmFieldSetting : Form
    {
        Status status;
        FieldSetting obj;
        private String Type;
        public frmFieldSetting(string type)
        {
            InitializeComponent();
            Type = type;
        }

        void LoadCombo()
        {
            cboField.DisplayMember = "Field";
            cboField.ValueMember = "Id";
            cboField.DataSource = FieldSetting.GetFieldForum();
        }

        void LoadData()
        {
            if (cboField.Items.Count > 0)
            {
                gridControl1.DataSource = FieldSetting.GetByFieldDataTable(cboField.Text,Type);
            }
        }

        void FormStatus()
        {
            switch (status)
            {
                case Status.Add:
                    btnAdd.Text = "Add";
                    btnEdit.Text = "Edit";
                    btnDelete.Enabled = true;
                    cboField.Enabled = true;
                    break;
                case Status.Edit:
                    btnAdd.Text = "Save";
                    btnEdit.Text = "Cancel";
                    btnDelete.Enabled = false;
                    cboField.Enabled = false;
                    break;
            }
        }

        bool CheckData()
        {
            if (string.IsNullOrEmpty(cboControl.Text.Trim()))
                return false;
            if (string.IsNullOrEmpty(cboAttribute.Text.Trim()))
                return false;
            if (string.IsNullOrEmpty(txtValue.Text.Trim()))
                return false;
            return true;
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            obj = new FieldSetting();
            status = Status.Add;

            LoadCombo();
            LoadData();

            cboAttribute.SelectedIndex = 0;
            cboControl.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckData() && cboField.Items.Count > 0)
            {
                switch (status)
                {
                    case Status.Add:
                        obj.Field = cboField.Text.Trim();
                        obj.Control = cboControl.Text.Trim();
                        obj.Attribute = cboAttribute.Text.Trim();
                        obj.Value = txtValue.Text.Trim();
                        obj.Type = Type;
                        FieldSetting.Insert(obj);

                        LoadData();
                        break;
                    case Status.Edit:
                        obj.Control = cboControl.Text.Trim();
                        obj.Attribute = cboAttribute.Text.Trim();
                        obj.Value = txtValue.Text.Trim();
                        obj.Type = Type;
                        FieldSetting.Update(obj);
                        LoadData();

                        status = Status.Add;
                        break;
                }
                FormStatus();
                txtValue.Text = "";
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (status != Status.Edit)
            {
                if (gridView1.RowCount > 0)
                {
                    int index = gridView1.FocusedRowHandle;
                    if (index >=0)
                    {
                        obj.ID = int.Parse(gridView1.GetRowCellValue(index,colID).ToString());
                        obj.Field = gridView1.GetRowCellValue(index, colField).ToString();
                        obj.Control = gridView1.GetRowCellValue(index, colControl).ToString();
                        obj.Attribute = gridView1.GetRowCellValue(index, colAttribute).ToString();
                        obj.Value = gridView1.GetRowCellValue(index, colValue).ToString();

                        cboControl.Text = obj.Control;
                        cboAttribute.Text = obj.Attribute;
                        txtValue.Text = obj.Value;
                        status = Status.Edit;
                    }
                }
            }
            else
            {
                status = Status.Add;
            }
            FormStatus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                int[] k=gridView1.GetSelectedRows();
                for (int i = 0; i < k.Length; i++)
                {
                    FieldSetting.Delete(int.Parse(gridView1.GetRowCellValue(k[i],colID).ToString()));
                }
                LoadData();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboField.SelectedIndex > -1)
            {
                LoadData();
            }
        }
    }
}

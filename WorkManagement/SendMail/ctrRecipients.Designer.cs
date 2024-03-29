namespace NewProject
{
    partial class ctrRecipients
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTen1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenGoi1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhom1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkEditNhom = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ItemLookUp_Nhom = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTenGoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDTNguoiLH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhomHH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTinhTrang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.ItemLookUp_DVT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.btnGet = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkEditNhom)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemLookUp_Nhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemLookUp_DVT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(6, 15);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkEditNhom});
            this.gridControl1.Size = new System.Drawing.Size(380, 325);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID1,
            this.colHo1,
            this.colTen1,
            this.colTenGoi1,
            this.colEmail1,
            this.colNhom1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.IndicatorWidth = 50;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNhom1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID1
            // 
            this.colID1.Caption = "ID";
            this.colID1.FieldName = "ID";
            this.colID1.Name = "colID1";
            // 
            // colHo1
            // 
            this.colHo1.AppearanceHeader.Options.UseTextOptions = true;
            this.colHo1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHo1.Caption = "Họ";
            this.colHo1.FieldName = "LastName";
            this.colHo1.Name = "colHo1";
            this.colHo1.OptionsColumn.AllowFocus = false;
            this.colHo1.Visible = true;
            this.colHo1.VisibleIndex = 0;
            this.colHo1.Width = 71;
            // 
            // colTen1
            // 
            this.colTen1.AppearanceHeader.Options.UseTextOptions = true;
            this.colTen1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTen1.Caption = "Tên";
            this.colTen1.FieldName = "FirstName";
            this.colTen1.Name = "colTen1";
            this.colTen1.OptionsColumn.AllowFocus = false;
            this.colTen1.Visible = true;
            this.colTen1.VisibleIndex = 1;
            this.colTen1.Width = 43;
            // 
            // colTenGoi1
            // 
            this.colTenGoi1.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenGoi1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenGoi1.Caption = "Tên gọi";
            this.colTenGoi1.FieldName = "CallName";
            this.colTenGoi1.Name = "colTenGoi1";
            this.colTenGoi1.OptionsColumn.AllowFocus = false;
            this.colTenGoi1.Visible = true;
            this.colTenGoi1.VisibleIndex = 2;
            this.colTenGoi1.Width = 60;
            // 
            // colEmail1
            // 
            this.colEmail1.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmail1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmail1.Caption = "Email";
            this.colEmail1.FieldName = "Email";
            this.colEmail1.Name = "colEmail1";
            this.colEmail1.OptionsColumn.AllowFocus = false;
            this.colEmail1.Visible = true;
            this.colEmail1.VisibleIndex = 3;
            this.colEmail1.Width = 123;
            // 
            // colNhom1
            // 
            this.colNhom1.AppearanceHeader.Options.UseTextOptions = true;
            this.colNhom1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNhom1.Caption = "Nhóm";
            this.colNhom1.ColumnEdit = this.lkEditNhom;
            this.colNhom1.FieldName = "Type";
            this.colNhom1.Name = "colNhom1";
            this.colNhom1.Visible = true;
            this.colNhom1.VisibleIndex = 4;
            // 
            // lkEditNhom
            // 
            this.lkEditNhom.AutoHeight = false;
            this.lkEditNhom.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkEditNhom.Name = "lkEditNhom";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControl1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 346);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recipients";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.gridControl2);
            this.groupBox2.Location = new System.Drawing.Point(462, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 346);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Customer List";
            // 
            // gridControl2
            // 
            this.gridControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl2.EmbeddedNavigator.Name = "";
            this.gridControl2.Location = new System.Drawing.Point(6, 15);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemLookUp_Nhom,
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2,
            this.ItemLookUp_DVT,
            this.repositoryItemCalcEdit1,
            this.repositoryItemCheckEdit1});
            this.gridControl2.Size = new System.Drawing.Size(422, 325);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMa,
            this.colHo,
            this.colNhom,
            this.colTenGoi,
            this.coDiaChi,
            this.colDienThoai,
            this.colEmail,
            this.colTen,
            this.colDTNguoiLH,
            this.colNhomHH,
            this.colGhiChu,
            this.colTinhTrang,
            this.colID});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.GroupCount = 1;
            this.gridView2.GroupPanelText = "<Kéo cột để nhóm đối tác>";
            this.gridView2.IndicatorWidth = 50;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNhom, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colMa
            // 
            this.colMa.AppearanceHeader.Options.UseTextOptions = true;
            this.colMa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMa.Caption = "Mã";
            this.colMa.FieldName = "Code";
            this.colMa.Name = "colMa";
            this.colMa.OptionsColumn.AllowFocus = false;
            this.colMa.Visible = true;
            this.colMa.VisibleIndex = 0;
            this.colMa.Width = 55;
            // 
            // colHo
            // 
            this.colHo.AppearanceHeader.Options.UseTextOptions = true;
            this.colHo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHo.Caption = "Họ";
            this.colHo.FieldName = "LastName";
            this.colHo.Name = "colHo";
            this.colHo.OptionsColumn.AllowFocus = false;
            this.colHo.Visible = true;
            this.colHo.VisibleIndex = 1;
            this.colHo.Width = 62;
            // 
            // colNhom
            // 
            this.colNhom.Caption = "Nhóm";
            this.colNhom.ColumnEdit = this.ItemLookUp_Nhom;
            this.colNhom.FieldName = "Type";
            this.colNhom.Name = "colNhom";
            this.colNhom.Width = 52;
            // 
            // ItemLookUp_Nhom
            // 
            this.ItemLookUp_Nhom.AutoHeight = false;
            this.ItemLookUp_Nhom.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ItemLookUp_Nhom.Name = "ItemLookUp_Nhom";
            // 
            // colTenGoi
            // 
            this.colTenGoi.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenGoi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenGoi.Caption = "Tên Gọi";
            this.colTenGoi.FieldName = "CallName";
            this.colTenGoi.Name = "colTenGoi";
            this.colTenGoi.OptionsColumn.AllowFocus = false;
            this.colTenGoi.Visible = true;
            this.colTenGoi.VisibleIndex = 3;
            this.colTenGoi.Width = 57;
            // 
            // coDiaChi
            // 
            this.coDiaChi.AppearanceHeader.Options.UseTextOptions = true;
            this.coDiaChi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coDiaChi.Caption = "Địa chỉ";
            this.coDiaChi.FieldName = "Address";
            this.coDiaChi.Name = "coDiaChi";
            this.coDiaChi.OptionsColumn.AllowFocus = false;
            this.coDiaChi.Width = 166;
            // 
            // colDienThoai
            // 
            this.colDienThoai.AppearanceHeader.Options.UseTextOptions = true;
            this.colDienThoai.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDienThoai.Caption = "Điện thoại";
            this.colDienThoai.FieldName = "Phone";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.OptionsColumn.AllowFocus = false;
            this.colDienThoai.Width = 65;
            // 
            // colEmail
            // 
            this.colEmail.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmail.Caption = "Email";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.OptionsColumn.AllowFocus = false;
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 4;
            this.colEmail.Width = 84;
            // 
            // colTen
            // 
            this.colTen.AppearanceHeader.Options.UseTextOptions = true;
            this.colTen.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTen.Caption = "Tên";
            this.colTen.FieldName = "FirstName";
            this.colTen.Name = "colTen";
            this.colTen.OptionsColumn.AllowFocus = false;
            this.colTen.Visible = true;
            this.colTen.VisibleIndex = 2;
            this.colTen.Width = 38;
            // 
            // colDTNguoiLH
            // 
            this.colDTNguoiLH.AppearanceHeader.Options.UseTextOptions = true;
            this.colDTNguoiLH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDTNguoiLH.Caption = "Fax";
            this.colDTNguoiLH.FieldName = "Fax";
            this.colDTNguoiLH.Name = "colDTNguoiLH";
            this.colDTNguoiLH.OptionsColumn.AllowFocus = false;
            this.colDTNguoiLH.Width = 87;
            // 
            // colNhomHH
            // 
            this.colNhomHH.Caption = "Các nhóm SP";
            this.colNhomHH.FieldName = "NhomLoaiHH";
            this.colNhomHH.Name = "colNhomHH";
            this.colNhomHH.Width = 114;
            // 
            // colGhiChu
            // 
            this.colGhiChu.AppearanceHeader.Options.UseTextOptions = true;
            this.colGhiChu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGhiChu.Caption = "Ghi chú";
            this.colGhiChu.FieldName = "Note";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.OptionsColumn.AllowFocus = false;
            this.colGhiChu.Visible = true;
            this.colGhiChu.VisibleIndex = 5;
            this.colGhiChu.Width = 90;
            // 
            // colTinhTrang
            // 
            this.colTinhTrang.Caption = "Ẩn";
            this.colTinhTrang.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colTinhTrang.FieldName = "TinhTrang";
            this.colTinhTrang.Name = "colTinhTrang";
            this.colTinhTrang.Width = 25;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = false;
            this.repositoryItemCheckEdit1.ValueUnchecked = true;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Width = 23;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // ItemLookUp_DVT
            // 
            this.ItemLookUp_DVT.AutoHeight = false;
            this.ItemLookUp_DVT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ItemLookUp_DVT.Name = "ItemLookUp_DVT";
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(403, 112);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(53, 23);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "<";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.Location = new System.Drawing.Point(403, 141);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(53, 23);
            this.btnGetAll.TabIndex = 4;
            this.btnGetAll.Text = "<<";
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(403, 260);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(53, 23);
            this.btnClearAll.TabIndex = 6;
            this.btnClearAll.Text = ">>";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(403, 231);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(53, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = ">";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ctrRecipients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnGetAll);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrRecipients";
            this.Size = new System.Drawing.Size(899, 352);
            this.Load += new System.EventHandler(this.ctrRecipients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkEditNhom)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemLookUp_Nhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemLookUp_DVT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.Columns.GridColumn colHo1;
        private DevExpress.XtraGrid.Columns.GridColumn colTen1;
        private DevExpress.XtraGrid.Columns.GridColumn colTenGoi1;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnGet;
        private DevExpress.XtraEditors.SimpleButton btnGetAll;
        private DevExpress.XtraEditors.SimpleButton btnClearAll;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colMa;
        private DevExpress.XtraGrid.Columns.GridColumn colHo;
        private DevExpress.XtraGrid.Columns.GridColumn colNhom;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ItemLookUp_Nhom;
        private DevExpress.XtraGrid.Columns.GridColumn colTenGoi;
        private DevExpress.XtraGrid.Columns.GridColumn coDiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colTen;
        private DevExpress.XtraGrid.Columns.GridColumn colDTNguoiLH;
        private DevExpress.XtraGrid.Columns.GridColumn colNhomHH;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn colTinhTrang;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ItemLookUp_DVT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colID1;
        private DevExpress.XtraGrid.Columns.GridColumn colNhom1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkEditNhom;

    }
}

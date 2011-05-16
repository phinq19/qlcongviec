namespace NewProject
{
    partial class ctrUpTopic
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
            this.btnContent = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSender = new DevExpress.XtraEditors.SimpleButton();
            this.btnResult = new DevExpress.XtraEditors.SimpleButton();
            this.btnSendmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnRecipients = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ctrSelectLink1 = new NewProject.ctrSelectLink();
            this.ctrPost1 = new NewProject.ctrPost();
            this.ctrContentTopic1 = new NewProject.ctrContentTopic();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnContent
            // 
            this.btnContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContent.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnContent.Appearance.Options.UseFont = true;
            this.btnContent.Location = new System.Drawing.Point(3, 54);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(147, 32);
            this.btnContent.TabIndex = 1;
            this.btnContent.Text = "Content";
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.btnSender);
            this.groupControl1.Controls.Add(this.btnResult);
            this.groupControl1.Controls.Add(this.btnSendmail);
            this.groupControl1.Controls.Add(this.btnPreview);
            this.groupControl1.Controls.Add(this.btnRecipients);
            this.groupControl1.Controls.Add(this.btnContent);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(152, 409);
            this.groupControl1.TabIndex = 2;
            // 
            // btnSender
            // 
            this.btnSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSender.Location = new System.Drawing.Point(2, 255);
            this.btnSender.Name = "btnSender";
            this.btnSender.Size = new System.Drawing.Size(147, 32);
            this.btnSender.TabIndex = 6;
            this.btnSender.Text = "Sender";
            this.btnSender.Visible = false;
            this.btnSender.Click += new System.EventHandler(this.btnSender_Click);
            // 
            // btnResult
            // 
            this.btnResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResult.Location = new System.Drawing.Point(2, 217);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(147, 32);
            this.btnResult.TabIndex = 5;
            this.btnResult.Text = "Result";
            this.btnResult.Visible = false;
            // 
            // btnSendmail
            // 
            this.btnSendmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendmail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSendmail.Appearance.Options.UseFont = true;
            this.btnSendmail.Location = new System.Drawing.Point(3, 86);
            this.btnSendmail.Name = "btnSendmail";
            this.btnSendmail.Size = new System.Drawing.Size(147, 32);
            this.btnSendmail.TabIndex = 4;
            this.btnSendmail.Text = "Up";
            this.btnSendmail.Click += new System.EventHandler(this.btnSendmail_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(2, 293);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(147, 32);
            this.btnPreview.TabIndex = 3;
            this.btnPreview.Text = "Preview";
            this.btnPreview.Visible = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnRecipients
            // 
            this.btnRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecipients.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRecipients.Appearance.Options.UseFont = true;
            this.btnRecipients.Location = new System.Drawing.Point(3, 23);
            this.btnRecipients.Name = "btnRecipients";
            this.btnRecipients.Size = new System.Drawing.Size(147, 32);
            this.btnRecipients.TabIndex = 2;
            this.btnRecipients.Text = "Select Link";
            this.btnRecipients.Click += new System.EventHandler(this.btnRecipients_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ctrSelectLink1);
            this.panelControl1.Controls.Add(this.ctrPost1);
            this.panelControl1.Controls.Add(this.ctrContentTopic1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 20);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(740, 387);
            this.panelControl1.TabIndex = 4;
            // 
            // ctrSelectLink1
            // 
            this.ctrSelectLink1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrSelectLink1.Location = new System.Drawing.Point(2, 2);
            this.ctrSelectLink1.Name = "ctrSelectLink1";
            this.ctrSelectLink1.Size = new System.Drawing.Size(736, 383);
            this.ctrSelectLink1.TabIndex = 3;
            // 
            // ctrPost1
            // 
            this.ctrPost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrPost1.Location = new System.Drawing.Point(2, 2);
            this.ctrPost1.Name = "ctrPost1";
            this.ctrPost1.Size = new System.Drawing.Size(736, 383);
            this.ctrPost1.TabIndex = 2;
            // 
            // ctrContentTopic1
            // 
            this.ctrContentTopic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrContentTopic1.Location = new System.Drawing.Point(2, 2);
            this.ctrContentTopic1.Name = "ctrContentTopic1";
            this.ctrContentTopic1.Size = new System.Drawing.Size(736, 383);
            this.ctrContentTopic1.TabIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.panelControl1);
            this.groupControl2.Location = new System.Drawing.Point(158, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(744, 409);
            this.groupControl2.TabIndex = 5;
            // 
            // ctrUpTopic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "ctrUpTopic";
            this.Size = new System.Drawing.Size(905, 415);
            this.Load += new System.EventHandler(this.ctrSendmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnContent;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnRecipients;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnSendmail;
        private DevExpress.XtraEditors.SimpleButton btnResult;
        private DevExpress.XtraEditors.SimpleButton btnSender;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private ctrContentTopic ctrContentTopic1;
        private ctrPost ctrPost1;
        private ctrSelectLink ctrSelectLink1;

    }
}

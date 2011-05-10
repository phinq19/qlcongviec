namespace NewProject
{
    partial class ctrSendmail
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
            this.ctrSend1 = new NewProject.ctrSend();
            this.ctrPreview1 = new NewProject.ctrPreview();
            this.ctrRecipients1 = new NewProject.ctrRecipients();
            this.ctrSender1 = new NewProject.ctrSender();
            this.ctrContent1 = new NewProject.ctrContent();
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
            this.btnContent.Location = new System.Drawing.Point(2, 86);
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
            this.btnSender.Location = new System.Drawing.Point(2, 23);
            this.btnSender.Name = "btnSender";
            this.btnSender.Size = new System.Drawing.Size(147, 32);
            this.btnSender.TabIndex = 6;
            this.btnSender.Text = "Sender";
            this.btnSender.Click += new System.EventHandler(this.btnSender_Click);
            // 
            // btnResult
            // 
            this.btnResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResult.Location = new System.Drawing.Point(2, 179);
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
            this.btnSendmail.Location = new System.Drawing.Point(2, 148);
            this.btnSendmail.Name = "btnSendmail";
            this.btnSendmail.Size = new System.Drawing.Size(147, 32);
            this.btnSendmail.TabIndex = 4;
            this.btnSendmail.Text = "Send Mail";
            this.btnSendmail.Click += new System.EventHandler(this.btnSendmail_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(2, 117);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(147, 32);
            this.btnPreview.TabIndex = 3;
            this.btnPreview.Text = "Preview";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnRecipients
            // 
            this.btnRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecipients.Location = new System.Drawing.Point(2, 55);
            this.btnRecipients.Name = "btnRecipients";
            this.btnRecipients.Size = new System.Drawing.Size(147, 32);
            this.btnRecipients.TabIndex = 2;
            this.btnRecipients.Text = "Recipients";
            this.btnRecipients.Click += new System.EventHandler(this.btnRecipients_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ctrSend1);
            this.panelControl1.Controls.Add(this.ctrPreview1);
            this.panelControl1.Controls.Add(this.ctrRecipients1);
            this.panelControl1.Controls.Add(this.ctrSender1);
            this.panelControl1.Controls.Add(this.ctrContent1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 20);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(740, 387);
            this.panelControl1.TabIndex = 4;
            // 
            // ctrSend1
            // 
            this.ctrSend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrSend1.Location = new System.Drawing.Point(2, 2);
            this.ctrSend1.Name = "ctrSend1";
            this.ctrSend1.Size = new System.Drawing.Size(736, 383);
            this.ctrSend1.TabIndex = 7;
            // 
            // ctrPreview1
            // 
            this.ctrPreview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrPreview1.Location = new System.Drawing.Point(2, 2);
            this.ctrPreview1.Name = "ctrPreview1";
            this.ctrPreview1.Size = new System.Drawing.Size(736, 383);
            this.ctrPreview1.TabIndex = 6;
            // 
            // ctrRecipients1
            // 
            this.ctrRecipients1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrRecipients1.Location = new System.Drawing.Point(2, 2);
            this.ctrRecipients1.Name = "ctrRecipients1";
            this.ctrRecipients1.Size = new System.Drawing.Size(736, 383);
            this.ctrRecipients1.TabIndex = 5;
            // 
            // ctrSender1
            // 
            this.ctrSender1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrSender1.Location = new System.Drawing.Point(2, 2);
            this.ctrSender1.Name = "ctrSender1";
            this.ctrSender1.Size = new System.Drawing.Size(736, 383);
            this.ctrSender1.TabIndex = 4;
            // 
            // ctrContent1
            // 
            this.ctrContent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrContent1.Location = new System.Drawing.Point(2, 2);
            this.ctrContent1.Name = "ctrContent1";
            this.ctrContent1.Size = new System.Drawing.Size(736, 383);
            this.ctrContent1.TabIndex = 3;
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
            // ctrSendmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "ctrSendmail";
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
        private ctrContent ctrContent1;
        private DevExpress.XtraEditors.SimpleButton btnSender;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private ctrSender ctrSender1;
        private ctrRecipients ctrRecipients1;
        private ctrPreview ctrPreview1;
        private ctrSend ctrSend1;
        private DevExpress.XtraEditors.GroupControl groupControl2;

    }
}

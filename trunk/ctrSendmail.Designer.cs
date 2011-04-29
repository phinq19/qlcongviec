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
            this.btnResult = new DevExpress.XtraEditors.SimpleButton();
            this.btnSendmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSender = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ctrContent1 = new NewProject.ctrContent();
            this.ctrSender1 = new NewProject.ctrSender();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnContent
            // 
            this.btnContent.Location = new System.Drawing.Point(0, 103);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(184, 32);
            this.btnContent.TabIndex = 1;
            this.btnContent.Text = "Content";
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSender);
            this.groupControl1.Controls.Add(this.btnResult);
            this.groupControl1.Controls.Add(this.btnSendmail);
            this.groupControl1.Controls.Add(this.btnPreview);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.btnContent);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(185, 409);
            this.groupControl1.TabIndex = 2;
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(0, 196);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(184, 32);
            this.btnResult.TabIndex = 5;
            this.btnResult.Text = "Result";
            // 
            // btnSendmail
            // 
            this.btnSendmail.Location = new System.Drawing.Point(0, 165);
            this.btnSendmail.Name = "btnSendmail";
            this.btnSendmail.Size = new System.Drawing.Size(184, 32);
            this.btnSendmail.TabIndex = 4;
            this.btnSendmail.Text = "Send Mail";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(0, 134);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(184, 32);
            this.btnPreview.TabIndex = 3;
            this.btnPreview.Text = "Preview";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(0, 72);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(184, 32);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "Recipients";
            // 
            // btnSender
            // 
            this.btnSender.Location = new System.Drawing.Point(0, 40);
            this.btnSender.Name = "btnSender";
            this.btnSender.Size = new System.Drawing.Size(184, 32);
            this.btnSender.TabIndex = 6;
            this.btnSender.Text = "Sender";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.ctrSender1);
            this.panelControl1.Controls.Add(this.ctrContent1);
            this.panelControl1.Location = new System.Drawing.Point(193, 26);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(709, 386);
            this.panelControl1.TabIndex = 4;
            // 
            // ctrContent1
            // 
            this.ctrContent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrContent1.Location = new System.Drawing.Point(2, 2);
            this.ctrContent1.Name = "ctrContent1";
            this.ctrContent1.Size = new System.Drawing.Size(705, 382);
            this.ctrContent1.TabIndex = 3;
            // 
            // ctrSender1
            // 
            this.ctrSender1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrSender1.Location = new System.Drawing.Point(2, 2);
            this.ctrSender1.Name = "ctrSender1";
            this.ctrSender1.Size = new System.Drawing.Size(705, 382);
            this.ctrSender1.TabIndex = 4;
            // 
            // ctrSendmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "ctrSendmail";
            this.Size = new System.Drawing.Size(905, 415);
            this.Load += new System.EventHandler(this.ctrSendmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnContent;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnSendmail;
        private DevExpress.XtraEditors.SimpleButton btnResult;
        private ctrContent ctrContent1;
        private DevExpress.XtraEditors.SimpleButton btnSender;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private ctrSender ctrSender1;

    }
}

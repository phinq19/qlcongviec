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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnSendmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnResult = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnContent
            // 
            this.btnContent.Location = new System.Drawing.Point(1, 54);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(184, 32);
            this.btnContent.TabIndex = 1;
            this.btnContent.Text = "Content";
            // 
            // groupControl1
            // 
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
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(1, 23);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(184, 32);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "Recipients";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(1, 85);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(184, 32);
            this.btnPreview.TabIndex = 3;
            this.btnPreview.Text = "Preview";
            // 
            // btnSendmail
            // 
            this.btnSendmail.Location = new System.Drawing.Point(1, 116);
            this.btnSendmail.Name = "btnSendmail";
            this.btnSendmail.Size = new System.Drawing.Size(184, 32);
            this.btnSendmail.TabIndex = 4;
            this.btnSendmail.Text = "Send Mail";
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(1, 147);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(184, 32);
            this.btnResult.TabIndex = 5;
            this.btnResult.Text = "Result";
            // 
            // ctrSendmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ctrSendmail";
            this.Size = new System.Drawing.Size(905, 415);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnContent;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnSendmail;
        private DevExpress.XtraEditors.SimpleButton btnResult;

    }
}

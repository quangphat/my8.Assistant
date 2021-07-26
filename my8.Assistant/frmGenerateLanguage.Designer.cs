namespace my8.Assistant
{
    partial class frmGenerateLanguage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.txtKey = new AutoControl.AutoTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVN = new AutoControl.AutoTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEng = new AutoControl.AutoTextBox();
            this.cbWriteToApi = new System.Windows.Forms.CheckBox();
            this.btnCreate = new ModernUI.Controls.BootstrapButton();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 166;
            this.label3.Tag = "";
            this.label3.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.BindingFor = "";
            this.txtKey.BindingName = "";
            this.txtKey.Location = new System.Drawing.Point(26, 67);
            this.txtKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(387, 22);
            this.txtKey.TabIndex = 165;
            this.txtKey.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(429, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 168;
            this.label1.Tag = "";
            this.label1.Text = "Vietnamese";
            // 
            // txtVN
            // 
            this.txtVN.BindingFor = "ApplicationSetting";
            this.txtVN.BindingName = "ILanguageFilePath";
            this.txtVN.Location = new System.Drawing.Point(432, 67);
            this.txtVN.Margin = new System.Windows.Forms.Padding(4);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(821, 22);
            this.txtVN.TabIndex = 167;
            this.txtVN.Tag = "";
            this.txtVN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVN_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 170;
            this.label2.Tag = "";
            this.label2.Text = "English";
            // 
            // txtEng
            // 
            this.txtEng.BindingFor = "ApplicationSetting";
            this.txtEng.BindingName = "ILanguageFilePath";
            this.txtEng.Location = new System.Drawing.Point(432, 127);
            this.txtEng.Margin = new System.Windows.Forms.Padding(4);
            this.txtEng.Name = "txtEng";
            this.txtEng.Size = new System.Drawing.Size(821, 22);
            this.txtEng.TabIndex = 169;
            this.txtEng.Tag = "";
            this.txtEng.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEng_KeyUp);
            // 
            // cbWriteToApi
            // 
            this.cbWriteToApi.AutoSize = true;
            this.cbWriteToApi.Location = new System.Drawing.Point(432, 180);
            this.cbWriteToApi.Name = "cbWriteToApi";
            this.cbWriteToApi.Size = new System.Drawing.Size(247, 21);
            this.cbWriteToApi.TabIndex = 171;
            this.cbWriteToApi.Text = "Ghi vào file message code của api";
            this.cbWriteToApi.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.AutoSize = true;
            this.btnCreate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCreate.BootstrapStyle = ModernUI.ModernUIManager.BootstrapStyle.Info;
            this.btnCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreate.Depth = 0;
            this.btnCreate.Icon = null;
            this.btnCreate.Location = new System.Drawing.Point(432, 225);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreate.MouseState = ModernUI.MouseState.HOVER;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(56, 36);
            this.btnCreate.TabIndex = 172;
            this.btnCreate.Text = "Run";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmGenerateLanguage
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 309);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cbWriteToApi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEng);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGenerateLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ngôn ngữ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private AutoControl.AutoTextBox txtKey;
        private System.Windows.Forms.Label label1;
        private AutoControl.AutoTextBox txtVN;
        private System.Windows.Forms.Label label2;
        private AutoControl.AutoTextBox txtEng;
        private System.Windows.Forms.CheckBox cbWriteToApi;
        private ModernUI.Controls.BootstrapButton btnCreate;
    }
}
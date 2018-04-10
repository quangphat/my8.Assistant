namespace my8.Assistant
{
    partial class frmSelectProject
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
            this.rdApi = new AutoControl.AutoMetroRadio();
            this.rdClient = new AutoControl.AutoMetroRadio();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdApi
            // 
            this.rdApi.AutoSize = true;
            this.rdApi.BindingFor = "";
            this.rdApi.BindingName = "";
            this.rdApi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdApi.Location = new System.Drawing.Point(169, 59);
            this.rdApi.Name = "rdApi";
            this.rdApi.Size = new System.Drawing.Size(41, 15);
            this.rdApi.TabIndex = 25;
            this.rdApi.Text = "Api";
            this.rdApi.UseSelectable = true;
            this.rdApi.ValueToCheck = 2;
            // 
            // rdClient
            // 
            this.rdClient.AutoSize = true;
            this.rdClient.BindingFor = "";
            this.rdClient.BindingName = "";
            this.rdClient.Checked = true;
            this.rdClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdClient.Location = new System.Drawing.Point(68, 59);
            this.rdClient.Name = "rdClient";
            this.rdClient.Size = new System.Drawing.Size(54, 15);
            this.rdClient.TabIndex = 24;
            this.rdClient.TabStop = true;
            this.rdClient.Text = "Client";
            this.rdClient.UseSelectable = true;
            this.rdClient.ValueToCheck = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(68, 111);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 26;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(156, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSelectProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 174);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.rdApi);
            this.Controls.Add(this.rdClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSelectProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectProject_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSelectProject_FormClosed);
            this.Load += new System.EventHandler(this.frmSelectProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AutoControl.AutoMetroRadio rdApi;
        private AutoControl.AutoMetroRadio rdClient;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
    }
}
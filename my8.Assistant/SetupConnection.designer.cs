namespace my8.Assistant
{
    partial class SetupConnection
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new AutoControl.AutoTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServer = new AutoControl.AutoTextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.autoMetroRadio1 = new AutoControl.AutoMetroRadio();
            this.autoMetroRadio4 = new AutoControl.AutoMetroRadio();
            this.autoTextBox1 = new AutoControl.AutoTextBox();
            this.autoMetroRadio5 = new AutoControl.AutoMetroRadio();
            this.txtPass = new AutoControl.AutoTextBox();
            this.autoTextBox2 = new AutoControl.AutoTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server:";
            // 
            // txtUserName
            // 
            this.txtUserName.BindingFor = "DatabaseInfo";
            this.txtUserName.BindingName = "UserName";
            this.txtUserName.Location = new System.Drawing.Point(106, 134);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(208, 20);
            this.txtUserName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Database:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(77, 272);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "OK";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtServer
            // 
            this.txtServer.BindingFor = "DatabaseInfo";
            this.txtServer.BindingName = "Server";
            this.txtServer.Location = new System.Drawing.Point(106, 94);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(208, 20);
            this.txtServer.TabIndex = 12;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(158, 272);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 15;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(239, 272);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 20;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // autoMetroRadio1
            // 
            this.autoMetroRadio1.AutoSize = true;
            this.autoMetroRadio1.BindingFor = "DatabaseInfo";
            this.autoMetroRadio1.BindingName = "DbType";
            this.autoMetroRadio1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoMetroRadio1.Location = new System.Drawing.Point(48, 41);
            this.autoMetroRadio1.Name = "autoMetroRadio1";
            this.autoMetroRadio1.Size = new System.Drawing.Size(74, 15);
            this.autoMetroRadio1.TabIndex = 17;
            this.autoMetroRadio1.Text = "Sql Server";
            this.autoMetroRadio1.UseSelectable = true;
            this.autoMetroRadio1.ValueToCheck = 1;
            this.autoMetroRadio1.Click += new System.EventHandler(this.autoMetroRadio1_Click);
            // 
            // autoMetroRadio4
            // 
            this.autoMetroRadio4.AutoSize = true;
            this.autoMetroRadio4.BindingFor = "DatabaseInfo";
            this.autoMetroRadio4.BindingName = "DbType";
            this.autoMetroRadio4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoMetroRadio4.Location = new System.Drawing.Point(239, 41);
            this.autoMetroRadio4.Name = "autoMetroRadio4";
            this.autoMetroRadio4.Size = new System.Drawing.Size(54, 15);
            this.autoMetroRadio4.TabIndex = 21;
            this.autoMetroRadio4.Text = "Neo4j";
            this.autoMetroRadio4.UseSelectable = true;
            this.autoMetroRadio4.ValueToCheck = 3;
            this.autoMetroRadio4.Click += new System.EventHandler(this.autoMetroRadio4_Click);
            // 
            // autoTextBox1
            // 
            this.autoTextBox1.BindingFor = "DatabaseInfo";
            this.autoTextBox1.BindingName = "Id";
            this.autoTextBox1.Location = new System.Drawing.Point(25, 301);
            this.autoTextBox1.Name = "autoTextBox1";
            this.autoTextBox1.Size = new System.Drawing.Size(37, 20);
            this.autoTextBox1.TabIndex = 22;
            this.autoTextBox1.Visible = false;
            // 
            // autoMetroRadio5
            // 
            this.autoMetroRadio5.AutoSize = true;
            this.autoMetroRadio5.BindingFor = "DatabaseInfo";
            this.autoMetroRadio5.BindingName = "DbType";
            this.autoMetroRadio5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoMetroRadio5.Location = new System.Drawing.Point(149, 41);
            this.autoMetroRadio5.Name = "autoMetroRadio5";
            this.autoMetroRadio5.Size = new System.Drawing.Size(62, 15);
            this.autoMetroRadio5.TabIndex = 23;
            this.autoMetroRadio5.Text = "Mongo";
            this.autoMetroRadio5.UseSelectable = true;
            this.autoMetroRadio5.ValueToCheck = 2;
            this.autoMetroRadio5.Click += new System.EventHandler(this.autoMetroRadio5_Click);
            // 
            // txtPass
            // 
            this.txtPass.BindingFor = "DatabaseInfo";
            this.txtPass.BindingName = "Password";
            this.txtPass.Location = new System.Drawing.Point(106, 180);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(208, 20);
            this.txtPass.TabIndex = 7;
            // 
            // autoTextBox2
            // 
            this.autoTextBox2.BindingFor = "DatabaseInfo";
            this.autoTextBox2.BindingName = "DatabaseName";
            this.autoTextBox2.Location = new System.Drawing.Point(106, 229);
            this.autoTextBox2.Name = "autoTextBox2";
            this.autoTextBox2.PasswordChar = '*';
            this.autoTextBox2.Size = new System.Drawing.Size(208, 20);
            this.autoTextBox2.TabIndex = 24;
            // 
            // SetupConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 328);
            this.Controls.Add(this.autoTextBox2);
            this.Controls.Add(this.autoMetroRadio5);
            this.Controls.Add(this.autoTextBox1);
            this.Controls.Add(this.autoMetroRadio4);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.autoMetroRadio1);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetupConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetupConnection_FormClosing);
            this.Load += new System.EventHandler(this.SetupConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private AutoControl.AutoTextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnect;
        private AutoControl.AutoTextBox txtServer;
        private System.Windows.Forms.Button btnWrite;
        private AutoControl.AutoMetroRadio autoMetroRadio1;
        private System.Windows.Forms.Button btnRemove;
        private AutoControl.AutoMetroRadio autoMetroRadio4;
        private AutoControl.AutoTextBox autoTextBox1;
        private AutoControl.AutoMetroRadio autoMetroRadio5;
        private AutoControl.AutoTextBox txtPass;
        private AutoControl.AutoTextBox autoTextBox2;
    }
}
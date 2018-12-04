namespace my8.Assistant
{
    partial class frmCreateProject
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
            this.txtProjectName = new AutoControl.AutoTextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.autoMetroRadio1 = new AutoControl.AutoMetroRadio();
            this.autoMetroRadio2 = new AutoControl.AutoMetroRadio();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtProjectName
            // 
            this.txtProjectName.BindingFor = "Project";
            this.txtProjectName.BindingName = "Name";
            this.txtProjectName.Location = new System.Drawing.Point(88, 29);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(178, 20);
            this.txtProjectName.TabIndex = 1;
            this.txtProjectName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProjectName_KeyDown);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(88, 132);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // autoMetroRadio1
            // 
            this.autoMetroRadio1.AutoSize = true;
            this.autoMetroRadio1.BindingFor = "Project";
            this.autoMetroRadio1.BindingName = "Type";
            this.autoMetroRadio1.Location = new System.Drawing.Point(88, 69);
            this.autoMetroRadio1.Name = "autoMetroRadio1";
            this.autoMetroRadio1.Size = new System.Drawing.Size(54, 15);
            this.autoMetroRadio1.TabIndex = 3;
            this.autoMetroRadio1.Text = "Client";
            this.autoMetroRadio1.UseSelectable = true;
            this.autoMetroRadio1.ValueToCheck = 0;
            // 
            // autoMetroRadio2
            // 
            this.autoMetroRadio2.AutoSize = true;
            this.autoMetroRadio2.BindingFor = "Project";
            this.autoMetroRadio2.BindingName = "Type";
            this.autoMetroRadio2.Location = new System.Drawing.Point(88, 99);
            this.autoMetroRadio2.Name = "autoMetroRadio2";
            this.autoMetroRadio2.Size = new System.Drawing.Size(41, 15);
            this.autoMetroRadio2.TabIndex = 4;
            this.autoMetroRadio2.Text = "Api";
            this.autoMetroRadio2.UseSelectable = true;
            this.autoMetroRadio2.ValueToCheck = 0;
            // 
            // frmCreateProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 167);
            this.Controls.Add(this.autoMetroRadio2);
            this.Controls.Add(this.autoMetroRadio1);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCreateProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create project";
            this.Load += new System.EventHandler(this.frmCreateProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private AutoControl.AutoTextBox txtProjectName;
        private System.Windows.Forms.Button btnCreate;
        private AutoControl.AutoMetroRadio autoMetroRadio1;
        private AutoControl.AutoMetroRadio autoMetroRadio2;
    }
}
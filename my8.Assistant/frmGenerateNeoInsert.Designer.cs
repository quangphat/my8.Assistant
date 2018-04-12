namespace my8.Assistant
{
    partial class frmGenerateNeoInsert
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
            this.txtCSharpClass = new System.Windows.Forms.RichTextBox();
            this.txtBuilder = new System.Windows.Forms.RichTextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCSharpClass
            // 
            this.txtCSharpClass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCSharpClass.Location = new System.Drawing.Point(0, -1);
            this.txtCSharpClass.Name = "txtCSharpClass";
            this.txtCSharpClass.Size = new System.Drawing.Size(328, 368);
            this.txtCSharpClass.TabIndex = 2;
            this.txtCSharpClass.Text = "";
            // 
            // txtBuilder
            // 
            this.txtBuilder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuilder.Location = new System.Drawing.Point(472, -1);
            this.txtBuilder.Name = "txtBuilder";
            this.txtBuilder.Size = new System.Drawing.Size(329, 368);
            this.txtBuilder.TabIndex = 3;
            this.txtBuilder.Text = "";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(364, 127);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 60);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(334, 77);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(132, 20);
            this.txtClassName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(335, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Class name";
            // 
            // frmGenerateNeoInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 369);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtBuilder);
            this.Controls.Add(this.txtCSharpClass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGenerateNeoInsert";
            this.Text = "Generate Neo insert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtCSharpClass;
        private System.Windows.Forms.RichTextBox txtBuilder;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label1;
    }
}
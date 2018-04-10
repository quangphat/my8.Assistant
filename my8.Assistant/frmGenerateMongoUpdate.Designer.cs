namespace my8.Assistant
{
    partial class frmGenerateMongoUpdate
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
            this.SuspendLayout();
            // 
            // txtCSharpClass
            // 
            this.txtCSharpClass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCSharpClass.Location = new System.Drawing.Point(-1, 2);
            this.txtCSharpClass.Name = "txtCSharpClass";
            this.txtCSharpClass.Size = new System.Drawing.Size(328, 368);
            this.txtCSharpClass.TabIndex = 1;
            this.txtCSharpClass.Text = "";
            // 
            // txtBuilder
            // 
            this.txtBuilder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuilder.Location = new System.Drawing.Point(470, 2);
            this.txtBuilder.Name = "txtBuilder";
            this.txtBuilder.Size = new System.Drawing.Size(329, 368);
            this.txtBuilder.TabIndex = 2;
            this.txtBuilder.Text = "";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(362, 144);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 60);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmGenerateMongoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 372);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtBuilder);
            this.Controls.Add(this.txtCSharpClass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGenerateMongoUpdate";
            this.Text = "Generate mongo update builder";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtCSharpClass;
        private System.Windows.Forms.RichTextBox txtBuilder;
        private System.Windows.Forms.Button btnCreate;
    }
}
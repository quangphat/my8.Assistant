﻿namespace my8.Assistant
{
    partial class frmGenerateReactModel
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
            this.txtReactModel = new System.Windows.Forms.RichTextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCSharpClass
            // 
            this.txtCSharpClass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCSharpClass.Location = new System.Drawing.Point(2, 0);
            this.txtCSharpClass.Name = "txtCSharpClass";
            this.txtCSharpClass.Size = new System.Drawing.Size(328, 368);
            this.txtCSharpClass.TabIndex = 0;
            this.txtCSharpClass.Text = "";
            this.txtCSharpClass.Click += new System.EventHandler(this.txtCSharpClass_Click);
            // 
            // txtReactModel
            // 
            this.txtReactModel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReactModel.Location = new System.Drawing.Point(436, 0);
            this.txtReactModel.Name = "txtReactModel";
            this.txtReactModel.Size = new System.Drawing.Size(329, 368);
            this.txtReactModel.TabIndex = 1;
            this.txtReactModel.Text = "";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(345, 108);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 60);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmGenerateReactModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 371);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtReactModel);
            this.Controls.Add(this.txtCSharpClass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGenerateReactModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create React model";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtCSharpClass;
        private System.Windows.Forms.RichTextBox txtReactModel;
        private System.Windows.Forms.Button btnCreate;
    }
}
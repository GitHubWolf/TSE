namespace TSE
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.richTextBoxAbout = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxAbout
            // 
            this.richTextBoxAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAbout.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxAbout.Name = "richTextBoxAbout";
            this.richTextBoxAbout.Size = new System.Drawing.Size(284, 262);
            this.richTextBoxAbout.TabIndex = 0;
            this.richTextBoxAbout.Text = resources.GetString("richTextBoxAbout.Text");
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.richTextBoxAbout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAbout";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxAbout;
    }
}
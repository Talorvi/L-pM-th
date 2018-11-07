namespace pdfExtractor
{
    partial class Form1
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
            this.ShowPDFbutton = new System.Windows.Forms.Button();
            this.PDFtextBox = new System.Windows.Forms.TextBox();
            this.MetadatalistBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ShowPDFbutton
            // 
            this.ShowPDFbutton.Location = new System.Drawing.Point(190, 326);
            this.ShowPDFbutton.Name = "ShowPDFbutton";
            this.ShowPDFbutton.Size = new System.Drawing.Size(118, 27);
            this.ShowPDFbutton.TabIndex = 0;
            this.ShowPDFbutton.Text = "Show PDF";
            this.ShowPDFbutton.UseVisualStyleBackColor = true;
            this.ShowPDFbutton.Click += new System.EventHandler(this.ShowPDFbutton_Click);
            // 
            // PDFtextBox
            // 
            this.PDFtextBox.Location = new System.Drawing.Point(13, 13);
            this.PDFtextBox.Multiline = true;
            this.PDFtextBox.Name = "PDFtextBox";
            this.PDFtextBox.Size = new System.Drawing.Size(249, 307);
            this.PDFtextBox.TabIndex = 1;
            // 
            // MetadatalistBox
            // 
            this.MetadatalistBox.FormattingEnabled = true;
            this.MetadatalistBox.ItemHeight = 16;
            this.MetadatalistBox.Location = new System.Drawing.Point(268, 12);
            this.MetadatalistBox.Name = "MetadatalistBox";
            this.MetadatalistBox.Size = new System.Drawing.Size(240, 308);
            this.MetadatalistBox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 358);
            this.Controls.Add(this.MetadatalistBox);
            this.Controls.Add(this.PDFtextBox);
            this.Controls.Add(this.ShowPDFbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ShowPDFbutton;
        private System.Windows.Forms.TextBox PDFtextBox;
        private System.Windows.Forms.ListBox MetadatalistBox;
    }
}


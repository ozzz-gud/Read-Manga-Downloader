namespace ReadMangaDownloader
{
    partial class ProgressForm
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
            this.chapters_progressBar = new System.Windows.Forms.ProgressBar();
            this.chapter_label = new System.Windows.Forms.Label();
            this.image_label = new System.Windows.Forms.Label();
            this.Image_progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // chapters_progressBar
            // 
            this.chapters_progressBar.Location = new System.Drawing.Point(12, 36);
            this.chapters_progressBar.Name = "chapters_progressBar";
            this.chapters_progressBar.Size = new System.Drawing.Size(411, 23);
            this.chapters_progressBar.TabIndex = 0;
            // 
            // chapter_label
            // 
            this.chapter_label.AutoSize = true;
            this.chapter_label.Location = new System.Drawing.Point(13, 13);
            this.chapter_label.Name = "chapter_label";
            this.chapter_label.Size = new System.Drawing.Size(66, 17);
            this.chapter_label.TabIndex = 1;
            this.chapter_label.Text = "Chapter: ";
            // 
            // image_label
            // 
            this.image_label.AutoSize = true;
            this.image_label.Location = new System.Drawing.Point(16, 66);
            this.image_label.Name = "image_label";
            this.image_label.Size = new System.Drawing.Size(50, 17);
            this.image_label.TabIndex = 2;
            this.image_label.Text = "Image:";
            // 
            // Image_progressBar
            // 
            this.Image_progressBar.Location = new System.Drawing.Point(12, 95);
            this.Image_progressBar.Name = "Image_progressBar";
            this.Image_progressBar.Size = new System.Drawing.Size(411, 23);
            this.Image_progressBar.TabIndex = 3;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 141);
            this.Controls.Add(this.Image_progressBar);
            this.Controls.Add(this.image_label);
            this.Controls.Add(this.chapter_label);
            this.Controls.Add(this.chapters_progressBar);
            this.Name = "ProgressForm";
            this.Text = "ProgressForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar chapters_progressBar;
        private System.Windows.Forms.Label chapter_label;
        private System.Windows.Forms.Label image_label;
        private System.Windows.Forms.ProgressBar Image_progressBar;
    }
}
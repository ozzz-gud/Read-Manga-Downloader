namespace ReadMangaDownloader
{
    partial class DownloadForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.url_textBox = new System.Windows.Forms.TextBox();
            this.show_button = new System.Windows.Forms.Button();
            this.chapters_checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.check_button = new System.Windows.Forms.Button();
            this.download_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // url_textBox
            // 
            this.url_textBox.Location = new System.Drawing.Point(0, 12);
            this.url_textBox.Name = "url_textBox";
            this.url_textBox.Size = new System.Drawing.Size(776, 22);
            this.url_textBox.TabIndex = 0;
            // 
            // show_button
            // 
            this.show_button.Location = new System.Drawing.Point(351, 54);
            this.show_button.Name = "show_button";
            this.show_button.Size = new System.Drawing.Size(75, 23);
            this.show_button.TabIndex = 1;
            this.show_button.Text = "Show";
            this.show_button.UseVisualStyleBackColor = true;
            this.show_button.Click += new System.EventHandler(this.show_button_Click);
            // 
            // chapters_checkedListBox
            // 
            this.chapters_checkedListBox.CheckOnClick = true;
            this.chapters_checkedListBox.FormattingEnabled = true;
            this.chapters_checkedListBox.Location = new System.Drawing.Point(12, 82);
            this.chapters_checkedListBox.Name = "chapters_checkedListBox";
            this.chapters_checkedListBox.Size = new System.Drawing.Size(764, 582);
            this.chapters_checkedListBox.TabIndex = 2;
            // 
            // check_button
            // 
            this.check_button.Location = new System.Drawing.Point(13, 53);
            this.check_button.Name = "check_button";
            this.check_button.Size = new System.Drawing.Size(75, 23);
            this.check_button.TabIndex = 3;
            this.check_button.Text = "check all";
            this.check_button.UseVisualStyleBackColor = true;
            this.check_button.Click += new System.EventHandler(this.check_button_Click);
            // 
            // download_button
            // 
            this.download_button.Location = new System.Drawing.Point(308, 670);
            this.download_button.Name = "download_button";
            this.download_button.Size = new System.Drawing.Size(145, 23);
            this.download_button.TabIndex = 4;
            this.download_button.Text = "Download Selected";
            this.download_button.UseVisualStyleBackColor = true;
            this.download_button.Click += new System.EventHandler(this.download_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 701);
            this.Controls.Add(this.download_button);
            this.Controls.Add(this.check_button);
            this.Controls.Add(this.chapters_checkedListBox);
            this.Controls.Add(this.show_button);
            this.Controls.Add(this.url_textBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox url_textBox;
        private System.Windows.Forms.Button show_button;
        private System.Windows.Forms.CheckedListBox chapters_checkedListBox;
        private System.Windows.Forms.Button check_button;
        private System.Windows.Forms.Button download_button;
    }
}


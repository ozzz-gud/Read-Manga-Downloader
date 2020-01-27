namespace WindowsFormApp
{
    partial class AddForm
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
            this.url_textBox = new System.Windows.Forms.TextBox();
            this.show_button = new System.Windows.Forms.Button();
            this.checkAll_button = new System.Windows.Forms.Button();
            this.add_button = new System.Windows.Forms.Button();
            this.chapterList_checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // url_textBox
            // 
            this.url_textBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.url_textBox.Location = new System.Drawing.Point(18, 19);
            this.url_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.url_textBox.Name = "url_textBox";
            this.url_textBox.Size = new System.Drawing.Size(495, 31);
            this.url_textBox.TabIndex = 0;
            // 
            // show_button
            // 
            this.show_button.Location = new System.Drawing.Point(532, 13);
            this.show_button.Name = "show_button";
            this.show_button.Size = new System.Drawing.Size(138, 40);
            this.show_button.TabIndex = 1;
            this.show_button.Text = "Показать";
            this.show_button.UseVisualStyleBackColor = true;
            this.show_button.Click += new System.EventHandler(this.Show_button_Click);
            // 
            // checkAll_button
            // 
            this.checkAll_button.Location = new System.Drawing.Point(18, 591);
            this.checkAll_button.Name = "checkAll_button";
            this.checkAll_button.Size = new System.Drawing.Size(138, 40);
            this.checkAll_button.TabIndex = 3;
            this.checkAll_button.Text = "Все";
            this.checkAll_button.UseVisualStyleBackColor = true;
            this.checkAll_button.Click += new System.EventHandler(this.CheckAll_button_Click);
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(177, 591);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(138, 40);
            this.add_button.TabIndex = 4;
            this.add_button.Text = "Добавить";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // chapterList_checkedListBox
            // 
            this.chapterList_checkedListBox.FormattingEnabled = true;
            this.chapterList_checkedListBox.Location = new System.Drawing.Point(18, 58);
            this.chapterList_checkedListBox.Name = "chapterList_checkedListBox";
            this.chapterList_checkedListBox.Size = new System.Drawing.Size(652, 524);
            this.chapterList_checkedListBox.TabIndex = 5;
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 647);
            this.Controls.Add(this.chapterList_checkedListBox);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.checkAll_button);
            this.Controls.Add(this.show_button);
            this.Controls.Add(this.url_textBox);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddForm";
            this.Text = "AddForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddForm_FormClosing);
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox url_textBox;
        private System.Windows.Forms.Button show_button;
        private System.Windows.Forms.Button checkAll_button;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.CheckedListBox chapterList_checkedListBox;
    }
}
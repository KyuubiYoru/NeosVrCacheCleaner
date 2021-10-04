﻿namespace NeosVrCache
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.button_cleanup = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_showCacheAcces = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_timeLimit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_cacheLimit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_fileAccess = new System.Windows.Forms.Label();
            this.label_currentFileCount = new System.Windows.Forms.Label();
            this.label_currentSize = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_deletedFcount = new System.Windows.Forms.Label();
            this.label_deletedSize = new System.Windows.Forms.Label();
            this.textbox_console = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_cleanup
            // 
            this.button_cleanup.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cleanup.Location = new System.Drawing.Point(598, 74);
            this.button_cleanup.Name = "button_cleanup";
            this.button_cleanup.Size = new System.Drawing.Size(100, 25);
            this.button_cleanup.TabIndex = 0;
            this.button_cleanup.Text = "Cleanup Cache";
            this.button_cleanup.UseVisualStyleBackColor = true;
            this.button_cleanup.Click += new System.EventHandler(this.buttonCleanUp_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(163, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(535, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "E:\\NeosVR\\Cache";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox_showCacheAcces);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_timeLimit);
            this.groupBox1.Controls.Add(this.button_cleanup);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_cacheLimit);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 105);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // checkBox_showCacheAcces
            // 
            this.checkBox_showCacheAcces.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showCacheAcces.Location = new System.Drawing.Point(565, 39);
            this.checkBox_showCacheAcces.Name = "checkBox_showCacheAcces";
            this.checkBox_showCacheAcces.Size = new System.Drawing.Size(143, 24);
            this.checkBox_showCacheAcces.TabIndex = 8;
            this.checkBox_showCacheAcces.Text = "Show Cache Access";
            this.checkBox_showCacheAcces.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(210, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Days";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(210, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "GB";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Only Delete Files Older Then:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_timeLimit
            // 
            this.textBox_timeLimit.Location = new System.Drawing.Point(163, 68);
            this.textBox_timeLimit.Name = "textBox_timeLimit";
            this.textBox_timeLimit.Size = new System.Drawing.Size(41, 20);
            this.textBox_timeLimit.TabIndex = 7;
            this.textBox_timeLimit.Text = "16";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cache Size Limit:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_cacheLimit
            // 
            this.textBox_cacheLimit.Location = new System.Drawing.Point(163, 39);
            this.textBox_cacheLimit.Name = "textBox_cacheLimit";
            this.textBox_cacheLimit.Size = new System.Drawing.Size(41, 20);
            this.textBox_cacheLimit.TabIndex = 5;
            this.textBox_cacheLimit.Text = "20";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(90, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cache Path:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_fileAccess);
            this.groupBox2.Controls.Add(this.label_currentFileCount);
            this.groupBox2.Controls.Add(this.label_currentSize);
            this.groupBox2.Location = new System.Drawing.Point(12, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 94);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Size";
            // 
            // label_fileAccess
            // 
            this.label_fileAccess.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label_fileAccess.Location = new System.Drawing.Point(6, 62);
            this.label_fileAccess.Name = "label_fileAccess";
            this.label_fileAccess.Size = new System.Drawing.Size(145, 23);
            this.label_fileAccess.TabIndex = 2;
            this.label_fileAccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_currentFileCount
            // 
            this.label_currentFileCount.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label_currentFileCount.Location = new System.Drawing.Point(6, 39);
            this.label_currentFileCount.Name = "label_currentFileCount";
            this.label_currentFileCount.Size = new System.Drawing.Size(145, 23);
            this.label_currentFileCount.TabIndex = 1;
            this.label_currentFileCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_currentSize
            // 
            this.label_currentSize.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label_currentSize.Location = new System.Drawing.Point(6, 16);
            this.label_currentSize.Name = "label_currentSize";
            this.label_currentSize.Size = new System.Drawing.Size(145, 23);
            this.label_currentSize.TabIndex = 0;
            this.label_currentSize.Text = "\r\n\r\n";
            this.label_currentSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label_deletedFcount);
            this.groupBox3.Controls.Add(this.label_deletedSize);
            this.groupBox3.Location = new System.Drawing.Point(571, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 94);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Deleted";
            // 
            // label_deletedFcount
            // 
            this.label_deletedFcount.Location = new System.Drawing.Point(6, 39);
            this.label_deletedFcount.Name = "label_deletedFcount";
            this.label_deletedFcount.Size = new System.Drawing.Size(88, 23);
            this.label_deletedFcount.TabIndex = 1;
            this.label_deletedFcount.Text = "Files: 0";
            this.label_deletedFcount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_deletedSize
            // 
            this.label_deletedSize.Location = new System.Drawing.Point(6, 16);
            this.label_deletedSize.Name = "label_deletedSize";
            this.label_deletedSize.Size = new System.Drawing.Size(88, 23);
            this.label_deletedSize.TabIndex = 0;
            this.label_deletedSize.Text = "Size: 0 B";
            this.label_deletedSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textbox_console
            // 
            this.textbox_console.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_console.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textbox_console.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.textbox_console.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.textbox_console.Location = new System.Drawing.Point(12, 223);
            this.textbox_console.Multiline = true;
            this.textbox_console.Name = "textbox_console";
            this.textbox_console.ReadOnly = true;
            this.textbox_console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textbox_console.Size = new System.Drawing.Size(714, 349);
            this.textbox_console.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 584);
            this.Controls.Add(this.textbox_console);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(447, 455);
            this.Name = "Form1";
            this.Text = "NeosVr Cache Cleaner";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button button_cleanup;

        private System.Windows.Forms.TextBox textBox_cacheLimit;

        private System.Windows.Forms.TextBox textBox_timeLimit;

        private System.Windows.Forms.Label label_fileAccess;

        private System.Windows.Forms.Label label_fileAcces;

        private System.Windows.Forms.CheckBox checkBox_showCacheAcces;

        private System.Windows.Forms.TextBox textbox_console;

        private System.Windows.Forms.TextBox _console;

        private System.Windows.Forms.Label label_currentFileCount;
        private System.Windows.Forms.Label label_deletedSize;
        private System.Windows.Forms.Label label_deletedFcount;
        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.Label label_currentSize;

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;

        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button button1;

        #endregion
    }
}
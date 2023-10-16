
namespace Kuwagata
{
    partial class KuwagataMainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KuwagataMainWindow));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.VerseLabel = new System.Windows.Forms.Label();
            this.VerseTextBox = new System.Windows.Forms.TextBox();
            this.VersionTextBox = new System.Windows.Forms.TextBox();
            this.Version = new System.Windows.Forms.Label();
            this.Retrieve = new System.Windows.Forms.Button();
            this.SeekForward = new System.Windows.Forms.Button();
            this.SeekBack = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.SelectionShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // VerseLabel
            // 
            this.VerseLabel.AutoSize = true;
            this.VerseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerseLabel.Location = new System.Drawing.Point(153, 34);
            this.VerseLabel.Name = "VerseLabel";
            this.VerseLabel.Size = new System.Drawing.Size(76, 29);
            this.VerseLabel.TabIndex = 0;
            this.VerseLabel.Text = "Verse";
            this.VerseLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // VerseTextBox
            // 
            this.VerseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerseTextBox.Location = new System.Drawing.Point(113, 76);
            this.VerseTextBox.Name = "VerseTextBox";
            this.VerseTextBox.Size = new System.Drawing.Size(146, 30);
            this.VerseTextBox.TabIndex = 1;
            this.VerseTextBox.Text = "Genesis 1:1";
            this.VerseTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.VerseTextBox.TextChanged += new System.EventHandler(this.VerseTextBox_TextChanged);
            // 
            // VersionTextBox
            // 
            this.VersionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionTextBox.Location = new System.Drawing.Point(113, 168);
            this.VersionTextBox.Name = "VersionTextBox";
            this.VersionTextBox.Size = new System.Drawing.Size(146, 30);
            this.VersionTextBox.TabIndex = 3;
            this.VersionTextBox.Text = "KJV";
            this.VersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.Location = new System.Drawing.Point(144, 126);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(95, 29);
            this.Version.TabIndex = 2;
            this.Version.Text = "Version";
            // 
            // Retrieve
            // 
            this.Retrieve.Location = new System.Drawing.Point(126, 239);
            this.Retrieve.Name = "Retrieve";
            this.Retrieve.Size = new System.Drawing.Size(122, 36);
            this.Retrieve.TabIndex = 4;
            this.Retrieve.Text = "Retrieve";
            this.Retrieve.UseVisualStyleBackColor = true;
            this.Retrieve.Click += new System.EventHandler(this.Retrieve_Click);
            // 
            // SeekForward
            // 
            this.SeekForward.Location = new System.Drawing.Point(254, 239);
            this.SeekForward.Name = "SeekForward";
            this.SeekForward.Size = new System.Drawing.Size(39, 36);
            this.SeekForward.TabIndex = 5;
            this.SeekForward.Text = ">";
            this.SeekForward.UseVisualStyleBackColor = true;
            this.SeekForward.Click += new System.EventHandler(this.SeekForward_Click);
            // 
            // SeekBack
            // 
            this.SeekBack.Location = new System.Drawing.Point(81, 239);
            this.SeekBack.Name = "SeekBack";
            this.SeekBack.Size = new System.Drawing.Size(39, 36);
            this.SeekBack.TabIndex = 6;
            this.SeekBack.Text = "<";
            this.SeekBack.UseVisualStyleBackColor = true;
            this.SeekBack.Click += new System.EventHandler(this.SeekBack_Click);
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(254, 307);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(116, 34);
            this.Settings.TabIndex = 7;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // SelectionShow
            // 
            this.SelectionShow.Location = new System.Drawing.Point(12, 12);
            this.SelectionShow.Name = "SelectionShow";
            this.SelectionShow.Size = new System.Drawing.Size(120, 22);
            this.SelectionShow.TabIndex = 8;
            this.SelectionShow.Text = "Show Selection";
            this.SelectionShow.UseVisualStyleBackColor = true;
            this.SelectionShow.Click += new System.EventHandler(this.SelectionShow_Click);
            // 
            // KuwagataMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 353);
            this.Controls.Add(this.SelectionShow);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.SeekBack);
            this.Controls.Add(this.SeekForward);
            this.Controls.Add(this.Retrieve);
            this.Controls.Add(this.VersionTextBox);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.VerseTextBox);
            this.Controls.Add(this.VerseLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "KuwagataMainWindow";
            this.Text = "Kuwagata";
            this.Load += new System.EventHandler(this.KuwagataMainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label VerseLabel;
        private System.Windows.Forms.TextBox VerseTextBox;
        private System.Windows.Forms.TextBox VersionTextBox;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Button Retrieve;
        private System.Windows.Forms.Button SeekForward;
        private System.Windows.Forms.Button SeekBack;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button SelectionShow;
    }
}
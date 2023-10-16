
namespace Kuwagata
{
    partial class KuwagataSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KuwagataSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.VerseOutputText = new System.Windows.Forms.TextBox();
            this.PathSelector1 = new System.Windows.Forms.Button();
            this.PathSelector2 = new System.Windows.Forms.Button();
            this.VersionOutputText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ApplyChanges = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DefaultBibleVersionText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Verse Output";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VerseOutputText
            // 
            this.VerseOutputText.Location = new System.Drawing.Point(201, 43);
            this.VerseOutputText.Name = "VerseOutputText";
            this.VerseOutputText.Size = new System.Drawing.Size(174, 22);
            this.VerseOutputText.TabIndex = 1;
            // 
            // PathSelector1
            // 
            this.PathSelector1.Location = new System.Drawing.Point(381, 43);
            this.PathSelector1.Name = "PathSelector1";
            this.PathSelector1.Size = new System.Drawing.Size(28, 23);
            this.PathSelector1.TabIndex = 2;
            this.PathSelector1.Text = "...";
            this.PathSelector1.UseVisualStyleBackColor = true;
            this.PathSelector1.Click += new System.EventHandler(this.PathSelector1_Click);
            // 
            // PathSelector2
            // 
            this.PathSelector2.Location = new System.Drawing.Point(381, 79);
            this.PathSelector2.Name = "PathSelector2";
            this.PathSelector2.Size = new System.Drawing.Size(28, 22);
            this.PathSelector2.TabIndex = 5;
            this.PathSelector2.Text = "...";
            this.PathSelector2.UseVisualStyleBackColor = true;
            this.PathSelector2.Click += new System.EventHandler(this.PathSelector2_Click);
            // 
            // VersionOutputText
            // 
            this.VersionOutputText.Location = new System.Drawing.Point(201, 79);
            this.VersionOutputText.Name = "VersionOutputText";
            this.VersionOutputText.Size = new System.Drawing.Size(173, 22);
            this.VersionOutputText.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Version Output";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ApplyChanges
            // 
            this.ApplyChanges.Location = new System.Drawing.Point(6, 449);
            this.ApplyChanges.Name = "ApplyChanges";
            this.ApplyChanges.Size = new System.Drawing.Size(99, 42);
            this.ApplyChanges.TabIndex = 6;
            this.ApplyChanges.Text = "Apply";
            this.ApplyChanges.UseVisualStyleBackColor = true;
            this.ApplyChanges.Click += new System.EventHandler(this.ApplyChanges_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(132, 449);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(99, 42);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "General";
            // 
            // DefaultBibleVersionText
            // 
            this.DefaultBibleVersionText.Location = new System.Drawing.Point(201, 113);
            this.DefaultBibleVersionText.Name = "DefaultBibleVersionText";
            this.DefaultBibleVersionText.Size = new System.Drawing.Size(173, 22);
            this.DefaultBibleVersionText.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Default Bible Version";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KuwagataSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 503);
            this.Controls.Add(this.DefaultBibleVersionText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.ApplyChanges);
            this.Controls.Add(this.PathSelector2);
            this.Controls.Add(this.VersionOutputText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PathSelector1);
            this.Controls.Add(this.VerseOutputText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(430, 550);
            this.MinimumSize = new System.Drawing.Size(430, 550);
            this.Name = "KuwagataSettings";
            this.ShowInTaskbar = false;
            this.Text = "Kuwagata Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KuwagataSettings_FormClosing);
            this.Load += new System.EventHandler(this.KuwagataSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox VerseOutputText;
        private System.Windows.Forms.Button PathSelector1;
        private System.Windows.Forms.Button PathSelector2;
        private System.Windows.Forms.TextBox VersionOutputText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ApplyChanges;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DefaultBibleVersionText;
        private System.Windows.Forms.Label label4;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Kuwagata
{
    public partial class KuwagataSettings : Form
    {
        ConfigValues configValues = Program.cv;

        public KuwagataSettings()
        {
            InitializeComponent();
        }

        private void KuwagataSettings_Load(object sender, EventArgs e)
        {
        }

        private void ApplyChanges_Click(object sender, EventArgs e)
        {
            configValues.SaveToConfig(this);
            Cancel_Click(null, null);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void ShowNewFile(TextBox dumper, string filter)
        {
            
            filter = filter != null ? filter : "All files (*.*)|*.*";

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = filter;
                ofd.FilterIndex = 0;
                ofd.RestoreDirectory = false;
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    dumper.Text = ofd.FileName;
                }
            }
        }
        private void PathSelector2_Click(object sender, EventArgs e)
        {
            ShowNewFile(VersionOutputText, null);
        }

        private void PathSelector1_Click(object sender, EventArgs e)
        {
            ShowNewFile(VerseOutputText, null);
        }
    }
}

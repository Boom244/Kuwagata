using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using IniParser.Model;

namespace Kuwagata
{
    public partial class KuwagataSettings : Form
    {
        ConfigValues configValues = Program.cv;
        Dictionary<String, Control> UIElements = new Dictionary<string, Control>();
        public KuwagataSettings()
        {
            InitializeComponent();
            UIElements.Add("Output/VerseOutput", VerseOutputText);
            UIElements.Add("Output/VersionOutput", VersionOutputText);
            UIElements.Add("VerseConfig/DefaultLoadedVersion", DefaultBibleVersionText);
        }


        private void KuwagataSettings_Load(object sender, EventArgs e)
        {
            string[] secVal;
            foreach (KeyValuePair<string,Control> kvp in UIElements)
            {
                secVal = kvp.Key.Split('/');
                kvp.Value.Text = configValues.Data[secVal[0]][secVal[1]].Split(',')[1];
            }
        }

        private void ApplyChanges_Click(object sender, EventArgs e)
        {
            Dictionary<String, dynamic> sendDictionary = new Dictionary<string, dynamic>();
            //I just figured out a better way to do this, but this is proof of concept rn.
            //TODO: Fix later with an array of UI objects
            sendDictionary.Add("Output/VerseOutput", VerseOutputText.Text);
            sendDictionary.Add("Output/VersionOutput", VersionOutputText.Text);
            sendDictionary.Add("VerseConfig/DefaultLoadedVersion", DefaultBibleVersionText.Text);
            configValues.SaveToConfig(sendDictionary);
            Cancel_Click(null, null);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.MainWindow.isShowingSettings = false;
            Program.activeForms.Remove(this);
        }


        void ShowNewFile(TextBox dumper, string filter)
        {

            filter = filter != null ? filter : "All files (*.*)|*.*";

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.CurrentDirectory;
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
            ShowNewFile(VersionOutputText, "Text files (*.txt*)|*.txt*");
        }

        private void PathSelector1_Click(object sender, EventArgs e)
        {
            ShowNewFile(VerseOutputText, "Text files (*.txt*)|*.txt*");
        }
        private void KuwagataSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Cancel_Click(null, null);
        }
    }
}

using System;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;

namespace Kuwagata
{
    public partial class KuwagataMainWindow : Form
    {
        //Needs to be public in case someone wants to disable the GUI from inside it.
        public bool isShowingSettings = false;
        public bool isShowingVerses = false;
        ToolTip SuggestionTip = new ToolTip();
        KuwagataSettings ks = new KuwagataSettings();
       public VerseHolder vs = new VerseHolder();
        public KuwagataMainWindow()
        {
            InitializeComponent();
            this.KeyUp += KuwagataMainWindow_OnKeyUp;
        }

        private void KuwagataMainWindow_Load(object sender, EventArgs e)
        {
            ErrorSignal.Image = SystemIcons.Error.ToBitmap();
        }

        private void KuwagataMainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Program.TransformQueue(false);
                    break;
                case Keys.Right:
                    Program.TransformQueue(true);
                    break;
                case Keys.Enter:
                    if (this.Handle == Program.GetForegroundWindow())
                    {
                        string Verse = VerseTextBox.Text;
                        string Version = VersionTextBox.Text;
                        HandleRequest(Verse, Version);
                    }  
                    break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Retrieve_Click(object sender, EventArgs e)
        {
            string Verse = VerseTextBox.Text;
            string Version = VersionTextBox.Text;
            HandleRequest(Verse, Version);
        }

        private void SeekForward_Click(object sender, EventArgs e)
        {
            Program.TransformQueue(true);
        }

        private void SeekBack_Click(object sender, EventArgs e)
        {
            Kuwagata.Program.TransformQueue(false);
        }

        //This is the only place we're going to be using Program.StartNewRequest so it's better to handle error handling in-house, so to speak.
        private void HandleRequest(string Verse, string Version)
        {
           try
            {
                Program.StartNewRequest(Verse, Version);
                ErrorSignal.Visible = false;
            } catch 
            {
                ErrorSignal.Visible = true;
            }
        }
        private void VerseTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private void VersionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
             */
        }

        private void Settings_Click(object sender, EventArgs e)
        {

            if (isShowingSettings)
            {
                ks.Hide();
                Program.activeForms.Remove(ks);
            }
            else
            {
                ks.Show();
                Program.activeForms.Add(ks);
                
            }

            isShowingSettings = !isShowingSettings;
        }

        private void VerseTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SelectionShow_Click(object sender, EventArgs e)
        {
            if (isShowingVerses)
            {
                vs.Hide();
                Program.activeForms.Remove(vs);
            }
            else
            {
                vs.Show();
                Program.activeForms.Remove(vs);
            }
            isShowingVerses = !isShowingVerses;
        }

        private void ErrorSignal_MouseHover(object sender, EventArgs e)
        {
            if (!ErrorSignal.Visible) { return; }
            SuggestionTip.SetToolTip(ErrorSignal, "Failed to retrieve verse. \n Please re-type and ensure request is formatted correctly.");
        }
    }
}

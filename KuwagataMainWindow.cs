using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VerseScraper_CSharp_Edition
{
    public  partial class KuwagataMainWindow : Form
    {

       
        public KuwagataMainWindow()
        {
            InitializeComponent();
        }

        private void KuwagataMainWindow_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Retrieve_Click(object sender, EventArgs e)
        {
            string Verse = VerseTextBox.Text;
            string Version = VersionTextBox.Text;
            Kuwagata.Program.StartNewRequest(Verse, Version);
        }

        private void SeekForward_Click(object sender, EventArgs e)
        {
            Kuwagata.Program.TransformQueue(true);
        }

        private void SeekBack_Click(object sender, EventArgs e)
        {
            Kuwagata.Program.TransformQueue(false);
        }

        private void VerseTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

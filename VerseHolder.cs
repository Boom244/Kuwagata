using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuwagata
{
    public partial class VerseHolder : Form
    {
       

        public VerseHolder()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void AssignNewCells(string[] verseReferences, string[] verses)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i > verseReferences.Length; i++)
            {
                string[] information = { verseReferences[i], verses[i] };
                dataGridView1.Rows.Add(information);
            }
        }

    }
}

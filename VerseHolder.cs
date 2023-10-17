using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
            Program.JumpQueue(e.RowIndex);
            HighlightRow(e.RowIndex);
        }

        public void HighlightRow(int index)
        {
            dataGridView1.ClearSelection(); //there's probably a better way of doing this....
            dataGridView1.Rows[index].Selected = true;
        }
        public void AssignNewCells(string[] verseReferences, string[] verses)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < verseReferences.Length; i++)
            {
                string[] information = { verseReferences[i], verses[i] };
                dataGridView1.Rows.Add(information);
            }
            HighlightRow(0);
        }

        private void VerseHolder_Load(object sender, EventArgs e)
        {

        }

        private void VerseHolder_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            Program.MainWindow.isShowingVerses = false;
            Program.activeForms.Remove(this);
        }

        private void VerseHolder_KeyUp(object sender, KeyEventArgs e)
        {
            if (Program.GetForegroundWindow() != this.Handle)
            {
                return;
            }
            switch(e.KeyCode)
            {
                case Keys.Down:
                case Keys.Enter:
                    Program.TransformQueue(true,false);
                    break;
                case Keys.Up:
                    Program.TransformQueue(false,false);
                    break;
                default:
                    break;
            }
        }
    }
}

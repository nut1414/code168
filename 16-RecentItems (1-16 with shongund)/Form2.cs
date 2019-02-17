using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _16_RecentItems
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            this.TopMost = true;
            InitializeComponent();
            numericUpDown1.Value = 50;
            numericUpDown2.Value = 25;
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if ((uint)numericUpDown1.Value < 5 || (uint)numericUpDown2.Value < 5 || (uint)numericUpDown1.Value > 220 || (uint)numericUpDown2.Value > 220)
            {
                MessageBox.Show("Must be more than 5x5 or lower than 220x220.");
            }
            else
            {

                Program.Globals.sx = (uint)numericUpDown1.Value;
                Program.Globals.sy = (uint)numericUpDown2.Value;
                Form1 frm = new Form1();

                for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
                {
                    Program.Globals.level[k - 1] = 0;
                }
                for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
                {
                    Program.Globals.levelplay[k - 1] = 0;
                }
                Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
                Program.Globals.loadd = true;
                Program.Globals.create = true;
                for (int k = 1; k <= Program.Globals.sx * Program.Globals.sy; ++k)
                {
                    Program.Globals.levelshut[k - 1] = 0;
                }
                Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}

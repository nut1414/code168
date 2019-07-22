using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace _16_RecentItems
{ 
    public partial class Form4 : Form
    {       
        public Form4()
        {
            InitializeComponent();
            trackBar1.ValueChanged += new System.EventHandler(TrackBar1_ValueChanged);
        }
        private void TrackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            Program.Globals.shutnow = trackBar1.Value + 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int x = 1; x <= Program.Globals.sx; ++x)
            {
                for (int y = 1; y <= Program.Globals.sy; ++y)
                {
                    int posp = (y - 1) * ((int)Program.Globals.sx) + x;
                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            Program.Globals.levelshut[posp - 1] = 1;
                        }
                        else if (x % 2 == 1)
                        {
                            Program.Globals.levelshut[posp - 1] = 2;
                        }
                    }
                    else if (y % 2 == 1)
                    {
                        if (x % 2 == 0)
                        {
                            Program.Globals.levelshut[posp - 1] = 2;
                        }
                        else if (x % 2 == 1)
                        {
                            Program.Globals.levelshut[posp - 1] = 1;
                        }
                    }
                }
            }
            Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            for (int x = 1; x <= Program.Globals.sx; ++x)
            {
                for (int y = 1; y <= Program.Globals.sy; ++y)
                {
                    int posp = (y - 1) * ((int)Program.Globals.sx) + x;
                    if (x % 2 == 0)
                    {
                        Program.Globals.levelshut[posp - 1] = 1;
                    }
                    else if (x % 2 == 1)
                    {
                        Program.Globals.levelshut[posp - 1] = 2;
                    }

                }
            }
            Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
            {
                Program.Globals.levelplay[k - 1] = 0;
            }

            for (int x = 1; x <= Program.Globals.sx; ++x)
            {
                for (int y = 1; y <= Program.Globals.sy; ++y)
                {
                    int posp = (int)((y - 1) * (Program.Globals.sx) + x);

                    if (Program.Globals.levelshut[posp - 1] == 1)
                    {
                        int last = (int)(16 * Program.Globals.sx) * (y - 1) + ((x - 1) * 4);
                        for (int colum = 0; colum <= 3; ++colum)
                        {
                            for (int line = 0; line <= 3; ++line)
                            {
                                Program.Globals.levelplay[last + line] = Program.Globals.level[last + line];
                            }
                            last = (int)(last + (4 * Program.Globals.sx));
                        }
                    }

                }
            }
            Program.Globals.omode = false;
            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.levelplay, Program.Globals.sx * 4, Program.Globals.sy * 4);

            delay();
            delay1();
            delay2();
            delay3();
        }
        private void delay()
        {
            Program.Globals._delayTimer.Start();
            Program.Globals._delayTimer.Interval = 1000;
            Program.Globals._delayTimer.Elapsed += _delayTimer_Elapsed;
        }

        private void _delayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Program.Globals._delayTimer.Stop();
            for (int x = 1; x <= Program.Globals.sx; ++x)
            {
                for (int y = 1; y <= Program.Globals.sy; ++y)
                {
                    int posp = (int)((y - 1) * (Program.Globals.sx) + x);

                    if (Program.Globals.levelshut[posp - 1] == 2)
                    {
                        int last = (int)(16 * Program.Globals.sx) * (y - 1) + ((x - 1) * 4);
                        for (int colum = 0; colum <= 3; ++colum)
                        {
                            for (int line = 0; line <= 3; ++line)
                            {
                                Program.Globals.levelplay[last + line] = Program.Globals.level[last + line];
                            }
                            last = (int)(last + (4 * Program.Globals.sx));
                        }
                    }

                }
            }
            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.levelplay, Program.Globals.sx * 4, Program.Globals.sy * 4);
        }

        private void delay1()
        {
            Program.Globals._delayTimer0.Start();
            Program.Globals._delayTimer0.Interval = 2000;
            Program.Globals._delayTimer0.Elapsed += _delayTimer_Elapsed1;
        }

        private void _delayTimer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            Program.Globals._delayTimer0.Stop();
            for (int x = 1; x <= Program.Globals.sx; ++x)
            {
                for (int y = 1; y <= Program.Globals.sy; ++y)
                {
                    int posp = (int)((y - 1) * (Program.Globals.sx) + x);

                    if (Program.Globals.levelshut[posp - 1] == 3)
                    {
                        int last = (int)(16 * Program.Globals.sx) * (y - 1) + ((x - 1) * 4);
                        for (int colum = 0; colum <= 3; ++colum)
                        {
                            for (int line = 0; line <= 3; ++line)
                            {
                                Program.Globals.levelplay[last + line] = Program.Globals.level[last + line];
                            }
                            last = (int)(last + (4 * Program.Globals.sx));
                        }
                    }

                }
            }
            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.levelplay, Program.Globals.sx * 4, Program.Globals.sy * 4);
        }

        private void delay2()
        {
            Program.Globals._delayTimer1.Start();
            Program.Globals._delayTimer1.Interval = 3000;
            Program.Globals._delayTimer1.Elapsed += _delayTimer_Elapsed2;
        }

        private void _delayTimer_Elapsed2(object sender, System.Timers.ElapsedEventArgs e)
        {
            Program.Globals._delayTimer1.Stop();
            for (int x = 1; x <= Program.Globals.sx; ++x)
            {
                for (int y = 1; y <= Program.Globals.sy; ++y)
                {
                    int posp = (int)((y - 1) * (Program.Globals.sx) + x);

                    if (Program.Globals.levelshut[posp - 1] == 4)
                    {
                        int last = (int)(16 * Program.Globals.sx) * (y - 1) + ((x - 1) * 4);
                        for (int colum = 0; colum <= 3; ++colum)
                        {
                            for (int line = 0; line <= 3; ++line)
                            {
                                Program.Globals.levelplay[last + line] = Program.Globals.level[last + line];
                            }
                            last = (int)(last + (4 * Program.Globals.sx));
                        }
                    }

                }
            }
            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.levelplay, Program.Globals.sx * 4, Program.Globals.sy * 4);
        }

        private void delay3()
        {
            Program.Globals._delayTimer2.Start();
            Program.Globals._delayTimer2.Interval = 4000;
            Program.Globals._delayTimer2.Elapsed += _delayTimer_Elapsed3;
        }

        private void _delayTimer_Elapsed3(object sender, System.Timers.ElapsedEventArgs e)
        {
            Program.Globals._delayTimer2.Stop();
            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
            Program.Globals.omode = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Program.Globals.sx * Program.Globals.sy; ++i)
            {
                Program.Globals.levelshut[i] = (uint)Program.Globals.shutnow;
            }
            Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Program.Globals.sx * Program.Globals.sy; ++i)
            {
                Program.Globals.levelshut[i] = 0;
            }
            Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
        }

		private void Form4_Load(object sender, EventArgs e)
		{

		}
	}
}

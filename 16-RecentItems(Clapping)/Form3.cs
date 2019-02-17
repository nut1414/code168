using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;
using SFML;
using SFML.Audio;
using SFML.Window;
using SFML.Graphics;

namespace _16_RecentItems
{
    public partial class Form3 : Form
    {
        PictureBox[] pictureBox = new PictureBox[40];
        Label[] textt = new Label[40];
        Label[] numm = new Label[40];
        public Label nowcolor = new Label();
        string[] al = new string[40] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/" };
        public Form3()
        {
            Program.Globals.getco = System.IO.File.ReadAllLines(@"1-1.palette");
            this.TopLevel = true;
            InitializeComponent();

            nowcolor.Size = new Size(50, 30);
            nowcolor.Font = new System.Drawing.Font("BCCcodePGMF", 20);
            nowcolor.Text = "1";
            nowcolor.Parent = this.pictureBox1;

            this.FormClosing += closereset;
            for (int i = 0; i < 10; i++)
            {
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
                pictureBox[i] = new PictureBox();
                pictureBox[i].BackColor = color0;
                pictureBox[i].BorderStyle = BorderStyle.FixedSingle;
                pictureBox[i].Size = new Size(32, 32);

                textt[i] = new Label();
                textt[i].Size = new Size(20, 18);
                textt[i].Text = al[i].ToString();
                textt[i].Parent = pictureBox[i];
                textt[i].BackColor = System.Drawing.Color.Transparent;
                textt[i].Font = new System.Drawing.Font("BCCcodePGMF", 14);
                if (i <= 3)
                {
                    textt[i].ForeColor = System.Drawing.Color.Black;
                }
                if (i >= 3)
                {
                    textt[i].ForeColor = System.Drawing.Color.White;
                }

                numm[i] = new Label();
                numm[i].Size = new Size(30, 20);
                numm[i].Text = (i + 1).ToString();
                numm[i].Parent = pictureBox[i];
                numm[i].BackColor = System.Drawing.Color.Transparent;
                numm[i].Font = new System.Drawing.Font("BCCcodePGMF", 10);
                if (i <= 3)
                {
                    numm[i].ForeColor = System.Drawing.Color.Black;
                }
                if (i >= 3)
                {
                    numm[i].ForeColor = System.Drawing.Color.White;
                }

                if (0 <= i && i <= 9)
                {
                    pictureBox[i].Location = new Point(i * 37, 0);
                }
                else if (10 <= i && i <= 19)
                {
                    pictureBox[i].Location = new Point((i - 10) * 37, 37);
                }
                else if (20 <= i && i <= 29)
                {
                    pictureBox[i].Location = new Point((i - 20) * 37, 74);
                }
                else if (30 <= i && i <= 39)
                {
                    pictureBox[i].Location = new Point((i - 30) * 37, 111);
                }
                textt[i].Location = new Point(0, -2);
                numm[i].Location = new Point(12, 14);
                this.panel1.Controls.Add(pictureBox[i]);
                pictureBox[i].MouseDown += picBoxs_Click;
                textt[i].MouseDown += picBoxs_Click1;
                numm[i].MouseDown += picBoxs_Click2;
            }
        }
        void picBoxs_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var box = sender as PictureBox;
                int i = Array.IndexOf(pictureBox, box);
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
                pictureBox1.BackColor = color0;
                Program.Globals.curcolor = i;
                nowcolor.Text = (i+1).ToString();
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var box = sender as PictureBox;
                int i = Array.IndexOf(pictureBox, box);
                //string[] getco = System.IO.File.ReadAllLines(@"1-16.palette");
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
                pictureBox2.BackColor = color0;
                Program.Globals.curcolor1 = i;
            }

        }
        void picBoxs_Click1(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var box = sender as Label;
                int i = Array.IndexOf(textt, box);
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
                pictureBox1.BackColor = color0;
                Program.Globals.curcolor = i;
                nowcolor.Text = (i+1).ToString();
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var box = sender as Label;
                int i = Array.IndexOf(textt, box);
                //string[] getco = System.IO.File.ReadAllLines(@"1-16.palette");
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
                pictureBox2.BackColor = color0;
                Program.Globals.curcolor1 = i;
            }

        }
        void picBoxs_Click2(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var box = sender as Label;
                int i = Array.IndexOf(numm, box);
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
                pictureBox1.BackColor = color0;
                Program.Globals.curcolor = i;
                nowcolor.Text = (i+1).ToString();
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var box = sender as Label;
                int i = Array.IndexOf(numm, box);
                //string[] getco = System.IO.File.ReadAllLines(@"1-16.palette");
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
                pictureBox2.BackColor = color0;
                Program.Globals.curcolor1 = i;
            }

        }
        void closereset(object sender, FormClosingEventArgs e)
        {
            Program.Globals.curcolor = 0;
            Program.Globals.curcolor1 = 0;
        }
        public void changecolor(int c)
        {
            int red = int.Parse(Program.Globals.getco[c - 1].Substring(0, 2), NumberStyles.AllowHexSpecifier);
            int green = int.Parse(Program.Globals.getco[c - 1].Substring(2, 2), NumberStyles.AllowHexSpecifier);
            int blue = int.Parse(Program.Globals.getco[c - 1].Substring(4, 2), NumberStyles.AllowHexSpecifier);
            System.Drawing.Color color0 = System.Drawing.Color.FromArgb(red, green, blue);
            pictureBox1.BackColor = color0;
        }

        private void Form3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            Vector2f posss = Program.Globals.renderwindow.MapPixelToCoords(new Vector2i(Program.Globals.frm.panel1.Size.Width / 2, Program.Globals.frm.panel1.Size.Height / 2));
            if (e.KeyCode == Keys.OemOpenBrackets)
            {
                if (Program.Globals.zoomlimit <= 20)
                {
                    ++Program.Globals.track;
                    Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                    Vector2f mousepos = Program.Globals.renderwindow.MapPixelToCoords(Mouse.GetPosition() - Program.Globals.renderwindow.Position);
                    Program.Globals.view2.Zoom(0.9f);
                    if (mousepos.X <= 0)
                    {
                        mousepos.X = 0;
                    }
                    if (mousepos.X >= 12 * Program.Globals.sx)
                    {
                        mousepos.X = 12 * Program.Globals.sx;
                    }
                    if (mousepos.Y <= 0)
                    {
                        mousepos.Y = 0;
                    }
                    if (mousepos.Y >= 12 * Program.Globals.sy)
                    {
                        mousepos.Y = 12 * Program.Globals.sy;
                    }
                    Program.Globals.view2.Center = mousepos;
                    Program.Globals.renderwindow.SetView(Program.Globals.view2);
                    Mouse.SetPosition(Program.Globals.renderwindow.MapCoordsToPixel(mousepos) + Program.Globals.renderwindow.Position);
                    ++Program.Globals.zoomlimit;
                    //Globals.frm.vScrollBar1.Value = (int)(Globals.view2.Size.Y - Globals.view2.Center.Y);
                    if (Program.Globals.track >= 0 && posss.X >= 0 && posss.Y >= 0)
                    {
                        Program.Globals.frm.vScrollBar1.Maximum = (int)(Program.Globals.sy * 12 * (1.1 * Program.Globals.track)) / 100;
                        if (Program.Globals.frm.vScrollBar1.Maximum >= (int)(posss.Y * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.vScrollBar1.Value = (int)(posss.Y * (1.1 * Program.Globals.track)) / 100;
                        }
                        else if (Program.Globals.frm.vScrollBar1.Maximum <= (int)(posss.Y * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.vScrollBar1.Value = Program.Globals.frm.vScrollBar1.Maximum;
                        }
                        else if ((int)(posss.Y * (1.1 * Program.Globals.track)) / 100 < 0)
                        {
                            Program.Globals.frm.vScrollBar1.Value = 1;
                        }

                        Program.Globals.frm.hScrollBar1.Maximum = (int)(Program.Globals.sx * 12 * (1.1 * Program.Globals.track)) / 100;
                        if (Program.Globals.frm.hScrollBar1.Maximum >= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = (int)(posss.X * (1.1 * Program.Globals.track)) / 100;
                        }
                        else if (Program.Globals.frm.hScrollBar1.Maximum <= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = Program.Globals.frm.hScrollBar1.Maximum;
                        }
                        else if ((int)(posss.X * (1.1 * Program.Globals.track)) / 100 < 0)
                        {
                            Program.Globals.frm.hScrollBar1.Value = 1;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.OemCloseBrackets)
            {
                if (Program.Globals.zoomlimit >= -1)
                {
                    --Program.Globals.track;
                    Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                    Vector2f mousepos = Program.Globals.renderwindow.MapPixelToCoords(Mouse.GetPosition() - Program.Globals.renderwindow.Position);
                    Program.Globals.view2.Zoom(1.1f);
                    if (mousepos.X <= 0)
                    {
                        mousepos.X = 0;
                    }
                    if (mousepos.X >= 12 * Program.Globals.sx)
                    {
                        mousepos.X = 12 * Program.Globals.sx;
                    }
                    if (mousepos.Y <= 0)
                    {
                        mousepos.Y = 0;
                    }
                    if (mousepos.Y >= 12 * Program.Globals.sy)
                    {
                        mousepos.Y = 12 * Program.Globals.sy;
                    }
                    Program.Globals.view2.Center = mousepos;
                    Program.Globals.renderwindow.SetView(Program.Globals.view2);
                    Mouse.SetPosition(Program.Globals.renderwindow.MapCoordsToPixel(mousepos) + Program.Globals.renderwindow.Position);
                    Program.Globals.zoomlimit = Program.Globals.zoomlimit - 1;
                    if (Program.Globals.track >= 0 && posss.X >= 0 && posss.Y >= 0)
                    {
                        Program.Globals.frm.vScrollBar1.Maximum = (int)(Program.Globals.sy * 12 * (1.1 * Program.Globals.track)) / 100;
                        if (Program.Globals.frm.vScrollBar1.Maximum >= (int)(posss.Y * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.vScrollBar1.Value = (int)(posss.Y * (1.1 * Program.Globals.track)) / 100;
                        }
                        else if (Program.Globals.frm.vScrollBar1.Maximum <= (int)(posss.Y * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.vScrollBar1.Value = Program.Globals.frm.vScrollBar1.Maximum;
                        }
                        else if ((int)(posss.Y * (1.1 * Program.Globals.track)) / 100 < 0)
                        {
                            Program.Globals.frm.vScrollBar1.Value = 1;
                        }

                        Program.Globals.frm.hScrollBar1.Maximum = (int)(Program.Globals.sx * 12 * (1.1 * Program.Globals.track)) / 100;
                        if (Program.Globals.frm.hScrollBar1.Maximum >= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = (int)(posss.X * (1.1 * Program.Globals.track)) / 100;
                        }
                        else if (Program.Globals.frm.hScrollBar1.Maximum <= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = Program.Globals.frm.hScrollBar1.Maximum;
                        }
                        else if ((int)(posss.X * (1.1 * Program.Globals.track)) / 100 < 0)
                        {
                            Program.Globals.frm.hScrollBar1.Value = 1;
                        }
                    }
                }
            }
            if (Program.Globals.loadkey == true)
            {
                Form3 f3 = Application.OpenForms.OfType<Form3>().FirstOrDefault();
                if (e.KeyCode == Keys.D1)
                {
                    f3.changecolor(1);
                    Program.Globals.curcolor = 1;
                    nowcolor.Text = "1";
                }
                if (e.KeyCode == Keys.D2)
                {
                    f3.changecolor(2);
                    Program.Globals.curcolor = 2;
                    nowcolor.Text = "2";
                }
                if (e.KeyCode == Keys.D3)
                {
                    f3.changecolor(3);
                    Program.Globals.curcolor = 3;
                    nowcolor.Text = "3";
                }
                if (e.KeyCode == Keys.D4)
                {
                    f3.changecolor(4);
                    Program.Globals.curcolor = 4;
                    nowcolor.Text = "4";
                }
                if (e.KeyCode == Keys.D5)
                {
                    f3.changecolor(5);
                    Program.Globals.curcolor = 5;
                    nowcolor.Text = "5";
                }
                if (e.KeyCode == Keys.D6)
                {
                    f3.changecolor(6);
                    Program.Globals.curcolor = 6;
                    nowcolor.Text = "6";
                }
                if (e.KeyCode == Keys.D7)
                {
                    f3.changecolor(7);
                    Program.Globals.curcolor = 7;
                    nowcolor.Text = "7";
                }
                if (e.KeyCode == Keys.D8)
                {
                    f3.changecolor(8);
                    Program.Globals.curcolor = 8;
                    nowcolor.Text = "8";
                }
                if (e.KeyCode == Keys.D9)
                {
                    f3.changecolor(9);
                    Program.Globals.curcolor = 9;
                    nowcolor.Text = "9";
                }
                if (e.KeyCode == Keys.D0)
                {
                    f3.changecolor(10);
                    Program.Globals.curcolor = 10;
                    nowcolor.Text = "10";
                }
                if (e.KeyCode == Keys.Q)
                {
                    f3.changecolor(11);
                    Program.Globals.curcolor = 11;
                    nowcolor.Text = "11";
                }
                if (e.KeyCode == Keys.W)
                {
                    f3.changecolor(12);
                    Program.Globals.curcolor = 12;
                    nowcolor.Text = "12";
                }
                /*if (e.KeyCode == Keys.E)
                {
                    f3.changecolor(13);
                    Program.Globals.curcolor = 13;
                }
                if (e.KeyCode == Keys.R)
                {
                    f3.changecolor(14);
                    Program.Globals.curcolor = 14;
                }
                if (e.KeyCode == Keys.T)
                {
                    f3.changecolor(15);
                    Program.Globals.curcolor = 15;
                }
                if (e.KeyCode == Keys.Y)
                {
                    f3.changecolor(16);
                    Program.Globals.curcolor = 16;
                }
                if (e.KeyCode == Keys.U)
                {
                    f3.changecolor(17);
                    Program.Globals.curcolor = 17;
                }
                if (e.KeyCode == Keys.I)
                {
                    f3.changecolor(18);
                    Program.Globals.curcolor = 18;
                }
                if (e.KeyCode == Keys.O)
                {
                    f3.changecolor(19);
                    Program.Globals.curcolor = 19;
                }
                if (e.KeyCode == Keys.P)
                {
                    f3.changecolor(20);
                    Program.Globals.curcolor = 20;
                }
                if (e.KeyCode == Keys.A)
                {
                    f3.changecolor(21);
                    Program.Globals.curcolor = 21;
                }
                if (e.KeyCode == Keys.S)
                {
                    f3.changecolor(22);
                    Program.Globals.curcolor = 22;
                }
                if (e.KeyCode == Keys.D)
                {
                    f3.changecolor(23);
                    Program.Globals.curcolor = 23;
                }
                if (e.KeyCode == Keys.F)
                {
                    f3.changecolor(24);
                    Program.Globals.curcolor = 24;
                }
                if (e.KeyCode == Keys.G)
                {
                    f3.changecolor(25);
                    Program.Globals.curcolor = 25;
                }
                if (e.KeyCode == Keys.H)
                {
                    f3.changecolor(26);
                    Program.Globals.curcolor = 26;
                }
                if (e.KeyCode == Keys.J)
                {
                    f3.changecolor(27);
                    Program.Globals.curcolor = 27;
                }
                if (e.KeyCode == Keys.K)
                {
                    f3.changecolor(28);
                    Program.Globals.curcolor = 28;
                }
                if (e.KeyCode == Keys.L)
                {
                    f3.changecolor(29);
                    Program.Globals.curcolor = 29;
                }
                if (e.KeyCode == Keys.OemSemicolon)
                {
                    f3.changecolor(30);
                    Program.Globals.curcolor = 30;
                }
                if (e.KeyCode == Keys.Z)
                {
                    f3.changecolor(31);
                    Program.Globals.curcolor = 31;
                }
                if (e.KeyCode == Keys.X)
                {
                    f3.changecolor(32);
                    Program.Globals.curcolor = 32;
                }
                if (e.KeyCode == Keys.C)
                {
                    f3.changecolor(33);
                    Program.Globals.curcolor = 33;
                }
                if (e.KeyCode == Keys.V)
                {
                    f3.changecolor(34);
                    Program.Globals.curcolor = 34;
                }
                if (e.KeyCode == Keys.B)
                {
                    f3.changecolor(35);
                    Program.Globals.curcolor = 35;
                }
                if (e.KeyCode == Keys.N)
                {
                    f3.changecolor(36);
                    Program.Globals.curcolor = 36;
                }
                if (e.KeyCode == Keys.M)
                {
                    f3.changecolor(37);
                    Program.Globals.curcolor = 37;
                }
                if (e.KeyCode == Keys.Oemcomma)
                {
                    f3.changecolor(38);
                    Program.Globals.curcolor = 38;
                }
                if (e.KeyCode == Keys.OemPeriod)
                {
                    f3.changecolor(39);
                    Program.Globals.curcolor = 39;
                }

                if (e.KeyCode == Keys.OemQuestion)
                {
                    f3.changecolor(40);
                    Program.Globals.curcolor = 40;
                }*/
                --Program.Globals.curcolor;
            }
        }
    }

}


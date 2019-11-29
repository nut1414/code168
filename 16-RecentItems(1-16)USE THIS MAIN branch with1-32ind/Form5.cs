using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace _16_RecentItems
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            numericUpDown1.Value = 1;
            numericUpDown3.Value = 1;
            numericUpDown2.Value = Program.Globals.sx;
            numericUpDown4.Value = Program.Globals.sy;
            numericUpDown1.Maximum = Program.Globals.sx;
            numericUpDown2.Maximum = Program.Globals.sx;
            numericUpDown3.Maximum = Program.Globals.sy;
            numericUpDown4.Maximum = Program.Globals.sy;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            //printDlg.ShowDialog();

            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPage;

            printDlg.Document = doc;

            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }
        int? x = null;
        int? y = null;
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int startx = 1;
            int stopx = 25;
            int starty = 1;
            int stopy = 50;
            for (int check = 1; check <= Convert.ToInt32(listView1.Items.Count.ToString()); ++check)
            {
                string[] getload1 = System.IO.File.ReadAllLines(listView1.Items[check - 1].SubItems[1].Text.ToString());
                Program.Globals.dx[check - 1] = Convert.ToInt32(getload1[0]);
                Program.Globals.dy[check - 1] = Convert.ToInt32(getload1[1]);
            }
            if (radioButton1.Checked == true)
            {
                startx = 1;
                stopx = Program.Globals.dx.Max();
                starty = 1;
                stopy = Program.Globals.dy.Max();
            }
            else if (radioButton2.Checked == true)
            {
                startx = (int)numericUpDown1.Value;
                stopx = (int)numericUpDown2.Value;
                starty = (int)numericUpDown3.Value;
                stopy = (int)numericUpDown4.Value;
            }

            if (!x.HasValue)
            {
                x = startx;
            }
            if (!y.HasValue)
            {
                y = starty;
            }

            if (y <= stopy)
            {
                Bitmap a4 = new Bitmap(2480, 3508);
                a4.SetResolution(300, 300);
                for (int table = 1; table <= Convert.ToInt32(listView1.Items.Count.ToString()); ++table)
                {
                    int very = (int)table / 5;
                    int honx = (int)table % 5;
                    if (honx == 0)
                    {
                        honx = 5;
                        --very;
                    }
                    string[] getload1 = System.IO.File.ReadAllLines(listView1.Items[table - 1].SubItems[1].Text.ToString());

                    for (int k = 1; k <= 16 * Program.Globals.dx[table - 1] * Program.Globals.dy[table - 1]; ++k)
                    {
                        Program.Globals.levelprint[k - 1] = (uint)Convert.ToInt32(getload1[k + 1]);
                    }
                    int lastt = (int)(16 * Program.Globals.dx[table - 1] * Program.Globals.dy[table - 1]);
                    for (int k = lastt + 3; k <= (lastt + 2 + Program.Globals.dx[table - 1] * Program.Globals.dy[table - 1]); ++k)
                    {
                        Program.Globals.levelshut[k - lastt - 3] = (uint)Convert.ToInt32(getload1[k - 1]);
                    }

                    if (x <= Program.Globals.dx[table - 1] && y <= Program.Globals.dy[table - 1])
                    {
                        Bitmap myBitmap;
						//xcounter and ycounter
						if  ((y >= 1) && (y <= 25) && (x == 21)) //dude experiment

                        {
                            Graphics.FromImage(a4).DrawString("1:32*", new Font("Quark", 12), Brushes.Black, new PointF(2150, 130)); ;
                            myBitmap = new Bitmap("Res/table2.png");
                            Graphics g = Graphics.FromImage(myBitmap);
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            for (int j = 0; j <= 3; ++j)
                            {
                                for (int i = 0; i <= 5; ++i)//table ex
                                {
                                    int index = j * Program.Globals.dx[table - 1] * 4 + (int)(y - 1) * Program.Globals.dx[table - 1] * 16 + (int)(x - 1) * 4 + i;
                                    string number = (Program.Globals.levelprint[index] + 1).ToString();

                                    if (number.Length == 1)
                                    {
                                        g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(16 + i * 78, 9 + j * 78));
                                    }
                                    else if (number.Length == 2)
                                    {
                                        g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(-2 + i * 78, 9 + j * 78));
                                    }
                                }
                            }


                            /*
                            //MessageBox.Show("Before Res");
                            myBitmap = new Bitmap("Res/table2.png");
                            //MessageBox.Show("After Res");
                            Graphics g = Graphics.FromImage(myBitmap);
							g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
							for (int j = 0; j <= 3; ++j)
							{
								for (int i = 0; i <= 1; ++i)//extra table ex
								{
									//major revamp dude  now using one page instead of spliting on to 2 page
									int index = (j) * Program.Globals.dx[table - 1] * 4 + (int)(y - 1) * Program.Globals.dx[table - 1] * 16 + (int)(x - 1) * 4 + (i) + 2;
									string number = (Program.Globals.levelprint[index] + 1).ToString();

									if (number.Length == 1)
									{
										g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(16 + (i * 79), 9 + j * 78));
									}
									else if (number.Length == 2)
									{
										g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(-2 + (i * 79), 9 + j * 78));
									}
								}
							}
							for (int j = 0; j <= 3; ++j)
							{
								for (int i = 0; i <= 1; ++i)
								{
									int index = j * Program.Globals.dx[table - 1] * 4 + (int)(y - 1) * Program.Globals.dx[table - 1] * 16 + (int)(x - 1) * 4 + i;
									string number = (Program.Globals.levelprint[index] + 1).ToString();
                                    
									if (number.Length == 1)
									{
										g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(174 + (i * 79), 9 + j * 78));
									}
									else if (number.Length == 2)
									{
										g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(154 + (i * 79), 9 + j * 78));
									}
								}
							}
                            */

                        }


                        else
						{
                            myBitmap = new Bitmap("Res/table.png");
                            Graphics g = Graphics.FromImage(myBitmap);
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            for (int j = 0; j <= 3; ++j)
                            {
                                for (int i = 0; i <= 5; ++i)//table ex
                                {
                                    int index = j * Program.Globals.dx[table - 1] * 4 + (int)(y - 1) * Program.Globals.dx[table - 1] * 16 + (int)(x - 1) * 4 + i;
                                    string number = (Program.Globals.levelprint[index] + 1).ToString();

                                    if (number.Length == 1)
                                    {
                                        g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(16 + i * 78, 9 + j * 78));
                                    }
                                    else if (number.Length == 2)
                                    {
                                        g.DrawString(number, new Font("Quark", 14), Brushes.Black, new PointF(-2 + i * 78, 9 + j * 78));
                                    }
                                }
                            }
                        }
                        Bitmap myBitmap1 = new Bitmap("Res/table1.png");
                        Graphics g1 = Graphics.FromImage(myBitmap1);
                        g1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        int last1 = (int)(Program.Globals.dx[table - 1]) * ((int)y - 1) + (((int)x - 1));
                        if (Program.Globals.levelshut[last1] != 0)
                        {
                            g1.DrawString(Program.Globals.levelshut[last1].ToString(), new Font("Quark", 14), Brushes.Black, new PointF(12, 4));
                        }

                        Graphics a44 = Graphics.FromImage(a4);
                        a44.DrawImage(myBitmap, new Point((465) * (honx - 1) + 80, (410) * (very) + 225));
                        if (Program.Globals.levelshut[last1] != 0)
                        {
                            
                                a44.DrawImage(myBitmap1, new Point((30 + 435) * (honx - 1) + 400, (410) * (very) + 463));
                            
                        }
                        a44.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        
                            if (table.ToString().Length == 1)
                            {
                                a44.DrawString(table.ToString(), new Font("Quark", 14), Brushes.Black, new PointF((27 + 435) * (honx - 1) + 220, (410) * (very) + 161));
                            }
                            else if (table.ToString().Length == 2)
                            {
                                a44.DrawString(table.ToString(), new Font("Quark", 14), Brushes.Black, new PointF((27 + 435) * (honx - 1) + 210, (410) * (very) + 161));
                            }
                        
                        
                        myBitmap.Dispose();
                        myBitmap1.Dispose();
                    }
                }
                Graphics text = Graphics.FromImage(a4);
                text.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                text.DrawString("Bangkok Christian College(1:16)", new Font("Quark", 24), Brushes.Black, new PointF(30, 30));
                int column = (int)(x);
                int row = (int)(y - 1);
                string alpha = Char.ConvertFromUtf32(row + 65);
                string columns = column.ToString();
                text.DrawString("Row: " + alpha + " Column: " + columns, new Font("Quark", 24), Brushes.Black, new PointF(1450, 30));
                //a4.Save("d:/dd/" + "Row " + alpha + " Column " + columns + ".png", ImageFormat.Png);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.DrawImage(a4, 0, 0);
                a4.Dispose();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

                x++;

                if (x >= stopx + 1)
                {
                    y++;
                    x = null;
                }

                if (y <= stopy)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    x = null;
                    y = null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BCC 1:16 files |*.bcc16|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (listView1.Items.Count + openFileDialog1.FileNames.Length <= 40)
                {
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        listView1.Items.Add((listView1.Items.Count + 1).ToString());
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(file.ToString());
                    }
                }
                else if (listView1.Items.Count + openFileDialog1.FileNames.Length > 40)
                {
                    MessageBox.Show("Max is 40 codes per page.");
                }
            }
            for (int check = 1; check <= Convert.ToInt32(listView1.Items.Count.ToString()); ++check)
            {
                string[] getload1 = System.IO.File.ReadAllLines(listView1.Items[check - 1].SubItems[1].Text.ToString());
                Program.Globals.dx[check - 1] = Convert.ToInt32(getload1[0]);
                Program.Globals.dy[check - 1] = Convert.ToInt32(getload1[1]);

                numericUpDown1.Maximum = Program.Globals.dx.Max();
                numericUpDown2.Maximum = Program.Globals.dx.Max();
                numericUpDown3.Maximum = Program.Globals.dy.Max();
                numericUpDown4.Maximum = Program.Globals.dy.Max();
                numericUpDown1.Value = 1;
                numericUpDown3.Value = 1;
                numericUpDown2.Value = Program.Globals.dx.Max();
                numericUpDown4.Value = Program.Globals.dy.Max();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Selected)
                {
                    listView1.Items[i].Remove();
                }
            }
            for (int i = 0; i < listView1.Items.Count; ++i)
            {
                listView1.Items[i].Text = (i + 1).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem selected = listView1.SelectedItems[0];
                    int indx = selected.Index;
                    int totl = listView1.Items.Count;

                    if (indx == 0)
                    {
                        listView1.Items.Remove(selected);
                        listView1.Items.Insert(totl - 1, selected);
                    }
                    else
                    {
                        listView1.Items.Remove(selected);
                        listView1.Items.Insert(indx - 1, selected);
                    }
                }
                else
                {
                    MessageBox.Show("You can only move one item at a time.");
                }
            }
            catch (Exception ex)
            {

            }
            for (int i = 0; i < listView1.Items.Count; ++i)
            {
                listView1.Items[i].Text = (i + 1).ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem selected = listView1.SelectedItems[0];
                    int indx = selected.Index;
                    int totl = listView1.Items.Count;

                    if (indx == totl - 1)
                    {
                        listView1.Items.Remove(selected);
                        listView1.Items.Insert(0, selected);
                    }
                    else
                    {
                        listView1.Items.Remove(selected);
                        listView1.Items.Insert(indx + 1, selected);
                    }
                }
                else
                {
                    MessageBox.Show("You can only move one item at a time.");
                }
            }
            catch (Exception ex)
            {

            }
            for (int i = 0; i < listView1.Items.Count; ++i)
            {
                listView1.Items[i].Text = (i + 1).ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            //new
            checkBox1.Checked = false;
            checkBox1.Enabled = false;
            //
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            label3.Enabled = false;
            label4.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            numericUpDown3.Enabled = true;
            numericUpDown4.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            label4.Enabled = true;
            //new
            checkBox1.Enabled = true;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = "(" + Char.ConvertFromUtf32((int)(numericUpDown3.Value + 64)).ToString() + ")";
            //new
            if(checkBox1.Checked==true)
            {
                numericUpDown4.Value = numericUpDown3.Value;

            }
          
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            label6.Text = "(" + Char.ConvertFromUtf32((int)(numericUpDown4.Value + 64)).ToString() + ")";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
            
            
               Random random = new Random();
            Color rndColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            label7.ForeColor = rndColor;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = 1;
            numericUpDown4.Value = 1;
            if (checkBox1.Checked == true)
            {

                numericUpDown4.Value = numericUpDown3.Value;
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            numericUpDown1.Value = 21;
            numericUpDown2.Value = 21;
            numericUpDown3.Value = 1;
            numericUpDown4.Value = 13;
            if (checkBox1.Checked == true)
            {

                numericUpDown4.Value = numericUpDown3.Value;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            numericUpDown1.Value = 21;
            numericUpDown2.Value = 21;
            numericUpDown3.Value = 19;
            numericUpDown4.Value = 25;
            if (checkBox1.Checked == true)
            {
                
                numericUpDown4.Value = numericUpDown3.Value;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 50;
            if (checkBox1.Checked == true)
            {
                
                numericUpDown4.Value = numericUpDown3.Value;
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                numericUpDown4.Enabled = false;
                numericUpDown4.Value = numericUpDown3.Value;
            }
            if (checkBox1.Checked == false)
            {
                numericUpDown4.Enabled = true;
               // numericUpDown4.Value = numericUpDown3.Value;
            }
        }
    }
}











/*for(int page = 1 ; page <= Program.Globals.sx * Program.Globals.sy ; ++page)
{
    for(int table = 1 ; table <= Convert.ToInt32(listBox1.Items.Count.ToString()); ++table)
    {
        string filenow = listBox1.Items[table].ToString();
        string[] getload = System.IO.File.ReadAllLines(filenow); 
        Program.Globals.sx = (uint)Convert.ToInt32(getload[0]);
        Program.Globals.sy = (uint)Convert.ToInt32(getload[1]);
        for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
        {
            Program.Globals.level[k - 1] = Convert.ToInt32(getload[k + 1]);
        }
    }
}
int x = 1;
int y = 1;
Bitmap myBitmap = new Bitmap("d:/pro.png");
Graphics g = Graphics.FromImage(myBitmap);
g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
//g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

int last = (int)(16 * Program.Globals.sx * (y - 1) + x);
for (int j = 0; j <= 3; ++j)
{
    for (int i = 0; i <= 3; ++i)
    {
        string number = Program.Globals.level[last + i - 1].ToString();

        if (number.Length == 1)
        {
            g.DrawString(number, new Font("Fontcraft", 18), Brushes.Black, new PointF(4 + i * 25,  2 + j * 25));
        }
        else if (number.Length == 2)
        {
            g.DrawString(number, new Font("Fontcraft", 18), Brushes.Black, new PointF(-1 + i * 25, 2 + j * 25));
        }
    }
    last = (int)(last + Program.Globals.sx * 4);
}
Bitmap a4 = new Bitmap(794,1123);
Graphics a44 = Graphics.FromImage(a4);
a44.DrawImage(myBitmap, new Point(30, 10));
a4.Save("d:/dd.png", ImageFormat.Png);
            
*/
/* FileInfo newFile = new FileInfo("d:/f.xlsx");
            ExcelPackage package = new ExcelPackage(newFile);
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

            int filenumber = 13;

                int very = (int)filenumber / 5;
                int honx = (int)filenumber % 5;
                if (honx == 0)
                {
                    honx = 5;
                    --very;
                }
                Console.WriteLine(honx + very);
                int county = 3 + (very) * 6;
                int countx = 1 + (honx - 1) * 6;
                Console.WriteLine(county + countx);
                for (int x = 1; x <= Program.Globals.sx; ++x)
                {
                    for (int y = 1; y <= Program.Globals.sy; ++y)
                    {
                        int last = (int)(16 * Program.Globals.sx * (y - 1) + x);
                        for (int j = 0; j <= 3; ++j)
                        {
                            for (int i = 0; i <= 3; ++i)
                            {
                                worksheet.Cells[county + j, countx + i].Value = (int)(Program.Globals.level[last + i - 1]);
                            }
                            last = (int)(last + Program.Globals.sx * 4);
                            package.Save();
                        }

                    }
                }*/
/*int x = 1;
            int y = 1;
            Image<Bgr, Byte> img = new Image<Bgr, byte>(202, 202, new Bgr(255, 255, 255));
            MCvFont f = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX_SMALL, 1.0, 1.0);
            //img.Draw("Hello, world", ref f, new Point(10, 80), new Bgr(0, 0, 0));
            LineSegment2D line1 = new LineSegment2D(new Point(0,0), new Point(0, 201));
            img.Draw(line1, new Bgr(Color.Black), 1);
            LineSegment2D line2 = new LineSegment2D(new Point(0, 0), new Point(201, 0));
            img.Draw(line2, new Bgr(Color.Black), 1);
            LineSegment2D line3 = new LineSegment2D(new Point(201, 0), new Point(201, 201));
            img.Draw(line3, new Bgr(Color.Black), 1);
            LineSegment2D line4 = new LineSegment2D(new Point(0, 201), new Point(201, 201));
            img.Draw(line4, new Bgr(Color.Black), 1);
            for (int i = 0; i <= 2; ++i)
            {
                LineSegment2D line = new LineSegment2D(new Point((i + 1) * 50 + 1, 0), new Point((i + 1) * 50 + 1, 200));
                img.Draw(line, new Bgr(Color.Black), 1);
            }
            for (int i = 0; i <= 2; ++i)
            {
                LineSegment2D line = new LineSegment2D(new Point(0, (i + 1) * 50 + 1), new Point(200, (i + 1) * 50 + 1));
                img.Draw(line, new Bgr(Color.Black), 1);
            }

            int last = (int)(16 * Program.Globals.sx * (y - 1) + x);
            for (int j = 0; j <= 3; ++j)
            {
                for (int i = 0; i <= 3; ++i)
                {
                    img.Draw(Program.Globals.level[last + i - 1].ToString(), ref f, new Point(i * 50, i * 50), new Bgr(0, 0, 0));
                }
                last = (int)(last + Program.Globals.sx * 4);
            }
            img.SmoothGaussian(7, 7, 5, 5);
            img.Save("d:/d.png");*/
/*Image<Bgr, Byte> img = new Image<Bgr, byte>(102, 102, new Bgr(255, 255, 255));
            MCvFont f = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX_SMALL, 1.0, 1.0);
            //img.Draw("Hello, world", ref f, new Point(10, 80), new Bgr(0, 0, 0));
            LineSegment2D line1 = new LineSegment2D(new Point(0, 0), new Point(0, 101));
            img.Draw(line1, new Bgr(Color.Black), 1);
            LineSegment2D line2 = new LineSegment2D(new Point(0, 0), new Point(101, 0));
            img.Draw(line2, new Bgr(Color.Black), 1);
            LineSegment2D line3 = new LineSegment2D(new Point(101, 0), new Point(101, 101));
            img.Draw(line3, new Bgr(Color.Black), 1);
            LineSegment2D line4 = new LineSegment2D(new Point(0, 101), new Point(101, 101));
            img.Draw(line4, new Bgr(Color.Black), 1);
            for (int i = 0; i <= 2; ++i)
            {
                LineSegment2D line = new LineSegment2D(new Point((i + 1) * 25 + 1, 0), new Point((i + 1) * 25 + 1, 100));
                img.Draw(line, new Bgr(Color.Black), 1);
            }
            for (int i = 0; i <= 2; ++i)
            {
                LineSegment2D line = new LineSegment2D(new Point(0, (i + 1) * 25 + 1), new Point(100, (i + 1) * 25 + 1));
                img.Draw(line, new Bgr(Color.Black), 1);
            }
            img.Save("d:/pro.png");*/
/*            int startx = 1;
            int stopx = 25;
            int starty = 1;
            int stopy = 50;
            for (int check = 1; check <= Convert.ToInt32(listView1.Items.Count.ToString()); ++check)
            {
                string[] getload1 = System.IO.File.ReadAllLines(listView1.Items[check - 1].SubItems[1].Text.ToString());
                Program.Globals.dx[check - 1] = Convert.ToInt32(getload1[0]);
                Program.Globals.dy[check - 1] = Convert.ToInt32(getload1[1]);
            }
            if(radioButton1.Checked == true)
            {
                startx = 1;
                stopx = Program.Globals.dx.Max();
                starty = 1;
                stopy = Program.Globals.dy.Max();
            }
            else if (radioButton2.Checked == true)
            {
                startx = (int)numericUpDown1.Value;
                stopx = (int)numericUpDown2.Value;
                starty = (int)numericUpDown3.Value;
                stopy = (int)numericUpDown4.Value;
            }
            for (int x = startx; x <= stopx; ++x)
            {
                for (int y = starty; y <= stopy; ++y)
                {
                    Bitmap a4 = new Bitmap(794, 1123);
                    for (int table = 1; table <= Convert.ToInt32(listView1.Items.Count.ToString()); ++table)
                    {
                        int very = (int)table / 5;
                        int honx = (int)table % 5;
                        if (honx == 0)
                        {
                            honx = 5;
                            --very;
                        }

                        //load file to levelprint[table]
                        string[] getload1 = System.IO.File.ReadAllLines(listView1.Items[table - 1].SubItems[1].Text.ToString());

                        for (int k = 1; k <= 16 * Program.Globals.dx[table - 1] * Program.Globals.dy[table - 1]; ++k)
                        {
                            Program.Globals.levelplay[k - 1] = Convert.ToInt32(getload1[k + 1]);
                        }

                        if (x <= Program.Globals.dx[table - 1] && y <= Program.Globals.dy[table - 1])
                        {
                            Bitmap myBitmap = new Bitmap("d:/pro.png");
                            Graphics g = Graphics.FromImage(myBitmap);
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            int last = (int)(16 * Program.Globals.dx[table - 1]) * (y - 1) + ((x - 1) * 4);
                            for (int j = 0; j <= 3; ++j)
                            {
                                for (int i = 0; i <= 3; ++i)
                                {
                                    string number = (Program.Globals.levelplay[last + i] + 1).ToString();

                                    if (number.Length == 1)
                                    {
                                        g.DrawString(number, new Font("Fontcraft", 18), Brushes.Black, new PointF(4 + i * 25, 2 + j * 25));
                                    }
                                    else if (number.Length == 2)
                                    {
                                        g.DrawString(number, new Font("Fontcraft", 18), Brushes.Black, new PointF(-1 + i * 25, 2 + j * 25));
                                    }
                                }
                                last = (int)(last + (Program.Globals.dx[table - 1] * 4));
                            }
                            //place bitmap on a4
                            Graphics a44 = Graphics.FromImage(a4);
                            a44.DrawImage(myBitmap, new Point((30 + 125) * (honx - 1) + 35, (125 + 24) * (very) + 90));

                        }


                    }
                    //save and send to print
                    Graphics text = Graphics.FromImage(a4);
                    text.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    text.DrawString("Bangkok Christian College", new Font("Fontcraft", 30), Brushes.Black, new PointF(10, 10));
                    int column = (int)(x);
                    int row = (int)(y - 1);
                    string alpha = Char.ConvertFromUtf32(row + 65);
                    string columns = column.ToString();
                    text.DrawString("Row: " + alpha + "  Column: " + columns, new Font("Fontcraft", 30), Brushes.Black, new PointF(500, 10));
                    a4.Save("d:/dd/" + "Row " + alpha + " Column " + columns + ".png", ImageFormat.Png);
                }
            }*/
/*int colorIndex = 0;
            MessageBox.Show("Select palette color, Then select image.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            string[] getco = new string[40];
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

            openFileDialog1.InitialDirectory = AppPath;
            openFileDialog1.DefaultExt = "palette";
            openFileDialog1.Filter = "Palette files |*.palette|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                getco = System.IO.File.ReadAllLines(openFileDialog1.FileName);
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

            Bitmap pic = null;
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            string AppPath2 = Path.GetDirectoryName(Application.ExecutablePath);

            openFileDialog1.InitialDirectory = AppPath2;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.gif, *.png) | *.jpg; *.jpeg; *.gif; *.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            DialogResult result2 = openFileDialog2.ShowDialog();
            if (result2 == DialogResult.OK)
            {
                pic = new Bitmap(openFileDialog2.FileName);
            }
            else if (result2 == DialogResult.Cancel)
            {
                return;
            }

            Color[] _colors = new Color[40];
            for (int i = 0; i < 40; i++)
            {
                int red = int.Parse(getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                _colors[i] = Color.FromArgb(red, green, blue);
            }

            for (int y = 1; y <= pic.Height; ++y)
            {
                for (int x = 1; x <= pic.Width; ++x)
                {
                    Color color = pic.GetPixel(x - 1, y - 1);
                    int leastDistance = int.MaxValue;
                    int red = Convert.ToInt32(color.R.ToString());
                    int green = Convert.ToInt32(color.G.ToString());
                    int blue = Convert.ToInt32(color.B.ToString());


                    for (int index = 0; index < _colors.Length; index++)
                    {
                        Color paletteColor = _colors[index];

                        int redDistance = paletteColor.R - red;
                        int greenDistance = paletteColor.G - green;
                        int blueDistance = paletteColor.B - blue;

                        int distance = (redDistance * redDistance) +
                                            (greenDistance * greenDistance) +
                                            (blueDistance * blueDistance);

                        if (distance < leastDistance)
                        {
                            colorIndex = index;
                            leastDistance = distance;
                            if (0 == distance)
                                break;
                        }
                    }
                    int posp = (y - 1) * (pic.Width * 4) + (x - 1);
                    Program.Globals.level[posp] = colorIndex;
                }
            }*/

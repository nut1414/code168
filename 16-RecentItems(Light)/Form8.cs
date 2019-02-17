using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace _16_RecentItems
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Globals.text = textBox1.Text;
            this.Close();

            FontDialog dlg = new FontDialog();
            dlg.Font = new System.Drawing.Font("BCCcodePGMF", 8);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int colorIndex = 0;
                Bitmap measureBmp = new Bitmap(1, 1);
                var measureGraphics = Graphics.FromImage(measureBmp);
                var stringSize = measureGraphics.MeasureString(Program.Globals.text, new System.Drawing.Font(dlg.Font.Name, dlg.Font.Size));
                int xs = (int)stringSize.Width;
                int ys = (int)stringSize.Height;

                measureBmp.Dispose();
                Bitmap textplot = new Bitmap(xs, ys);
                Graphics g = Graphics.FromImage(textplot);
                Rectangle ImageSize = new Rectangle(0, 0, xs, ys);
                g.FillRectangle(Brushes.White, ImageSize);
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(Program.Globals.text, new System.Drawing.Font(dlg.Font.Name, dlg.Font.Size), Brushes.Green, new PointF(0, 0));
                //TextRenderer.DrawText(g, Program.Globals.text, new System.Drawing.Font(dlg.Font.Name, dlg.Font.Size) , new Point(0, 0), SystemColors.ControlText);

                int rightw = textplot.Width - 1;
                int leftw = 0;
                int righth = textplot.Height - 1;
                int lefth = 0;

                for (int i = textplot.Width - 1; i >= 0; i--)
                {
                    for (int j = textplot.Height - 1; j >= 0; j--)
                    {
                        if (textplot.GetPixel(i, j).R != 255 && textplot.GetPixel(i, j).G != 255 && textplot.GetPixel(i, j).B != 255)
                        {
                            if(i <= rightw)
                            {
                                rightw = i;
                            }
                            if (leftw <= i)
                            {
                                leftw = i;
                            }
                            if(j <= righth)
                            {
                                righth = j;
                            }
                            if (lefth <= j)
                            {
                                lefth = j;
                            }
                        }
                    }
                }
                ++rightw;
                Bitmap textplott = new Bitmap(textplot.Width - (textplot.Width - leftw - 2) - rightw + 4, ys - righth - (textplot.Height - lefth - 1));
                Graphics gt = Graphics.FromImage(textplott);
                Rectangle ImageSizet = new Rectangle(0, 0, textplot.Width - (textplot.Width - leftw - 2) - rightw+ 4, ys - righth - (textplot.Height - lefth - 1));
                gt.FillRectangle(Brushes.White, ImageSizet);
                gt.DrawString(Program.Globals.text, new System.Drawing.Font(dlg.Font.Name, dlg.Font.Size), Brushes.Black, new PointF(-(textplot.Width - leftw - 2) + 3, -righth));
                textplott.Save("d:/dd.png", System.Drawing.Imaging.ImageFormat.Png);

                System.Drawing.Color[] _colors = new System.Drawing.Color[12];
                for (int i = 0; i < 12; i++)
                {
                    int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                    int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                    int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                    _colors[i] = System.Drawing.Color.FromArgb(red, green, blue);
                }

                for (int y = 1; y <= textplott.Height; ++y)
                {
                    for (int x = 1; x <= textplott.Width; ++x)
                    {
                        System.Drawing.Color color = textplott.GetPixel(x - 1, y - 1);
                        int leastDistance = int.MaxValue;
                        int red = Convert.ToInt32(color.R.ToString());
                        int green = Convert.ToInt32(color.G.ToString());
                        int blue = Convert.ToInt32(color.B.ToString());


                        for (int index = 0; index < _colors.Length; index++)
                        {
                            System.Drawing.Color paletteColor = _colors[index];

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
                        int posp1 = (y - 1 + (int)(Program.Globals.yy / 3)) * (int)(Program.Globals.sx) + (x - 1 + (int)(Program.Globals.xx / 3));
                        if (posp1 < Program.Globals.sx * Program.Globals.sy && colorIndex != 0)
                        {
                            Program.Globals.level[posp1] = (uint)Program.Globals.curcolor;
                        }
                    }
                }
                textplot.Dispose();
                textplott.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

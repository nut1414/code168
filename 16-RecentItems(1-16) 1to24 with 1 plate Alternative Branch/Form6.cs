using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;

namespace _16_RecentItems
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        bool success2 = false;
        public uint colorIndex = 0;
        Bitmap pic = null;
        string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();

            openFileDialog2.InitialDirectory = AppPath;
            openFileDialog2.Filter = "Image files (*.jpg, *.jpeg, *.gif, *.png, *.bmp) | *.jpg; *.jpeg; *.gif; *.png; *.bmp";
            openFileDialog2.FilterIndex = 1;
            openFileDialog2.Multiselect = false;

            DialogResult result2 = openFileDialog2.ShowDialog();
            if (result2 == DialogResult.OK)
            {
                pic = new Bitmap(openFileDialog2.FileName);
                numericUpDown1.Value = pic.Width;
                numericUpDown2.Value = pic.Height;
                success2 = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (success2 == true)
            {
                Bitmap temp = new Bitmap((int)numericUpDown1.Value, (int)numericUpDown2.Value, PixelFormat.Format32bppRgb);
                Graphics newImage = Graphics.FromImage(temp);
                newImage.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                newImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newImage.DrawImage(pic, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown1.Value , (int)numericUpDown2.Value );
                success2 = false;

                Color[] _colors = new Color[40];
                for (int i = 0; i < 40; i++)
                {
                    int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                    int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                    int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                    _colors[i] = Color.FromArgb(red, green, blue);
                }

                for (int y = 1; y <= temp.Height; ++y)
                {
                    for (int x = 1; x <= temp.Width; ++x)
                    {
                        Color color = temp.GetPixel(x - 1, y - 1);
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
                                colorIndex = (uint)index;
                                leastDistance = distance;
                                if (0 == distance)
                                    break;
                            }
                        }
                        int posp = (y - 1) * ((int)Program.Globals.sx * 4) + (x - 1);
                        if (posp < Program.Globals.sx * Program.Globals.sy * 16)
                        {
                            Program.Globals.level[posp] = colorIndex;
                        } 
                    }
                }
                Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
                this.Close();
            }
            else if (success2 == false)
            {
                MessageBox.Show("Missing image");
            }
  

        }
    }
}

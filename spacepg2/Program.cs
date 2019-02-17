using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SFML;
using SFML.Audio;
using SFML.Window;
using SFML.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace _16_RecentItems
{
    public static class Program
    {
        public class Globals
        {
            public static DrawingSurface rendersurface = new DrawingSurface();
            public static SFML.Graphics.RenderWindow renderwindow = new SFML.Graphics.RenderWindow(Globals.rendersurface.Handle);
            public static SFML.Graphics.View view2;
            public static uint[] level = new uint[800000];
            public static uint[,] level1 = new uint[800000,25];
            public static uint[] levelshut = new uint[800000];
            public static uint[] levelplay = new uint[800000];
            public static uint[] levelprint = new uint[800000];
            public static uint[] levelshow = new uint[800000];
            public static uint[] temp = new uint[800000];
            public static uint[] numFrame = new uint[25];
            public static uint[] levelframe = new uint[25];
            public static uint sx = 50;
            public static uint sy = 25;
            public static TileMap map = new TileMap();
            public static Openshut map1 = new Openshut();
            public static Vector2u tiless = new Vector2u(12, 12);
            public static Vector2u opensize = new Vector2u(12, 12);
            public static Form1 frm = new Form1();
            public static Form3 frm1 = new Form3();
            public static bool create = false;
            public static bool loadd = false;
            public static int curcolor = 0;
            public static int curcolor1 = 0;
            public static int zoomlimit = 7;
            public static bool showb = true;
            public static bool nowpaint = false;
            public static bool nownorpaint = true;
            public static bool noweraser = false;
            public static bool nowdimmer = false;
            public static bool nowtext = false;
            public static string[] getco = new string[] { };
            public static int shutnow = 1;
            public static bool savechange = false;
            public static int check;
            public static int step = 2;
            public static System.Timers.Timer _delayTimer = new System.Timers.Timer();
            public static System.Timers.Timer _delayTimer0 = new System.Timers.Timer();
            public static System.Timers.Timer _delayTimer1 = new System.Timers.Timer();
            public static System.Timers.Timer _delayTimer2 = new System.Timers.Timer();
            public static bool sup = false;
            public static bool sdown = false;
            public static int kw;
            public static int resizex = 1280;
            public static int resizey = 573;
            public static int filetype = 0;
            public static int[] dx = new int[40];
            public static int[] dy = new int[40];
            public static string text = "";
            public static bool loadkey = false;
            public static float xx;
            public static float yy;
            public static int track = 0;
            public static Vector2f trackpoint;
            public static string fcurrent;
            public static int page = 1;
            public static int maxpage = 1;
            public static Form10 fcontrol = null;
        }
        public class TileMap : Drawable
        {
            public bool load(string tileset, SFML.Window.Vector2u tileSize, uint[] tiles, uint width, uint height)
            {
                m_vertices.PrimitiveType = SFML.Graphics.PrimitiveType.Quads;
                m_vertices.Resize(width * height * 4);


                for (uint i = 0; i < width; ++i)
                {
                    for (uint j = 0; j < height; ++j)
                    {

                        uint tileNumber = tiles[i + j * width];

                        long tu = tileNumber % (m_tileset.Size.X / tileSize.X);
                        long tv = tileNumber / (m_tileset.Size.X / tileSize.X);


                        uint index = (i + j * width) * 4;


                        m_vertices[index + 0] = new Vertex(new Vector2f(i * tileSize.X, j * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                        m_vertices[index + 1] = new Vertex(new Vector2f((i + 1) * tileSize.X, j * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                        m_vertices[index + 2] = new Vertex(new Vector2f((i + 1) * tileSize.X, (j + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                        m_vertices[index + 3] = new Vertex(new Vector2f(i * tileSize.X, (j + 1) * tileSize.Y), new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y));
                    }
                }

                return true;
            }

            void Drawable.Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
            {

                states.Texture = m_tileset;

                target.Draw(m_vertices, states);
            }

            public SFML.Graphics.Texture m_tileset = new SFML.Graphics.Texture(@"Res/tileset.png");
            private SFML.Graphics.VertexArray m_vertices = new SFML.Graphics.VertexArray();

        }
        public class Openshut : Drawable
        {
            public bool load(string tileset, SFML.Window.Vector2u tileSize, uint[] tiles, uint width, uint height)
            {
                m_vertices.PrimitiveType = SFML.Graphics.PrimitiveType.Quads;
                m_vertices.Resize(width * height * 4);
                for (uint i = 0; i < width; ++i)
                {
                    for (uint j = 0; j < height; ++j)   
                    {

                        uint tileNumber = tiles[i + j * width];

                        long tu = tileNumber % (m_tileset.Size.X / tileSize.X);
                        long tv = tileNumber / (m_tileset.Size.X / tileSize.X);


                        uint index = (i + j * width) * 4;


                        m_vertices[index + 0] = new Vertex(new Vector2f(i * tileSize.X, j * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                        m_vertices[index + 1] = new Vertex(new Vector2f((i + 1) * tileSize.X, j * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                        m_vertices[index + 2] = new Vertex(new Vector2f((i + 1) * tileSize.X, (j + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                        m_vertices[index + 3] = new Vertex(new Vector2f(i * tileSize.X, (j + 1) * tileSize.Y), new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y));
                    }
                }

                return true;
            }

            void Drawable.Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
            {

                states.Texture = m_tileset;

                target.Draw(m_vertices, states);
            }

            public SFML.Graphics.Texture m_tileset = new SFML.Graphics.Texture(@"Res/openshut.png");
            private SFML.Graphics.VertexArray m_vertices = new SFML.Graphics.VertexArray();

        }
        public class table2 : Drawable
        {
            public bool loadd()
            {
                t1.PrimitiveType = SFML.Graphics.PrimitiveType.Lines;
                t1.Resize(150 * 150 * 2);
                for (uint j = 0; j <= Globals.sx; ++j)
                {
                    uint index1 = j * 2;
                    t1[index1] = new Vertex(new Vector2f(j * 12, 0), SFML.Graphics.Color.Black);
                    t1[index1 + 1] = new Vertex(new Vector2f(j * 12, Globals.sy * 12), SFML.Graphics.Color.Black);
                }
                return true;
            }

            void Drawable.Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
            {
                target.Draw(t1, states);
            }
            public SFML.Graphics.VertexArray t1 = new SFML.Graphics.VertexArray();

        }
        public class table2y : Drawable
        {
            public bool loadd()
            {
                t1.PrimitiveType = SFML.Graphics.PrimitiveType.Lines;
                t1.Resize(150 * 150 * 2);
                for (uint j = 0; j <= Globals.sy; ++j)
                {
                    uint index1 = j * 2;
                    t1[index1] = new Vertex(new Vector2f(0, j * 12), SFML.Graphics.Color.Black);
                    t1[index1 + 1] = new Vertex(new Vector2f(Globals.sx * 12, j * 12), SFML.Graphics.Color.Black);
                }
                return true;
            }

            void Drawable.Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
            {
                target.Draw(t1, states);
            }
            public SFML.Graphics.VertexArray t1 = new SFML.Graphics.VertexArray();

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] 
        static void Main()
        {
            for (int k = 1; k <= Program.Globals.sx * Program.Globals.sy; ++k)
            {
                Program.Globals.levelshut[k - 1] = 0;
            }
            for (int k = 1; k <= Program.Globals.sx * Program.Globals.sy; ++k)
            {
                Program.Globals.levelplay[k - 1] = 0;
            }

            Application.EnableVisualStyles();
            Globals.frm.Size = new System.Drawing.Size(1280, 720);

            Globals.rendersurface.Size = new System.Drawing.Size(Globals.resizex, Globals.resizey);
            Globals.frm.panel1.Controls.Add(Globals.rendersurface);
            Globals.rendersurface.Location = new System.Drawing.Point(0, 0);
            Globals.rendersurface.Select();
            table2 xl = new table2();
            table2y xll = new table2y();
            Globals.frm.Resize += new EventHandler(fitto);
            Globals.rendersurface.MouseDown += new System.Windows.Forms.MouseEventHandler(paint);
            Globals.rendersurface.MouseMove += new System.Windows.Forms.MouseEventHandler(tell);
            Globals.frm.panel1.SizeChanged += new EventHandler(resize1);

            Globals.frm.Show();


            Form3 key = new Form3();
            key.Show(Program.Globals.frm);
            Program.Globals.loadkey = true;
            key.Location = new Point(Program.Globals.frm.Size.Width - key.Size.Width - 35, Program.Globals.frm.Size.Height - key.Size.Height - 60);


            Globals.view2 = new SFML.Graphics.View(new FloatRect(1.0f, 1.0f, Globals.resizex, Globals.resizey));
            Vector2f center = new Vector2f(Globals.sx * 6, Globals.sy * 6);
            Globals.view2.Center = center;
            while (Globals.frm.Visible)
            {
                System.Windows.Forms.Application.DoEvents();
                Globals.renderwindow.DispatchEvents();
                Globals.renderwindow.Clear(new SFML.Graphics.Color(224, 224, 224));
                if (Globals.create == true)
                {
                    if (Globals.loadd == true)
                    {
                        xl.t1.Clear();
                        xll.t1.Clear();
                        xl.loadd();
                        xll.loadd();
                        Vector2f centerload = new Vector2f(Globals.sx * 6, Globals.sy * 6);
                        Globals.view2.Center = centerload;
                        Globals.loadd = false;
                    }
                    Globals.renderwindow.Draw(Globals.map);
                    if (Globals.showb == true)
                    {
                        Globals.renderwindow.Draw(xl);
                        Globals.renderwindow.Draw(xll);
                    }

                }
                Globals.renderwindow.Display();
                Globals.renderwindow.SetView(Globals.view2);
            }
        }
        static void resize1(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            Globals.resizex = control.Size.Width;
            Globals.resizey = control.Size.Height;
            Globals.zoomlimit = 5;
            Globals.view2 = Globals.renderwindow.GetView();
            Globals.rendersurface.Size = new Size(Globals.resizex, Globals.resizey);
            FloatRect visibleArea = new FloatRect(0, 0, Globals.resizex, Globals.resizey);
            Globals.renderwindow.SetView(new SFML.Graphics.View(visibleArea));
            Vector2f center = new Vector2f(Globals.sx * 6, Globals.sy * 6);
            Globals.view2.Center = center;
            Globals.renderwindow.SetView(Globals.view2);
            Program.Globals.frm.vScrollBar1.Maximum = 1;
            Program.Globals.frm.hScrollBar1.Maximum = 1;
            Program.Globals.frm.vScrollBar1.Value = Program.Globals.frm.vScrollBar1.Maximum;
            Program.Globals.frm.hScrollBar1.Value = Program.Globals.frm.hScrollBar1.Maximum;
        }
        static void paint(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                    Vector2i pixelpos = new Vector2i(e.X, e.Y);
                    Vector2f worldPos = Globals.renderwindow.MapPixelToCoords(pixelpos);

                    float ix = worldPos.X / 12;
                    //++ix;

                    float iy = worldPos.Y / 12;
                    //++iy;

                    uint ixx = (uint)ix;
                    uint iyy = (uint)iy;

                    ++ixx;
                    ++iyy;

                    uint posp = (iyy - 1) * (Globals.sx) + ixx;

                    if (Globals.nowpaint == true)
                    {
                        if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                        {

                            Globals.level1[(int)posp - 1, Globals.page] = (uint)Globals.curcolor;
                            Globals.savechange = true;
                        }
                    }
                    if (Globals.nownorpaint == true)
                    {
                        if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                        {
                            if (Globals.level1[(int)posp - 1, Globals.page] != (uint)Globals.curcolor)
                            {
                                Globals.level1[(int)posp - 1, Globals.page] = (uint)Globals.curcolor;
                                Globals.savechange = true;
                            }
                        }
                    }
                    else if (Globals.noweraser == true)
                    {
                        if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                        {
                            if (Globals.level[(int)posp - 1] != 0)
                            {
                                Globals.level1[(int)posp - 1, Globals.page] = 0;
                            }
                            Globals.savechange = true;
                        }
                    }
                    else if (Globals.nowdimmer == true)
                    {
                        if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                        {
                            if (Globals.level1[(int)posp - 1, Globals.page] > 0)
                            {
                                uint dimm = Globals.level1[(int)posp - 1, Globals.page] - 1;
                                Globals.level1[(int)posp - 1, Globals.page] = dimm;
                                Globals.savechange = true;
                            }
                        }
                    }
                    else if (Globals.nowtext == true)
                    {
                        if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                        {
                            if (Globals.getco.Length == 0)
                            {
                                MessageBox.Show("Load palette first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                return;
                            }
                            Globals.xx = worldPos.X;
                            Globals.yy = worldPos.Y;
                            Form8 textt = new Form8();
                            textt.ShowDialog(Globals.frm);


                            Globals.savechange = true;
                        }
                    }
                    for (int k = 0; k < Globals.sx * Globals.sy; ++k)
                    {
                        Globals.levelshow[k] = Globals.level1[k, Globals.page];
                    }
                    Globals.map.load("tileset.png", Globals.tiless, Globals.levelshow, Globals.sx, Globals.sy);
                
                

            }
            if (e.Button == MouseButtons.Right)
            {

                Vector2i pixelpos = new Vector2i(e.X, e.Y);
                Vector2f worldPos = Globals.renderwindow.MapPixelToCoords(pixelpos);

                float ix = worldPos.X / 12;
                //++ix;

                float iy = worldPos.Y / 12;
                //++iy;

                uint ixx = (uint)ix;
                uint iyy = (uint)iy;

                ++ixx;
                ++iyy;

                uint posp = (iyy - 1) * (Globals.sx) + ixx;
                if (Globals.nowpaint == true)
                {
                    if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                    {
                        Globals.level1[(int)posp - 1, Globals.page ] = (uint)Globals.curcolor1;
                        Globals.savechange = true;
                    }
                }
                if (Globals.nownorpaint == true)
                {
                    if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                    {
                        Globals.level1[(int)posp - 1, Globals.page ] = (uint)Globals.curcolor1;
                        Globals.savechange = true;
                    }
                }

                for (int k = 0; k < Globals.sx * Globals.sy; ++k)
                {
                    Globals.levelshow[k] = Globals.level1[k, Globals.page];
                }
                Globals.map.load("tileset.png", Globals.tiless, Globals.levelshow, Globals.sx, Globals.sy);

            }
        }
        static void tell(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Globals.create == true)
            {
                Vector2i pixelpos1 = new Vector2i(e.X, e.Y);
                Vector2f worldPos1 = Globals.renderwindow.MapPixelToCoords(pixelpos1);
                Globals.xx = worldPos1.X;
                Globals.yy = worldPos1.Y;
                int column = (int)((worldPos1.X / 12) + 1);
                int row = (int)(worldPos1.Y / 12);
                string alpha = Char.ConvertFromUtf32(row + 65);
                //string columns = column.ToString();
                int posp1 = ((int)(worldPos1.Y / 12)) * ((int)Globals.sx) + (int)(worldPos1.X / 12);
                if (column >= 0 && column <= Globals.sx)
                {
                    Globals.frm.toolStripStatusLabel2.Text = "Column: " + column.ToString();
                }
                if (row >= 0 && row <= Globals.sy && row > -1 && row < 25)
                {
                    Globals.frm.toolStripStatusLabel1.Text = "Row: " + alpha;
                }
                if (posp1 >= 0 && posp1 <= Globals.sx * Globals.sy)
                {
                    Globals.frm.toolStripStatusLabel3.Text = "Color: " + (Globals.level[(int)posp1] + 1);
                }
                Globals.trackpoint = worldPos1;

                if (e.Button == MouseButtons.Left)
                {
                        Vector2i pixelpos = new Vector2i(e.X, e.Y);
                        Vector2f worldPos = Globals.renderwindow.MapPixelToCoords(pixelpos);

                        float ix = worldPos.X / 12;
                        //++ix;

                        float iy = worldPos.Y / 12;
                        //++iy;

                        uint ixx = (uint)ix;
                        uint iyy = (uint)iy;

                        ++ixx;
                        ++iyy;

                        uint posp = (iyy - 1) * (Globals.sx) + ixx;

                        if (Globals.nowpaint == true)
                        {
                            if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                            {
                                Globals.level1[(int)posp - 1, Globals.page ] = (uint)Globals.curcolor;
                                Globals.savechange = true;
                            }
                        }
                        else if (Globals.noweraser == true)
                        {
                            if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                            {
                                Globals.level1[(int)posp - 1, Globals.page] = 0;
                                Globals.savechange = true;
                            }
                        }
                        else if (Globals.nowdimmer == true)
                        {
                            if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                            {
                                if (Globals.level1[(int)posp - 1, Globals.page ] > 0)
                                {
                                    uint dimm = Globals.level1[(int)posp - 1, Globals.page - 1] - 1;
                                    Globals.level1[(int)posp - 1, Globals.page ] = dimm;
                                    Globals.savechange = true;
                                }
                            }
                        }
                        else if (Globals.nowtext == true)
                        {
                            if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                            {
                                if (Globals.getco.Length == 0)
                                {
                                    MessageBox.Show("Load palette first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    return;
                                }
                                Globals.xx = worldPos.X;
                                Globals.yy = worldPos.Y;
                                Form8 textt = new Form8();
                                textt.ShowDialog(Globals.frm);


                                Globals.savechange = true;
                            }
                        }
                        for (int k = 0; k < Globals.sx * Globals.sy; ++k)
                        {
                            Globals.levelshow[k] = Globals.level1[k, Globals.page];
                        }
                        Globals.map.load("tileset.png", Globals.tiless, Globals.levelshow, Globals.sx, Globals.sy);
                    
                    

                }
                if (e.Button == MouseButtons.Right)
                {

                    Vector2i pixelpos = new Vector2i(e.X, e.Y);
                    Vector2f worldPos = Globals.renderwindow.MapPixelToCoords(pixelpos);

                    float ix = worldPos.X / 12;
                    //++ix;

                    float iy = worldPos.Y / 12;
                    //++iy;

                    uint ixx = (uint)ix;
                    uint iyy = (uint)iy;

                    ++ixx;
                    ++iyy;

                    uint posp = (iyy - 1) * (Globals.sx) + ixx;
                    if (Globals.nowpaint == true)
                    {
                        if (0 <= posp && posp <= Globals.sx * Globals.sy && worldPos.X <= Globals.sx * 12 && worldPos.Y <= Globals.sy * 12 && worldPos.X >= 0 && worldPos.Y >= 0)
                        {
                            Globals.level1[(int)posp - 1, Globals.page] = (uint)Globals.curcolor1;
                            Globals.savechange = true;
                        }
                    }

                    for (int k = 0; k < Globals.sx * Globals.sy; ++k)
                    {
                        Globals.levelshow[k] = Globals.level1[k, Globals.page];
                    }
                    Globals.map.load("tileset.png", Globals.tiless, Globals.levelshow, Globals.sx, Globals.sy);

                }

            }

        }
        static void fitto(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            Globals.zoomlimit = 5;
            Globals.view2 = Globals.renderwindow.GetView();
            Globals.rendersurface.Size = new Size(Globals.resizex, Globals.resizey);
            FloatRect visibleArea = new FloatRect(0, 0, Globals.resizex, Globals.resizey);
            Globals.renderwindow.SetView(new SFML.Graphics.View(visibleArea));
            Vector2f center = new Vector2f(Globals.sx * 6, Globals.sy * 6);
            Globals.view2.Center = center;
            Globals.renderwindow.SetView(Globals.view2);

        }
        public class DrawingSurface : System.Windows.Forms.UserControl
        {
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                // don't call base.OnPaint(e) to prevent forground painting
                //base.OnPaint(e);
            }
            protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
            {
                // don't call base.OnPaintBackground(e) to prevent background painting
                //base.OnPaintBackground(pevent);
            }
            private void InitializeComponent()
            {
                this.SuspendLayout();
                this.ResumeLayout(false);
            }
            protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
            {
                Vector2f posss = Globals.renderwindow.MapPixelToCoords(new Vector2i(Globals.frm.panel1.Size.Width / 2, Globals.frm.panel1.Size.Height / 2));
                //Globals.frm.vScrollBar1
                if (e.Delta > 0)
                {
                    if (Globals.zoomlimit < 35)
                    {
                        ++Globals.track;
                        Globals.view2 = Globals.renderwindow.GetView();
                        Vector2f mousepos = Globals.renderwindow.MapPixelToCoords(Mouse.GetPosition() - Globals.renderwindow.Position);
                        Globals.view2.Zoom(0.909090909090909090909090909090909090909090909f);
                        if (mousepos.X <= 0)
                        {
                            mousepos.X = 0;
                        }
                        if (mousepos.X >= 12 * Globals.sx)
                        {
                            mousepos.X = 12 * Globals.sx;
                        }
                        if (mousepos.Y <= 0)
                        {
                            mousepos.Y = 0;
                        }
                        if (mousepos.Y >= 12 * Globals.sy)
                        {
                            mousepos.Y = 12 * Globals.sy;
                        }
                        Globals.view2.Center = mousepos;
                        Globals.renderwindow.SetView(Globals.view2);
                        Mouse.SetPosition(Globals.renderwindow.MapCoordsToPixel(mousepos) + Globals.renderwindow.Position);
                        ++Globals.zoomlimit;
                        //Globals.frm.vScrollBar1.Value = (int)(Globals.view2.Size.Y - Globals.view2.Center.Y);
                        if (Globals.track >= 0 && posss.X >= 0 && posss.Y >= 0)
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
                if (e.Delta < 0)
                {

                    if (Globals.zoomlimit > 0)
                    {
                        --Globals.track;
                        Globals.view2 = Globals.renderwindow.GetView();
                        Vector2f mousepos = Globals.renderwindow.MapPixelToCoords(Mouse.GetPosition() - Globals.renderwindow.Position);
                        Globals.view2.Zoom(1.1f);
                        if (mousepos.X <= 0)
                        {
                            mousepos.X = 0;
                        }
                        if (mousepos.X >= 12 * Globals.sx)
                        {
                            mousepos.X = 12 * Globals.sx;
                        }
                        if (mousepos.Y <= 0)
                        {
                            mousepos.Y = 0;
                        }
                        if (mousepos.Y >= 12 * Globals.sy)
                        {
                            mousepos.Y = 12 * Globals.sy;
                        }
                        Globals.view2.Center = mousepos;
                        Globals.renderwindow.SetView(Globals.view2);
                        Mouse.SetPosition(Globals.renderwindow.MapCoordsToPixel(mousepos) + Globals.renderwindow.Position);
                        --Globals.zoomlimit;
                        if (Globals.track >= 0 && posss.X >= 0 && posss.Y >= 0)
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
            }
            protected override bool     ProcessDialogKey(Keys keyData)
            {
                Vector2f posss = Globals.renderwindow.MapPixelToCoords(new Vector2i(Globals.frm.panel1.Size.Width / 2, Globals.frm.panel1.Size.Height / 2));
                if (keyData == Keys.Right)
                {
                    Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                    Vector2f move = new Vector2f((float)((5) * (1)), 0);
                    Program.Globals.view2.Move(move);
                    Program.Globals.renderwindow.SetView(Program.Globals.view2);
                    if (Program.Globals.track >= 0 && posss.X >= 0)
                    {
                        Program.Globals.frm.hScrollBar1.Maximum = (int)(Program.Globals.sx * 12 * (1.1 * Program.Globals.track)) / 100;
                        if (Program.Globals.frm.hScrollBar1.Maximum >= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = (int)(posss.X * (1.1 * Program.Globals.track)) / 100;
                        }
                        else if (Program.Globals.frm.hScrollBar1.Maximum <= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = Program.Globals.frm.hScrollBar1.Maximum;
                        }
                        else if ((int)(posss.X * (1.1 * Program.Globals.track)) / 100 < 1)
                        {
                            Program.Globals.frm.hScrollBar1.Value = 1;
                        }
                    }
                }
                if (keyData == Keys.Left)
                {
                    Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                    Vector2f move = new Vector2f((float)((5) * (-1) ), 0);
                    Program.Globals.view2.Move(move);
                    Program.Globals.renderwindow.SetView(Program.Globals.view2);
                    if (Program.Globals.track >= 0 && posss.X >= 0)
                    {
                        Program.Globals.frm.hScrollBar1.Maximum = (int)(Program.Globals.sx * 12 * (1.1 * Program.Globals.track)) / 100;
                        if (Program.Globals.frm.hScrollBar1.Maximum >= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = (int)(posss.X * (1.1 * Program.Globals.track)) / 100;
                        }
                        else if (Program.Globals.frm.hScrollBar1.Maximum <= (int)(posss.X * (1.1 * Program.Globals.track)) / 100)
                        {
                            Program.Globals.frm.hScrollBar1.Value = Program.Globals.frm.hScrollBar1.Maximum;
                        }
                        else if ((int)(posss.X * (1.1 * Program.Globals.track)) / 100 < 1)
                        {
                            Program.Globals.frm.hScrollBar1.Value = 1;
                        }
                    }
                }
                if (keyData == Keys.Up)
                {
                    Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                    Vector2f move = new Vector2f(0, (float)((0.5) * (-1) * (float)(1.1 * Program.Globals.track)));
                    Program.Globals.view2.Move(move);
                    Program.Globals.renderwindow.SetView(Program.Globals.view2);
                    if (Program.Globals.track >= 0 && posss.Y >= 0)
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
                    }
                }
                if (keyData == Keys.Down)
                {
                    Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                    Vector2f move = new Vector2f(0, (float)((0.5) * (1) * (float)(1.1 * Program.Globals.track)));
                    Program.Globals.view2.Move(move);
                    Program.Globals.renderwindow.SetView(Program.Globals.view2);
                    if (Program.Globals.track >= 0 && posss.Y >= 0)
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
                    }
                }
                if (keyData == Keys.OemOpenBrackets)
                {
                    if (Globals.zoomlimit <= 20)
                    {
                        ++Globals.track;
                        Globals.view2 = Globals.renderwindow.GetView();
                        Vector2f mousepos = Globals.renderwindow.MapPixelToCoords(Mouse.GetPosition() - Globals.renderwindow.Position);
                        Globals.view2.Zoom(0.9f);
                        if (mousepos.X <= 0)
                        {
                            mousepos.X = 0;
                        }
                        if (mousepos.X >= 12 * Globals.sx)
                        {
                            mousepos.X = 12 * Globals.sx;
                        }
                        if (mousepos.Y <= 0)
                        {
                            mousepos.Y = 0;
                        }
                        if (mousepos.Y >= 12 * Globals.sy)
                        {
                            mousepos.Y = 12 * Globals.sy;
                        }
                        Globals.view2.Center = mousepos;
                        Globals.renderwindow.SetView(Globals.view2);
                        Mouse.SetPosition(Globals.renderwindow.MapCoordsToPixel(mousepos) + Globals.renderwindow.Position);
                        ++Globals.zoomlimit;
                        //Globals.frm.vScrollBar1.Value = (int)(Globals.view2.Size.Y - Globals.view2.Center.Y);
                        if (Globals.track >= 0 && posss.X >= 0 && posss.Y >= 0)
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
                if (keyData == Keys.OemCloseBrackets)
                {
                    if (Globals.zoomlimit >= -1)
                    {
                        --Globals.track;
                        Globals.view2 = Globals.renderwindow.GetView();
                        Vector2f mousepos = Globals.renderwindow.MapPixelToCoords(Mouse.GetPosition() - Globals.renderwindow.Position);
                        Globals.view2.Zoom(1.1f);
                        if (mousepos.X <= 0)
                        {
                            mousepos.X = 0;
                        }
                        if (mousepos.X >= 12 * Globals.sx)
                        {
                            mousepos.X = 12 * Globals.sx;
                        }
                        if (mousepos.Y <= 0)
                        {
                            mousepos.Y = 0;
                        }
                        if (mousepos.Y >= 12 * Globals.sy)
                        {
                            mousepos.Y = 12 * Globals.sy;
                        }
                        Globals.view2.Center = mousepos;
                        Globals.renderwindow.SetView(Globals.view2);
                        Mouse.SetPosition(Globals.renderwindow.MapCoordsToPixel(mousepos) + Globals.renderwindow.Position);
                        Globals.zoomlimit = Globals.zoomlimit - 1;
                        if (Globals.track >= 0 && posss.X >= 0 && posss.Y >= 0)
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
                    if (keyData == Keys.D1)
                    {
                        f3.changecolor(1);
                        Globals.curcolor = 1;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "1";
                    }
                    if (keyData == Keys.D2)
                    {
                        f3.changecolor(2);
                        Globals.curcolor = 2;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "2";
                    }
                    if (keyData == Keys.D3)
                    {
                        f3.changecolor(3);
                        Globals.curcolor = 3;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "3";
                    }
                    if (keyData == Keys.D4)
                    {
                        f3.changecolor(4);
                        Globals.curcolor = 4;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "4";
                    }
                    if (keyData == Keys.D5)
                    {
                        f3.changecolor(5);
                        Globals.curcolor = 5;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "5";
                    }
                    if (keyData == Keys.D6)
                    {
                        f3.changecolor(6);
                        Globals.curcolor = 6;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "6";
                    }
                    if (keyData == Keys.D7)
                    {
                        f3.changecolor(7);
                        Globals.curcolor = 7;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "7";
                    }
                    if (keyData == Keys.D8)
                    {
                        f3.changecolor(8);
                        Globals.curcolor = 8;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "8";
                    }
                    if (keyData == Keys.D9)
                    {
                        f3.changecolor(9);
                        Globals.curcolor = 9;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "9";
                    }
                    if (keyData == Keys.D0)
                    {
                        f3.changecolor(10);
                        Globals.curcolor = 10;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "10";
                    }
                    if (keyData == Keys.Q)
                    {
                        f3.changecolor(11);
                        Globals.curcolor = 11;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "11";
                    }
                    if (keyData == Keys.W)
                    {
                        f3.changecolor(12);
                        Globals.curcolor = 12;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "12";
                    }
                    /*if (keyData == Keys.E)
                    {
                        f3.changecolor(13);
                        Globals.curcolor = 13;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "13";
                    }
                    if (keyData == Keys.R)
                    {
                        f3.changecolor(14);
                        Globals.curcolor = 14;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "14";
                    }
                    if (keyData == Keys.T)
                    {
                        f3.changecolor(15);
                        Globals.curcolor = 15;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "15";
                    }
                    if (keyData == Keys.Y)
                    {
                        f3.changecolor(16);
                        Globals.curcolor = 16;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "16";
                    }
                    if (keyData == Keys.U)
                    {
                        f3.changecolor(17);
                        Globals.curcolor = 17;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "17";
                    }
                    if (keyData == Keys.I)
                    {
                        f3.changecolor(18);
                        Globals.curcolor = 18;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "18";
                    }
                    if (keyData == Keys.O)
                    {
                        f3.changecolor(19);
                        Globals.curcolor = 19;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "19";
                    }
                    if (keyData == Keys.P)
                    {
                        f3.changecolor(20);
                        Globals.curcolor = 20;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "20";
                    }
                    if (keyData == Keys.A)
                    {
                        f3.changecolor(21);
                        Globals.curcolor = 21;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "21";
                    }
                    if (keyData == Keys.S)
                    {
                        f3.changecolor(22);
                        Globals.curcolor = 22;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "22";
                    }
                    if (keyData == Keys.D)
                    {
                        f3.changecolor(23);
                        Globals.curcolor = 23;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "23";
                    }
                    if (keyData == Keys.F)
                    {
                        f3.changecolor(24);
                        Globals.curcolor = 24;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "24";
                    }
                    if (keyData == Keys.G)
                    {
                        f3.changecolor(25);
                        Globals.curcolor = 25;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "25";
                    }
                    if (keyData == Keys.H)
                    {
                        f3.changecolor(26);
                        Globals.curcolor = 26;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "26";
                    }
                    if (keyData == Keys.J)
                    {
                        f3.changecolor(27);
                        Globals.curcolor = 27;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "27";
                    }
                    if (keyData == Keys.K)
                    {
                        f3.changecolor(28);
                        Globals.curcolor = 28;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "28";
                    }
                    if (keyData == Keys.L)
                    {
                        f3.changecolor(29);
                        Globals.curcolor = 29;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "29";
                    }
                    if (keyData == Keys.OemSemicolon)
                    {
                        f3.changecolor(30);
                        Globals.curcolor = 30;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "30";
                    }
                    if (keyData == Keys.Z)
                    {
                        f3.changecolor(31);
                        Globals.curcolor = 31;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "31";
                    }
                    if (keyData == Keys.X)
                    {
                        f3.changecolor(32);
                        Globals.curcolor = 32;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "32";
                    }
                    if (keyData == Keys.C)
                    {
                        f3.changecolor(33);
                        Globals.curcolor = 33;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "33";
                    }
                    if (keyData == Keys.V)
                    {
                        f3.changecolor(34);
                        Globals.curcolor = 34;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "34";
                    }
                    if (keyData == Keys.B)
                    {
                        f3.changecolor(35);
                        Globals.curcolor = 35;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "35";
                    }
                    if (keyData == Keys.N)
                    {
                        f3.changecolor(36);
                        Globals.curcolor = 36;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "36";
                    }
                    if (keyData == Keys.M)
                    {
                        f3.changecolor(37);
                        Globals.curcolor = 37;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "37";
                    }
                    if (keyData == Keys.Oemcomma)
                    {
                        f3.changecolor(38);
                        Globals.curcolor = 38;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "38";
                    }
                    if (keyData == Keys.OemPeriod)
                    {
                        f3.changecolor(39);
                        Globals.curcolor = 39;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "39";
                    }

                    if (keyData == Keys.OemQuestion)
                    {
                        f3.changecolor(40);
                        Globals.curcolor = 40;
                        --Globals.curcolor;
                        f3.nowcolor.Text = "40";
                    }*/
                }

                return true;
            }
        }


    }
}


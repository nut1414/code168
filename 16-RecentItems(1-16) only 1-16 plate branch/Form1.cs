using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using SFML;
using SFML.Audio;
using SFML.Window;
using SFML.Graphics;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;
using RibbonLib.Controls.Events;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;
using UndoMethods;

namespace _16_RecentItems
{
    public enum RibbonMarkupCommands : uint
    {
        cmdApplicationMenu = 1000,
        cmdButtonNew = 1001,
        cmdButtonOpen = 1002,
        cmdButtonSave = 1003,
        cmdButtonExit = 1004,
        cmdRecentItems = 1005,
        cmdPaint = 10006,
        cmdEraser = 10007,
        cmdDimmer = 10008,
        cmdText = 10009,
        cmdShowG = 10016,
        cmdShowB = 10015,
        cmdKeycolor = 10012,
        cmdexcel = 10014,
        cmdOpenshut = 10017,
        cmdPrint = 1006,
        Import = 1007,
        Export = 1008,
        Preview = 10013,
        About = 10019,
        cmdButtonSaveAs = 10021,
        cmdnorPaint = 10025
    }   
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        private RibbonRecentItems _ribbonRecentItems;

        List<RecentItemsPropertySet> _recentItems;
        private RibbonLib.Controls.RibbonButton _ButtonNew;
        private RibbonLib.Controls.RibbonButton _ButtonOpen;
        private RibbonLib.Controls.RibbonButton _ButtonSave;
        private RibbonLib.Controls.RibbonToggleButton _Paint;
        private RibbonLib.Controls.RibbonToggleButton _norPaint;
        private RibbonLib.Controls.RibbonToggleButton _Eraser;
        private RibbonLib.Controls.RibbonToggleButton _Dimmer;
        private RibbonLib.Controls.RibbonToggleButton _Text;
        private RibbonLib.Controls.RibbonToggleButton _ShowG;
        private RibbonLib.Controls.RibbonToggleButton _ShowB;
        private RibbonLib.Controls.RibbonButton _Keycolor;
        private RibbonLib.Controls.RibbonButton _excel;
        private RibbonLib.Controls.RibbonToggleButton _Openshut;
        private RibbonLib.Controls.RibbonToggleButton _Print;
        private RibbonLib.Controls.RibbonToggleButton _Import;
        private RibbonLib.Controls.RibbonToggleButton _Export;
        private RibbonLib.Controls.RibbonToggleButton _Preview;
        private RibbonLib.Controls.RibbonToggleButton _About;
        private RibbonLib.Controls.RibbonToggleButton _Exit;
        private RibbonLib.Controls.RibbonToggleButton _ButtonSaveAs;
        public static Form1 Current;
        public Form1()
        {
            InitializeComponent();
            Current = this;
            
            const int IMAGE_CURSOR = 2;
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero,
                @"Res/paint.cur",
                IMAGE_CURSOR,
                0,
                0,
                LR_LOADFROMFILE);

            Cursor paintcur = new Cursor(ipImage);

            this.Cursor = paintcur;

            this.FormClosing += new FormClosingEventHandler(closebut);

            _ribbonRecentItems = new RibbonRecentItems(_ribbon, (uint)RibbonMarkupCommands.cmdRecentItems);

            _ribbonRecentItems.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_recentItems_ExecuteEvent);

            _ButtonNew = new RibbonLib.Controls.RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonNew);

            _ButtonNew.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ButtonNew_ExecuteEvent);

            _ButtonOpen = new RibbonLib.Controls.RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonOpen);

            _ButtonOpen.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ButtonOpen_ExecuteEvent);

            _ButtonSave = new RibbonLib.Controls.RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonSave);

            _ButtonSave.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ButtonSave_ExecuteEvent);

            _Paint = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdPaint);

            _Paint.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Paint_ExecuteEvent);

            _Eraser = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdEraser);

            _Eraser.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Eraser_ExecuteEvent);

            _Dimmer = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdDimmer);

            _Dimmer.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Dimmer_ExecuteEvent);

            _Text = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdText);

            _Text.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Text_ExecuteEvent);

            _ShowB = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdShowB);

            _ShowB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ShowB_ExecuteEvent);

            _ShowG = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdShowG);

            _ShowG.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ShowG_ExecuteEvent);

            _Keycolor = new RibbonLib.Controls.RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdKeycolor);

            _Keycolor.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Keycolor_ExecuteEvent);

            _excel = new RibbonLib.Controls.RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdexcel);

            _excel.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_excel_ExecuteEvent);

            _Openshut = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdOpenshut);

            _Openshut.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Openshut_ExecuteEvent);

            _Print = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdPrint);

            _Print.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Print_ExecuteEvent);

            _Import = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.Import);

            _Import.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Import_ExecuteEvent);

            _Export = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.Export);

            _Export.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Export_ExecuteEvent);

            _Preview = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.Preview);

            _Preview.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Preview_ExecuteEvent);

            _About = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.About);

            _About.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_About_ExecuteEvent);

            _Exit = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonExit);

            _Exit.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_Exit_ExecuteEvent);

            _ButtonSaveAs = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonSaveAs);

            _ButtonSaveAs.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ButtonSaveAs_ExecuteEvent);

            _norPaint = new RibbonLib.Controls.RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdnorPaint);

            _norPaint.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_norPaint_ExecuteEvent);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitRecentItems();
            _norPaint.BooleanValue = true;
            _ShowB.BooleanValue = true;
            _ShowG.BooleanValue = true;
        }
        void closebut(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (Program.Globals.savechange == false)
                {
                    Application.Exit();
                }
                if (Program.Globals.savechange == true)
                {
                    if (Program.Globals.create == true)
                    {
                        DialogResult result2 = MessageBox.Show("Do you want to save changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result2 == DialogResult.Yes)
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                            string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

                            saveFileDialog1.InitialDirectory = AppPath;

                            saveFileDialog1.FileName = "BCC_Code1.bcc16";

                            saveFileDialog1.Title = "Save!";

                            saveFileDialog1.DefaultExt = "bccc16";

                            saveFileDialog1.Filter = "bcc16 file |*.bcc16|All Files (*.*)|*.*";

                            saveFileDialog1.FilterIndex = 1;

                            saveFileDialog1.RestoreDirectory = true;

                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                Program.Globals.savechange = false;
                                string name = saveFileDialog1.FileName;
                                int takes = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                                int takes1 = (int)(Program.Globals.sx * Program.Globals.sy);
                                int endl = (int)Program.Globals.sx;
                                string results = Program.Globals.level.Take(takes).Select((i, index) => index % (Program.Globals.sx * 4) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                                string results1 = Program.Globals.levelshut.Take(takes1).Select((i, index) => index % (Program.Globals.sx) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                                string type = (Program.Globals.sx + System.Environment.NewLine + Program.Globals.sy + System.Environment.NewLine);
                                System.IO.File.WriteAllText(saveFileDialog1.FileName, type + results + results1);
                                Application.Exit();
                            }

                        }
                        if (result2 == DialogResult.No)
                        {
                            Application.Exit();
                        }
                        if (result2 == DialogResult.Cancel)
                        {
                            Program.Globals.savechange = true;
                            e.Cancel = true;
                        }

                    }
                    else if (Program.Globals.create == false)
                    {
                        Application.Exit();
                    }
                }

            }
        }
        void _Exit_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (Program.Globals.savechange == false)
            {
                Application.Exit();
            }
            if (Program.Globals.savechange == true)
            {
                if (Program.Globals.create == true)
                {
                    DialogResult result2 = MessageBox.Show("Do you want to save changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                        string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

                        saveFileDialog1.InitialDirectory = AppPath;

                        saveFileDialog1.FileName = "BCC_Code1.bcc16";

                        saveFileDialog1.Title = "Save!";

                        saveFileDialog1.DefaultExt = "bccc16";

                        saveFileDialog1.Filter = "bcc16 file |*.bcc16|All Files (*.*)|*.*";

                        saveFileDialog1.FilterIndex = 1;

                        saveFileDialog1.RestoreDirectory = true;

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Program.Globals.savechange = false;
                            string name = saveFileDialog1.FileName;
                            int takes = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                            int takes1 = (int)(Program.Globals.sx * Program.Globals.sy);
                            int endl = (int)Program.Globals.sx;
                            string results = Program.Globals.level.Take(takes).Select((i, index) => index % (Program.Globals.sx * 4) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                            string results1 = Program.Globals.levelshut.Take(takes1).Select((i, index) => index % (Program.Globals.sx) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                            string type = (Program.Globals.sx + System.Environment.NewLine + Program.Globals.sy + System.Environment.NewLine);
                            System.IO.File.WriteAllText(saveFileDialog1.FileName, type + results + results1);
                            Application.Exit();
                        }

                    }
                    if (result2 == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    if (result2 == DialogResult.Cancel)
                    {
                        Program.Globals.savechange = true;
                    }

                }
                else if (Program.Globals.create == false)
                {
                    Application.Exit();
                }
            }
        }
        void _ButtonSaveAs_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (Program.Globals.frm.Text != "CODE 163!")
            {
                Program.Globals.savechange = false;
                string name = Program.Globals.fcurrent;
                int takes = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                int takes1 = (int)(Program.Globals.sx * Program.Globals.sy);
                int endl = (int)Program.Globals.sx;
                string results = Program.Globals.level.Take(takes).Select((i, index) => index % (Program.Globals.sx * 4) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                string results1 = Program.Globals.levelshut.Take(takes1).Select((i, index) => index % (Program.Globals.sx) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                string type = (Program.Globals.sx + System.Environment.NewLine + Program.Globals.sy + System.Environment.NewLine);
                System.IO.File.WriteAllText(Program.Globals.fcurrent, type + results + results1);
                
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

                saveFileDialog1.InitialDirectory = AppPath;

                saveFileDialog1.FileName = "BCC_Code1.bcc16";

                saveFileDialog1.Title = "Save!";

                saveFileDialog1.DefaultExt = "bccc16";

                saveFileDialog1.Filter = "bcc16 file |*.bcc16|All Files (*.*)|*.*";

                saveFileDialog1.FilterIndex = 1;

                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string name = saveFileDialog1.FileName;
                    int takes = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                    int takes1 = (int)(Program.Globals.sx * Program.Globals.sy);
                    int endl = (int)Program.Globals.sx;
                    string results = Program.Globals.level.Take(takes).Select((i, index) => index % (Program.Globals.sx * 4) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                    string results1 = Program.Globals.levelshut.Take(takes1).Select((i, index) => index % (Program.Globals.sx) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                    string type = (Program.Globals.sx + System.Environment.NewLine + Program.Globals.sy + System.Environment.NewLine);
                    System.IO.File.WriteAllText(saveFileDialog1.FileName, type + results + results1);
                    Program.Globals.frm.Text = saveFileDialog1.FileName;
                    Program.Globals.fcurrent = saveFileDialog1.FileName;
                }
            }
        }
        void _Print_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Form5 print = new Form5();
            print.ShowDialog(Program.Globals.frm);

        }
        void _Preview_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (Program.Globals.loadkey == false)
            {
                MessageBox.Show("Load palette before preview");
                return;
            }
            Bitmap ex = null;

            System.Drawing.Color[] _colors = new System.Drawing.Color[40];
            for (int i = 0; i < 40; i++)
            {
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                _colors[i] = System.Drawing.Color.FromArgb(red, green, blue);
            }
            ex = new Bitmap((int)Program.Globals.sx * 4, (int)Program.Globals.sy * 4);

            for (int y = 1; y <= ex.Height; ++y)
            {
                for (int x = 1; x <= ex.Width; ++x)
                {
                    int posp = (y - 1) * ((int)Program.Globals.sx * 4) + (x - 1);
                    if (Program.Globals.level[posp] < 0)
                    {
                        Program.Globals.level[posp] = 0;
                    }
                    if (Program.Globals.level[posp] > 39)
                    {
                        Program.Globals.level[posp] = 39;
                    }
                    ex.SetPixel(x - 1, y - 1, _colors[Program.Globals.level[posp]]);
                }
            }
            Form7 load7 = new Form7();
            load7.pictureBox1.Image = ex;
            load7.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            load7.Show(Program.Globals.frm);
        }
        void _About_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Form9 about = new Form9();
            about.ShowDialog(Program.Globals.frm);
        }
        void _Export_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
            Bitmap ex = null;
            if (Program.Globals.loadkey == false)
            {
                MessageBox.Show("Load palette before export");
                return;
            }

            System.Drawing.Color[] _colors = new System.Drawing.Color[40];
            for (int i = 0; i < 40; i++)
            {
                int red = int.Parse(Program.Globals.getco[i].Substring(0, 2), NumberStyles.AllowHexSpecifier);
                int green = int.Parse(Program.Globals.getco[i].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                int blue = int.Parse(Program.Globals.getco[i].Substring(4, 2), NumberStyles.AllowHexSpecifier);
                _colors[i] = System.Drawing.Color.FromArgb(red, green, blue);
            }
            ex = new Bitmap((int)Program.Globals.sx * 4 , (int)Program.Globals.sy * 4);

            for (int y = 1; y <= ex.Height; ++y)
            {
                for (int x = 1; x <= ex.Width; ++x)
                {
                    int posp = (y - 1) * ((int)Program.Globals.sx * 4) + (x - 1);
                    if (Program.Globals.level[posp] < 0)
                    {
                        Program.Globals.level[posp] = 0;
                    }
                    if (Program.Globals.level[posp] > 39)
                    {
                        Program.Globals.level[posp] = 39;
                    }
                    ex.SetPixel(x - 1, y - 1, _colors[Program.Globals.level[posp]]);
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = AppPath;

            saveFileDialog1.FileName = "Export1";

            saveFileDialog1.Title = "Export image";

            saveFileDialog1.DefaultExt = "png";

            saveFileDialog1.Filter = "PNG file |*.png|All Files (*.*)|*.*";

            saveFileDialog1.FilterIndex = 1;

            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ex.Save(saveFileDialog1.FileName, ImageFormat.Png);
            }
        }
        void _Import_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (Program.Globals.loadkey == false)
            {
                MessageBox.Show("Load palette before import");
                return;
            }
            Form6 imports = new Form6();
            imports.ShowDialog(Program.Globals.frm);
        }
        void _Keycolor_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
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
                string[] getco = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                Form3 key = new Form3();
                key.Show(Program.Globals.frm);
                Program.Globals.loadkey = true;
                key.Location = new Point(Program.Globals.frm.Size.Width - key.Size.Width - 35, Program.Globals.frm.Size.Height - key.Size.Height - 60);
            }
            
        }
        void _Openshut_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            const int IMAGE_CURSOR = 2;
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero,
                @"Res/paint.cur",
                IMAGE_CURSOR,
                0,
                0,
                LR_LOADFROMFILE);

            Cursor textcur = new Cursor(ipImage);

            this.Cursor = textcur;

            if (_Openshut.BooleanValue == false)
            {
                _Openshut.BooleanValue = true;
            }
            else if (_Openshut.BooleanValue == true)
            {
                _Openshut.BooleanValue = true;

                _Paint.BooleanValue = false;
                _Eraser.BooleanValue = false;
                _Dimmer.BooleanValue = false;
                _Text.BooleanValue = false;
                Program.Globals.nowtext = false;
                Program.Globals.nowpaint = false;
                Program.Globals.nowdimmer = false;
                Program.Globals.noweraser = false;

                Form4 ocontrol = new Form4();
                if (_Openshut.BooleanValue == true)
                {
                    Program.Globals.omode = true;
                    ocontrol.Show(Program.Globals.frm);

                }
                else if (_Openshut.BooleanValue == false)
                {
                    Program.Globals.omode = false;
                }
                Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
            }
        }
        void _excel_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (Program.Globals.loadkey == false)
            {
                MessageBox.Show("Load palette before Fill");
                return;
            }
            for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
            {
                Program.Globals.level[k - 1] = (uint)Program.Globals.curcolor;
            }
            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
        }
        void _ShowB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (_ShowB.BooleanValue == true)
            {
                Program.Globals.showb = true;
            }
            else if (_ShowB.BooleanValue == false)
            {
                Program.Globals.showb = false;
            } 
        }
        void _ShowG_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (_ShowG.BooleanValue == true)
            {
                Program.Globals.showg = true;
            }
            else if (_ShowG.BooleanValue == false)
            {
                Program.Globals.showg = false;
            }
        }
        void _ButtonNew_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (Program.Globals.savechange == false)
            {
                Form2 frm2 = new Form2();
                frm2.ShowDialog(Program.Globals.frm);
            }
            if (Program.Globals.savechange == true)
            {
                if (Program.Globals.create == true)
                {
                    DialogResult result2 = MessageBox.Show("Do you want to save changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                        string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

                        saveFileDialog1.InitialDirectory = AppPath;

                        saveFileDialog1.FileName = "BCC_Code1.bcc16";

                        saveFileDialog1.Title = "Save!";

                        saveFileDialog1.DefaultExt = "bccc16";

                        saveFileDialog1.Filter = "bcc16 file |*.bcc16|All Files (*.*)|*.*";

                        saveFileDialog1.FilterIndex = 1;

                        saveFileDialog1.RestoreDirectory = true;

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Program.Globals.savechange = false;
                            string name = saveFileDialog1.FileName;
                            int takes = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                            int takes1 = (int)(Program.Globals.sx * Program.Globals.sy);
                            int endl = (int)Program.Globals.sx;
                            string results = Program.Globals.level.Take(takes).Select((i, index) => index % (Program.Globals.sx * 4) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                            string results1 = Program.Globals.levelshut.Take(takes1).Select((i, index) => index % (Program.Globals.sx) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                            string type = (Program.Globals.sx + System.Environment.NewLine + Program.Globals.sy + System.Environment.NewLine);
                            System.IO.File.WriteAllText(saveFileDialog1.FileName, type + results + results1);
                            Program.Globals.fcurrent = "CODE 163!";
                            Form2 frm2 = new Form2();
                            frm2.Show();
                        }

                    }
                    if (result2 == DialogResult.No)
                    {
                        Program.Globals.savechange = false;
                        Form2 frm2 = new Form2();
                        frm2.ShowDialog(Program.Globals.frm);
                        Program.Globals.fcurrent = "CODE 163!";
                    }
                    if (result2 == DialogResult.Cancel)
                    {
                        Program.Globals.savechange = true;
                    }

                }
                else if (Program.Globals.create == false)
                {

                        Form2 frm2 = new Form2();
                        frm2.Show();
                }
            }
            
        }
        void _norPaint_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
                if (form.Text == "Openshut Control")
                {
                    form.Close();
                    break;
                }

            const int IMAGE_CURSOR = 2;
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero,
                @"Res/paint.cur",
                IMAGE_CURSOR,
                0,
                0,
                LR_LOADFROMFILE);

            Cursor paintcur = new Cursor(ipImage);

            this.Cursor = paintcur;

            if (_norPaint.BooleanValue == false)
            {
                _norPaint.BooleanValue = true;
            }
            _Paint.BooleanValue = false;
            _Eraser.BooleanValue = false;
            _Dimmer.BooleanValue = false;
            _Text.BooleanValue = false;
            _Openshut.BooleanValue = false;
            Program.Globals.nownorpaint = true;
            Program.Globals.nowpaint = false;
            Program.Globals.noweraser = false;
            Program.Globals.nowdimmer = false;
            Program.Globals.nowtext = false;
            Program.Globals.omode = false;
        }
        void _Paint_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
                if (form.Text == "Openshut Control")
                {
                    form.Close();
                    break;
                }

            const int IMAGE_CURSOR = 2;
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero,
                @"Res/paint.cur",
                IMAGE_CURSOR,
                0,
                0,
                LR_LOADFROMFILE);

            Cursor paintcur = new Cursor(ipImage);

            this.Cursor = paintcur;

            if(_Paint.BooleanValue == false)
            {
                _Paint.BooleanValue = true;
            }
            _Eraser.BooleanValue = false;
            _Dimmer.BooleanValue = false;
            _Text.BooleanValue = false;
            _Openshut.BooleanValue = false;
            _norPaint.BooleanValue = false;
            Program.Globals.nownorpaint = false;
            Program.Globals.nowpaint = true;
            Program.Globals.noweraser = false;
            Program.Globals.nowdimmer = false;
            Program.Globals.nowtext = false;
            Program.Globals.omode = false;
        }
        void _Eraser_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
                if (form.Text == "Openshut Control")
                {
                    form.Close();
                    break;
                }
            const int IMAGE_CURSOR = 2;
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero,
                @"Res/eraser.cur",
                IMAGE_CURSOR,
                0,
                0,
                LR_LOADFROMFILE);

            Cursor erasercur = new Cursor(ipImage);

            this.Cursor = erasercur;

            if (_Eraser.BooleanValue == false)
            {
                _Eraser.BooleanValue = true;
            }
            _Paint.BooleanValue = false;
            _Dimmer.BooleanValue = false;
            _Text.BooleanValue = false;
            _Openshut.BooleanValue = false;
            _norPaint.BooleanValue = false;
            Program.Globals.nownorpaint = false;
            Program.Globals.noweraser = true;
            Program.Globals.nowpaint = false;
            Program.Globals.nowdimmer = false;
            Program.Globals.nowtext = false;
            Program.Globals.omode = false;
        }
        void _Dimmer_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
                if (form.Text == "Openshut Control")
                {
                    form.Close();
                    break;
                }
            const int IMAGE_CURSOR = 2;
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero,
                @"Res/dimmer.cur",
                IMAGE_CURSOR,
                0,
                0,
                LR_LOADFROMFILE);

            Cursor dimcur = new Cursor(ipImage);

            this.Cursor = dimcur;

            if (_Dimmer.BooleanValue == false)
            {
                _Dimmer.BooleanValue = true;
            }
            _Eraser.BooleanValue = false;
            _Paint.BooleanValue = false;
            _Text.BooleanValue = false;
            _Openshut.BooleanValue = false;
            _norPaint.BooleanValue = false;
            Program.Globals.nownorpaint = false;
            Program.Globals.nowdimmer = true;
            Program.Globals.nowpaint = false;
            Program.Globals.noweraser = false;
            Program.Globals.omode = false;
            Program.Globals.nowtext = false;
        }
        void _Text_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
                if (form.Text == "Openshut Control")
                {
                    form.Close();
                    break;
                }
            const int IMAGE_CURSOR = 2;
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero,
                @"Res/text.cur",
                IMAGE_CURSOR,
                0,
                0,
                LR_LOADFROMFILE);

            Cursor textcur = new Cursor(ipImage);

            this.Cursor = textcur;

            if (_Text.BooleanValue == false)
            {
                _Text.BooleanValue = true;
            }
            _Paint.BooleanValue = false;
            _Eraser.BooleanValue = false;
            _Dimmer.BooleanValue = false;
            _Openshut.BooleanValue = false;
            _norPaint.BooleanValue = false;
            Program.Globals.nownorpaint = false;
            Program.Globals.nowtext = true;
            Program.Globals.nowpaint = false;
            Program.Globals.nowdimmer = false;
            Program.Globals.noweraser = false;
            Program.Globals.omode = false;
        }
        void _ButtonSave_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            string AppPath = Path.GetDirectoryName(Application.ExecutablePath); 

            saveFileDialog1.InitialDirectory = AppPath;

            saveFileDialog1.FileName = "BCC_Code1.bcc16";

            saveFileDialog1.Title = "Save!";

            saveFileDialog1.DefaultExt = "bccc16";

            saveFileDialog1.Filter = "bcc16 file |*.bcc16|All Files (*.*)|*.*";

            saveFileDialog1.FilterIndex = 1;

            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string name = saveFileDialog1.FileName;
                int takes = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                int takes1 = (int)(Program.Globals.sx * Program.Globals.sy);
                int endl = (int)Program.Globals.sx;
                string results = Program.Globals.level.Take(takes).Select((i, index) => index % (Program.Globals.sx * 4) == -1 ? i.ToString(): i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                string results1 = Program.Globals.levelshut.Take(takes1).Select((i, index) => index % (Program.Globals.sx) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                string type = (Program.Globals.sx + System.Environment.NewLine + Program.Globals.sy + System.Environment.NewLine);
                System.IO.File.WriteAllText(saveFileDialog1.FileName, type + results + results1);
                Program.Globals.frm.Text = saveFileDialog1.FileName;
                Program.Globals.fcurrent = saveFileDialog1.FileName;
            }
            
        }
        void _ButtonOpen_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (Program.Globals.savechange == false)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "BCC 1:16 files |*.bcc16|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Multiselect = false;
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] getload = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                    Program.Globals.sx = (uint)Convert.ToInt32(getload[0]);
                    Program.Globals.sy = (uint)Convert.ToInt32(getload[1]);
                    int last = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                    for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
                    {
                        Program.Globals.level[k - 1] = (uint)Convert.ToInt32(getload[k + 1]);
                    }
                    for (int k = last + 3; k <= (last + 2 + Program.Globals.sx * Program.Globals.sy); ++k)
                    {
                        Program.Globals.levelshut[k - last - 3] = (uint)Convert.ToInt32(getload[k - 1]);

                    }
                    Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
                    Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
                    Program.Globals.create = true;
                    Program.Globals.loadd = true;
                    Program.Globals.frm.Text = openFileDialog1.FileName;
                    Program.Globals.fcurrent = openFileDialog1.FileName;
                }
            }

            if (Program.Globals.savechange == true)
            {
                if (Program.Globals.create == true)
                {
                    DialogResult result2 = MessageBox.Show("Do you want to save changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        Program.Globals.savechange = false;

                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                        string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

                        saveFileDialog1.InitialDirectory = AppPath;

                        saveFileDialog1.FileName = "BCC_Code1.bcc16";

                        saveFileDialog1.Title = "Save!";

                        saveFileDialog1.DefaultExt = "bccc16";

                        saveFileDialog1.Filter = "bcc16 file |*.bcc16|All Files (*.*)|*.*";

                        saveFileDialog1.FilterIndex = 1;

                        saveFileDialog1.RestoreDirectory = true;

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Program.Globals.savechange = false;
                            string name = saveFileDialog1.FileName;
                            int takes = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                            int takes1 = (int)(Program.Globals.sx * Program.Globals.sy);
                            int endl = (int)Program.Globals.sx;
                            string results = Program.Globals.level.Take(takes).Select((i, index) => index % (Program.Globals.sx * 4) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                            string results1 = Program.Globals.levelshut.Take(takes1).Select((i, index) => index % (Program.Globals.sx) == -1 ? i.ToString() : i.ToString() + "\r\n").Aggregate((s1, s2) => s1 + s2);
                            string type = (Program.Globals.sx + System.Environment.NewLine + Program.Globals.sy + System.Environment.NewLine);
                            System.IO.File.WriteAllText(saveFileDialog1.FileName, type + results + results1);
                            Program.Globals.frm.Text = saveFileDialog1.FileName;
                        }

                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "BCC 1:16 files |*.bcc16|All Files (*.*)|*.*";
                        openFileDialog1.FilterIndex = 1;
                        openFileDialog1.Multiselect = false;
                        DialogResult result = openFileDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string[] getload = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                            Program.Globals.sx = (uint)Convert.ToInt32(getload[0]);
                            Program.Globals.sy = (uint)Convert.ToInt32(getload[1]);
                            int last = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                            for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
                            {
                                Program.Globals.level[k - 1] = (uint)Convert.ToInt32(getload[k + 1]);
                            }
                            for (int k = last + 3; k <= (last + 2 + Program.Globals.sx * Program.Globals.sy); ++k)
                            {
                                Program.Globals.levelshut[k - last - 3] = (uint)Convert.ToInt32(getload[k - 1]);

                            }
                            Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
                            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
                            Program.Globals.create = true;
                            Program.Globals.loadd = true;
                            Program.Globals.frm.Text = openFileDialog1.FileName;
                            Program.Globals.fcurrent = openFileDialog1.FileName;
                        }
                    }
                    if (result2 == DialogResult.No)
                    {
                        Program.Globals.savechange = false;

                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "BCC 1:16 files |*.bcc16|All Files (*.*)|*.*";
                        openFileDialog1.FilterIndex = 1;
                        openFileDialog1.Multiselect = false;
                        DialogResult result = openFileDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string[] getload = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                            Program.Globals.sx = (uint)Convert.ToInt32(getload[0]);
                            Program.Globals.sy = (uint)Convert.ToInt32(getload[1]);
                            int last = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                            for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
                            {
                                Program.Globals.level[k - 1] = (uint)Convert.ToInt32(getload[k + 1]);
                            }
                            for (int k = last + 3; k <= (last + 2 + Program.Globals.sx * Program.Globals.sy); ++k)
                            {
                                Program.Globals.levelshut[k - last - 3] = (uint)Convert.ToInt32(getload[k - 1]);

                            }
                            Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
                            Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
                            Program.Globals.create = true;
                            Program.Globals.loadd = true;
                            Program.Globals.frm.Text = openFileDialog1.FileName;
                            Program.Globals.fcurrent = openFileDialog1.FileName;
                        }
                    }
                    if (result2 == DialogResult.Cancel)
                    {

                    }
                }
                else if (Program.Globals.create == false)
                {
                    Program.Globals.savechange = false;

                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.Filter = "BCC 1:16 files |*.bcc16|All Files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.Multiselect = false;
                    DialogResult result = openFileDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string[] getload = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                        Program.Globals.sx = (uint)Convert.ToInt32(getload[0]);
                        Program.Globals.sy = (uint)Convert.ToInt32(getload[1]);
                        int last = (int)(16 * Program.Globals.sx * Program.Globals.sy);
                        for (int k = 1; k <= 16 * Program.Globals.sx * Program.Globals.sy; ++k)
                        {
                            Program.Globals.level[k - 1] = (uint)Convert.ToInt32(getload[k + 1]);
                        }
                        for (int k = last + 3; k <= (last + 2 + Program.Globals.sx * Program.Globals.sy); ++k)
                        {
                            Program.Globals.levelshut[k - last - 3] = (uint)Convert.ToInt32(getload[k - 1]);

                        }
                        Program.Globals.map1.load("tileset.png", Program.Globals.opensize, Program.Globals.levelshut, Program.Globals.sx, Program.Globals.sy);
                        Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.level, Program.Globals.sx * 4, Program.Globals.sy * 4);
                        Program.Globals.create = true;
                        Program.Globals.loadd = true;
                        Program.Globals.frm.Text = openFileDialog1.FileName;
                        Program.Globals.fcurrent = openFileDialog1.FileName;
                    }
                }
            }
            
            

        }
        private void InitRecentItems()
        {
            // prepare list of recent items
            _recentItems = new List<RecentItemsPropertySet>();
            _recentItems.Add(new RecentItemsPropertySet()
                             {
                                 Label = "Recent item 1",
                                 LabelDescription = "Recent item 1 description",
                                 Pinned = true
                             });
            _recentItems.Add(new RecentItemsPropertySet()
                             {
                                 Label = "Recent item 2",
                                 LabelDescription = "Recent item 2 description",
                                 Pinned = false
                             });

            _ribbonRecentItems.RecentItems = _recentItems;
        }
        void _recentItems_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (e.Key.PropertyKey == RibbonProperties.RecentItems)
            {
                // go over recent items
                object[] objectArray = (object[])e.CurrentValue.PropVariant.Value;
                for (int i = 0; i < objectArray.Length; ++i)
                {
                    IUISimplePropertySet propertySet = objectArray[i] as IUISimplePropertySet;

                    if (propertySet != null)
                    {
                        PropVariant propLabel;
                        propertySet.GetValue(ref RibbonProperties.Label, 
                                             out propLabel);
                        string label = (string)propLabel.Value;

                        PropVariant propLabelDescription;
                        propertySet.GetValue(ref RibbonProperties.LabelDescription, 
                                             out propLabelDescription);
                        string labelDescription = (string)propLabelDescription.Value;

                        PropVariant propPinned;
                        propertySet.GetValue(ref RibbonProperties.Pinned, 
                                             out propPinned);
                        bool pinned = (bool)propPinned.Value;

                        // update pinned value
                        _recentItems[i].Pinned = pinned;
                    }
                }
            }
            else if (e.Key.PropertyKey == RibbonProperties.SelectedItem)
            {
                // get selected item index
                uint selectedItem = (uint)e.CurrentValue.PropVariant.Value;

                // get selected item label
                PropVariant propLabel;
                e.CommandExecutionProperties.GetValue(ref RibbonProperties.Label, 
                                                    out propLabel);
                string label = (string)propLabel.Value;

                // get selected item label description
                PropVariant propLabelDescription;
                e.CommandExecutionProperties.GetValue(ref RibbonProperties.LabelDescription, 
                                                    out propLabelDescription);
                string labelDescription = (string)propLabelDescription.Value;

                // get selected item pinned value
                PropVariant propPinned;
                e.CommandExecutionProperties.GetValue(ref RibbonProperties.Pinned, 
                                                    out propPinned);
                bool pinned = (bool)propPinned.Value;
            }

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                Vector2f posss = Program.Globals.renderwindow.MapPixelToCoords(new Vector2i(Program.Globals.frm.panel1.Size.Width / 2, Program.Globals.frm.panel1.Size.Height / 2));
                Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                Vector2f move = new Vector2f(0, (float)((0.5) * (e.NewValue - e.OldValue) * (float)(1.1 * Program.Globals.track)));
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

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                Vector2f posss = Program.Globals.renderwindow.MapPixelToCoords(new Vector2i(Program.Globals.frm.panel1.Size.Width / 2, Program.Globals.frm.panel1.Size.Height / 2));
                Program.Globals.view2 = Program.Globals.renderwindow.GetView();
                Vector2f move = new Vector2f((float)((0.5) * (e.NewValue - e.OldValue) * (float)(1.1 * Program.Globals.track)), 0);
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
        }

		private void _ribbon_Click(object sender, EventArgs e)
		{

		}
	}
}

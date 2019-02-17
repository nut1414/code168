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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            for (int i = 1; i <= Program.Globals.maxpage; ++i)
                listView1.Items.Add(i.ToString());
            listView1.Items[0].Selected = true;
        }

        private void button3_Click(object sender, EventArgs e)
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
            Program.Globals.maxpage = listView1.Items.Count;
        }

        private void button1_Click(object sender, EventArgs e)
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
            Program.Globals.maxpage = listView1.Items.Count;
        }

        private void button2_Click(object sender, EventArgs e)
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
            Program.Globals.maxpage = listView1.Items.Count;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Add((listView1.Items.Count + 1).ToString());
            Program.Globals.maxpage = listView1.Items.Count;
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView1.SelectedItems[0];
                Program.Globals.page = selected.Index + 1;
                for (int k = 0; k < Program.Globals.sx * Program.Globals.sy; ++k)
                {
                    Program.Globals.levelshow[k] = Program.Globals.level1[k, Program.Globals.page];
                }
                Program.Globals.map.load("tileset.png", Program.Globals.tiless, Program.Globals.levelshow, Program.Globals.sx, Program.Globals.sy);
            }
        }
    }
}
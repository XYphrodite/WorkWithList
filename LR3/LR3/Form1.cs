using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace LR3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.KeyPress += (sender, e) => e.Handled = true;
            comboBox2.KeyPress += (sender, e) => e.Handled = true;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) //strip открыть
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();
            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(OpenDlg.FileName, Encoding.GetEncoding(1251));
                richTextBox1.Text = Reader.ReadToEnd();
                Reader.Close();
            }
            OpenDlg.Dispose();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) //strip сохранить
        {
            OpenFileDialog SaveDlg = new OpenFileDialog();
            if (SaveDlg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Writer = new StreamWriter(SaveDlg.FileName);
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    Writer.WriteLine((string)listBox2.Items[i]);
                }
                Writer.Close();
            }
            SaveDlg.Dispose();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) //strip выход
        {
            Application.Exit();
        }

        private void button14_Click(object sender, EventArgs e) //ок
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox1.BeginUpdate();
            string[] Strings = richTextBox1.Text.Split(new char[] { '\n', '\t', ' ' });
            foreach (string s in Strings)
            {
                string Str = s.Trim();
                if (Str == String.Empty) continue;
                if (radioButton1.Checked) listBox1.Items.Add(Str);
                if (radioButton2.Checked)
                {
                    if (Regex.IsMatch(Str, @"\d")) listBox1.Items.Add(Str);
                }
                if (radioButton3.Checked)
                {
                    if (Regex.IsMatch(Str, @"\w")) listBox1.Items.Add(Str);
                }
            }
            listBox1.EndUpdate();
        }

        private void button13_Click(object sender, EventArgs e) //выход
        {
            Application.Exit();
        }

        private void button12_Click(object sender, EventArgs e) //сброс
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            richTextBox1.Clear();
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "Сортировка по ...";
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = "Сортировка по ...";
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e) //очистить первый раздел
        {
            listBox1.Items.Clear();
        }

        private void button10_Click(object sender, EventArgs e) //очистить второй раздел
        {
            listBox2.Items.Clear();
        }

        private void button11_Click(object sender, EventArgs e) //поиск
        {
            listBox3.Items.Clear();
            string Find = textBox1.Text;
            if (checkBox1.Checked)
            {
                foreach(string String in listBox1.Items)
                {
                    if (String.Contains(Find)==true) listBox3.Items.Add(String);
                }
            }
            if (checkBox2.Checked)
            {
                foreach (string String in listBox2.Items)
                {
                    if (String.Contains(Find)==true) listBox3.Items.Add(String);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e) //добавить
        {
            Form2 AddRec = new Form2();
            AddRec.Owner = this;
            AddRec.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e) //удалить
        {
            for (int i =listBox1.Items.Count-1; i >= 0; i--)
            {
                if (listBox1.GetSelected(i)) listBox1.Items.RemoveAt(i);
            }
            for (int i = listBox2.Items.Count - 1; i >= 0; i--)
            {
                if (listBox2.GetSelected(i)) listBox2.Items.RemoveAt(i);
            }
        }

        private void button1_Click(object sender, EventArgs e) //переносы
        {
            listBox2.BeginUpdate();
            foreach (object item in listBox1.SelectedItems)
            {
                listBox2.Items.Add(item);
            }
            listBox2.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.BeginUpdate();
            foreach (object item in listBox2.SelectedItems)
            {
                listBox1.Items.Add(item);
            }
            listBox1.EndUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(listBox2.Items);
            listBox2.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e) //сортировки
        {
            if (comboBox1.SelectedIndex == 0)
            {
                listBox1.Sorted = true;
            }
            ArrayList list = new ArrayList();
            if (comboBox1.SelectedIndex == 1)
            {
                foreach (object L in listBox1.Items)
                {
                    list.Add(L);
                }
                list.Sort();
                list.Reverse();
                listBox1.Items.Clear();
                foreach(object L in list)
                {
                    listBox1.Items.Add(L);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                listBox2.Sorted = true;
            }
            ArrayList list = new ArrayList();
            if (comboBox1.SelectedIndex == 1)
            {
                foreach (object L in listBox2.Items)
                {
                    list.Add(L);
                }
                list.Sort();
                list.Reverse();
                listBox2.Items.Clear();
                foreach (object L in list)
                {
                    listBox2.Items.Add(L);
                }
            }
        }
    }
}

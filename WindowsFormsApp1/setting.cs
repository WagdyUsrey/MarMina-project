using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class setting : Form
    {
        StartForm s;
        
        public setting(StartForm s)
        {
            InitializeComponent();
            this.s = s;
        }

        private void setting_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            //StartForm s = new StartForm();
            s.Show();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> l = new List<String>();
            l.Add(textBox2.Text);
            l.Add(textBox3.Text);
            l.Add(textBox4.Text);
            l.Add(textBox5.Text);
            Random r = new Random();
            String x = s.transform(textBox1.Text);
            for (int i = 0; i < 4; i++)
            {
                String x1=  l[r.Next(l.Count - 1)];
                l.Remove(x1);
                x += ('*'+s.transform(x1));

            }
            x += ('*' + s.transform(textBox2.Text));
            FileStream stream = new FileStream("questions.txt",FileMode.Append);
            StreamWriter SW = new StreamWriter(stream);
            SW.WriteLine(x);
            SW.Close();
            stream.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0 &&
                textBox3.Text.Length != 0 && textBox4.Text.Length != 0 &&
                textBox5.Text.Length != 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;

        }
    }
}

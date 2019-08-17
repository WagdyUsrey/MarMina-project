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
                savebtn.Enabled = true;
            else
                savebtn.Enabled = false;

        }

        private void UserNametb_TextChanged(object sender, EventArgs e)
        {
            if (Passwordtb.Text.Length > 5 && UserNametb.Text.Length != 0)
                SignInbtn.Enabled = true;
            else
                SignInbtn.Enabled = false;
        }

        private void SignInbtn_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("admin.txt", FileMode.OpenOrCreate);
            StreamReader SR = new StreamReader (stream);
            bool flag = true;
            while (SR.Peek() != -1)
            {
                String x = SR.ReadLine();
                String[] x1 = x.Split('*');
                if(s.transform(x1[0]) == UserNametb.Text && s.transform(x1[1]) == Passwordtb.Text)
                {
                    tabControl.Visible = true;
                    UserNametb.Clear();
                    UserNametb.Visible = false;
                    label7.Visible = false;
                    label6.Visible = false;
                    SignInbtn.Visible = false;
                    Passwordtb.Clear();
                    Passwordtb.Visible = false;
                    flag = false;
                    break;
                }
            }
            SR.Close();
            stream.Close();
            if (flag)
            {
                MessageBox.Show("you enter a wrong admin name or admin password");
                Passwordtb.Clear();
                UserNametb.Clear();
            }

        }

        private void AdminNametb_TextChanged(object sender, EventArgs e)
        {
            if (AdminNametb.Text.Length != 0 && AdminPasswordtb.Text.Length > 5)
                SaveAdmin.Enabled = true;
            else
                SaveAdmin.Enabled = false;
        }

        private void AdminPasswordtb_TextChanged(object sender, EventArgs e)
        {
            if (AdminNametb.Text.Length != 0 && AdminPasswordtb.Text.Length > 5)
                SaveAdmin.Enabled = true;
            else
                SaveAdmin.Enabled = false;
        }

        private void SaveAdmin_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("admin.txt", FileMode.Append);
            StreamWriter SW = new StreamWriter(stream);
            SW.WriteLine(s.transform(AdminNametb.Text) + '*' + s.transform(AdminPasswordtb.Text));

            SW.Close();
            stream.Close();

            AdminPasswordtb.Clear();
            AdminNametb.Clear();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("questions.txt", FileMode.Append);
            StreamWriter SW = new StreamWriter(stream);
            List<String> l = new List<String>();

            l.Add(textBox2.Text);
            l.Add(textBox3.Text);
            l.Add(textBox4.Text);
            l.Add(textBox5.Text);

            Random r = new Random();
            String q = s.transform(textBox1.Text) + '*';

            for (int i = 0; i < 4; i++)
            {
                String x = l[r.Next(l.Count - 1)];
                q += (s.transform(x) +'*');
                l.Remove(x);
            }
            q += s.transform(textBox2.Text);
            SW.WriteLine(q);

            SW.Close();
            stream.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0 && textBox3.Text.Length != 0 && textBox4.Text.Length != 0 && textBox5.Text.Length != 0)
                savebtn.Enabled = true;
            else
                savebtn.Enabled = false;
        }

        private void setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

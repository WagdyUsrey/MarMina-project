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
    
    public partial class StartForm : Form
    {
        DateTime d; 
        String name;
        int Qnumber;
        int CorrectAnswer;
        List<question> l;
        String questionFileName = "questions.txt";
        question q1;
        setting s;
        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void nametb_TextChanged(object sender, EventArgs e)
        {
            if(nametb.Text.Length != 0)
            {
                Startbtn.Enabled = true;
            }
            else
            {
                Startbtn.Enabled = false;
            }
        }

        private void Startbtn_Click(object sender, EventArgs e)
        {
            this.Startbtn.Hide();
            this.label2.Hide();
            this.nametb.Hide();
            this.settingbtn.Hide();

            name = nametb.Text;

            this.q.Show();
            this.choices.Show();
            this.nextbtn.Show();

            Qnumber = 0;
            CorrectAnswer = 0;

            l = new List<question>();
            d = DateTime.Now;
            loadQuestions();
            nextQuestion();
        }

        private void nextQuestion()
        {
            if (Qnumber == 10)
            {
                // end of exam
                DateTime d2 = DateTime.Now;
                if (    q1.Right(   choices.SelectedItem.ToString()    )    )
                    CorrectAnswer++;

                MessageBox.Show(name+" you answered "+CorrectAnswer+" questions correct from total 10 questions");

                FileStream stream = new FileStream("user.txt", FileMode.Append);
                StreamWriter SW = new StreamWriter(stream);
                SW.WriteLine(   transform( name + " started at " + d.ToString() + " and finished at " + d2.ToString() +
                    " and answered " + CorrectAnswer + " questions correct from total 10 questions")    );

                SW.Close();
                stream.Close();

                this.q.Hide();
                this.choices.Hide();
                this.nextbtn.Hide();

                this.Startbtn.Show();
                this.label1.Show();
                this.label2.Show();
                this.nametb.Show();
                this.settingbtn.Show();

                nametb.Clear();

                return;
            }
            if (Qnumber != 0)
            {
                if (q1.Right(choices.SelectedItem.ToString()))
                    CorrectAnswer++;
            }



            Random r = new Random();
            q1 = l[ r.Next(0, l.Count - 1)  ];
            l.Remove(q1);
            q.Text = q1.Q;
            choices.Items.Clear();
            choices.Text = ""; 
            choices.Items.Add(q1.ch1);
            choices.Items.Add(q1.ch2);
            choices.Items.Add(q1.ch3);
            choices.Items.Add(q1.ch4);
            Qnumber++;
            nextbtn.Enabled = false;

        }

        private void loadQuestions()
        {
            //where we stoped
            FileStream stream = new FileStream(questionFileName, FileMode.OpenOrCreate);
            StreamReader SR = new StreamReader(stream);

            while (SR.Peek() != -1)
            {
                String [] x = SR.ReadLine().Split('*');

                l.Add(new question(transform(x[0]), transform(x[5]), transform(x[1]),
                    transform(x[2]), transform(x[3]), transform(x[4])));
            }
            SR.Close();
            stream.Close();
        }

        private void nextbtn_Click(object sender, EventArgs e)
        {
            nextQuestion();
        }

        private void choices_SelectedIndexChanged(object sender, EventArgs e)
        {
            nextbtn.Enabled = true;
        }

        private void settingbtn_Click(object sender, EventArgs e)
        {
            if(s == null)
                s = new setting(this);
            s.Show();
            this.Hide();
        }

        public string transform(string input)
        {
            string output = "";
            foreach (char ch in input)
            {
                if (ch % 256 < 128)
                {
                    char c = (char)(ch + 128);
                    output += c;
                }
                else
                {
                    char c = (char)(ch - 128);
                    output += c;
                }
            }
            return output;
        }   

    }

    class question
    {
        public String Q;
        public String a;
        public String ch1;
        public String ch2;
        public String ch3;
        public String ch4;

        public question(String q, String a, String ch1, String ch2, String ch3, String ch4)
        {
            this.Q = q;
            this.a = a;
            this.ch1 = ch1;
            this.ch2 = ch2;
            this.ch3 = ch3;
            this.ch4 = ch4;
        }

        public bool Right(String x)
        {
            return (x == this.a);
        }
    }
}

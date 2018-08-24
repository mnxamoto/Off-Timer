using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Off_Таймер
{
    public partial class Form1 : Form
    {
        DateTime time1;
        float KolSecond;
        string fileName;

        public Form1()
        {
            TopMost = true;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();

            time1 = dateTimePicker1.Value;

            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Value = dateTimePicker2.Value.AddHours(dateTimePicker1.Value.Hour);
            dateTimePicker2.Value = dateTimePicker2.Value.AddMinutes(dateTimePicker1.Value.Minute);
            dateTimePicker2.Value = dateTimePicker2.Value.AddSeconds(dateTimePicker1.Value.Second);

            KolSecond = time1.Second + time1.Minute * 60 + time1.Hour * 3600;
            KolSecond = KolSecond / 100;
            timer3.Enabled = true;
            timer3.Start();
            timer3.Interval = Convert.ToInt32(1000 * KolSecond);

            progressBar1.Value = 0;
            textBox5.Text = "0%";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time1 = time1.AddSeconds(-1);
            textBox4.Text = time1.ToString("HH:mm:ss");
            this.Text = textBox4.Text;

            if (textBox4.Text == "00:01:00")
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();

            time1 = DateTime.MinValue.AddYears(2000);

            dateTimePicker1.Value = time1;
            dateTimePicker1.Value = dateTimePicker1.Value.AddHours(dateTimePicker2.Value.Hour - DateTime.Now.Hour);
            dateTimePicker1.Value = dateTimePicker1.Value.AddMinutes(dateTimePicker2.Value.Minute - DateTime.Now.Minute);
            dateTimePicker1.Value = dateTimePicker1.Value.AddSeconds(dateTimePicker2.Value.Second - DateTime.Now.Second);

            time1 = dateTimePicker1.Value;

            KolSecond = time1.Second + time1.Minute * 60 + time1.Hour * 3600;
            KolSecond = KolSecond / 100;
            timer3.Enabled = true;
            timer3.Start();
            timer3.Interval = Convert.ToInt32(1000 * KolSecond);

            progressBar1.Value = 0;
            textBox5.Text = "0%";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            textBox5.Text = Convert.ToString(progressBar1.Value) + "%";

            if (progressBar1.Value == 100)
            {
                timer3.Enabled = false;
                timer1.Enabled = false;
                textBox4.Text = "00:00:00";

                if (checkBox1.Checked == true)
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "/c shutdown -s -t 00";
                    p.Start();              
                }

                if (checkBox2.Checked == true)
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "/c restart -s -t 00";
                    p.Start(); 
                }

                if (checkBox3.Checked == true)
                {
                    System.Diagnostics.Process.Start(fileName); //Открытие файла
                }



                if (checkBox4.Checked == true)
                {

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;

                textBox6.Text = fileName;

                MessageBox.Show("Не забудь нажать галочку, блядь, блять!\r\n(С уважением администрация)", "Варнинг сука! Варнинг!");

                checkBox3.Checked = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer3.Enabled = false;
            timer1.Enabled = false;

            dateTimePicker2.Value = DateTime.Now;

            progressBar1.Value = 0;
            textBox5.Text = "0%";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.MinValue.AddYears(2000);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("dd MMMM yyyy");
            textBox3.Text = DateTime.Now.ToString("HH:mm:ss");

            dateTimePicker1.Value = DateTimePicker.MinimumDateTime.AddHours(4);

            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;

            textBox4.Select();

            button1_Click(null, null);
        }
    }
}

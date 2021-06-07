using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Auto_Run_Collection
{
    public partial class AutoRun : Form
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        DateTime itime = DateTime.Now;
        int data = 0;
        Task timerTask;
        public AutoRun()
        {
            InitializeComponent();
            textBox3.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            int time = Convert.ToInt32(txtTime.Text);
            if (comboBox1.Text == "Day")
                timerTask = RunPeriodically(runCollection, TimeSpan.FromDays(time), tokenSource.Token);
            if (comboBox1.Text == "Hours")
                timerTask = RunPeriodically(runCollection, TimeSpan.FromHours(time), tokenSource.Token);
            if (comboBox1.Text == "Minute")
                timerTask = RunPeriodically(runCollection, TimeSpan.FromMinutes(time), tokenSource.Token);
            if (comboBox1.Text == "Seconds")
                timerTask = RunPeriodically(runCollection, TimeSpan.FromSeconds(time), tokenSource.Token);
            button2.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            button2.Enabled = false;
            button1.Enabled = true;
        }
        async Task RunPeriodically(Action action, TimeSpan interval, CancellationToken token)
        {
            while (true)
            {
                action();
                await Task.Delay(interval, token);
            }
        }
        void runCollection()
        {
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.CreateNoWindow = true;
            ps.FileName = "cmd.exe";
            ps.WindowStyle = ProcessWindowStyle.Normal;
            if (data==1)
                ps.Arguments = @"/k cd " + @textBox1.Text.Replace(@"\\", @"\") + " && newman run " + textBox2.Text +" -d " + textBox3.Text +" -r htmlextra";
            else
                ps.Arguments = @"/k cd " + @textBox1.Text.Replace(@"\\", @"\") + " && newman run " + textBox2.Text + " -r htmlextra";
            Process.Start(ps);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (data == 0)
            {
                data = 1;
                textBox3.Enabled = true;
            }
            else
            {
                data = 0;
                textBox3.Enabled = false;
                textBox3.Clear();
            }
        }
    }
}

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
        public AutoRun()
        {
            InitializeComponent();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(textBox4.Text);
            Task timerTask = RunPeriodically(runCollection, TimeSpan.FromSeconds(time), tokenSource.Token);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
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
            //ps.Arguments = @"/k cd C:\Users\diepg\Postman\Collection && newman run Input_Output.postman_collection.json -d in_out.csv -r htmlextra";
            ps.Arguments = @"/k cd " + @textBox1.Text.Replace(@"\\", @"\") + " && newman run " + textBox2.Text +" -d " + textBox3.Text +" -r htmlextra";
            Process.Start(ps);
        }
    }
}

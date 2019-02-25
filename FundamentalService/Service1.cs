using Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalService
{
    public partial class Service1 : ServiceBase
    {
        public static bool x = true;
        public Service1()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            this.OnStart(null);
        }

        ScheduleTimer Timer = new ScheduleTimer();
        protected override void OnStart(string[] args)
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.log";
            System.IO.File.AppendAllLines(strPath, new[] { "Starting time : " + DateTime.Now.ToString() });
            this.timer1.Start();
            Timer.Elapsed += new ScheduledEventHandler(timer_Elapsed);
            Timer.AddEvent(new ScheduledTime("Daily", "16:25"));
            Timer.Start();
        }

        protected override void OnStop()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.log";
            System.IO.File.AppendAllLines(strPath, new[] { "Stop time : " + DateTime.Now.ToString() });
            this.timer1.Stop();
            Timer.Stop();
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.log";
            System.IO.File.AppendAllLines(strPath, new[] { "..calling time : " + DateTime.Now.ToString() });

            if (x)
            {
                var server = new ServerSocket();
                server.StartListening();
                x = false;
                System.IO.File.AppendAllLines(strPath, new[] { "Server Run : " + DateTime.Now.ToString() });
            }

        }

        private void timer_Elapsed(object sender, ScheduledEventArgs e)
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.log";
            System.IO.File.AppendAllLines(strPath, new[] { "Schedule Time : " + DateTime.Now.ToString() });

            var server = new ServerSocket();
            server.StartListening();
        }
    }
}

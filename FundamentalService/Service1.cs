using Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace FundamentalService
{
    public partial class Service1 : ServiceBase
    {
        string dateStart = ConfigurationManager.AppSettings["StartProgramGetData"];
        public static bool run = true;
        public static string nameFile = @"\Fundamental" + DateTime.Now.ToString("yyyyMMdd") + ".log";
        public static string strPath = AppDomain.CurrentDomain.BaseDirectory + @"\logs";
        public static string fullPath = strPath + nameFile;
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
            if (!Directory.Exists(strPath))  // if it doesn't exist, create
                Directory.CreateDirectory(strPath);

            System.IO.File.AppendAllLines(fullPath, new[] { "[INFO] Starting time : " + DateTime.Now.ToString() });
            this.timer1.Start();
            Timer.Elapsed += new ScheduledEventHandler(SET2DataBase);
            Timer.AddEvent(new ScheduledTime("Daily", dateStart));
            Timer.Start();
        }

        protected override void OnStop()
        {
            System.IO.File.AppendAllLines(fullPath, new[] { "[INFO] Stop time : " + DateTime.Now.ToString() });
            this.timer1.Stop();
            Timer.Stop();
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //System.IO.File.AppendAllLines(fullPath, new[] { "..calling time : " + DateTime.Now.ToString() });

            if (run)
            {
                var server = new ServerSocket();
                server.StartListening();
                run = false;
                System.IO.File.AppendAllLines(fullPath, new[] { "[INFO] Server Run : " + DateTime.Now.ToString() });
            }

        }

        private void SET2DataBase(object sender, ScheduledEventArgs e)
        {
            System.IO.File.AppendAllLines(fullPath, new[] { "[INFO] Schedule Time : " + DateTime.Now.ToString() });
            var getData = new FundamentalSET100();
            getData.Run();
            System.IO.File.AppendAllLines(fullPath, new[] { "[INFO] UpdateDatabase : " + DateTime.Now.ToString() });

        }
    }
}

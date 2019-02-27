﻿using Schedule;
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
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Config                                                          |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        string dateStart = ConfigurationManager.AppSettings["StartProgramGetData"];
        private static string strPath = AppDomain.CurrentDomain.BaseDirectory + @"\logs";
        private static Plog log = new Plog();
        public static bool run = true;
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Constructor                                                     |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public Service1()
        {
            InitializeComponent();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Helper Function                                                 |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public void OnDebug()
        {
            this.OnStart(null);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | OnStart Function                                                |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        ScheduleTimer Timer = new ScheduleTimer();
        protected override void OnStart(string[] args)
        {
            if (!Directory.Exists(strPath))  // if it doesn't exist, create
                Directory.CreateDirectory(strPath);

            log.LOGI("[Service::OnStart] Service starting");
            this.timer1.Start();
            Timer.Elapsed += new ScheduledEventHandler(SET2DataBase);
            Timer.AddEvent(new ScheduledTime("Daily", dateStart));
            Timer.Start();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | OnStop Function                                                 |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        protected override void OnStop()
        {
            log.LOGI("[Service::OnStop] Service stop");
            this.timer1.Stop();
            Timer.Stop();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other Function                                                  |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //System.IO.File.AppendAllLines(fullPath, new[] { "..calling time : " + DateTime.Now.ToString() });
            if (run)
            {
                var server = new ServerSocket();
                server.StartListening();
                run = false;
                log.LOGI("[Service::timer1_Elapsed] Socket server Run");
            }

        }
        private void SET2DataBase(object sender, ScheduledEventArgs e)
        {
            log.LOGI("[Service::SET2DataBase] Task scheduler running");
            var getData = new FundamentalSET100();
            getData.Run();
            log.LOGI("[Service::SET2DataBase] Update database ");

        }
    }
}

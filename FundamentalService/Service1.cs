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
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Config                                                          |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        string dateStart = ConfigurationManager.AppSettings["StartProgramGetData"];
        int TimerResetKey = int.Parse(ConfigurationManager.AppSettings["TimerResetKey"]) * 60000;
        int TimerNews = int.Parse(ConfigurationManager.AppSettings["TimerNews"]) * 60000;
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
            // Set Timer
            this.timer2.Interval = TimerResetKey;
            this.timer3.Interval = TimerNews;
            this.timer1.Start();
            this.timer2.Start();
            this.timer3.Start();
            SetAccessToken();
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
            this.timer2.Stop();
            this.timer3.Stop();
            Timer.Stop();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Timer Function                                                  |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
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
            var getData = new FundamentalSET100();
            getData.Run();
        }
        private void Kaohoon2Database(object sender, System.Timers.ElapsedEventArgs e)
        {
            var getData = new FundamentalKaohoon();
            var getData1 = new FundamentalSET100();
            getData.Run();
            getData1.RunNews();
        }
        private void ResetAccessToken(object sender, System.Timers.ElapsedEventArgs e)
        {
            SetAccessToken();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other Function                                                  |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        private void SetAccessToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var process = new GetDatabase();
            process.SetAccessToken(token);
            process.GetAccessToken();
            log.LOGI("[Service::ResetAccessToken] Reset Access Token");
        }
    }
}

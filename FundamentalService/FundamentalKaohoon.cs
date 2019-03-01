using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FundamentalService
{
    class FundamentalKaohoon
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Config                                                          |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        private static string DatabaseServer = ConfigurationManager.AppSettings["DatabaseServer"];
        private static string Database = ConfigurationManager.AppSettings["Database"];
        private static string Username = ConfigurationManager.AppSettings["DatabaseUsername"];
        private static string Password = ConfigurationManager.AppSettings["DatabasePassword"];
        private static Plog log = new Plog();
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class KaohoonData
        {
            public KaohoonData() { }

            // Properties.
            public string Kaohoon_data_id { get; set; } = "null";
            public string Symbol { get; set; } = "null";
            public string Date { get; set; } = "null";
            public string Year { get; set; } = "null";
            public string Quarter { get; set; } = "null";
            public string TargetPrice { get; set; } = "null";
            public string Trends { get; set; } = "null";
            public string Divide { get; set; } = "null";
            public string Topic { get; set; } = "null";

        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Main Function                                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public void Run()
        {
            log.LOGI("[FundamentalKaohoon::Run] Start update data Kaohoon");
            var url1 = $"https://marketdata.set.or.th/mkt/sectorquotation.do?sector=SET100&language=th&country=TH";
            List<string> symbols = new List<string>();
            // Using HtmlAgilityPack
            var Webget1 = new HtmlWeb();
            var doc1 = Webget1.Load(url1);

            foreach (HtmlNode node in doc1.DocumentNode.SelectNodes("//td//a"))
            {
                string utf8_String = node.InnerText;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                utf8_String = utf8_String.Replace(" ", String.Empty);
                if (utf8_String.IndexOf("\n") >= 0)
                {
                    utf8_String = utf8_String.Substring(2, utf8_String.Length - 4);
                    symbols.Add(utf8_String);
                }

            }

            for (var i = 0; i < symbols.Count; i++)
                IndividualStock(symbols[i]);
            log.LOGI("[FundamentalKaohoon::Run] End update data Kaohoon");
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | ScrapingWeb Function                                            |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        static public void IndividualStock(string symbol)
        {

            var url = $"https://www.kaohoon.com/content/tag/{symbol}";
            List<string> topics = new List<string>();
            List<string> date = new List<string>();
            // Using HtmlAgilityPack
            var Webget1 = new HtmlWeb();
            var doc1 = Webget1.Load(url);

            List<KaohoonData> kaohoon_data = new List<KaohoonData>();
            foreach (HtmlNode node in doc1.DocumentNode.SelectNodes("//article//header//h2//a"))
            {
                string utf8_String = node.InnerText;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                var result = CutString(utf8_String);
                KaohoonData kaohoon_tmp = new KaohoonData();
                kaohoon_tmp.Topic = $"'{utf8_String}'";
                kaohoon_tmp.Symbol = result[0];
                if (result[0] == symbol)
                    for (var i = 0; i < result.Length; i++)
                    {
                        if (result[i].IndexOf("ไตรมาส") > -1 && i + 1 < result.Length)
                        {
                            if (result[i + 1].IndexOf("/") > -1)
                            {
                                var Quarter_Year = result[i + 1].Split('/');
                                kaohoon_tmp.Quarter = Quarter_Year[0].Substring(Quarter_Year[0].Length - 1, 1);
                                kaohoon_tmp.Year = Quarter_Year[1].Substring(0, 2);
                            }
                            else if (result[i].IndexOf("/") > -1)
                            {
                                var Quarter_Year = result[i].Split('/');
                                kaohoon_tmp.Quarter = Quarter_Year[0].Substring(Quarter_Year[0].Length - 1, 1);
                                kaohoon_tmp.Year = Quarter_Year[1].Substring(0, 2);
                            }
                        }
                        else if (result[i].IndexOf("เป้า") > -1 && i + 1 < result.Length)
                        {
                            double x;
                            var isNumbel = double.TryParse(result[i + 1], out x);
                            if (!isNumbel)
                            {
                                kaohoon_tmp.TargetPrice = Regex.Match(result[i + 1], @"\d+").Value;
                                if (kaohoon_tmp.TargetPrice == "")
                                    kaohoon_tmp.TargetPrice = "null";
                            }
                            else
                                kaohoon_tmp.TargetPrice = result[i + 1];
                        }
                        else if (result[i].IndexOf("ปันผล") > -1 && i + 1 < result.Length)
                        {
                            kaohoon_tmp.Divide = $"'{result[i]} {result[i + 1]}'";
                        }
                        else if (result[i].IndexOf("%") > -1)
                        {
                            kaohoon_tmp.Trends = $"'{result[i - 1]} {result[i]}'";
                        }
                    }
                else
                {
                    kaohoon_tmp.Symbol = "null";
                }
                kaohoon_data.Add(kaohoon_tmp);
            }

            int index = 0;
            foreach (HtmlNode node in doc1.DocumentNode.SelectNodes("//article//header//div//span[@class='posted-on']//a"))
            {
                string utf8_String = node.ChildNodes[0].InnerText;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                utf8_String = utf8_String.Replace(",", String.Empty);
                kaohoon_data[index].Kaohoon_data_id = ChangeKaohoonDataIdFormat(utf8_String, kaohoon_data[index].Symbol);
                kaohoon_data[index++].Date = ChangeDateFormat(utf8_String);
            }

            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            // Insert database finance_stat_daily
            foreach (var value in kaohoon_data)
            {
                if (value.Symbol != "null")
                {
                    string sql = $"Select * from dbo.kaohoon_data where Kaohoon_data_id = {value.Kaohoon_data_id} AND Symbol = '{value.Symbol}'";
                    SqlCommand command = new SqlCommand(sql, cnn);
                    command.Parameters.AddWithValue("@zip", "india");
                    bool event_case = true;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sql = $"UPDATE dbo.kaohoon_data SET " +
                                $"Kaohoon_data_id={value.Kaohoon_data_id}," +
                                $"Symbol='{value.Symbol}'," +
                                $"Date={value.Date}," +
                                $"Year={value.Year}," +
                                $"Quarter={value.Quarter}," +
                                $"TargetPrice={value.TargetPrice}," +
                                $"Trends={value.Trends}," +
                                $"Divide={value.Divide}," +
                                $"Topic={value.Topic}" +
                                $"where Kaohoon_data_id = {value.Kaohoon_data_id} AND Symbol = '{value.Symbol}'";
                            event_case = false;
                        }
                        else
                        {
                            sql = "Insert into dbo.kaohoon_data " +
                            "(Kaohoon_data_id,Symbol,Date,Year,Quarter,TargetPrice,Trends,Divide,Topic) values (" +
                            $"{value.Kaohoon_data_id}," +
                            $"'{value.Symbol}'," +
                            $"{value.Date}," +
                            $"{value.Year}," +
                            $"{value.Quarter}," +
                            $"{value.TargetPrice}," +
                            $"{value.Trends}," +
                            $"{value.Divide}," +
                            $"{value.Topic})";
                        }
                    }
                    if (event_case)
                        InsertDatebase(sql, cnn);
                    else
                        UpdateDatebase(sql, cnn);
                }
            }

            cnn.Close();
            GC.Collect();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database Function                                               |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public static void UpdateDatebase(string sql, SqlConnection cnn)
        {
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();

            command = new SqlCommand(sql, cnn);

            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();

            command.Dispose();
        }
        public static void InsertDatebase(string sql, SqlConnection cnn)
        {
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();

            command = new SqlCommand(sql, cnn);

            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other    Function                                               |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public static string ChangeDateFormat(string date)
        {
            if (date == "null")
                return date;

            var parts = date.Split(' ');
            int mm;
            switch (parts[0])
            {
                case "มกราคม":
                    mm = 1;
                    break;
                case "กุมภาพันธ์":
                    mm = 2;
                    break;
                case "มีนาคม":
                    mm = 3;
                    break;
                case "เมษายน":
                    mm = 4;
                    break;
                case "พฤษภาคม":
                    mm = 5;
                    break;
                case "มิถุนายน":
                    mm = 6;
                    break;
                case "กรกฎาคม":
                    mm = 7;
                    break;
                case "สิงหาคม":
                    mm = 8;
                    break;
                case "กันยายน":
                    mm = 9;
                    break;
                case "ตุลาคม":
                    mm = 10;
                    break;
                case "พฤศจิกายน":
                    mm = 11;
                    break;
                default:
                    mm = 12;
                    break;
            }
            int dd = Convert.ToInt32(parts[1]);
            int yy = Convert.ToInt32(parts[2]);

            return $"'{yy}-{mm}-{dd}'";
        }
        public static string ChangeKaohoonDataIdFormat(string date, string symbol)
        {
            var parts = date.Split(' ');
            int mm;
            switch (parts[0])
            {
                case "มกราคม":
                    mm = 1;
                    break;
                case "กุมภาพันธ์":
                    mm = 2;
                    break;
                case "มีนาคม":
                    mm = 3;
                    break;
                case "เมษายน":
                    mm = 4;
                    break;
                case "พฤษภาคม":
                    mm = 5;
                    break;
                case "มิถุนายน":
                    mm = 6;
                    break;
                case "กรกฎาคม":
                    mm = 7;
                    break;
                case "สิงหาคม":
                    mm = 8;
                    break;
                case "กันยายน":
                    mm = 9;
                    break;
                case "ตุลาคม":
                    mm = 10;
                    break;
                case "พฤศจิกายน":
                    mm = 11;
                    break;
                default:
                    mm = 12;
                    break;
            }
            int dd = Convert.ToInt32(parts[1]);
            int yy = Convert.ToInt32(parts[2]);

            return $"'{dd}{mm}{yy}{symbol}'";
        }
        public static string[] CutString(string input)
        {

            var parts = input.Split(' ');

            return parts;
        }
    }
}

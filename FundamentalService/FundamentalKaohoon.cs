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
            public string Kaohoon_data_id { get; set; }
            public string Symbol { get; set; }
            public string Date { get; set; }
            public string Year { get; set; }
            public string Quarter { get; set; }
            public string TargetPrice { get; set; }
            public string Trends { get; set; }
            public string Divide { get; set; }
            public string Topic { get; set; }

        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Main Function                                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public void Run()
        {
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

            IndividualStock(symbols);
            log.LOGI("[FundamentalKaohoon::Run] Success update data Kaohoon");
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | ScrapingWeb Function                                            |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        static public void IndividualStock(List<string> symbols)
        {

            var url = $"https://www.kaohoon.com/content/category/%E0%B8%AA%E0%B9%88%E0%B8%AD%E0%B8%87%E0%B8%81%E0%B8%A5%E0%B9%89%E0%B8%AD%E0%B8%87%E0%B8%AB%E0%B8%B8%E0%B9%89%E0%B8%99%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%95%E0%B8%B1%E0%B8%A7";
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
                kaohoon_tmp.Topic = $"{utf8_String}";
                kaohoon_tmp.Symbol = result[0];
                var matchingvalues = symbols.FirstOrDefault(stringToCheck => stringToCheck.Contains(result[0]));
                if (matchingvalues != null)
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
                                    kaohoon_tmp.TargetPrice = null;
                            }
                            else
                                kaohoon_tmp.TargetPrice = result[i + 1];
                        }
                        else if (result[i].IndexOf("ปันผล") > -1 && i + 1 < result.Length)
                        {
                            kaohoon_tmp.Divide = $"{result[i]} {result[i + 1]}";
                        }
                        else if (result[i].IndexOf("%") > -1)
                        {
                            kaohoon_tmp.Trends = $"{result[i - 1]} {result[i]}";
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

            // Insert database kaohoon_data
            foreach (var value in kaohoon_data)
                if (value.Symbol != "null")
                    StatementDatabase(value, "kaohoon_data", $"Kaohoon_data_id = '{value.Kaohoon_data_id}' AND Symbol = '{value.Symbol}'");
            GC.Collect();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database Function                                               |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public static void UpdateDatebase(string sql, SqlConnection cnn)
        {
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                command = new SqlCommand(sql, cnn);
                adapter.UpdateCommand = new SqlCommand(sql, cnn);
                adapter.UpdateCommand.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                log.LOGE($"[FundamentalSET100::UpdateDatebase]  {sql}");
            }
        }
        public static void InsertDatebase(string sql, SqlConnection cnn)
        {
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                command = new SqlCommand(sql, cnn);
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                log.LOGE($"[FundamentalSET100::InsertDatebase]  {sql}");
            }
        }
        public static void StatementDatabase(object item, string db, string where)
        {
            string sql = "";
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            sql = $"Select * from dbo.{db} where {where}";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            bool event_case = false;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    sql = GetInsertSQL(item, db);
                    event_case = true;
                }
            }
            if (event_case)
                InsertDatebase(sql, cnn);

            cnn.Close();
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
                case "January":
                    mm = 1;
                    break;
                case "February":
                    mm = 2;
                    break;
                case "March":
                    mm = 3;
                    break;
                case "April":
                    mm = 4;
                    break;
                case "May":
                    mm = 5;
                    break;
                case "June":
                    mm = 6;
                    break;
                case "July":
                    mm = 7;
                    break;
                case "August":
                    mm = 8;
                    break;
                case "September":
                    mm = 9;
                    break;
                case "October":
                    mm = 10;
                    break;
                case "November":
                    mm = 11;
                    break;
                default:
                    mm = 12;
                    break;
            }
            int dd = Convert.ToInt32(parts[1]);
            int yy = Convert.ToInt32(parts[2]);

            return $"{yy}-{mm}-{dd}";
        }
        public static string ChangeKaohoonDataIdFormat(string date, string symbol)
        {
            var parts = date.Split(' ');
            int mm;
            switch (parts[0])
            {
                case "January":
                    mm = 1;
                    break;
                case "February":
                    mm = 2;
                    break;
                case "March":
                    mm = 3;
                    break;
                case "April":
                    mm = 4;
                    break;
                case "May":
                    mm = 5;
                    break;
                case "June":
                    mm = 6;
                    break;
                case "July":
                    mm = 7;
                    break;
                case "August":
                    mm = 8;
                    break;
                case "September":
                    mm = 9;
                    break;
                case "October":
                    mm = 10;
                    break;
                case "November":
                    mm = 11;
                    break;
                default:
                    mm = 12;
                    break;
            }
            int dd = Convert.ToInt32(parts[1]);
            int yy = Convert.ToInt32(parts[2]);

            return $"{dd}{mm}{yy}{symbol}";
        }
        public static string[] CutString(string input)
        {

            var parts = input.Split(' ');

            return parts;
        }
        public static string GetInsertSQL(object item, string db)
        {
            string sql = $"INSERT INTO dbo.{db} (:columns:) VALUES (:values:);";

            string[] columns = new string[item.GetType().GetProperties().Count()];
            string[] values = new string[item.GetType().GetProperties().Count()];
            int i = 0;
            foreach (var propertyInfo in item.GetType().GetProperties())
            {
                columns[i] = propertyInfo.Name;
                values[i++] = (string)(propertyInfo.GetValue(item, null));
            }

            //replacing the markers with the desired column names and values
            sql = FillColumnsAndValuesIntoInsertQuery(sql, columns, values);

            return sql;
        }
        public static string GetUpdateSQL(object item, string db, string whare)
        {
            string sql = $"UPDATE dbo.{db} SET :update: WHERE {whare} ;";

            string[] columns = new string[item.GetType().GetProperties().Count()];
            string[] values = new string[item.GetType().GetProperties().Count()];
            int i = 0;
            foreach (var propertyInfo in item.GetType().GetProperties())
            {
                columns[i] = propertyInfo.Name;
                values[i++] = (string)(propertyInfo.GetValue(item, null));
            }

            //replacing the markers with the desired column names and values
            sql = FillColumnsAndValuesIntoUpdateQuery(sql, columns, values);

            return sql;
        }
        public static string FillColumnsAndValuesIntoInsertQuery(string query, string[] columns, string[] values)
        {
            //joining the string arrays with a comma character
            string columnnames = string.Join(",", columns);
            //adding values with single quotation marks around them to handle errors related to string values
            string valuenames = ("'" + string.Join("','", values) + "'").Replace("''", "null");
            //replacing the markers with the desired column names and values
            return query.Replace(":columns:", columnnames).Replace(":values:", valuenames);
        }
        public static string FillColumnsAndValuesIntoUpdateQuery(string query, string[] columns, string[] values)
        {
            string result = "";
            for (int i = 0; i < columns.Length; i++)
                if (values[i] != null)
                    result += $"{columns[i]} = '{values[i]}'" + (i + 1 != columns.Length ? ", " : " ");
                else
                    result += $"{columns[i]} = null" + (i + 1 != columns.Length ? ", " : " ");
            //replacing the markers with the desired column names and values
            return query.Replace(":update:", result);
        }
    }
}

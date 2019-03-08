using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalService
{
    class GetDatabase
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Config                                                          |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        private static string accessToken = "qewrasd5a1e8q4120as5d4qw0e21asd56qw0e5as3q68q6";
        private static string DatabaseServer = ConfigurationManager.AppSettings["DatabaseServer"];
        private static string Database = ConfigurationManager.AppSettings["Database"];
        private static string Username = ConfigurationManager.AppSettings["DatabaseUsername"];
        private static string Password = ConfigurationManager.AppSettings["DatabasePassword"];
        private static string AuthUsername = ConfigurationManager.AppSettings["ServerUsername"];
        private static string AuthPassword = ConfigurationManager.AppSettings["ServerPassword"];
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class FinanceInfo
        {

            public FinanceInfo() { }

            // Properties.
            public string Date { get; set; }
            public string Symbol { get; set; }
            public string Year { get; set; }
            public string Quarter { get; set; }
            public string Asset { get; set; }
            public string Liabilities { get; set; }
            public string Equity { get; set; }
            public string Paid_up_cap { get; set; }
            public string Revenue { get; set; }
            public string NetProfit { get; set; }
            public string EPS { get; set; }
            public string ROA { get; set; }
            public string ROE { get; set; }
            public string NetProfitMargin { get; set; }
        }
        public class FinanceStat
        {

            public FinanceStat() { }

            // Properties.
            public string Date { get; set; }
            public string Symbol { get; set; }
            public string Year { get; set; }
            public string Lastprice { get; set; }
            public string Market_cap { get; set; }
            public string FS_date { get; set; }
            public string PE { get; set; }
            public string PBV { get; set; }
            public string BookValue_Share { get; set; }
            public string Dvd_Yield { get; set; }
        }
        public class Fundamental
        {

            public Fundamental() { }

            // Properties.
            public List<FinanceInfo> finance_info_yearly { get; set; }
            public List<FinanceInfo> finance_info_quarter { get; set; }
            public List<FinanceStat> finance_stat_yearly { get; set; }
            public List<FinanceStat> finance_stat_daily { get; set; }
        }
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
        public class Message
        {

            public Message()
            {
                message = "error !!";
            }

            // Properties.
            public string message { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Main Function                                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public dynamic GetFundamentalDB(string input)
        {
            var param = input.Split('/');
            switch (input)
            {
                case "fundamental":
                    return GetFundamental();
                case "fundamental/finance_info_yearly":
                    return GetFinanceInfoYearly();
                case "fundamental/finance_info_quarter":
                    return GetFinanceInfoQuarter();
                case "fundamental/finance_stat_yearly":
                    return GetFinanceStatYearly();
                case "fundamental/finance_stat_daily":
                    return GetFinanceStatDaily();
                default:
                    {
                        if (param[1] == "finance_info_yearly" && param.Length == 3)
                            return GetFinanceInfoYearly(param[2]);
                        else if (param[1] == "finance_info_yearly" && param[2] == "year" && param.Length == 4)
                            return GetFinanceInfoYearly(null, param[3]);
                        else if (param[1] == "finance_info_yearly" && param[2] == "date" && param.Length == 4)
                            return GetFinanceInfoYearly(null, null, param[3]);
                        else if (param[1] == "finance_info_yearly" && param[2] == "date" && param.Length == 5)
                            return GetFinanceInfoYearly(param[3], null, param[4]);
                        else if (param[1] == "finance_info_yearly" && param.Length == 4)
                            return GetFinanceInfoYearly(param[2], param[3]);
                        else if (param[1] == "finance_info_quarter" && param.Length == 3)
                            return GetFinanceInfoQuarter(param[2]);
                        else if (param[1] == "finance_info_quarter" && param[2] == "year" && param.Length == 4)
                            return GetFinanceInfoQuarter(null, param[3]);
                        else if (param[1] == "finance_info_quarter" && param[2] == "date" && param.Length == 4)
                            return GetFinanceInfoQuarter(null, null, param[3]);
                        else if (param[1] == "finance_info_quarter" && param[2] == "date" && param.Length == 5)
                            return GetFinanceInfoQuarter(param[3], null, param[4]);
                        else if (param[1] == "finance_info_quarter" && param.Length == 4)
                            return GetFinanceInfoQuarter(param[2], param[3]);
                        else if (param[1] == "finance_stat_yearly" && param.Length == 3)
                            return GetFinanceStatYearly(param[2]);
                        else if (param[1] == "finance_stat_yearly" && param[2] == "year" && param.Length == 4)
                            return GetFinanceStatYearly(null, param[3]);
                        else if (param[1] == "finance_stat_yearly" && param[2] == "date" && param.Length == 4)
                            return GetFinanceStatYearly(null, null, param[3]);
                        else if (param[1] == "finance_stat_yearly" && param[2] == "date" && param.Length == 5)
                            return GetFinanceStatYearly(param[3], null, param[4]);
                        else if (param[1] == "finance_stat_yearly" && param.Length == 4)
                            return GetFinanceStatYearly(param[2], param[3]);
                        else if (param[1] == "finance_stat_daily" && param.Length == 3)
                            return GetFinanceStatDaily(param[2]);
                        else if (param[1] == "finance_stat_daily" && param[2] == "year" && param.Length == 4)
                            return GetFinanceStatDaily(null, param[3]);
                        else if (param[1] == "finance_stat_daily" && param[2] == "date" && param.Length == 4)
                            return GetFinanceStatDaily(null, null, param[3]);
                        else if (param[1] == "finance_stat_daily" && param[2] == "date" && param.Length == 5)
                            return GetFinanceStatDaily(param[3], null, param[4]);
                        else if (param[1] == "finance_stat_daily" && param.Length == 4)
                            return GetFinanceStatDaily(param[2], param[3]);
                        else if (param[0] == "fundamental" && param[1] == "year" && param.Length == 3)
                            return GetFundamental(null, param[2]);
                        else if (param[0] == "fundamental" && param[1] == "date" && param.Length == 3)
                            return GetFundamental(null, null, param[2]);
                        else if (param[0] == "fundamental" && param[1] == "date" && param.Length == 4)
                            return GetFundamental(param[2], null, param[3]);
                        else if (param[0] == "fundamental" && param.Length == 2)
                            return GetFundamental(param[1]);
                        else if (param[0] == "fundamental" && param.Length == 3)
                            return GetFundamental(param[1], param[2]);
                        else
                            return new Message();
                    }
            }

        }
        public dynamic GetKaohoonDB(string input)
        {
            var param = input.Split('/');
            switch (input)
            {
                case "kaohoon":
                    return GetKaohoonData();
                default:
                    {
                        if (param[0] == "kaohoon" && param[1] == "date" && param.Length == 3)
                            return GetKaohoonData(null, param[2]);
                        else if (param[0] == "kaohoon" && param.Length == 2)
                            return GetKaohoonData(param[1]);
                        else if (param[0] == "kaohoon" && param.Length == 3)
                            return GetKaohoonData(param[1], param[2]);
                        else
                            return new Message();
                    }
            }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Access Token Function                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public string AccessToken(string input)
        {
            dynamic json = JsonConvert.DeserializeObject(input);
            if (json.AccessToken == accessToken)
                return "true";
            else
                return "false";
        }
        public string Authorization(string input)
        {
            dynamic json = JsonConvert.DeserializeObject(input);
            var security = new EncryptionHelper();
            if (json.Username == security.Encrypt(AuthUsername) && json.Password == security.Encrypt(AuthPassword))
                return GetAccessToken();
            else
                return "false";
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database fundamental Function                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public Fundamental GetFundamental(string symbol = null, string year = null, string date = null)
        {
            var tmp = new Fundamental();
            if (symbol == null && year == null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly();
                tmp.finance_info_quarter = GetFinanceInfoQuarter();
                tmp.finance_stat_yearly = GetFinanceStatYearly();
                tmp.finance_stat_daily = GetFinanceStatDaily();
            }
            else if (symbol != null && year == null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol);
            }
            else if (symbol == null && year != null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(null, year);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(null, year);
                tmp.finance_stat_yearly = GetFinanceStatYearly(null, year);
                tmp.finance_stat_daily = GetFinanceStatDaily(null, year);
            }
            else if (symbol == null && year == null && date != null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(null, null, date);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(null, null, date);
                tmp.finance_stat_yearly = GetFinanceStatYearly(null, null, date);
                tmp.finance_stat_daily = GetFinanceStatDaily(null, null, date);
            }
            else if (symbol != null && year != null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol, year);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol, year);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol, year);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol, year);
            }
            else if (symbol != null && year == null && date != null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol, null, date);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol, null, date);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol, null, date);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol, null, date);
            }
            return tmp;
        }
        public List<FinanceInfo> GetFinanceInfoYearly(string symbol = null, string year = null, string date = null)
        {
            List<FinanceInfo> finance_info_yearly = new List<FinanceInfo>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_yearly";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_yearly where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_yearly where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}' AND Date = '{date}'";

            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tmp = new FinanceInfo
                    {
                        Date = String.Format("{0}", reader["Date"]),
                        Symbol = String.Format("{0}", reader["Symbol"]),
                        Year = String.Format("{0}", reader["Year"]),
                        Asset = String.Format("{0}", reader["Assets"]),
                        Liabilities = String.Format("{0}", reader["Liabilities"]),
                        Equity = String.Format("{0}", reader["Equity"]),
                        Paid_up_cap = String.Format("{0}", reader["Paid_up_cap"]),
                        Revenue = String.Format("{0}", reader["Revenue"]),
                        NetProfit = String.Format("{0}", reader["NetProfit"]),
                        EPS = String.Format("{0}", reader["EPS"]),
                        ROA = String.Format("{0}", reader["ROA"]),
                        ROE = String.Format("{0}", reader["ROE"]),
                        NetProfitMargin = String.Format("{0}", reader["NetProfitMargin"]),
                    };

                    finance_info_yearly.Add(tmp);
                }
            }
            return finance_info_yearly;
        }
        public List<FinanceInfo> GetFinanceInfoQuarter(string symbol = null, string year = null, string date = null)
        {
            List<FinanceInfo> finance_info_quarter = new List<FinanceInfo>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_quarter";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_quarter where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_quarter where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}' AND Date = '{date}'";

            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tmp = new FinanceInfo
                    {
                        Date = String.Format("{0}", reader["Date"]),
                        Symbol = String.Format("{0}", reader["Symbol"]),
                        Year = String.Format("{0}", reader["Year"]),
                        Quarter = String.Format("{0}", reader["Quarter"]),
                        Asset = String.Format("{0}", reader["Assets"]),
                        Liabilities = String.Format("{0}", reader["Liabilities"]),
                        Equity = String.Format("{0}", reader["Equity"]),
                        Paid_up_cap = String.Format("{0}", reader["Paid_up_cap"]),
                        Revenue = String.Format("{0}", reader["Revenue"]),
                        NetProfit = String.Format("{0}", reader["NetProfit"]),
                        EPS = String.Format("{0}", reader["EPS"]),
                        ROA = String.Format("{0}", reader["ROA"]),
                        ROE = String.Format("{0}", reader["ROE"]),
                        NetProfitMargin = String.Format("{0}", reader["NetProfitMargin"]),
                    };

                    finance_info_quarter.Add(tmp);
                }
            }
            return finance_info_quarter;
        }
        public List<FinanceStat> GetFinanceStatYearly(string symbol = null, string year = null, string date = null)
        {
            List<FinanceStat> finance_stat_yearly = new List<FinanceStat>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_yearly where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}' AND Date = '{date}'";

            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tmp = new FinanceStat
                    {
                        Date = String.Format("{0}", reader["Date"]),
                        Symbol = String.Format("{0}", reader["Symbol"]),
                        Year = String.Format("{0}", reader["Year"]),
                        Lastprice = String.Format("{0}", reader["Lastprice"]),
                        Market_cap = String.Format("{0}", reader["Market_cap"]),
                        FS_date = String.Format("{0}", reader["FS_date"]),
                        PE = String.Format("{0}", reader["PE"]),
                        PBV = String.Format("{0}", reader["PBV"]),
                        BookValue_Share = String.Format("{0}", reader["Bookvalue_share"]),
                        Dvd_Yield = String.Format("{0}", reader["dvd_yield"]),
                    };

                    finance_stat_yearly.Add(tmp);
                }
            }
            return finance_stat_yearly;
        }
        public List<FinanceStat> GetFinanceStatDaily(string symbol = null, string year = null, string date = null)
        {
            List<FinanceStat> finance_stat_daily = new List<FinanceStat>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_daily";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_daily where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_daily where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}' AND Date = '{date}'";

            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tmp = new FinanceStat
                    {
                        Date = String.Format("{0}", reader["Date"]),
                        Symbol = String.Format("{0}", reader["Symbol"]),
                        Year = String.Format("{0}", reader["Year"]),
                        Lastprice = String.Format("{0}", reader["Lastprice"]),
                        Market_cap = String.Format("{0}", reader["Market_cap"]),
                        FS_date = String.Format("{0}", reader["FS_date"]),
                        PE = String.Format("{0}", reader["PE"]),
                        PBV = String.Format("{0}", reader["PBV"]),
                        BookValue_Share = String.Format("{0}", reader["Bookvalue_share"]),
                        Dvd_Yield = String.Format("{0}", reader["dvd_yield"]),
                    };

                    finance_stat_daily.Add(tmp);
                }
            }
            return finance_stat_daily;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database kaohoon Function                                       |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public List<KaohoonData> GetKaohoonData(string symbol = null, string date = null)
        {
            List<KaohoonData> kaohoon_data = new List<KaohoonData>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null && date == null)
                sql = $"Select * from dbo.kaohoon_data";
            else if (symbol != null && date == null)
                sql = $"Select * from dbo.kaohoon_data where Symbol = '{symbol}'";
            else if (symbol == null && date != null)
                sql = $"Select * from dbo.kaohoon_data where Date = '{date}'";
            else
                sql = $"Select * from dbo.kaohoon_data where Symbol = '{symbol}' AND Date = '{date}'";

            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tmp = new KaohoonData
                    {
                        Kaohoon_data_id = String.Format("{0}", reader["Kaohoon_data_id"]),
                        Symbol = String.Format("{0}", reader["Symbol"]),
                        Date = String.Format("{0}", reader["Date"]),
                        Year = String.Format("{0}", reader["Year"]),
                        Quarter = String.Format("{0}", reader["Quarter"]),
                        TargetPrice = String.Format("{0}", reader["TargetPrice"]),
                        Trends = String.Format("{0}", reader["Trends"]),
                        Divide = String.Format("{0}", reader["Divide"]),
                        Topic = String.Format("{0}", reader["Topic"]),
                    };
                    kaohoon_data.Add(tmp);
                }
            }
            return kaohoon_data;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other Function                                                  |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public void SetAccessToken(string input)
        {
            accessToken = input;
        }
        public string GetAccessToken()
        {
            return accessToken;
        }
    }
}


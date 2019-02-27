using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        private static string DatabaseServer = ConfigurationManager.AppSettings["DatabaseServer"];
        private static string Database = ConfigurationManager.AppSettings["Database"];
        private static string Username = ConfigurationManager.AppSettings["Username"];
        private static string Password = ConfigurationManager.AppSettings["Password"];
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
        public class Message
        {

            public Message() {
                message = "error !!";
            }

            // Properties.
            public string message { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Main Function                                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public dynamic GetDB(string input)
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
                        if(param[0] == "fundamental" && param.Length == 2)
                            return GetFundamental(param[1]);
                        else if (param[0] == "fundamental" && param[1] == "finance_info_yearly" && param.Length == 3)
                            return GetFinanceInfoYearly(param[2]);
                        else if (param[0] == "fundamental" && param[1] == "finance_info_quarter" && param.Length == 3)
                            return GetFinanceInfoQuarter(param[2]);
                        else if (param[0] == "fundamental" && param[1] == "finance_stat_yearly" && param.Length == 3)
                            return GetFinanceStatYearly(param[2]);
                        else if (param[0] == "fundamental" && param[1] == "finance_stat_daily" && param.Length == 3)
                            return GetFinanceStatDaily(param[2]);
                        else if (param[0] == "fundamental" && param.Length == 3)
                            return GetFundamental(param[1], param[2]);
                        else if (param[0] == "fundamental" && param[1] == "finance_info_yearly" && param.Length == 4)
                            return GetFinanceInfoYearly(param[2], param[3]);
                        else if (param[0] == "fundamental" && param[1] == "finance_info_quarter" && param.Length == 4)
                            return GetFinanceInfoQuarter(param[2], param[3]);
                        else if (param[0] == "fundamental" && param[1] == "finance_stat_yearly" && param.Length == 4)
                            return GetFinanceStatYearly(param[2], param[3]);
                        else if (param[0] == "fundamental" && param[1] == "finance_stat_daily" && param.Length == 4)
                            return GetFinanceStatDaily(param[2], param[3]);
                        else
                            return new Message();
                    }
            }

        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other Function                                                  |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public Fundamental GetFundamental(string symbol = null, string year = null)
        {
            var tmp = new Fundamental();
            if (symbol == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly();
                tmp.finance_info_quarter = GetFinanceInfoQuarter();
                tmp.finance_stat_yearly = GetFinanceStatYearly();
                tmp.finance_stat_daily = GetFinanceStatDaily();
            }
            else if (year == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol);
            }else
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol, year);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol, year);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol, year);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol, year);
            }
            return tmp;
        }
        public List<FinanceInfo> GetFinanceInfoYearly(string symbol = null, string year = null)
        {
            List<FinanceInfo> finance_info_yearly = new List<FinanceInfo>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null)
                sql = $"Select * from dbo.finance_info_yearly";
            else if(year == null)
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}'";
            else
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}' AND Year = '{year}'";

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
        public List<FinanceInfo> GetFinanceInfoQuarter(string symbol = null, string year = null)
        {
            List<FinanceInfo> finance_info_quarter = new List<FinanceInfo>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null)
                sql = $"Select * from dbo.finance_info_quarter";
            else if (year == null)
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}'";
            else
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}' AND Year = '{year}'";
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
        public List<FinanceStat> GetFinanceStatYearly(string symbol = null, string year = null)
        {
            List<FinanceStat> finance_stat_yearly = new List<FinanceStat>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null)
                sql = $"Select * from dbo.finance_stat_yearly";
            else if (year == null)
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}'";
            else
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}' AND Year = '{year}'";
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
        public List<FinanceStat> GetFinanceStatDaily(string symbol = null, string year = null)
        {
            List<FinanceStat> finance_stat_daily = new List<FinanceStat>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null)
                sql = $"Select * from dbo.finance_stat_daily";
            else if (year == null)
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}'";
            else
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}' AND Year = '{year}'";
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

    }
}


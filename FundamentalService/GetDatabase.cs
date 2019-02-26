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

        private static string DatabaseServer = ConfigurationManager.AppSettings["DatabaseServer"];
        private static string Database = ConfigurationManager.AppSettings["Database"];
        private static string Username = ConfigurationManager.AppSettings["Username"];
        private static string Password = ConfigurationManager.AppSettings["Password"];
        public static string nameFile = @"\Fundamental" + DateTime.Now.ToString("yyyyMMdd") + ".log";
        public static string strPath = AppDomain.CurrentDomain.BaseDirectory + @"\logs";
        public static string fullPath = strPath + nameFile;

        public List<FinanceInfo> GetFinanceInfoYearly()
        {
            List<FinanceInfo> finance_info_yearly = new List<FinanceInfo>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = $"Select * from dbo.finance_info_yearly";
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

    }
}


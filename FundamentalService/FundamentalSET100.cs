using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalService
{
    class FundamentalSET100
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
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Main Function                                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public void Run()
        {
            log.LOGI("[FundamentalSET100::Run] Start update data SET100");
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
                CompanyHighlights(symbols[i]);
            log.LOGI("[FundamentalSET100::Run] End update data SET100");
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | ScrapingWeb Function                                            |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public static void CompanyHighlights(string symbol)
        {
            var url = $"https://www.set.or.th/set/companyhighlight.do?symbol={symbol}&ssoPageId=5";

            // List cut string finance info
            List<string> date_info = new List<string>();
            List<string> year_info = new List<string>();
            List<string> quarter = new List<string>();
            List<string> asset = new List<string>();
            List<string> liabilities = new List<string>();
            List<string> equity = new List<string>();
            List<string> paid_up_cap = new List<string>();
            List<string> revenue = new List<string>();
            List<string> net_profit = new List<string>();
            List<string> eps = new List<string>();
            List<string> roa = new List<string>();
            List<string> roe = new List<string>();
            List<string> net_profit_margin = new List<string>();

            // List cut string finance stat
            List<string> date_stat = new List<string>();
            List<string> year_stat = new List<string>();
            List<string> lastprice = new List<string>();
            List<string> market_cap = new List<string>();
            List<string> fs_date = new List<string>();
            List<string> pe = new List<string>();
            List<string> pbv = new List<string>();
            List<string> book_value_share = new List<string>();
            List<string> dvd_yield = new List<string>();

            // Using HtmlAgilityPack
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);

            // tmp variable
            var run = "start";

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr//td"))
            {
                string utf8_String = node.ChildNodes[0].InnerHtml;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                int index = utf8_String.IndexOf("&");

                if (index > 0)
                    utf8_String = utf8_String.Substring(0, utf8_String.IndexOf("&"));

                if (utf8_String == "N/A" || utf8_String == "N.A." || utf8_String == "-")
                    utf8_String = "null";


                if (utf8_String == "สินทรัพย์รวม")
                    run = utf8_String;
                else if (utf8_String == "หนี้สินรวม")
                    run = utf8_String;
                else if (utf8_String == "ส่วนของผู้ถือหุ้น")
                    run = utf8_String;
                else if (utf8_String == "มูลค่าหุ้นที่เรียกชำระแล้ว")
                    run = utf8_String;
                else if (utf8_String == "รายได้รวม")
                    run = utf8_String;
                else if (utf8_String == "กำไรสุทธิ")
                    run = utf8_String;
                else if (utf8_String == "กำไรต่อหุ้น (บาท)")
                    run = utf8_String;
                else if (utf8_String == "ROA(%)")
                    run = utf8_String;
                else if (utf8_String == "ROE(%)")
                    run = utf8_String;
                else if (utf8_String == "อัตรากำไรสุทธิ(%)")
                    run = utf8_String;
                else if (utf8_String == "ราคาล่าสุด(บาท)")
                    run = utf8_String;
                else if (utf8_String == "มูลค่าหลักทรัพย์ตามราคาตลาด")
                    run = utf8_String;
                else if (utf8_String == "วันที่ของงบการเงินที่ใช้คำนวณค่าสถิติ")
                    run = utf8_String;
                else if (utf8_String == "P/E (เท่า)")
                    run = utf8_String;
                else if (utf8_String == "P/BV (เท่า)")
                    run = utf8_String;
                else if (utf8_String == "มูลค่าหุ้นทางบัญชีต่อหุ้น (บาท)")
                    run = utf8_String;
                else if (utf8_String == "อัตราส่วนเงินปันผลตอบแทน(%)")
                    run = utf8_String;

                if (utf8_String != run)
                    if (run == "สินทรัพย์รวม")
                        asset.Add(utf8_String);
                    else if (run == "หนี้สินรวม")
                        liabilities.Add(utf8_String);
                    else if (run == "ส่วนของผู้ถือหุ้น")
                        equity.Add(utf8_String);
                    else if (run == "มูลค่าหุ้นที่เรียกชำระแล้ว")
                        paid_up_cap.Add(utf8_String);
                    else if (run == "รายได้รวม")
                        revenue.Add(utf8_String);
                    else if (run == "กำไรสุทธิ")
                        net_profit.Add(utf8_String);
                    else if (run == "กำไรต่อหุ้น (บาท)")
                        eps.Add(utf8_String);
                    else if (run == "ROA(%)")
                        roa.Add(utf8_String);
                    else if (run == "ROE(%)")
                        roe.Add(utf8_String);
                    else if (run == "อัตรากำไรสุทธิ(%)")
                        net_profit_margin.Add(utf8_String);
                    else if (run == "ราคาล่าสุด(บาท)")
                        lastprice.Add(utf8_String);
                    else if (run == "มูลค่าหลักทรัพย์ตามราคาตลาด")
                        market_cap.Add(utf8_String);
                    else if (run == "วันที่ของงบการเงินที่ใช้คำนวณค่าสถิติ")
                        fs_date.Add(utf8_String);
                    else if (run == "P/E (เท่า)")
                        pe.Add(utf8_String);
                    else if (run == "P/BV (เท่า)")
                        pbv.Add(utf8_String);
                    else if (run == "มูลค่าหุ้นทางบัญชีต่อหุ้น (บาท)")
                        book_value_share.Add(utf8_String);
                    else if (run == "อัตราส่วนเงินปันผลตอบแทน(%)")
                        dvd_yield.Add(utf8_String);
            }

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//th"))
            {
                string utf8_String = node.ChildNodes[0].InnerHtml;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                if (utf8_String == "งวดงบการเงิน<br> ณ วันที่")
                    run = utf8_String;
                if (utf8_String == "ค่าสถิติสำคัญ<br> ณ วันที่")
                    run = utf8_String;

                if (utf8_String != run)
                    if (run == "งวดงบการเงิน<br> ณ วันที่" && utf8_String.IndexOf("/") >= 0)
                    {
                        var index = utf8_String.IndexOf(">") + 1;
                        date_info.Add(utf8_String.Substring(index, utf8_String.Length - index));
                        year_info.Add(utf8_String.Substring(utf8_String.Length - 4, 4));
                        quarter.Add(utf8_String.Substring(0, index - 4));
                    }
                    else if (run == "ค่าสถิติสำคัญ<br> ณ วันที่")
                    {
                        date_stat.Add(utf8_String);
                        year_stat.Add(utf8_String.Substring(utf8_String.Length - 4, 4));
                    }

            }

            List<FinanceInfo> finance_info_yearly = new List<FinanceInfo>();
            List<FinanceInfo> finance_info_quarter = new List<FinanceInfo>();
            List<FinanceStat> finance_stat_yearly = new List<FinanceStat>();
            List<FinanceStat> finance_stat_daily = new List<FinanceStat>();

            for (int i = 0; i < date_stat.Count; i++)
            {
                string fsDate = "";
                if (fs_date[i].IndexOf("/") >= 0)
                    fsDate = fs_date[i];
                else
                    fsDate = "null";
                var tmp1 = new FinanceStat
                {
                    Date = date_stat[i],
                    Symbol = symbol,
                    Year = year_stat[i],
                    Lastprice = lastprice[i],
                    Market_cap = market_cap[i],
                    FS_date = fsDate,
                    PE = pe[i],
                    PBV = pbv[i],
                    BookValue_Share = book_value_share[i],
                    Dvd_Yield = dvd_yield[i],
                };
                if (i < date_stat.Count - 1)
                {
                    finance_stat_yearly.Add(tmp1);
                    var tmp = new FinanceInfo
                    {
                        Date = date_info[i],
                        Symbol = symbol,
                        Year = year_info[i],
                        Quarter = quarter[i],
                        Asset = asset[i],
                        Liabilities = liabilities[i],
                        Equity = equity[i],
                        Paid_up_cap = paid_up_cap[i],
                        Revenue = revenue[i],
                        NetProfit = net_profit[i],
                        EPS = eps[i],
                        ROA = roa[i],
                        ROE = roe[i],
                        NetProfitMargin = net_profit_margin[i],
                    };
                    if (tmp.Quarter.IndexOf("ไตรมาส") >= 0)
                    {
                        tmp.Quarter = quarter[i].Substring(quarter[i].IndexOf("ไตรมาส") + 6 , 1);
                        finance_info_quarter.Add(tmp);
                    }
                    else
                        finance_info_yearly.Add(tmp);
                }
                else
                {
                    finance_stat_daily.Add(tmp1);
                }

            }

            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //Console.WriteLine($"{symbol}: Connection Open !");

            // Insert database finance_info_yearly
            foreach (var value in finance_info_yearly)
            {
                string sql = $"Select * from dbo.finance_info_yearly where Date={ChangeDateFormat(value.Date)} AND Symbol='{value.Symbol}'";
                SqlCommand command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@zip", "india");
                bool event_case = true;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sql = $"UPDATE dbo.finance_info_yearly SET " +
                            $"Year='{ChangeYearFormat(value.Year)}'," +
                            $"Assets={CutStrignMoney(value.Asset)}," +
                            $"Liabilities={CutStrignMoney(value.Liabilities)}," +
                            $"Equity={CutStrignMoney(value.Equity)}," +
                            $"Paid_up_cap={CutStrignMoney(value.Paid_up_cap)}," +
                            $"Revenue={CutStrignMoney(value.Revenue)}," +
                            $"NetProfit={CutStrignMoney(value.NetProfit)}," +
                            $"EPS={CutStrignMoney(value.EPS)}," +
                            $"ROA={CutStrignMoney(value.ROA)}," +
                            $"ROE={CutStrignMoney(value.ROE)}," +
                            $"NetProfitMargin={CutStrignMoney(value.NetProfitMargin)}" +
                            $"where Date = { ChangeDateFormat(value.Date) } AND Symbol = '{value.Symbol}'";
                        event_case = false;
                    }
                    else
                    {
                        sql = "Insert into dbo.finance_info_yearly " +
                        "(Date,Symbol,Year,Assets,Liabilities,Equity,Paid_up_cap,Revenue,NetProfit,EPS,ROA,ROE,NetProfitMargin,Type) values (" +
                        $"{ChangeDateFormat(value.Date)}," +
                        $"'{value.Symbol}'," +
                        $"'{ChangeYearFormat(value.Year)}'," +
                        $"{CutStrignMoney(value.Asset)}," +
                        $"{CutStrignMoney(value.Liabilities)}," +
                        $"{CutStrignMoney(value.Equity)}," +
                        $"{CutStrignMoney(value.Paid_up_cap)}," +
                        $"{CutStrignMoney(value.Revenue)}," +
                        $"{CutStrignMoney(value.NetProfit)}," +
                        $"{CutStrignMoney(value.EPS)}," +
                        $"{CutStrignMoney(value.ROA)}," +
                        $"{CutStrignMoney(value.ROE)}," +
                        $"{CutStrignMoney(value.NetProfitMargin)}," +
                        $"null)";
                        //Insert database
                    }
                }
                if (event_case)
                    InsertDatebase(sql, cnn);
                else
                    UpdateDatebase(sql, cnn);

            }

            // Insert database finance_info_quarter
            foreach (var value in finance_info_quarter)
            {
                string sql = $"Select * from dbo.finance_info_quarter where Date={ChangeDateFormat(value.Date)} AND Symbol='{value.Symbol}'";
                SqlCommand command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@zip", "india");
                bool event_case = true;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sql = $"UPDATE dbo.finance_info_quarter SET " +
                            $"Year='{ChangeYearFormat(value.Year)}'," +
                            $"Quarter={value.Quarter}," +
                            $"Assets={CutStrignMoney(value.Asset)}," +
                            $"Liabilities={CutStrignMoney(value.Liabilities)}," +
                            $"Equity={CutStrignMoney(value.Equity)}," +
                            $"Paid_up_cap={CutStrignMoney(value.Paid_up_cap)}," +
                            $"Revenue={CutStrignMoney(value.Revenue)}," +
                            $"NetProfit={CutStrignMoney(value.NetProfit)}," +
                            $"EPS={CutStrignMoney(value.EPS)}," +
                            $"ROA={CutStrignMoney(value.ROA)}," +
                            $"ROE={CutStrignMoney(value.ROE)}," +
                            $"NetProfitMargin={CutStrignMoney(value.NetProfitMargin)} " +
                            $"where Date = { ChangeDateFormat(value.Date) } AND Symbol = '{value.Symbol}'";
                        event_case = false;

                    }
                    else
                    {
                        sql = "Insert into dbo.finance_info_quarter " +
                        "(Date,Symbol,Year,Quarter,Assets,Liabilities,Equity,Paid_up_cap,Revenue,NetProfit,EPS,ROA,ROE,NetProfitMargin,Type) values (" +
                        $"{ChangeDateFormat(value.Date)}," +
                        $"'{value.Symbol}'," +
                        $"'{ChangeYearFormat(value.Year)}'," +
                        $"'{value.Quarter}'," +
                        $"{CutStrignMoney(value.Asset)}," +
                        $"{CutStrignMoney(value.Liabilities)}," +
                        $"{CutStrignMoney(value.Equity)}," +
                        $"{CutStrignMoney(value.Paid_up_cap)}," +
                        $"{CutStrignMoney(value.Revenue)}," +
                        $"{CutStrignMoney(value.NetProfit)}," +
                        $"{CutStrignMoney(value.EPS)}," +
                        $"{CutStrignMoney(value.ROA)}," +
                        $"{CutStrignMoney(value.ROE)}," +
                        $"{CutStrignMoney(value.NetProfitMargin)}," +
                        $"null)";
                    }
                }
                if (event_case)
                    InsertDatebase(sql, cnn);
                else
                    UpdateDatebase(sql, cnn);
            }

            // Insert database finance_stat_yearly
            foreach (var value in finance_stat_yearly)
            {
                string sql = $"Select * from dbo.finance_stat_yearly where Date={ChangeDateFormat(value.Date)} AND Symbol='{value.Symbol}'";
                SqlCommand command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@zip", "india");
                bool event_case = true;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sql = $"UPDATE dbo.finance_stat_yearly SET " +
                            $"Year='{ChangeYearFormat(value.Year)}'," +
                            $"Lastprice={CutStrignMoney(value.Lastprice)}," +
                            $"Market_cap={CutStrignMoney(value.Market_cap)}," +
                            $"FS_date={ChangeDateFormat(value.FS_date)}," +
                            $"PE={CutStrignMoney(value.PE)}," +
                            $"PBV={CutStrignMoney(value.PBV)}," +
                            $"Bookvalue_share={CutStrignMoney(value.BookValue_Share)}," +
                            $"dvd_yield={CutStrignMoney(value.Dvd_Yield)}" +
                            $"where Date = { ChangeDateFormat(value.Date) } AND Symbol = '{value.Symbol}'";
                        event_case = false;
                    }
                    else
                    {
                        sql = "Insert into dbo.finance_stat_yearly " +
                        "(Date,Symbol,Year,Lastprice,Market_cap,FS_date,PE,PBV,Bookvalue_share,dvd_yield) values (" +
                        $"{ChangeDateFormat(value.Date)}," +
                        $"'{value.Symbol}'," +
                        $"'{ChangeYearFormat(value.Year)}'," +
                        $"{CutStrignMoney(value.Lastprice)}," +
                        $"{CutStrignMoney(value.Market_cap)}," +
                        $"{ChangeDateFormat(value.FS_date)}," +
                        $"{CutStrignMoney(value.PE)}," +
                        $"{CutStrignMoney(value.PBV)}," +
                        $"{CutStrignMoney(value.BookValue_Share)}," +
                        $"{CutStrignMoney(value.Dvd_Yield)})";
                    }
                }
                if (event_case)
                    InsertDatebase(sql, cnn);
                else
                    UpdateDatebase(sql, cnn);
            }

            // Insert database finance_stat_daily
            foreach (var value in finance_stat_daily)
            {
                string sql = $"Select * from dbo.finance_stat_daily where Date={ChangeDateFormat(value.Date)} AND Symbol='{value.Symbol}'";
                SqlCommand command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@zip", "india");
                bool event_case = true;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sql = $"UPDATE dbo.finance_stat_daily SET " +
                            $"Year='{ChangeYearFormat(value.Year)}'," +
                            $"Lastprice={CutStrignMoney(value.Lastprice)}," +
                            $"Market_cap={CutStrignMoney(value.Market_cap)}," +
                            $"FS_date={ChangeDateFormat(value.FS_date)}," +
                            $"PE={CutStrignMoney(value.PE)}," +
                            $"PBV={CutStrignMoney(value.PBV)}," +
                            $"Bookvalue_share={CutStrignMoney(value.BookValue_Share)}," +
                            $"dvd_yield={CutStrignMoney(value.Dvd_Yield)}" +
                            $"where Date = { ChangeDateFormat(value.Date) } AND Symbol = '{value.Symbol}'";
                        event_case = false;
                    }
                    else
                    {
                        sql = "Insert into dbo.finance_stat_daily " +
                        "(Date,Symbol,Year,Lastprice,Market_cap,FS_date,PE,PBV,Bookvalue_share,dvd_yield) values (" +
                        $"{ChangeDateFormat(value.Date)}," +
                        $"'{value.Symbol}'," +
                        $"'{ChangeYearFormat(value.Year)}'," +
                        $"{CutStrignMoney(value.Lastprice)}," +
                        $"{CutStrignMoney(value.Market_cap)}," +
                        $"{ChangeDateFormat(value.FS_date)}," +
                        $"{CutStrignMoney(value.PE)}," +
                        $"{CutStrignMoney(value.PBV)}," +
                        $"{CutStrignMoney(value.BookValue_Share)}," +
                        $"{CutStrignMoney(value.Dvd_Yield)})";
                    }
                }
                if (event_case)
                    InsertDatebase(sql, cnn);
                else
                    UpdateDatebase(sql, cnn);
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
        public static string CutStrignMoney(string money)
        {
            if (money == "null")
                return "0";
            else
                return money.Replace(",", "").Replace("*", "");
        }
        public static string ChangeDateFormat(string date)
        {
            if (date == "null")
                return date;

            string str = date + "/";
            var parts = str.Split('/');
            int dd = Convert.ToInt32(parts[0]);
            int mm = Convert.ToInt32(parts[1]);
            int yy = Convert.ToInt32(parts[2]) - 543;

            return $"'{yy}-{mm}-{dd}'";
        }
        public static string ChangeYearFormat(string year)
        {
            int yy = Convert.ToInt32(year) - 543;

            return $"{yy}";
        }
    }
}

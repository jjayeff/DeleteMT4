using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalService
{
    class Plog
    {
        public static string nameFile = @"\Fundamental-" + DateTime.Now.ToString("yyyyMMdd") + ".log";
        public static string strPath = AppDomain.CurrentDomain.BaseDirectory + @"\logs";
        public static string fullPath = strPath + nameFile;
        public void LOGI(string input)
        {
            string result = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            result += " [INFO]  ";
            result += input;
            File.AppendAllLines(fullPath, new[] { result });
        }

        public void LOGE(string input)
        {
            string result = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            result += " [ERROR] ";
            result += input;
            File.AppendAllLines(fullPath, new[] { result });
        }

        public void LOGW(string input)
        {
            string result = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            result += " [WARN]  ";
            result += input;
            File.AppendAllLines(fullPath, new[] { result });
        }
    }
}

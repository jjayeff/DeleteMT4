using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalService
{
    class ServerSocket
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

        private static string SocketPort = ConfigurationManager.AppSettings["SocketPort"];
        public static string nameFile = @"\Fundamental" + DateTime.Now.ToString("yyyyMMdd") + ".log";
        public static string strPath = AppDomain.CurrentDomain.BaseDirectory + @"\logs";
        public static string fullPath = strPath + nameFile;
        // Incoming data from the client.  
        public static string data = null;

        public void StartListening()
        {
            int port = Int32.Parse(SocketPort);
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);


                    // Show the data on the console.  
                    File.AppendAllLines(fullPath, new[] { "[INFO] Text received : " + data });
                    Console.WriteLine("Text received : {0}", data);
                    var x = new GetDatabase();
                    var y = x.GetFinanceInfoYearly();
                    string output = JsonConvert.SerializeObject(y);


                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(output);

                    handler.Send(Encoding.ASCII.GetBytes(output));
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input port: ");
            int n = int.Parse(Console.ReadLine());
            TcpClient tcpc = new TcpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("172.16.0.1"), n);

            try
            {
                tcpc.Connect(ip);
                Console.WriteLine("PORT OPENED! ");
                tcpc.Close();
            }
            catch
            {
                Console.WriteLine("OFFLINE! ");
            }

            byte[] data = Encoding.ASCII.GetBytes("aaaaa");
            UdpClient udpc = new UdpClient();
            udpc.Connect(ip);
            udpc.Send(data, data.Length);
            var timeToWait = TimeSpan.FromSeconds(10);
            var asyncResult = udpc.BeginReceive(null, null);
            asyncResult.AsyncWaitHandle.WaitOne(timeToWait);
            if (asyncResult.IsCompleted)
            {
                try
                {
                    udpc.Receive(ref ip);
                    Console.WriteLine("PORT OPENED! ");
                    udpc.Close();
                }
                catch
                {
                    Console.WriteLine("PORT CLOSED ");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("TIME OUT AFTER 10s!");
            }
          
        }
    }
}

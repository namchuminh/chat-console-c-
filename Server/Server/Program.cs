using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class Program
    {
        public static void SendData(object client)
        {
            Socket clientSocket = (Socket)client;
            string message = "";
            do
            {
                message = (string)Console.ReadLine();
                byte[] data = ASCIIEncoding.ASCII.GetBytes(message);
                clientSocket.Send(data);
            } while (message.ToLower() != "thoat");
        }

        public static void ReceiveData(object client)
        {
            Socket clientSocket = (Socket)client;
            string message = "";
            
            while(message.ToLower() != "thoat")
            {
                byte[] receive = new byte[1024];
                clientSocket.Receive(receive);
                message = ASCIIEncoding.ASCII.GetString(receive);
                Console.WriteLine("<client>: " + message);
            }

        }
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint s_iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                Socket serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(s_iep);
                serverSocket.Listen(10);
                Console.WriteLine("Cho doi client ket noi toi server!");
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("Da co client ket noi toi server!");
                //Trao đổi tin ở đây

                Thread t1 = new Thread(new ParameterizedThreadStart(SendData));
                Thread t2 = new Thread(new ParameterizedThreadStart(ReceiveData));

                t1.Start(clientSocket);
                t2.Start(clientSocket);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

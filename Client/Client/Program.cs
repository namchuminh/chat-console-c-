using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
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
                byte[] data = new byte[1024];
                clientSocket.Receive(data);
                message = ASCIIEncoding.ASCII.GetString(data);
                Console.WriteLine("<server>: " + message);
            }
        }
        static void Main(string[] args)
        {
            IPEndPoint s_iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            Socket clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(s_iep);
            Console.WriteLine("Da ket noi toi server: " + s_iep);
            //Trao doi tin o day

            Thread t1 = new Thread(new ParameterizedThreadStart(SendData));
            Thread t2 = new Thread(new ParameterizedThreadStart(ReceiveData));

            t1.Start(clientSocket);
            t2.Start(clientSocket);
        }
    }
}

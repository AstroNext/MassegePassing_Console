using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening ...");
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received message from ( "+client.Client.Handle+" ) : " + dataReceived + "\n");
            Console.WriteLine("Write Your message to response :");
            var response = Console.ReadLine();
            Console.WriteLine("Response : " + response);
            var sendData = Encoding.ASCII.GetBytes(response);
            nwStream.Write(sendData, 0, sendData.Length);
            client.Close();
            listener.Stop();
            Console.ReadLine();
        }
    }
}

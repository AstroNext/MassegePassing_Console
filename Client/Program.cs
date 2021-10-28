using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Title = "Client";
            Console.WriteLine("Enter your text to send : ");
            string textToSend = Console.ReadLine();
            try
            {
                TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
                Console.Title = "Client " + client.Client.Handle;
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
                Console.WriteLine("Sending : " + textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                Console.ReadLine();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex : " + ex.Message);
            }
        }
    }
}

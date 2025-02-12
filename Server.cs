using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

UdpClient udpServer = new UdpClient();
IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

for (int i = 0; i < 10; i++)
{
    string message = i.ToString();
    byte[] data = Encoding.UTF8.GetBytes(message);
    
    udpServer.Send(data, data.Length, clientEndPoint);
    Console.WriteLine($"Skickade: {message}");

    Thread.Sleep(500); // Vänta lite för att simulera separata paket
}

udpServer.Close();
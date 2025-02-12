// Uppgift 1 

// using System;
// using System.Diagnostics;

// class Program
// {
//     static void Main()
//     {
//         string url = "https://youtu.be/dQw4w9WgXcQ";
//         for (int i = 0; i < 25; i++)
//         {
//             Process.Start(new ProcessStartInfo
//             {
//                 FileName = url,
//                 UseShellExecute = true
//             });
//         }
//     }
// }


// Uppgift 2
// using System;
// using System.Net;
// using System.Net.Sockets;
// using System.Text;

// class Program
// {
//     private const int portNum = 13;

//     public static void Main(string[] args)
//     {
//         bool done = false;

//         var listener = new TcpListener(IPAddress.Any, portNum);
//         listener.Start();

//         while (!done)
//         {
//             Console.Write("Waiting for connection...");
//             TcpClient client = listener.AcceptTcpClient();

//             Console.WriteLine("Connection accepted.");
//             NetworkStream ns = client.GetStream();

//             byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

//             try
//             {
//                 ns.Write(byteTime, 0, byteTime.Length);
//                 ns.Close();
//                 client.Close();
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e.ToString());
//             }
//         }

//         listener.Stop();
//         return;
//     }
// }

// Uppgift 3

// using System;
// using System.Net;
// using System.Net.Sockets;
// using System.Text;

// class Server
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Startar servern...");

//         // Skapar en TCP-lyssnare på port 13000
//         TcpListener lyssnare = new TcpListener(IPAddress.Any, 13000);
//         lyssnare.Start();
//         Console.WriteLine("Servern har startats. Väntar på anslutningar...");

//         while (true)
//         {
//             // Väntar på att en klient ska ansluta
//             TcpClient klient = lyssnare.AcceptTcpClient();
//             Console.WriteLine("En klient har anslutit!");

//             // Hämtar dataströmmen från klienten
//             NetworkStream ström = klient.GetStream();
//             byte[] buffer = new byte[1024];
//             int antalLäsnaByte = ström.Read(buffer, 0, buffer.Length);

//             // Konverterar byte till sträng
//             string meddelande = Encoding.UTF8.GetString(buffer, 0, antalLäsnaByte);
//             Console.WriteLine($"Mottaget meddelande: {meddelande}");

//             // Stänger anslutningen
//             ström.Close();
//             klient.Close();
//         }
//     }
// }

// uppgift 4
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Task.Run(() => StartServer()); // Starta servern i en separat tråd
        Thread.Sleep(500); // Vänta lite så att servern startar innan klienten börjar

        StartClient(); // Starta klienten i huvudtråden
    }

    static void StartServer()
    {
        UdpClient udpServer = new UdpClient();
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

        for (int i = 0; i < 10; i++)
        {
            string message = i.ToString();
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpServer.Send(data, data.Length, clientEndPoint);
            Console.WriteLine($"[SERVER] Skickade: {message}");
            Thread.Sleep(500); // Simulera separata paket
        }

        udpServer.Close();
    }

    static void StartClient()
    {
        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 12345);

        for (int i = 0; i < 10; i++)
        {
            byte[] receivedData = udpClient.Receive(ref serverEndPoint);
            string receivedMessage = Encoding.UTF8.GetString(receivedData);
            Console.WriteLine($"[KLIENT] Mottog: {receivedMessage}");
        }

        udpClient.Close();
    }
}




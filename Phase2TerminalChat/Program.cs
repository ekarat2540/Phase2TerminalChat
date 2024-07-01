using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

class Sender
{
    public static async Task Main(string[] args)
    {
        try
        {
            string serverIp = "192.168.1.100"; // IP ของ Server
            int serverPort = 5713;

            TcpClient serverClient = new TcpClient(serverIp, serverPort);
            NetworkStream serverStream = serverClient.GetStream();
            StreamWriter serverWriter = new StreamWriter(serverStream);
            StreamReader serverReader = new StreamReader(serverStream);

            serverWriter.WriteLine("SENDER");
            serverWriter.Flush();
            Console.WriteLine("Enter message here...");

            string message;
            while ((message = Console.ReadLine()) != null)
            {
                serverWriter.WriteLine(message);
                serverWriter.Flush();
            }

            serverWriter.Close();
            serverReader.Close();
            serverStream.Close();
            serverClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

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
            string ipAndPort = await serverReader.ReadLineAsync();
            var split = ipAndPort.Split(":");
            string ipSender = split[0];
            string port = split[1];
            serverReader.Close();
            serverWriter.Close();
            serverStream.Close();
            serverClient.Close();
            TcpClient client2 = new TcpClient(ipSender, int.Parse(port));
            NetworkStream stream2 = client2.GetStream();
            StreamWriter serverWriter2 = new StreamWriter(stream2);
            string message;
            while ((message = Console.ReadLine()) != null)
            {
                serverWriter2.WriteLine(message);
                serverWriter2.Flush();
            }
            serverWriter2.Close();
            stream2.Close();
            client2.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

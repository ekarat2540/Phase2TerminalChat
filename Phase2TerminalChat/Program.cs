using System.IO;
using System.Net;
using System.Net.Sockets;

class Sender
{
    public static async Task Main(string[] args)
    {
        try
        {
            string ipAddress = "192.168.1.100";
            int Port = 5713;
            TcpClient serverClient = new TcpClient(ipAddress, Port);
            NetworkStream serverStream = serverClient.GetStream();
            StreamWriter serverWriter = new StreamWriter(serverStream);
            serverWriter.WriteLine("SENDER");
            serverWriter.Flush();
           
            Console.WriteLine("Enter message here");
            StreamReader streamReader = new StreamReader(serverStream);
            string ipAndPort = await streamReader.ReadLineAsync();
            serverWriter.Close();
            serverStream.Close();
            serverClient.Close();
            var split = ipAndPort.Split(":");
            string ipSender = split[0];
            string port = split[1];
            TcpListener listener = new TcpListener(IPAddress.Parse(ipSender), int.Parse(port));
            listener.Start();
            TcpClient receiverClient = await listener.AcceptTcpClientAsync();
            NetworkStream receiverStream = receiverClient.GetStream();
            StreamWriter writerReceiver = new StreamWriter(receiverStream);

            string message;
            while ((message = Console.ReadLine()) != null)
            {
                writerReceiver.WriteLine(message);
                writerReceiver.Flush();
            }
            writerReceiver.Close();
            receiverStream.Close();
            receiverClient.Close();
            listener.Stop();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

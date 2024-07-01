using System.Net.Sockets;

class Sender
{
    public static async Task Main(string[] args)
    {
        try
        {
            string ipAddress = "192.168.1.100";
            int Port = 5713;
            TcpClient receiverClient = new TcpClient(ipAddress,Port);
            NetworkStream receiverStream = receiverClient.GetStream();
            StreamWriter writer = new StreamWriter(receiverStream);
            writer.WriteLine("SENDER");
            writer.Flush();
            Console.WriteLine("Enter message here");
            string message;
            while((message = Console.ReadLine())  != null)
            {
                writer.WriteLine(message);
                writer.Flush();
            }
            writer.Close();
            receiverStream.Close();
            receiverClient.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

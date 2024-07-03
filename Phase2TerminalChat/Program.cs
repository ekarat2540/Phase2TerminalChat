using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Sender
{
    public static async Task Main(string[] args)
    {
        try
        {
            string serverIp = "192.168.1.102"; // IP ของ Server
            int serverPort = 5713;

            UdpClient udpClient = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

            string registerMessage = "SENDER";
            byte[] registerData = Encoding.UTF8.GetBytes(registerMessage);
            await udpClient.SendAsync(registerData, registerData.Length, serverEndPoint);

            UdpReceiveResult result = await udpClient.ReceiveAsync();
            string receiverInfo = Encoding.UTF8.GetString(result.Buffer);

            if (receiverInfo == "NO_RECEIVER")
            {
                Console.WriteLine("No receiver found.");
                return;
            }

            var split = receiverInfo.Split(':');
            string receiverIp = split[0];
            int receiverPort = int.Parse(split[1]);

            IPEndPoint receiverEndPoint = new IPEndPoint(IPAddress.Parse(receiverIp), receiverPort);

            Console.WriteLine("Enter messages to send. Type 'exit' to quit.");
            string message;
            while ((message = Console.ReadLine()) != null && message.ToLower() != "exit")
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                await udpClient.SendAsync(data, data.Length, receiverEndPoint);
            }

            udpClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

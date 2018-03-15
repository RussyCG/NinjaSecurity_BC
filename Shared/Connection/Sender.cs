using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class Sender
    {
        private int PORT_NO;
        private string SERVER_IP = "192.168.111.112";

        public Sender(int PortNum = 950)
        {
            this.PORT_NO = PortNum;
        }

        public void SendData(string textToSend)
        {
            EventManagement.EventManager.ReportNewEvent("Test", EventManagement.EventTypes.DEBUG);
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //---send the text---
            Console.WriteLine("Sending : " + textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            string Response = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            EventManagement.EventManager.ReportNewEvent("Data was sent to: " + SERVER_IP, EventManagement.EventTypes.INFO);
            client.Close();
        }
    }
}

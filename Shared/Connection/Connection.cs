using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class Connection
    {
        #region Fields

        #region Common

        private const int PORT_NO = 950;
        private const string SERVER_NAME = "localhost";
        private const string SERVER_IP = "127.0.0.1";
        private const string LOCAL_IP = "127.0.0.1";

        #endregion

        #region Server

        TcpListener listener;

        #endregion

        #region Client
        
        #endregion

        #endregion

        #region Constructors

        public Connection()
        {

        }

        #endregion

        #region Methods

        public bool SendData()
        {
            if (true)
            {
                return true;
            }
            return false;
        }

        public bool StartListening()
        {
            try
            {
                IPAddress localAddress = IPAddress.Parse(LOCAL_IP);
                listener = new TcpListener(localAddress, PORT_NO);
                Listen();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private void Listen()
        {
            listener.Start();
            Thread thread = new Thread(new ThreadStart(() =>
            {
                TcpClient client = listener.AcceptTcpClient();

                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received : " + dataReceived);

                nwStream.Write(buffer, 0, bytesRead);
                client.Close();
                listener.Stop();
            }));
        }
        
        private IPAddress GetIPAddressFromDNSName(string DNSName)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(DNSName);
            return hostEntry.AddressList.Length > 0 ? hostEntry.AddressList[0] : null;
        }

        private void AwaitData()
        {
            
        }

        public bool SendData(string strToSend)
        {
            return true;
        }

        #endregion
    }
}

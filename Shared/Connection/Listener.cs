using EventManagement;
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
    public class Listener
    {
        private int PORT_NO;
        private static TcpListener tcpListener;
        private const string LOCAL_ADDRESS = "127.0.0.1";

        /// <summary>
        /// Creates a new listener
        /// </summary>
        /// <param name="port">Port to bind on, default of 950</param>
        public Listener(int port = 950)
        {
            PORT_NO = port;
        }

        public bool StartListening()
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(() => {
                    tcpListener = new TcpListener(IPAddress.Parse(LOCAL_ADDRESS), PORT_NO);
                    tcpListener.Start();
                }))
                { Name = "TcpListenerStartingThread" };

                thread.Start();
                thread.Join();
                AwaitConnection();
                EventManager.ReportNewEvent(string.Format("TcpListener Started on Port {0}", PORT_NO), EventTypes.INFO);
                return true;
            }
            catch (Exception)
            {
                EventManager.ReportNewEvent(string.Format("Unable to start listening on Port {0}", PORT_NO), EventTypes.ERROR);
                return false;
            }
        }

        private void AwaitConnection()
        {
            new Thread(new ThreadStart(() =>
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                NetworkStream nwStream = client.GetStream();

                byte[] buffer = new byte[client.ReceiveBufferSize];

                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                string dataRecieved = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                new Action(() =>
                {
                    Console.WriteLine(dataRecieved);
                }).Invoke();
                
                string returnText = "DataRecieved";

                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnText);

                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                client.Close();
                AwaitConnection();
            }))
            { Name = "ListenerThread" }.Start();
        }

        public bool StopListening()
        {
            try
            {
                tcpListener.Stop();
                EventManager.ReportNewEvent(string.Format("TcpListener Stop on Port {0}", PORT_NO), EventTypes.WARNING);
                return true;
            }
            catch (Exception)
            {
                EventManager.ReportNewEvent(string.Format("Unable to stop listening on Port {0}", PORT_NO), EventTypes.ERROR);
                throw;
            }
        }
    }
}

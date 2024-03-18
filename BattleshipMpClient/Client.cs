using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient
{
    class Client
    {
        public static TcpClient client = new TcpClient();

        public static void ConnectToServer(string ip, string port)
        {
            //client = new TcpClient();
            //IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
            //client.Connect(ipEnd);

            try
            {
                client.BeginConnect(IPAddress.Parse(ip), int.Parse(port), ConnectCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                client.Dispose();
                client = null;
            }
        }

        public static void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                client.EndConnect(asyncResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

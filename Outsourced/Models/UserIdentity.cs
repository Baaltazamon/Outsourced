using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Outsourced.Models
{
    public static class UserIdentity
    {
        public static List<LogEnter> username = new List<LogEnter>();

        public static IPAddress GetIPAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }
    }

    public class LogEnter
    {
        public string username;
        public DateTime dateEnter;
        public IPAddress ip;
    }
}

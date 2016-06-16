using BPTServer.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server(IPAddress.Any);
            s.StartListening();
            Console.ReadLine();
        }
    }
}

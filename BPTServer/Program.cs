using BPTServer.Networking;
using BPTServer.Poker;
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
            for (int i = 0; i < 199; i++)
            {

            }
            Server s = new Server(IPAddress.Any);
            s.StartListening();
            while (true)
            {

                string str = Console.ReadLine();

                if (str == "1")
                {

                }

                else if (str == "quit")
                {
                    break;
                }
            }
        }
    }
}

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
                    Card c = new Card();
                    c.Suit = "Hearts";
                    c.Value = 9;
                    c.Name = "9 of hearts";
                    Card c2 = new Card();
                    c2.Suit = "Hearts";
                    c2.Value = 7;
                    c2.Name = "9 of clubs";
                    Card c3 = new Card();
                    c3.Suit = "Clubs";
                    c3.Value = 6;
                    c3.Name = "9 of heart";
                    Card c4 = new Card();
                    c4.Suit = "Hearts";
                    c4.Value = 5;
                    c4.Name = "5 of spades";
                    Card c5 = new Card();
                    c5.Suit = "Hearts";
                    c5.Value = 8;
                    c5.Name = "5 of diamonds";
                    Hand h = new Hand();
                    h.ListHand[0] = c;
                    h.ListHand[1] = c2;
                    h.ListHand[2] = c3;
                    h.ListHand[3] = c4;
                    h.ListHand[4] = c5;
                }
                else if (str == "quit")
                {
                    break;
                }
            }
        }
    }
}

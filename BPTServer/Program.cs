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

                    User[] testUsers = new User[6];
                    for (int i = 0; i < 6; i++)
                    {
                        User u = new User("Test " + i, "");
                        testUsers[i] = u;
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        Hand h = new Hand();
                        testUsers[i].PlayerHand = h;
                    }

                    Table testTable = new Table(testUsers[0], 6);

                    for (int i = 0; i < 6; i++)
                    {
                        Seat sea = new Seat(i, testUsers[i], true, false);
                        testTable.Seats[i] = sea;
                    }

                    testTable.Seats[5].SeatedUser.IsSmallBlind = true;

                    Table.tables.Add(testTable);

                    Dealer.NewDealer(0);
                    Dealer d = new Dealer();
                    d = Dealer.dealers[0];
                    while (true)
                    {
                        string test = Console.ReadLine();
                        if (test == "2")
                        {
                            d.ShuffleDeck();
                        }
                        else if (test == "3")
                        {
                            d.PrintDeck();

                        }
                        else if (test == "d")
                        {
                            d.DealCards();
                            foreach (Seat seat in Table.tables[0].Seats)
                            {
                                string testS = String.Format("Player {0} card one: {1} card two: {2}",
                                    seat.SeatedUser.UserName, seat.SeatedUser.PlayerHand.GivenCardOne.Name,
                                    seat.SeatedUser.PlayerHand.GivenCardTwo.Name);
                                Console.WriteLine(testS);
                            }
                        }
                    }
                }

                else if (str == "a")
                {
                    
                }
            }
        }
    }
}

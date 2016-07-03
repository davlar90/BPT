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

                    testTable.Seats[2].IsOccupied = false;
                    testTable.Seats[4].IsOccupied = false;
                    testTable.Seats[1].IsOccupied = false;

                    testTable.Seats[5].IsOccupied = false;


                    Table.tables.Add(testTable);


                    Dealer d = new Dealer();
                    d.NewDealer(Table.tables[0].TableID);
                    d = Dealer.dealers[0];
                    while (true)
                    {
                        string test = Console.ReadLine();
                        if (test == "loop")
                        {
                            int count = 0;
                            for (int i = 0; i < 30000; i++)
                            {
                                d.DealNewHand();
                                d.DealFlop();
                                d.DealTurn();
                                d.DealRiver();

                           List<User> winners = Rules.CheckWinners(Table.tables[0]);
                                if (winners[0].PlayerHand.NameOfHand.Contains("Full house"))
                                {

                                    
                                    if (winners.Count() > 0)
                                    {
                                        count++;
                                        Console.WriteLine("Royal Straight Flush");
                                        foreach (Seat seat in Table.tables[0].Seats)
                                        {
                                            if (seat.IsOccupied)
                                            {
                                                string testS = String.Format("Player {0} CARDS: {1}          {2}",
    seat.SeatedUser.UserName, seat.SeatedUser.PlayerHand.GivenCardOne.Name,
    seat.SeatedUser.PlayerHand.GivenCardTwo.Name);
                                                Console.WriteLine(testS);
                                            }

                                        }
                                        Console.WriteLine(".......");

                                        foreach (Card card in Table.tables[0].TablesCards)
                                        {
                                            Console.WriteLine("Table cards: " + card.Name);
                                        }
                                        Console.WriteLine(".......");
                                        foreach (User u in winners)
                                        {
                                            Console.WriteLine(String.Format("{0} HandName {1} Points {2} " , u.UserName,
                                                u.PlayerHand.NameOfHand, u.PlayerHand.HandsValue));
                                        }
                                    }
                                }
                            }
                            Console.WriteLine(count);
                        }
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
                            d.DealNewHand();
                            d.DealFlop();
                            d.DealTurn();
                            d.DealRiver();
                            foreach (Card card in Table.tables[0].TablesCards)
                            {
                                Console.WriteLine("Table cards: " + card.Name);
                            }
                            Console.WriteLine("....");
                            foreach (Seat seat in Table.tables[0].Seats)
                            {
                                if (seat.IsOccupied)
                                {
                                    string testS = String.Format("Player {0} CARDS: {1} {2}",
    seat.SeatedUser.UserName, seat.SeatedUser.PlayerHand.GivenCardOne.Name,
    seat.SeatedUser.PlayerHand.GivenCardTwo.Name);

                                    Console.Write(testS);
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    if (seat.SeatedUser.IsDealer) Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write("Dealer " + seat.SeatedUser.IsDealer.ToString() + " ");
                                    Console.ResetColor();
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    if (seat.SeatedUser.IsSmallBlind) Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write("SmallBlind " + seat.SeatedUser.IsSmallBlind.ToString() + " ");
                                    Console.ResetColor();
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    if (seat.SeatedUser.IsBigBlind) Console.BackgroundColor = ConsoleColor.Green;
                                    Console.WriteLine("BigBlind " + seat.SeatedUser.IsBigBlind.ToString() + " ");
                                    Console.ResetColor();
                                }



                            }
                            List<User> winners = Rules.CheckWinners(Table.tables[0]);

                            foreach (User u in winners)
                            {
                                Console.WriteLine("Winner(s): " + u.UserName);
                                Console.WriteLine(" Hand: " + u.PlayerHand.NameOfHand + " " + u.PlayerHand.HandsValue);
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Deck Length. Dealers deck length.");

                            Console.WriteLine(d.DealersDeck.Length + " " + d.TempDeck.Length);
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

﻿using BPTServer.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Dealer
    {
        public static List<Dealer> dealers = new List<Dealer>();
        public Card[] DealersDeck { get; set; }
        public Card[] TempDeck { get; set; }
        public int NumberOfHandsDealt { get; set; }
        public int DealerID { get; set; }

        public static Random rnd = new Random();

        ///<summary>
        ///Where dealerID should be the same as the tables ID.
        ///</summary>
        public static void NewDealer(int dealerID)
        {
            
            Dealer d = new Dealer();
            d.DealerID = dealerID;
            d.DealersDeck = Deck.NewDeck();
            d.ShuffleDeck();
            dealers.Add(d);
        }

        public void PrintDeck()
        {
            foreach (Card card in DealersDeck)
            {
                Console.WriteLine(card.Name);
            }
            Console.WriteLine(" Deck count: " + DealersDeck.Length);
        }

        public void ShuffleDeck()
        {
            Card[] DealersDeckShuffle = DealersDeck.OrderBy(x => rnd.Next()).ToArray();
            DealersDeck = DealersDeckShuffle;
        }

        public void DealCards()
        {
            NumberOfHandsDealt++;
            TempDeck = DealersDeck;
            List<User> users = new List<User>();
            int counter = 0;
            int smallBlindPos = -99; 
            foreach (Seat seat in Table.tables[DealerID].Seats)
            {
                if (seat.IsOccupied)
                {
                    users.Add(seat.SeatedUser);
                }
            }
            int numberOfPlayers = users.Count();
            foreach (User user in users)
            {
                if (user.IsSmallBlind)
                {
                    smallBlindPos = counter;
                    counter = 0;
                    break;
                }
                counter++;
            }
            int elseCounter = 0;
            for (int i = smallBlindPos; i < (users.Count() + smallBlindPos); i++) // Deals first card to all players
            {
                if (i < users.Count())
                {
                    users[i].PlayerHand.GivenCardOne = TempDeck[TempDeck.Length - 1];

                    RemoveOneCardFromDeck();
                }
                else
                {
                    users[elseCounter].PlayerHand.GivenCardOne = TempDeck[TempDeck.Length - 1];
                    RemoveOneCardFromDeck();
                    elseCounter++;
                }
            }
            elseCounter = 0;
            for (int i = smallBlindPos; i < (users.Count() + smallBlindPos); i++) // Deals second card to all players
            {
                if (i < users.Count())
                {
                    users[i].PlayerHand.GivenCardTwo = TempDeck[TempDeck.Length - 1];

                    RemoveOneCardFromDeck();
                }
                else
                {
                    users[elseCounter].PlayerHand.GivenCardTwo = TempDeck[TempDeck.Length - 1];
                    RemoveOneCardFromDeck();
                    elseCounter++;
                }
            }
            int c = 0;
            foreach (Seat seat in Table.tables[DealerID].Seats)
            {
                seat.SeatedUser = users[c];
                c++;
            }

        }
        public void DealFlop()
        {
            RemoveOneCardFromDeck(); //Burn card
            Table.tables[DealerID].TablesCards[0] = TempDeck[TempDeck.Length - 1];
            RemoveOneCardFromDeck();
            Table.tables[DealerID].TablesCards[1] = TempDeck[TempDeck.Length - 1];
            RemoveOneCardFromDeck();
            Table.tables[DealerID].TablesCards[2] = TempDeck[TempDeck.Length - 1];
            RemoveOneCardFromDeck();
        }
        public void DealTurn()
        {
            RemoveOneCardFromDeck();
            Table.tables[DealerID].TablesCards[3] = TempDeck[TempDeck.Length - 1];
            RemoveOneCardFromDeck();
        }
        public void DealRiver()
        {
            RemoveOneCardFromDeck();
            Table.tables[DealerID].TablesCards[4] = TempDeck[TempDeck.Length - 1];
            RemoveOneCardFromDeck();
        }

        public void RemoveOneCardFromDeck()
        {
            TempDeck = TempDeck.Take(TempDeck.Count() - 1).ToArray();
        }
    }
}

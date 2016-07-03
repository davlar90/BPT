using BPTServer.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Dealer
    {
        public static Dealer[] dealers = new Dealer[30];
        public Card[] DealersDeck { get; set; }
        public Card[] TempDeck { get; set; }
        public int NumberOfHandsDealt { get; set; }
        public int DealerID { get; set; }

        public static Random rnd = new Random();

        ///<summary>
        ///Where dealerID should be the same as the tables ID.
        ///</summary>
        public void NewDealer(int dealerID)
        {
            Dealer d = new Dealer();
            d.DealerID = dealerID;
            d.DealersDeck = Deck.NewDeck();
            d.ShuffleDeck();
            d.ChooseDealer();
            dealers[dealerID] = d;
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

            TempDeck = DealersDeck;
        }

        public void DealCards()
        {
            NumberOfHandsDealt++;
            List<User> users = new List<User>();
            int counter = 0;
            int smallBlindPos = -99; 
            foreach (Seat seat in Table.tables[DealerID].Seats)
            {
                if (seat.IsOccupied)
                {

                    seat.SeatedUser.PlayerHand = new Hand();
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

            for (int i = 0; i < numberOfPlayers; i++)
            {
                users[i].PlayerHand.TableAndHand[0] = users[i].PlayerHand.GivenCardOne;
                users[i].PlayerHand.TableAndHand[1] = users[i].PlayerHand.GivenCardTwo;
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

            for (int i = 0; i < Table.tables[DealerID].Seats.Length; i++)
            {
                Table.tables[DealerID].Seats[i].SeatedUser.PlayerHand.TableAndHand[2] = Table.tables[DealerID].TablesCards[0];
                Table.tables[DealerID].Seats[i].SeatedUser.PlayerHand.TableAndHand[3] = Table.tables[DealerID].TablesCards[1];
                Table.tables[DealerID].Seats[i].SeatedUser.PlayerHand.TableAndHand[4] = Table.tables[DealerID].TablesCards[2];
                Table.tables[DealerID].Seats[i].SeatedUser.PlayerHand.TableAndHand[5] = Table.tables[DealerID].TablesCards[3];
                Table.tables[DealerID].Seats[i].SeatedUser.PlayerHand.TableAndHand[6] = Table.tables[DealerID].TablesCards[4];
            }

        }
        public void RemoveOneCardFromDeck()
        {
            TempDeck = TempDeck.Take(TempDeck.Count() - 1).ToArray();
        }

        public void DealNewHand()
        {
            ShuffleDeck();
            MoveBlinds();
            DealCards();
        }

        private void MoveBlinds()
        {
            Table t = Table.tables[DealerID];

            int newDealerPos = 0;
            int newSmallBlindPos = 0;
            int newBigBlindPos = 0;

            for (int i = 0; i < t.TableSize; i++)
            {
                if (t.Seats[i].IsOccupied)
                {
                    t.Seats[i].SeatedUser.IsSmallBlind = false;
                    t.Seats[i].SeatedUser.IsBigBlind = false;
                }
            }

            for (int i = 0; i < t.TableSize; i++)
            {
                if (t.Seats[i].IsOccupied)
                {
                    if (t.Seats[i].SeatedUser.IsDealer)
                    {
                        t.Seats[i].SeatedUser.IsDealer = false;
                        newDealerPos = i + 1;
                        if (newDealerPos == t.TableSize) newDealerPos = 0;
                        break;
                    }
                }
            }
            int index = newDealerPos;



            while (true)
            {
                
                if (t.Seats[index].IsOccupied)
                {
                    t.Seats[index].SeatedUser.IsDealer = true;
                    newSmallBlindPos = index + 1;
                    if (newSmallBlindPos == t.TableSize)
                    {
                        newSmallBlindPos = 0;
                    }
                    break;
                }
                index++;
                if (index == t.TableSize) index = 0;
            }
            index = newSmallBlindPos;

            if (GetPlayersAtTable() == 2) //Headsup
            {
                index -= 1;
                if (index == -1) index = t.TableSize - 1;
            }

            while (true)
            {
                
                if (t.Seats[index].IsOccupied)
                {
                    t.Seats[index].SeatedUser.IsSmallBlind = true;
                    newBigBlindPos = index + 1;
                    if (newBigBlindPos == t.TableSize)
                    {
                        newBigBlindPos = 0;
                    }
                    break;
                }
                index++;
                if (index == t.TableSize) index = 0;
            }
            index = newBigBlindPos;


            while (true)
            {
                if (t.Seats[index].IsOccupied)
                {
                    t.Seats[index].SeatedUser.IsBigBlind = true;
                    break;
                }
                index++;
                if (index == t.TableSize)
                {
                    index = 0;
                }
            }

            Table.tables[DealerID] = t;

        }
        private void ChooseDealer()
        {
            int numberOfPlayers = GetPlayersAtTable();
            int dealerPos = rnd.Next(numberOfPlayers);

            int counter = 0;

            for (int i = 0; i < Table.tables[DealerID].TableSize; i++)
            {
                if (Table.tables[DealerID].Seats[i].IsOccupied)
                {
                    if (counter == dealerPos)
                    {
                        Table.tables[DealerID].Seats[i].SeatedUser.IsDealer = true;
                        break;
                    }
                    counter++;
                }
            }
        }
        private int GetPlayersAtTable()
        {
            int count = 0;
            foreach (Seat s in Table.tables[DealerID].Seats)
            {
                if (s.IsOccupied)
                {
                    count++;
                }
            }
            return count;
        }
    }
}

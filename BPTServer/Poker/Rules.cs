using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Rules     //Rules for checking hand.
    {
        
        public static List<User> CheckWinners(Table t)
        {
            List<User> players = new List<User>();
            List<User> winners = new List<User>();
            for (int i = 0; i < t.Seats.Count(); i++)
            {
                if (t.Seats[i].IsOccupied)
                {
                    t.Seats[i].SeatedUser.PlayerHand = GetHandsValue(t.Seats[i].SeatedUser.PlayerHand);
                    players.Add(t.Seats[i].SeatedUser);
                }
            }
            List<User> sortedPlayers = players.OrderByDescending(o => o.PlayerHand.HandsValue).ToList();
            int numberOfPlayers = sortedPlayers.Count();
            winners.Add(sortedPlayers[0]);
            for (int i = 1; i < numberOfPlayers; i++)
            {
                if (sortedPlayers[i].PlayerHand.HandsValue == sortedPlayers[0].PlayerHand.HandsValue)
                {
                    winners.Add(sortedPlayers[i]);
                }
            }
            return winners;
        }

        public static Hand GetHandsValue(Hand h)
        {
            /*      Points for each hand:
             *      Straight Flush = 900.
             *      Four of a kind = 800.
             *      Full house     = 700.
             *      Flush          = 600.
             *      Straight       = 500.
             *      Three of a kind= 400.
             *      Two pairs      = 300
             *      One Pair       = 200
             *      High card      = Each high card gets it's own number as points.
             *      */


           
            List<Card> sortedTableAndHand = h.TableAndHand.OrderByDescending(o => o.Value).ToList();



            Card[] flush = CheckHandForFlush(h);
            Card[] straight = CheckHandForStraight(h);
            if ((straight.Length == 5) && (flush.Length == 5))
            {
                h.NameOfHand = "Straight Flush. " + flush[0].Name + " to " + flush[4].Name;
                h.HandsValue = 900 + flush[0].Value;
                if (flush[0].Value == 14)
                {
                    h.NameOfHand = "Royal Straight Flush.";
                    h.HandsValue = 914;
                }
            }
            else if (flush.Length == 5)
            {
                h.NameOfHand = "Flush of " + flush[0].Suit + ".";
                h.HandsValue = 600 + flush[0].Value + flush[1].Value
                     + flush[2].Value + flush[3].Value + flush[4].Value;
            }
            else if (straight.Length == 5)
            {
                h.NameOfHand = "Straight " + straight[0].Name + " to " + straight[4].Name + ".";
                h.HandsValue = 500 + straight[0].Value;
            }
            else
            {

                h = CheckHandSameValuedCards(h);
            }


            return h;
        }

        public static Card[] CheckHandForStraight(Hand h)
        {
            Card[] straight = new Card[5];
            int straightStartingIndex = 0;
            List<Card> sortedTableAndHand = h.TableAndHand.OrderByDescending(o => o.Value).ToList();
            
            int straightCounter = 0; // If this reaches 4, then is straight.
            for (int i = 0; i < 6; i++)
            {
                if (sortedTableAndHand[i].Value == (sortedTableAndHand[i + 1].Value + 1))
                {
                    straightCounter++;
                    if (i == 5)
                    {
                        if (sortedTableAndHand[6].Value == sortedTableAndHand[5].Value)
                        {
                            straightCounter++;
                        }
                    }
                    if (straightCounter == 4) break;
                }
                else
                {
                    straightCounter = 0;
                    straightStartingIndex++;
                    if (straightStartingIndex == 3) break;
                }
            }
            if (straightCounter >= 4)
            {
                for (int i = 0; i < 5; i++)
                {
                    straight[i] = sortedTableAndHand[straightStartingIndex + i];
                }
                return straight;
            }

            return h.TableAndHand;
        }

        public static Hand CheckHandSameValuedCards(Hand h)
        {
            int handsValue = 0;
            string nameHand = "";

            List<List<Card>> cardsCheckedList = CheckCardsOfSameValue(h);
            List<Card> cardsChecked = cardsCheckedList[0];


            int sameValueCount = cardsChecked.Count();
            if (sameValueCount > 1)
            {
                
                if(sameValueCount == 2) //Single Pair
                {
                    handsValue += 200;
                    nameHand = "Pair of " + cardsChecked[0].Value + "'s";
                }
                else if (sameValueCount == 3)
                {
                    //three of a kind
                    nameHand = "Three of a kind. " + cardsChecked[0].Value + "'s";
                    handsValue += 400;

                }
                else if (sameValueCount == 4)
                {
                    if ((cardsChecked[0].Value == cardsChecked[1].Value) && 
                        (cardsChecked[0].Value == cardsChecked[2].Value))
                    {   // Four of a kind
                        nameHand = "Four of a kind " + cardsChecked[0].Value + "'s";
                        handsValue += 800;
                    }
                    else
                    {
                        if (cardsChecked[0].Value == cardsChecked[1].Value)  //Twopair
                        {
                            nameHand = "Two pairs. " + cardsChecked[0].Value + "'s and " + cardsChecked[2].Value + "'s";
                            handsValue += 300;
                        }
                        else // Twopair
                        {
                            nameHand = "Two pairs. " + cardsChecked[0].Value + "'s and " + cardsChecked[1].Value + "'s";

                            handsValue += 300;
                        }
                    }
                }
                else if (sameValueCount == 5)
                {
                    int counter = 0;
                    foreach (Card card in cardsChecked)
                    {
                        if (cardsChecked[0].Value == cardsChecked[counter].Value)
                        {
                            counter++;
                        }
                    }
                    string value = "";
                    foreach (Card cards in cardsChecked)
                    {
                        if (cards.Value != cardsChecked[0].Value)
                        {
                            value = cards.Name;
                            break;
                        }
                    }
                    if (counter == 3) // Full house. First card in list is the 3pair
                    {
                        nameHand = "Full house. " + cardsChecked[0].Value +
                            "'s and " + value + "'s";
                        handsValue += 700;
                    }
                    else // Full House. 
                    {
                        nameHand = "Full house with three of " + value +
                            " and two of " + cardsChecked[0].Name + ".";
                        handsValue += 700;
                    }


                }
                else // High card
                {

                }
            }
            string highCards = "";
            if (cardsCheckedList.Count() == 2) //If there is two lists in this list then there are highcards.
            {
                highCards = " High card(s):";
                List<Card> highCardList = cardsCheckedList[1];

                foreach (Card card in highCardList)
                {
                    highCards += (" " + card.Name);
                    handsValue += card.Value;
                }
            }
            
            h.HandsValue = handsValue;
            h.NameOfHand = nameHand + highCards;
            return h;
        }

        private static List<List<Card>> CheckCardsOfSameValue(Hand h)
        {
            List<Card> returnCards = new List<Card>();

            int[] values = new int[7];
            int[] counters = new int[7];

            for (int i = 0; i < 7; i++)
            {
                values[i] = h.TableAndHand[i].Value;
            }
            int count = 0;
            foreach (Card c in h.TableAndHand)
            {
                if (c.Value == values[0]) counters[count]++;
                if (c.Value == values[1]) counters[count]++;
                if (c.Value == values[2]) counters[count]++;
                if (c.Value == values[3]) counters[count]++;
                if (c.Value == values[4]) counters[count]++;
                if (c.Value == values[5]) counters[count]++;
                if (c.Value == values[6]) counters[count]++;

                count++;
            }

            for (int i = 0; i < 7; i++)
            {
                if (counters[i] > 1)
                {
                    returnCards.Add(h.TableAndHand[i]);
                }
            }

            if (returnCards.Count() > 5)
            {
                int m = 0;

                List<Card> tempListThreePairs = new List<Card>();
                List<Card> tempReturnCards = new List<Card>();
                for (int i = 0; i < 7; i++)
                {
                    if (counters[i] == 4)
                    {
                        tempReturnCards.Add(h.TableAndHand[i]);
                        tempReturnCards.Add(h.TableAndHand[i]);
                        tempReturnCards.Add(h.TableAndHand[i]);
                        tempReturnCards.Add(h.TableAndHand[i]);
                        returnCards = tempReturnCards;
                        break;
                    }
                    else if (counters[i] == 3)
                    {

                        tempReturnCards.Add(h.TableAndHand[i]);
                        tempReturnCards.Add(h.TableAndHand[i]);
                        tempReturnCards.Add(h.TableAndHand[i]);
                        for (int j = 0; j < 7; j++)
                        {
                            int c = 0;
                            if (counters[j] == 2)
                            {
                                c = j;
                                for (int k = 0; k < 7; k++)
                                {
                                    if ((k != c) && (counters[k] == 2))
                                    {
                                        if (counters[c] > counters[k])
                                        {
                                            tempReturnCards.Add(h.TableAndHand[c]);
                                            tempReturnCards.Add(h.TableAndHand[c]);
                                        }
                                        else
                                        {
                                            tempReturnCards.Add(h.TableAndHand[k]);
                                            tempReturnCards.Add(h.TableAndHand[k]);

                                        }
                                        returnCards = tempReturnCards;
                                    }
                                }
                            }

                        }
                        m++;

                        if (m == 2)
                        {
                                tempReturnCards.Add(h.TableAndHand[i]);
                                tempReturnCards.Add(h.TableAndHand[i]);
                                returnCards = tempReturnCards;
                        }
                    }
                    else if (counters[i] == 2)
                    {
                        tempListThreePairs.Add(h.TableAndHand[i]);
                    }
                }
                if (tempListThreePairs.Count() != 0)
                {
                    List<Card> sortThreePairs = tempListThreePairs.OrderByDescending(o => o.Value).ToList();
                    sortThreePairs.RemoveAt(5);
                    sortThreePairs.RemoveAt(4);

                    returnCards = sortThreePairs;
                }
            }

            

            List<Card> sortedReturnCards = returnCards.OrderByDescending(o => o.Value).ToList();

            List<Card> highCardList = new List<Card>();
            foreach (Card card in h.TableAndHand)
            {
                if (!returnCards.Contains(card))
                {
                    highCardList.Add(card);
                    
                }
            }

            List<Card> sortedHighCardList = highCardList.OrderByDescending(o => o.Value).ToList();
            List<List<Card>> listOfCardLists = new List<List<Card>>();
            listOfCardLists.Add(returnCards);
            for (int i = 0; i < 10; i++)
            {

                if (returnCards.Count() + sortedHighCardList.Count() > 5)
                {
                    sortedHighCardList.RemoveAt(sortedHighCardList.Count - 1);
                }
                else break;
            }
            listOfCardLists.Add(sortedHighCardList);
            return listOfCardLists;

        }

        private static Card[] CheckHandForFlush(Hand h)
        {
            List<Card> sortedTableAndHand = h.TableAndHand.OrderByDescending(o => o.Suit).ToList();

            Card[] flush = new Card[5];
            
            int clubs = 0;
            int diamonds = 0;
            int hearts = 0;
            int spades = 0;
            foreach (Card c in h.TableAndHand)
            {
                switch (c.Suit)
                {
                    case "Clubs":
                        clubs++;
                        break;
                    case "Diamonds":
                        diamonds++;
                            break;
                    case "Hearts":
                        hearts++;
                        break;
                    case "Spades":
                        spades++;
                        break;

                    default:
                        break;
                }

            }
            int count = 0;
            if (clubs > 4)
            {
                for (int i = 0; i < 7; i++)
                {
                    if ((sortedTableAndHand[i].Suit == "Clubs") && (count < 5))
                    {
                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                return flush;
            }
            if (hearts > 4)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (sortedTableAndHand[i].Suit == "Hearts" && (count < 5))
                    {
                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                return flush;
            }
            if (diamonds > 4)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (sortedTableAndHand[i].Suit == "Diamonds" && (count < 5))
                    {
                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                return flush;
            }
            if (spades > 4)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (sortedTableAndHand[i].Suit == "Spades" && (count < 5))
                    {
                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                return flush;
            }

            return h.TableAndHand;
        }
    }
}

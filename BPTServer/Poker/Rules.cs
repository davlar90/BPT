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



            List<Card> flush = CheckHandForFlush(h);
            Card[] straight = CheckHandForStraight(h);
            if (straight.Count() == 5)
            {
                Hand tempHand = new Hand();
                for (int i = 0; i < 5; i++)
                {
                    tempHand.ListHand[i] = straight[i];
                }
                List<Card> tempFlush = CheckHandForFlush(tempHand);
                if (tempFlush.Count() == 5)
                {
                    h.NameOfHand = "Straight Flush. " + flush[0].Name + " to " + flush[4].Name;
                    h.HandsValue = 9000 + (flush[0].Value * 10);
                    if (flush[0].Value == 14)
                    {
                        h.NameOfHand = "Royal Straight Flush.";
                        h.HandsValue = 9999;
                    }
                }
                else
                {
                    h.NameOfHand = "Straight " + straight[0].Name + " to " + straight[4].Name + ".";
                    h.HandsValue = 5000 + (straight[0].Value * 10);
                }

            }
            else if (flush.Count() == 5)
            {
                h.NameOfHand = "Flush of " + flush[0].Suit + ".";
                h.HandsValue = 6000 + ((flush[0].Value * 10) + flush[1].Value
                     + flush[2].Value + flush[3].Value + flush[4].Value);
            }
            else
            {
                h = CheckHandSameValuedCards(h);
            }


            return h;
        }

        public static Card[] CheckHandForStraight(Hand h)
        {

            // All cards must be single in this list!!
            List<Card> sortedTableAndHand = h.TableAndHand.OrderByDescending(o => o.Value).ToList();
            HashSet<int> values = new HashSet<int>(); // Type of property
            sortedTableAndHand.RemoveAll(i => !values.Add(i.Value));
            sortedTableAndHand = sortedTableAndHand.OrderByDescending(o => o.Value).ToList();

            Card[] straight = new Card[5];


            if (sortedTableAndHand[0].Value == 14)
            {
                Card c = new Card();
                c.Suit = sortedTableAndHand[0].Suit;
                c.Value = 1;
                c.Name = "Ace(1) of " + c.Suit;
                sortedTableAndHand.Add(c);
            }
            if (sortedTableAndHand.Count() == 8)
            {
                if (sortedTableAndHand[2].Value == (sortedTableAndHand[3].Value + 1) &&
                     sortedTableAndHand[2].Value == (sortedTableAndHand[4].Value + 2) || (sortedTableAndHand[3].Value ==
                     sortedTableAndHand[5 + 2].Value))
                {
                    if (sortedTableAndHand[1].Value - 1 == sortedTableAndHand[2].Value)
                    {
                        if (sortedTableAndHand[0].Value - 2 == sortedTableAndHand[2].Value)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                straight[i] = sortedTableAndHand[i];
                            }
                            return straight;
                        }
                        else if (sortedTableAndHand[5].Value + 1 == sortedTableAndHand[4].Value)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                straight[i] = sortedTableAndHand[i + 1];
                            }
                            return straight;
                        }
                    }
                    else if (sortedTableAndHand[5].Value + 1 == sortedTableAndHand[4].Value)
                    {
                        if (sortedTableAndHand[6].Value + 2 == sortedTableAndHand[4].Value)
                        {

                            if (sortedTableAndHand[7].Value + 3 == sortedTableAndHand[4].Value)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    straight[i] = sortedTableAndHand[i + 3];
                                }
                                return straight;
                            }
                            else
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    straight[i] = sortedTableAndHand[i + 2];
                                }
                                return straight;
                            }

                        }
                    }
                }
            }
            else if (sortedTableAndHand.Count() == 7)
            {

                if (sortedTableAndHand[2].Value == (sortedTableAndHand[3].Value + 1) &&
                    sortedTableAndHand[2].Value == (sortedTableAndHand[4].Value + 2))
                {
                    if (sortedTableAndHand[1].Value - 1 == sortedTableAndHand[2].Value)
                    {
                        if (sortedTableAndHand[0].Value - 2 == sortedTableAndHand[2].Value)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                straight[i] = sortedTableAndHand[i];
                            }
                            return straight;
                        }
                        else if (sortedTableAndHand[5].Value + 1 == sortedTableAndHand[4].Value)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                straight[i] = sortedTableAndHand[i + 1];
                            }
                            return straight;
                        }
                    }
                    else if (sortedTableAndHand[5].Value + 1 == sortedTableAndHand[4].Value)
                    {
                        if (sortedTableAndHand[6].Value + 2 == sortedTableAndHand[4].Value)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                straight[i] = sortedTableAndHand[i + 2];
                            }
                            return straight;
                        }
                    }
                }
            }
            else if (sortedTableAndHand.Count() > 4)
            {
                if (sortedTableAndHand[0].Value == sortedTableAndHand[1].Value + 1)
                    if (sortedTableAndHand[0].Value == sortedTableAndHand[2].Value + 2)
                        if (sortedTableAndHand[0].Value == sortedTableAndHand[3].Value + 3)
                            if (sortedTableAndHand[0].Value == sortedTableAndHand[4].Value + 4)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    straight[i] = sortedTableAndHand[i];
                                }
                                return straight;
                            }
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
                    handsValue += 2000 + (cardsChecked[0].Value * 10);
                    nameHand = "Pair of " + GetNameFromCardValue(cardsChecked[0]) + "'s";
                }
                else if (sameValueCount == 3)
                {
                    //three of a kind
                    nameHand = "Three of a kind. " + GetNameFromCardValue(cardsChecked[0]) + "'s";
                    handsValue += 4000 + (cardsChecked[0].Value * 10);

                }
                else if (sameValueCount == 4)
                {
                    if ((cardsChecked[0].Value == cardsChecked[1].Value) && 
                        (cardsChecked[0].Value == cardsChecked[2].Value))
                    {   // Four of a kind
                        nameHand = "Four of a kind " + GetNameFromCardValue(cardsChecked[0]) + "'s";
                        handsValue += 8000 + (cardsChecked[0].Value * 10);
                    }
                    else
                    {
                        if (cardsChecked[0].Value == cardsChecked[1].Value)  //Twopair
                        {
                            nameHand = "Two pairs. " + GetNameFromCardValue(cardsChecked[0]) + 
                                "'s and " + GetNameFromCardValue(cardsChecked[2]) + "'s";
                            if (cardsChecked[0].Value > cardsChecked[2].Value)
                            {
                                handsValue += 3000 + (cardsChecked[0].Value * 10);
                            }
                            else
                            {
                                handsValue += 3000 + (cardsChecked[2].Value * 10);
                            }
                        }
                        else // Twopair
                        {
                            nameHand = "Two pairs. " + GetNameFromCardValue(cardsChecked[0]) + 
                                "'s and " + GetNameFromCardValue(cardsChecked[1]) + "'s";

                            if (cardsChecked[0].Value > cardsChecked[1].Value)
                            {
                                handsValue += 3000 + (cardsChecked[0].Value * 10);
                            }
                            else
                            {
                                handsValue += 3000 + (cardsChecked[1].Value * 10);
                            }
                        }
                    }
                }
                else if (sameValueCount == 5)
                {
                    int counter = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (cardsChecked[0].Value == cardsChecked[i].Value) counter++;
                    }
                    string thePair = "";
                    int thePairIndex = 0;
                    foreach (Card cards in cardsChecked)
                    {
                        if (cards.Value != cardsChecked[0].Value)
                        {
                            thePairIndex++;
                            thePair = GetNameFromCardValue(cards);
                            break;
                        }
                    }
                    if (counter == 3) // Full house. First card in list is the 3pair
                    {
                        nameHand = "Full house. " + "3 " + GetNameFromCardValue(cardsChecked[0]) +
                            "'s and 2 " + thePair + "'s";
                        handsValue += 7000 + (cardsChecked[0].Value * 10) + cardsChecked[thePairIndex].Value;
                    }
                    else // Full House. 
                    {
                        nameHand = "Full house. 3 " + GetNameFromCardValue(cardsChecked[thePairIndex]) +
                            "'s and 2 " + GetNameFromCardValue(cardsChecked[0]) + "'s";
                        handsValue += 7000 + (cardsChecked[thePairIndex].Value * 10) + cardsChecked[0].Value;
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

        public static string GetNameFromCardValue(Card c)
        {
            string name = "";
            if (c.Value > 10)
            {
                switch (c.Value)
                {
                    case 11:
                        name = "Jack";
                        break;

                    case 12:
                        name = "Queen";
                        break;

                    case 13:
                        name = "King";
                        break;

                    case 14:
                        name = "Ace";
                        break;

                    default:
                        break;
                }
            }
            else name = c.Value.ToString();
            return name;
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

                List<Card> tempListFoursAndThrees = new List<Card>();
                List<Card> tempListThreePairs = new List<Card>();
                List<Card> tempReturnCards = new List<Card>();
                for (int i = 0; i < 7; i++)
                {
                    if (counters[i] == 4)
                    {
                        tempReturnCards.Add(h.TableAndHand[i]);
                        
                    }
                    else if (counters[i] == 3)
                    {
                        tempReturnCards.Add(h.TableAndHand[i]);
                    }
                    else if (counters[i] == 2)
                    {
                        tempListThreePairs.Add(h.TableAndHand[i]);
                    }
                }
                returnCards.Clear();
                if (tempListThreePairs.Count() != 0)
                {
                    List<Card> sortThreePairs = tempListThreePairs.OrderByDescending(o => o.Value).ToList();
                    if (sortThreePairs.Count() == 6)
                    {
                        sortThreePairs.RemoveAt(5);
                        sortThreePairs.RemoveAt(4);
                        returnCards = sortThreePairs;

                    }
                    else if (sortThreePairs.Count() == 4)
                    {
                        sortThreePairs.RemoveAt(3);
                        sortThreePairs.RemoveAt(2);
                        returnCards = sortThreePairs;
                    }
                }
                foreach (Card c in tempReturnCards)
                {
                    returnCards.Add(c);
                }
            }

            

            List<Card> sortedReturnCards = returnCards.OrderByDescending(o => o.Value).ToList();
            if (sortedReturnCards.Count() == 6)
            {
                sortedReturnCards.RemoveAt(sortedReturnCards.Count - 1);
                returnCards = sortedReturnCards;
            }

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
                    if (sortedHighCardList.Count > 0)
                    {
                        sortedHighCardList.RemoveAt(sortedHighCardList.Count - 1);
                    }
                }
                else break;
            }
            listOfCardLists.Add(sortedHighCardList);
            return listOfCardLists;

        }

        private static List<Card> CheckHandForFlush(Hand h)
        {
            List<Card> sortedTableAndHand = new List<Card>();
            if (h.ListHand[0] != null)
            {
                sortedTableAndHand = h.ListHand.OrderByDescending(o => o.Suit).ToList();
            }
            else
            {
                sortedTableAndHand = h.TableAndHand.OrderByDescending(o => o.Suit).ToList();

            }
            Card[] flush = new Card[5];
            
            int clubs = 0;
            int diamonds = 0;
            int hearts = 0;
            int spades = 0;
            foreach (Card c in sortedTableAndHand)
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
                for (int i = 0; i < sortedTableAndHand.Count(); i++)
                {
                    if ((sortedTableAndHand[i].Suit == "Clubs") && (count < 5))
                    {

                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                List<Card> sortedFlush = flush.OrderByDescending(o => o.Value).ToList();

                return sortedFlush;
            }
            if (hearts > 4)
            {
                for (int i = 0; i < sortedTableAndHand.Count(); i++)
                {
                    if (sortedTableAndHand[i].Suit == "Hearts" && (count < 5))
                    {
                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                List<Card> sortedFlush = flush.OrderByDescending(o => o.Value).ToList();

                return sortedFlush;
            }
            if (diamonds > 4)
            {
                for (int i = 0; i < sortedTableAndHand.Count(); i++)
                {
                    if (sortedTableAndHand[i].Suit == "Diamonds" && (count < 5))
                    {
                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                List<Card> sortedFlush = flush.OrderByDescending(o => o.Value).ToList();

                return sortedFlush;
            }
            if (spades > 4)
            {
                for (int i = 0; i < sortedTableAndHand.Count(); i++)
                {
                    if (sortedTableAndHand[i].Suit == "Spades" && (count < 5))
                    {
                        flush[count] = sortedTableAndHand[i];
                        count++;
                    }
                }
                List<Card> sortedFlush = flush.OrderByDescending(o => o.Value).ToList();

                return sortedFlush;
            }
            sortedTableAndHand.Add(sortedTableAndHand[0]);
            return sortedTableAndHand;
        }
    }
}

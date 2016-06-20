using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Rules     //Rules for checking hand.
    {
        public static int GetHandsValue(Hand h)
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
            List<Card> sortedListHand = h.ListHand.OrderByDescending(o => o.Value).ToList();
            int handValue = 0;
            if ((CheckHandForStraight(h)) && (CheckHandForFlush(h)))
            {
                
                handValue = 900;
            }
            else if (CheckHandForFlush(h))
            {
                handValue = 600 + sortedListHand[0].Value;
            }
            else if (CheckHandForStraight(h))
            {
                handValue = 500 + sortedListHand[0].Value;
            }
            else
            {
                handValue = CheckHandSameValuedCards(h);
            }


            return handValue;
        }

        public static bool CheckHandForStraight(Hand h)
        {
            bool isStraight = false;
            List<Card> sortedListHand = h.ListHand.OrderByDescending(o => o.Value).ToList();
            
            int straightCounter = 0; // If this reaches 4, then is straight.
            for (int i = 0; i < 4; i++)
            {
                if (sortedListHand[i].Value == (sortedListHand[i + 1].Value + 1))
                {
                    straightCounter++;
                }
            }
            if (straightCounter == 4) isStraight = true;

            return isStraight;
        }

        public static int CheckHandSameValuedCards(Hand h)
        {
            int handsValue = 0;

            List<List<Card>> cardsCheckedList = CheckCardsOfSameValue(h);
            List<Card> cardsChecked = cardsCheckedList[0];


            int sameValueCount = cardsChecked.Count();
            if (sameValueCount > 1)
            {
                
                if(sameValueCount == 2) //Single Pair
                {
                    handsValue += 200;

                    Console.WriteLine("You have a pair of " + cardsChecked[0].Name);
                }
                else if (sameValueCount == 3)
                {
                    //three of a kind
                    handsValue += 400;
                    Console.WriteLine("You have three of a kid of " + cardsChecked[0].Name);

                }
                else if (sameValueCount == 4)
                {
                    if ((cardsChecked[0].Value == cardsChecked[1].Value) && 
                        (cardsChecked[0].Value == cardsChecked[2].Value))
                    {   // Four of a kind
                        handsValue += 800;
                        Console.WriteLine("You four of a kind of " + cardsChecked[0].Name);
                    }
                    else
                    {
                        if (cardsChecked[0].Value == cardsChecked[1].Value)  //Twopair
                        {
                            handsValue += 300;
                            Console.WriteLine("You have two pairs of " + cardsChecked[0].Name +
                                " " + cardsChecked[2].Name);
                        }
                        else // Twopair
                        {
                            handsValue += 300;
                            Console.WriteLine("You have two pairs of " + cardsChecked[0].Name +
                                " " + cardsChecked[1].Name);
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
                        handsValue += 700;
                        Console.WriteLine("You have a full house. Three of " + cardsChecked[0].Name +
                            " and two of " + value);
                    }
                    else // Full House. 
                    {
                        handsValue += 700;
                        Console.WriteLine("You have a full house. Two of " + cardsChecked[0].Name +
                            " and three of " + value);
                    }


                }
                else // High card
                {

                }
            }
            if (cardsCheckedList.Count() == 2)
            {
                List<Card> highCardList = cardsCheckedList[1];
                foreach (Card card in highCardList)
                {
                    Console.WriteLine("Highcard('s): " + card.Value);
                    handsValue += card.Value;
                }
            }

            return handsValue;
        }

        private bool CheckStraight(Hand h)
        {
            throw new NotImplementedException();
        }

        private static List<List<Card>> CheckCardsOfSameValue(Hand h)
        {
            List<Card> returnCards = new List<Card>();
            int one = h.ListHand[0].Value;
            int two = h.ListHand[1].Value;
            int three = h.ListHand[2].Value;
            int four = h.ListHand[3].Value;
            int five = h.ListHand[4].Value;
            int oneCounter = 0;
            int twoCounter = 0;
            int threeCounter = 0;
            int fourCounter = 0;
            int fiveCounter = 0;
            foreach (Card c in h.ListHand)
            {
                if (c.Value == one) oneCounter++;
                if (c.Value == two) twoCounter++;
                if (c.Value == three) threeCounter++;
                if (c.Value == four) fourCounter++;
                if (c.Value == five) fiveCounter++;
            }

            if (oneCounter > 1 )
            {
                if (oneCounter > 2)
                {

                    if (oneCounter > 3)
                    {
                        returnCards.Add(h.ListHand[0]);
                    }
                    else
                    {
                        returnCards.Add(h.ListHand[0]);
                    }
                }
                else
                {
                    returnCards.Add(h.ListHand[0]);
                }
            }
            else
            {
            }

            if (twoCounter > 1)
            {
                if (twoCounter > 2)
                {

                    if (twoCounter > 3)
                    {
                        returnCards.Add(h.ListHand[1]);
                    }
                    else
                    {
                        returnCards.Add(h.ListHand[1]);
                    }
                }
                else
                {
                    returnCards.Add(h.ListHand[1]);
                }
            }
            else
            {
            }
            if (threeCounter > 1)
            {
                if (threeCounter > 2)
                {

                    if (threeCounter > 3)
                    {
                        returnCards.Add(h.ListHand[2]);
                    }
                    else
                    {
                        returnCards.Add(h.ListHand[2]);
                    }
                }
                else
                {
                    returnCards.Add(h.ListHand[2]);
                }
            }
            else
            {
            }
            if (fourCounter > 1)
            {
                if (fourCounter > 2)
                {

                    if (fourCounter > 3)
                    {
                        returnCards.Add(h.ListHand[3]);
                    }
                    else
                    {
                        returnCards.Add(h.ListHand[3]);
                    }
                }
                else
                {
                    returnCards.Add(h.ListHand[3]);
                }
            }
            else
            {
            }
            if (fiveCounter > 1)
            {
                if (fiveCounter > 2)
                {

                    if (fiveCounter > 3)
                    {
                        returnCards.Add(h.ListHand[4]);
                    }
                    else
                    {
                        returnCards.Add(h.ListHand[4]);
                    }
                }
                else
                {
                    returnCards.Add(h.ListHand[4]);
                }
            }
            else
            {
            }
            int cardsWithSameValue = returnCards.Count();
            List<Card> highCardList = new List<Card>();
            foreach (Card card in h.ListHand)
            {
                if (!returnCards.Contains(card))
                {
                    highCardList.Add(card);
                    
                }
            }
            List<Card> sortedHighCardList = highCardList.OrderByDescending(o => o.Value).ToList();
            List<List<Card>> listOfCardLists = new List<List<Card>>();
            listOfCardLists.Add(returnCards);
            listOfCardLists.Add(sortedHighCardList);
            return listOfCardLists;

        }



        private static bool CheckStraightFlush(Hand h)
        {
            throw new NotImplementedException();
        }

        private static bool CheckHandForFlush(Hand h)
        {
            int clubs = 0;
            int diamonds = 0;
            int hearts = 0;
            int spades = 0;
            bool hasFlush = false;
            foreach (Card c in h.ListHand)
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

            if ((clubs == 5) || (hearts == 5) || (diamonds == 5) || (spades == 5))
            {
                hasFlush = true;
            }

            return hasFlush;
        }
    }
}

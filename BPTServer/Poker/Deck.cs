using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Deck
    {
        public Card[] Deck52 { get; set; } = new Card[52];

        public string[] TheDeck { get; set; } = {
                            "c2","c3","c4","c5","c6","c7","c8","c9","ct","cj","cq","ck", "ca"
                            ,"d2","d3","d4","d5","d6","d7","d8","d9","dt","dj","dq","dk", "da"
                            ,"h2","h3","h4","h5","h6","h7","h8","h9","ht","hj","hq","hk", "ha"
                            ,"s2","s3","s4","s5","s6","s7","s8","s9","st","sj","sq","sk", "sa"};


        public Deck NewDeck()
        {
            int i = 0;
            Deck d = new Deck();
            foreach (string card in d.TheDeck)
            {
                Card c = new Card();
                char suit = card[0];
                char value = card[1];
                
                switch (value)
                {
                    case 'a':
                        c.Value = 14;
                        break;
                    case '2':
                        c.Value = 2;
                        break;
                    case '3':
                        c.Value = 3;
                        break;
                    case '4':
                        c.Value = 4;
                        break;
                    case '5':
                        c.Value = 5;
                        break;
                    case '6':
                        c.Value = 6;
                        break;
                    case '7':
                        c.Value = 7;
                        break;
                    case '8':
                        c.Value = 8;
                        break;
                    case '9':
                        c.Value = 9;
                        break;
                    case 't':
                        c.Value = 10;
                        break;
                    case 'j':
                        c.Value = 11;
                        break;
                    case 'q':
                        c.Value = 12;
                        break;
                    case 'k':
                        c.Value = 13;
                        break;

                    default:
                        break;
                }

                switch (suit)
                {
                    case 'c':
                        c.Suit = "Clubs";
                        break;
                    case 'd':
                        c.Suit = "Diamonds";
                        break;
                    case 'h':
                        c.Suit = "Hearts";
                        break;
                    case 's':
                        c.Suit = "Spades";
                        break;
                    default:
                        break;
                }

                switch (c.Value)
                {
                    case 11:
                        c.Name = "Jack" + " of " + c.Suit;
                        break;
                    case 12:
                        c.Name = "Queen" + " of " + c.Suit;
                        break;
                    case 13:
                        c.Name = "King" + " of " + c.Suit;
                        break;
                    case 14:
                        c.Name = "Ace" + " of " + c.Suit;
                        break;
                    default:
                        c.Name = c.Value.ToString() + " of " + c.Suit;
                        break;
                }
                
                d.Deck52[i] = c;
                i++;
            }
            return d;
        }
        public void PrintDeck(Deck d)
        {
            foreach (Card c in d.Deck52)
            {
                Console.WriteLine(c.Name);
            }
        }
    }
}

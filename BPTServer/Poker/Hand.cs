using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    public class Hand
    {
        public Card GivenCardOne { get; set; }
        public Card GivenCardTwo { get; set; }

        public Card TableCardOne { get; set; }
        public Card TableCardTwo { get; set; }
        public Card TableCardThree { get; set; }

        public string NameOfHand { get; set; }
        public int HandsValue { get; set; }

        public Card[] ListHand { get; set; } = new Card[5];

        public Hand CompleteHand(Hand h)
        {
            h.ListHand[0] = h.GivenCardOne;
            h.ListHand[1] = h.GivenCardTwo;
            h.ListHand[2] = h.TableCardOne;
            h.ListHand[3] = h.TableCardTwo;
            h.ListHand[4] = h.TableCardThree;

            return h;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    public class Card
    {
        public Card[] Deck { get; set; }
        public int Value { get; set; }
        public string Suit { get; set; }            
        public string Name { get; set; }

    }
}

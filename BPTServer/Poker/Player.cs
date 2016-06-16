using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Player
    {
        public string PlayerName { get; set; }
        public int Chips { get; set; }
        public bool IsDealer { get; set; }
        public bool IsSmallBlind { get; set; }
        public bool IsBigBlind { get; set; }
        public int TableSeatNumber { get; set; }

        public static List<Player> players = new List<Player>();

        public Player(string playerName, int chips, int tableSeatNumber)
        {
            this.PlayerName = playerName;
            this.Chips = chips;
            this.TableSeatNumber = tableSeatNumber;
        }

        public void AddPlayer(Player p)
        {
            players.Add(p);
        }
    }
}

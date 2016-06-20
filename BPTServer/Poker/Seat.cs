using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Seat
    {
        public int SeatNumber { get; set; }
        public User SeatedUser { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsOpen { get; set; }
        public bool IsReadyToStart { get; set; }

        public Seat(int seatNumber, User seatedUser, bool isOccupied, bool isOpen)
        {
            this.SeatNumber = seatNumber;
            this.SeatedUser = seatedUser;
            this.IsOccupied = isOccupied;
            this.IsOpen = isOpen;
        }

    }
}

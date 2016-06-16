using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class TableSeat
    {
        public int SeatNumber { get; set; }
        public Player SeatedPlayer { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsOpen { get; set; }

        public static TableSeat[] tableTwo = new TableSeat[2];
        public static TableSeat[] tableSix = new TableSeat[6];
        public static TableSeat[] tableNine = new TableSeat[9];


        public static void PlayerTakeSeat(int seatNumber, Player player, int table)
        {
            TableSeat seat = new TableSeat();
            seat.SeatNumber = seatNumber;
            seat.SeatedPlayer = player;
            seat.IsOccupied = true;
            seat.IsOpen = false;

            switch (table)
            {
                case 2:
                    tableTwo[seatNumber] = seat;
                    break;
                case 6:
                    tableSix[seatNumber] = seat;
                    break;

                case 9:
                    tableNine[seatNumber] = seat;
                    break;

                default:
                    break;
            }

        }

        public static void FillTableWithSeats(int size)
        {
            for (int i = 0; i < size; i++)
            {
                TableSeat seat = new TableSeat();
                seat.SeatNumber = i;
                seat.IsOccupied = false;
                seat.IsOpen = true;

                if (size == 2)
                {
                    tableSix[i] = seat;
                }
                else if (size == 6)
                {
                    tableSix[i] = seat;
                }
                else if (size == 9)
                {
                    tableNine[i] = seat;
                }
            }
        }
    }
}

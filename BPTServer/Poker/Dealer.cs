using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Dealer
    {

      
        public static string[] shuffledDeck = new string[52];

        public static void ShuffleDeck()    //Shuffles deck.
        {



            string[] tempDeck = new string[52];
            tempDeck = LoadNewDeck();
            Random r = new Random();
            for (int i = 0; i < 52; i++)
            {
                int number = r.Next(shuffledDeck.Length);
                shuffledDeck[i] = tempDeck[number];
                tempDeck = SlimArray(tempDeck, number);
            }

            
        }

        public static string[] LoadNewDeck()
        {
            string[] deck = {
                            "ca","c2","c3","c4","c5","c6","c7","c8","c9","ct","cj","cq","ck",
                            "da","d2","d3","d4","d5","d6","d7","d8","d9","dt","dj","dq","dk",
                            "ha","h2","h3","h4","h5","h6","h7","h8","h9","ht","hj","hq","hk",
                            "sa","s2","s3","s4","s5","s6","s7","s8","s9","st","sj","sq","sk"};

            return deck;
        }

        //public static void DealCardsPlayers()
        //{
        //    Table.PlaceMarkers();
        //    Player.SetPlayerBlinds();
        //    Player.PayBlinds();

        //    int pos = 0;
        //    for (int i = 0; i < Player.players.Length; i++)
        //    {
        //        if (Player.players[i].IsDealer == true)
        //        {
        //            pos = Player.players[i].TablePosition - 1;
        //        }
        //    }

        //    for (int i = 0; i < Player.players.Length; i++)     //Deal first card to every player.
        //    {

        //        if (Player.players[i].HaveFolded != true)
        //        {
        //            if (pos < 0) pos = Player.players.Length - 1;
        //            string firstCard = "";
        //            firstCard = dealersDeck[dealersDeck.Length - 1].ToString();
        //            dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);
        //            Player.players[pos].FirstCard = firstCard;

        //            pos = pos - 1;
        //        }




        //    }

        //    for (int i = 0; i < Player.players.Length; i++)     //Deal second card to every player.
        //    {
        //        if (Player.players[i].HaveFolded != true)
        //        {
        //            if (pos < 0) pos = Player.players.Length - 1;

        //            string secondCard = "";
        //            secondCard = dealersDeck[dealersDeck.Length - 1].ToString();
        //            dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);
        //            Player.players[pos].SecondCard = secondCard;
        //            pos = pos - 1;
        //        }

        //    }
        //    Table.StartingPlayer(); //Setting starting player to Table.PlayerTurn
        //    Player.players[Table.PlayersTurn].IsPlayerTurn = true;




        //}

        //public static void DealFlop()
        //{
        //    FormStart fs = new FormStart();

        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1); //Removes one card
        //    Table.FlopFirst = dealersDeck[dealersDeck.Length - 1];
        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);
        //    Table.FlopSecond = dealersDeck[dealersDeck.Length - 1];
        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);
        //    Table.FlopThird = dealersDeck[dealersDeck.Length - 1];
        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);
        //    fs.ShowFlop();
        //}
        //public static void DealTurn()
        //{
        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1); //Removes one card
        //    Table.Turn = dealersDeck[dealersDeck.Length - 1];
        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);


        //}

        //public static void DealRiver()
        //{
        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);
        //    Table.River = dealersDeck[dealersDeck.Length - 1];
        //    dealersDeck = SlimArray(dealersDeck, dealersDeck.Length - 1);


        //}

        //public static void ChoseDealer()
        //{
        //    Random r = new Random();
        //    int rand = r.Next(Player.players.Length);
        //    Player.players[rand].IsDealer = true;

        //}


        public static string[] SlimArray(string[] array, int index)
        {
            string[] newArray = new string[array.Length - 1];
            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }
            for (int i = index + 1; i < array.Length; i++)
            {
                newArray[i - 1] = array[i];

            }
            return newArray;
        }

    }
}

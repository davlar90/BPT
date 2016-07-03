using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using BPTServer.Poker;

namespace BPTServer.Networking
{
    class Server
    {
        public static Hashtable connectedUsers = new Hashtable(30);
        public static Hashtable connections = new Hashtable(30);
        private IPAddress ipAddress;
        private TcpClient tcpClient;

        public Server(IPAddress ip)
        {
            ipAddress = ip;
        }

        private Thread threadListener;
        private TcpListener tcpListenerClient;
        bool ServerRunning = false;

        public static void AddUser(TcpClient tcpUser, string userName)
        {
            connectedUsers.Add(userName, tcpUser);
            connections.Add(tcpUser, userName);
            Console.WriteLine("Connected clients: " + connectedUsers.Count.ToString());
            
        }

        public static void SendBroadcast(string message)
        {
            StreamWriter swSenderSender;
            TcpClient[] tcpClients = new TcpClient[Server.connectedUsers.Count];

            Server.connectedUsers.Values.CopyTo(tcpClients, 0);

            for (int i = 0; i < tcpClients.Length; i++)
            {
                try
                {
                    if (message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine(message);
                    swSenderSender.Flush();
                    swSenderSender = null;

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message, " Removed user: " + tcpClients[i].ToString());

                    RemoveUser(tcpClients[i]);

                }
            }
        }
        public static void Whisper(string from, string to, string message)
        {
            StreamWriter swSenderSender;
            TcpClient[] tcpClients = new TcpClient[Server.connectedUsers.Count];

            connectedUsers.Values.CopyTo(tcpClients, 0);
            int i = -1;
            try
            {


                foreach (string userName in connectedUsers.Keys)
                {
                    i++;
                    if (to == userName)
                    {
                        swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                        swSenderSender.WriteLine("cmdFromServerChatWhisper¤" + from + "¤ whispers: ¤" + message);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message, " Removed user: " + connections[i].ToString());

                RemoveUser(tcpClients[i]);
            }



        }

        public static void SendCommandAllClients(string command)
        {
            StreamWriter swSenderSender;

            TcpClient[] tcpClients = new TcpClient[Server.connectedUsers.Count];
            Server.connectedUsers.Values.CopyTo(tcpClients, 0);
            //command += "¤";
            //string sendString = "";
            //string[] splitted = command.Split('¤');
            // int tempConnPlayers = connectedUsers.Count - 1;

            for (int i = 0; i < tcpClients.Length; i++)
            {
                try
                {
                    if (command.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    else
                    {
                        
                        //switch (splitted[0])
                        //{
                        //    case "cmdFromNewPlayer":
                        //        sendString = "cmdFromNewPlayer";
                        //        break;

                        //    case "cmdUserDisconnected":
                        //        sendString = "cmdUserDisconnected¤" + splitted[1];
                        //        break;

                        //    case "cmdFromServerNewTableAddedSixSeats":
                        //        sendString = "cmdFromServerNewTableAddedSixSeats¤" + splitted[1] +
                        //            "¤" + splitted[2];
                        //        break;

                        //    case "cmdFromServerUpdateTable":
                        //        sendString = "cmdFromServerUpdateTable¤" + splitted[1] + "¤" + splitted[2] +
                        //           "¤" + splitted[3];
                        //        break;


                        //    default:
                        //        break;
                        //}

                        swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                        swSenderSender.WriteLine(command);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message, " Removed user: " + connectedUsers[i].ToString());

                    RemoveUser(tcpClients[i]);
                }
            }

        }
        public static void SendDataToSingleClient(string to, string data)
        {
            StreamWriter swSenderSender;

            TcpClient[] tcpClients = new TcpClient[Server.connectedUsers.Count];
            Server.connectedUsers.Values.CopyTo(tcpClients, 0);
            int i = -1;


            foreach (string user in connectedUsers.Keys)
            {
                i++;
                if (user == to)
                {
                    try
                    {
                        if (data.Trim() == "" || tcpClients[i] == null)
                        {
                            continue;
                        }

                        swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                        swSenderSender.WriteLine(data);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message, " Removed user: " + connectedUsers[i].ToString());

                        RemoveUser(tcpClients[i]);
                    }
                }
            }

            
                
            
        }
        public static void SendMessage(string from, string message)
        {
            StreamWriter swSenderSender;

            TcpClient[] tcpClients = new TcpClient[Server.connectedUsers.Count];
            Server.connectedUsers.Values.CopyTo(tcpClients, 0);





            for (int i = 0; i < tcpClients.Length; i++)
            {
                try
                {
                    if (message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine("cmdFromServerChatAll¤" + from + "¤ says: ¤" + message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message, " Removed user: " + connectedUsers[i].ToString());

                    RemoveUser(tcpClients[i]);
                }
            }

        }

        public void StartListening()
        {
            IPAddress ipA = ipAddress;
            tcpListenerClient = new TcpListener(ipA, 19777);
            tcpListenerClient.Start();

            ServerRunning = true;

            threadListener = new Thread(KeepListening); ;
            threadListener.Start();
        }

        private void KeepListening()
        {
            while (ServerRunning == true)
            {
                tcpClient = tcpListenerClient.AcceptTcpClient();

                Connection newConnection = new Connection(tcpClient);
            }
        }

        public static void RemoveUser(TcpClient tcpUser)
        {
            if (connections[tcpUser] != null)
            {

                string name = connections[tcpUser].ToString();
                    
                Console.WriteLine(name + " disconnected");
                Server.connectedUsers.Remove(Server.connections[tcpUser]);
                Server.connections.Remove(tcpUser);

                SendCommandAllClients("cmdUserDisconnected¤" + name);
                Console.WriteLine("Connected clients: " + connectedUsers.Count.ToString());
            }
        }

        public static void Move()
        {
            StreamWriter swSenderSender;
            TcpClient[] tcpClients = new TcpClient[Server.connectedUsers.Count];

            connectedUsers.Values.CopyTo(tcpClients, 0);
            for (int i = 0; i < connectedUsers.Count; i++)
            {
                swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                swSenderSender.WriteLine("NEWMOVE");
                swSenderSender.Flush();
                swSenderSender = null;
            }
        }

        public static void StartGame(int tableID)
        {
            int numberOfPlayers = 0;
            foreach (Seat seat in Table.tables[tableID].Seats)
            {
                if (seat.IsOccupied)
                {
                    numberOfPlayers++;
                }
            }
            if (numberOfPlayers > 1)
            {
                Table.tables[tableID].NumberOfPlayers = numberOfPlayers;
                Dealer d = new Dealer();
                d.NewDealer(tableID);
                Dealer.dealers[tableID].DealNewHand();
                for (int i = 0; i < Table.tables[tableID].TableSize; i++)
                {
                    if (Table.tables[tableID].Seats[i].IsOccupied)
                    {
                        SendDataToSingleClient(Table.tables[tableID].Seats[i].SeatedUser.UserName, String.Format("cmdFromServerGivePlayerCards¤{0}¤{1}¤{2}"
                           , tableID , Table.tables[tableID].Seats[i].SeatedUser.PlayerHand.GivenCardOne.Name, Table.tables[tableID].Seats[i].SeatedUser.PlayerHand.GivenCardTwo.Name));
                    }
                }
            }
            else
            {
                Console.WriteLine("Can't start a game with less than 2 players");
            }
        }


    }
}

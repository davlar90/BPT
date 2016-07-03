using BPTServer.Poker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BPTServer.Networking
{
    class Connection
    {

        TcpClient tcpClient;

        private Thread threadSender;
        private StreamReader srRecevier;
        private StreamWriter swSender;
        private string currentUser;
        private string stringResponse;


        public Connection(TcpClient tcpClientConnection)
        {
            tcpClient = tcpClientConnection;
            threadSender = new Thread(AcceptClient);

            threadSender.Start();
        }

        public void CloseConnection()
        {
            tcpClient.Close();
            srRecevier.Close();
            swSender.Close();

        }

        public void AcceptClient()
        {
            srRecevier = new StreamReader(tcpClient.GetStream());
            swSender = new StreamWriter(tcpClient.GetStream());

            currentUser = srRecevier.ReadLine();

            if (currentUser != "")
            {
                if (Server.connectedUsers.Contains(currentUser) == true)
                {
                    swSender.WriteLine("0|This username already exists");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else if (currentUser == "Administrator")
                {
                    swSender.WriteLine("0|This username is reserved.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else
                {
                    swSender.WriteLine("1");
                    swSender.Flush();
                    Server.AddUser(tcpClient, currentUser);
                    User u = new User(currentUser, currentUser);
                    u.AddUser(u);
                    
                }

            }
            else
            {
                CloseConnection();
                return;
            }

            try
            {
                while ((stringResponse = srRecevier.ReadLine()) != "")
                {
                    if (stringResponse == null)
                    {
                        Console.WriteLine("StringResponse is null");
                        Server.RemoveUser(tcpClient);
                        break;     
                    }
                    else if (stringResponse.StartsWith("."))
                    {
                        string str = stringResponse.Substring(0, stringResponse.IndexOf("¤"));
                        switch (str)
                        {
                            case ".w":
                                string[] splitted = stringResponse.Split('¤');
                                Server.Whisper(currentUser, splitted[2], splitted[3]);
                                break;

                            default:
                                break;
                        }
                    }
                    else if ((stringResponse.StartsWith("cmd")) && (!stringResponse.Contains("¤")))
                    {
                        switch (stringResponse)
                        {
                            case "cmdNewPlayer":
                                    Server.SendCommandAllClients("cmdFromNewPlayer");
                                break;

                           

                            case "cmdRequestPlayerList":
                                foreach (string user in Server.connectedUsers.Keys)
                                {
                                    Server.SendDataToSingleClient(currentUser, "cmdFromServerShowPlayers¤" + user);
                                }
                                break;
                            case "cmdRequestTableList":
                                if (Table.tables.Count != 0)
                                {
                                    foreach (Table table in Table.tables)
                                    {
                                        Server.SendDataToSingleClient(currentUser, "cmdFromServerActiveTables¤" +
                                           table.Host.UserName + "¤" + table.TableSize.ToString() + "¤" + 
                                           table.TableID.ToString());
                                    }
                                }

                                break;

                            default:
                                break;
                        }
                    }
                    else if ((stringResponse.StartsWith("cmd")) && (stringResponse.Contains("¤")))
                    {
                        string[] splitted = stringResponse.Split('¤');
                        switch (splitted[0])
                        {
                            case "cmdNewTableSixSeats":
                                User u = User.GetUser(splitted[1]);
                                Table t = new Table(u, 6);
                                t.AddTableToList(t);
                                Server.SendCommandAllClients("cmdFromServerNewTableAddedSixSeats¤" + splitted[1] +
                                    "¤" + t.TableID.ToString());
                                Console.WriteLine("Active Tables: " + Table.tables.Count.ToString()
                                    + " " + t.Seats[0].SeatedUser.UserName);
                                break;

                            case "cmdSit":
                                int seatPos = int.Parse(splitted[3]);
                                User userJoin = User.GetUser(splitted[1]);
                                int joinTableID = int.Parse(splitted[2]);

                                Table.tables[joinTableID].Seats[seatPos].SeatedUser = userJoin;
                                Table.tables[joinTableID].Seats[seatPos].IsOccupied = true;


                                //Now tell all connected clients to add user to seat in table with table ID.
                                Server.SendCommandAllClients("cmdNewPlayerJoinedTable¤" + splitted[2] + "¤" +
                                                seatPos.ToString() + "¤" + userJoin.UserName + "¤update");
                                break;

                            case "cmdGetSeatedPlayers":
                                
                                for (int i = 0; i < Table.tables[int.Parse(splitted[1])].TableSize; i++)
                                {
                                    if (Table.tables[int.Parse(splitted[1])].Seats[i].IsOccupied)
                                    {
                                          Server.SendDataToSingleClient(currentUser, "cmdFromServerGetSeatedPlayers¤" + splitted[1] + "¤" + i.ToString() + "¤" +
                                              Table.tables[int.Parse(splitted[1])].Seats[i].SeatedUser.UserName);
                                    }
                                }



                                break;

                            case "cmdChatAll":
                                Server.SendMessage(currentUser, splitted[1]);
                                break;

                            case "cmdChatWhisper":
                                Server.Whisper(currentUser, splitted[1], splitted[2]);
                                break;

                            case "cmdSyncTables":
                                int occupiedSeats = 0;
                                foreach (Seat seat in Table.tables[int.Parse(splitted[1])].Seats)
                                {
                                 
                                    if (seat.IsOccupied)
                                    {
                                        Server.SendDataToSingleClient(currentUser, "cmdNewPlayerJoinedTable¤" +
                                        splitted[1] + "¤" + seat.SeatNumber + "¤" + seat.SeatedUser.UserName);
                                        occupiedSeats++;
                                    }
                                   
                                }
                                if (occupiedSeats == 0)
                                {
                                    Console.WriteLine("No taken seats");
                                }
                                break;

                            case "cmdIsUserReadyToStart":
                                if (splitted[1] == "yes")
                                {
                                    Table.tables[int.Parse(splitted[2])].Seats[int.Parse(splitted[3])].IsReadyToStart = true;
                                    Console.WriteLine("Player: " + Table.tables[(int.Parse(splitted[2]))].Seats[int.Parse(
                                             splitted[3])].SeatedUser.UserName +
                                             " on table " + Table.tables[int.Parse(splitted[2])].TableID + " is ready to start");

                                }
                                else if (splitted[1] == "no")
                                {
                                    Table.tables[int.Parse(splitted[2])].Seats[int.Parse(splitted[3])].IsReadyToStart = false;
                                    Console.WriteLine("Player: " + Table.tables[(int.Parse(splitted[2]))].Seats[int.Parse(
                                             splitted[3])].SeatedUser.UserName +
                                             " on table " + Table.tables[int.Parse(splitted[2])].TableID + " is NOT ready to start");
                                }

                                break;

                            case "cmdTryStartGame":
                                int tableID = int.Parse(splitted[1]);

                                int numberOfReadys = 0;
                                int numberOfPlayers = 0;
                                List<string> notReadyPlayers = new List<string>();

                                foreach (Seat seat in Table.tables[tableID].Seats)
                                {
                                    if (seat.IsOccupied)
                                    {
                                        if (seat.IsReadyToStart)
                                        {
                                            numberOfReadys++;
                                        }
                                        else
                                        {
                                            notReadyPlayers.Add(seat.SeatedUser.UserName);
                                        }
                                        numberOfPlayers++;
                                    }
                                }

                                if (numberOfPlayers == numberOfReadys)
                                {
                                    Server.SendCommandAllClients("cmdFromServerStartGame¤" + tableID);

                                    Server.StartGame(tableID);
                                }
                                else
                                {
                                    string playersNotReady = "";
                                    foreach (string player in notReadyPlayers)
                                    {
                                        playersNotReady += ("¤" + player);
                                    }
                                    if (notReadyPlayers.Count == 0)
                                    {
                                        playersNotReady = "¤";
                                    }
                                    Server.SendCommandAllClients("cmdFromServerPlayersNotReady¤" +
                                        tableID + playersNotReady);
                                }
                                break;
                                
                            case "cmdGetThisTableInfo":
                                Table tempTable = Table.tables[int.Parse(splitted[1])];
                                string response = "";
                                    int avaibleSeats = 0;
                                    foreach (Seat seat in tempTable.Seats)
                                    {
                                        if (seat.IsOccupied)
                                        { 

                                            if (seat.IsOccupied) avaibleSeats++;
                                        }
                                    }
                                    response = "cmdFromServerGetThisTableInfo¤" + tempTable.Host.UserName +
                                    "¤" + avaibleSeats + "¤" + tempTable.Seats.Count() + "¤" + splitted[1];
                                    Server.SendDataToSingleClient(currentUser, response);

                                
                                    
                                

                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Removed user: " + currentUser);

                Server.RemoveUser(tcpClient);

            }



        }
       

    }
}

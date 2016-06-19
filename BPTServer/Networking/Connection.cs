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
                        break;      // TEST BREAK I ADDED, DONT KNOW IF THIS IS GOOD OR NOT YET.
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
                                    Server.SendCommandAllClients(stringResponse);
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
                                Console.WriteLine("Active Tables: " + Table.tables.Count.ToString());
                                break;

                            case "cmdJoinTable":
                                int i = 0; // Seat Pos.
                                User userJoin = User.GetUser(splitted[1]);
                                int joinTableID = int.Parse(splitted[2]);
                                Table joinTable = Table.GetTable(joinTableID);

                                foreach (Seat seat in joinTable.Seats)
                                {
                                    if (!seat.IsOccupied)
                                    {
                                        seat.SeatedUser = userJoin;
                                        seat.IsOccupied = true;
                                        Table.tables[joinTableID].Seats[i] = seat;

                                        //Now tell all connected clients to add user to seat in table with table ID.
                                        Server.SendCommandAllClients("cmdFromServerUpdateTable¤" + splitted[2] + "¤" +
                                                i.ToString() + "¤" + userJoin.UserName);
                                        break;
                                    }
                                    i++;
                                }
                                
                                break;

                            case "cmdChatAll":
                                Server.SendMessage(currentUser, splitted[1]);
                                break;

                            case "cmdChatWhisper":
                                Server.Whisper(currentUser, splitted[1], splitted[2]);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, " Removed user: " + tcpClient.ToString());

                Server.RemoveUser(tcpClient);

            }



        }
       

    }
}

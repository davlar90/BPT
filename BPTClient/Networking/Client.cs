using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPTClient.Networking
{
    public class Client
    {



        public static List<Client> listClients = new List<Client>();
        
        private static string UserName = "Unknown";
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;
        private Thread thrMessaging;
        private IPAddress ipAddr;
        public bool Connected;
        public int counterServerCommandsRecevied = -1;

        public int tableDataCounter = 0;

        public void Connect(string ip, int port)
        {
            if (Connected == false)
            {
                initializeConnection(ip, port);
            }
            else
            {
                string disConnect = UserName + " requested to close conn.";
                CloseConnection(disConnect);
            }

        }
        private void initializeConnection(string ip, int port)
        {


            tcpServer = new TcpClient();

            UserName = User.Users[0].UserName;

            ipAddr = IPAddress.Parse(ip);
            tcpServer.Connect(ipAddr, port);

            Connected = true;
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(UserName);
            swSender.Flush();

            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();

        }



        private void ReceiveMessages()
        {

            srReceiver = new StreamReader(tcpServer.GetStream());
          
            string ConResponse = srReceiver.ReadLine();

            if (ConResponse[0] == '1')
            {

                frmMain fm = frmMain.listFrmMain[0];
                fm.SetStateConnected();
                fm.toolTipBar.Text = " Connected as: " + UserName;
                fm.ChangeColor();
                
                SendMessage("cmdNewPlayer");

            }
            
            else 
            {
                string Reason = "Not Connected: ";
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                Console.WriteLine(Reason);
                return;
            }
            
            while (Connected)
            {
                try
                {
                    string strFromServer = srReceiver.ReadLine();
                    counterServerCommandsRecevied++;
                    frmMain fm = frmMain.listFrmMain[0];

                    string[] splitted = strFromServer.Split('¤');

                    // Command Simple:
                    if ((strFromServer.StartsWith("cmd")) && (!strFromServer.Contains("¤")))
                    {
                        //Command
                        switch (strFromServer)
                        {
                            case "cmdFromNewPlayer":
                                fm.ClearShowPlayers();
                                SendMessage("cmdRequestPlayerList");
                                break;

                            default:
                                break;
                        }
                    }
                    //Command with data
                    else if ((strFromServer.StartsWith("cmd")) && (strFromServer.Contains("¤")))
                    {
                        switch (splitted[0])
                        {
                            case "cmdFromServerStartGame":
                                frmMain.frmTables[int.Parse(splitted[1])].DelAppendToChat("Starting game...");
                                break;
                                
                            case "cmdUserDisconnected":
                                fm.RemovePlayerFromList(splitted[1]);
                                break;

                            case "cmdFromServerNewTableAddedSixSeats":

                                // All clients adds table to list.
                                int tempTableID = int.Parse(splitted[2]);
                                User u = User.GetUser(splitted[1]);
                                Table t = new Table(u, 6, tempTableID);
                                t.AddTableToList(t);
                                fm.UpDateTableList();
                                break;

                            case "cmdFromServerActiveTables":
                                User tempUser = User.GetUser(splitted[1]);
                                Table tempTable = new Table(tempUser, int.Parse(splitted[2]), int.Parse(splitted[3]));
                                bool tableAlreadyInList = false; //Check is table already exists.
                                if (Table.tables.Count > 0)
                                {
                                    for (int i = 0; i < Table.tables.Count; i++)
                                    {
                                        if (Table.tables[i].TableID == int.Parse(splitted[3]))
                                        {
                                            tableAlreadyInList = true;
                                        }
                                    }
                                }

                                if (tableAlreadyInList == false)
                                {
                                     tempTable.AddTableToList(tempTable); 
                                }

                                fm.UpDateTableList();
                                break;

                            case "cmdNewPlayerJoinedTable":
                                    int tableNr = int.Parse(splitted[1]);
                                    int seatNr = int.Parse(splitted[2]);
                                    User userJoinTable = User.GetUser(splitted[3]);

                                    Table.tables[tableNr].Seats[seatNr].SeatedUser = userJoinTable;
                                    Table.tables[tableNr].Seats[seatNr].IsOccupied = true;
                                if (splitted[4] != null)
                                {
                                    if (splitted[4] == "update")
                                    {
                                        frmMain.frmTables[tableNr].DelUpdateTables();
                                        frmMain.frmTables[tableNr].DelAppendToChat(splitted[3] + " joined.");
                                    }
                                }

                                break;

                            case "cmdFromServerShowPlayers":
                                fm.ShowPlayers(splitted[1]);
                                if (!User.Users.Contains(User.GetUser(splitted[1])))
                                {
                                    User newUser = new User(splitted[1], "");
                                    newUser.AddUser(newUser);

                                    SendMessage("cmdRequestTableList");
                                }


                                break;

                            case "cmdFromServerChatAll":
                                fm.AppendTextBoxChat(splitted[1] + splitted[2] + splitted[3]);
                                break;

                            case "cmdFromServerChatWhisper":
                                fm.AppendTextBoxChat(splitted[1] + splitted[2] + splitted[3]);
                                break;

                            case "cmdFromServerPlayersNotReady":
                                int temp = 0;
                                string tempPlayers = "";
                                foreach (string player in splitted)
                                {
                                    if (temp > 1)
                                    {
                                        tempPlayers += (" [" + player + "]");
                                    }
                                    temp++;
                                }
                                frmMain.frmTables[int.Parse(splitted[1])].DelAppendToChat(
                                    "The host tried to start the game but " + tempPlayers + " is not ready.");
                                break;

                            case "cmdFromServerGetThisTableInfo":
                               
                                    string tableInfo = String.Format("Host: {0} | Slots ({1}/{2})", splitted[1],
                                                     splitted[2], splitted[3]);
                                    fm.TableInfolblTableInfo(tableInfo);
                                break;

                            default:
                                break;
                        }
                    }

                }
                catch 
                {

                    
                }

            }
        }
        public void SendMessage(string message)
        {
            //Cant be empty.
            if (message != "")
            {
                swSender.WriteLine(message);
                swSender.Flush();

            }
            
                

        }
        public void CloseConnection(string Reason)
        {

            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }
    }
}

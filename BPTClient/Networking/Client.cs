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
                
                frmMain fm = new frmMain();
                //fm = frmMain.listFrmMain[0];
                // update label1.Text in frmMain to "connected"

                fm = frmMain.listFrmMain[0];

                fm.toolTipBar.Text = " Connected";
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
                    frmMain fm = new frmMain();
                    fm = frmMain.listFrmMain[0];

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
                            case "cmdUserDisconnected":
                                fm.RemovePlayerFromList(splitted[1]);
                                break;
                            default:
                                break;
                        }
                    }
                    else if (strFromServer.StartsWith("@"))
                    {
                        string user = strFromServer.Substring(1, strFromServer.Length - 1);
                        fm.ShowPlayers(user);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void SendMessage(string message)
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

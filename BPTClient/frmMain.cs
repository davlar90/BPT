using BPTClient.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPTClient
{
    public partial class frmMain : Form
    {
        public static List<frmMain> listFrmMain = new List<frmMain>();
        public static frmTable[] frmTables = new frmTable[30];
        public static bool[] IsTableFull = new bool[30]; //adds full tables to this list.

        public delegate void delSetValue(string value);
        public delegate void delNoValue();

        public void DelSetFullTable(string index)
        {

            if (this.InvokeRequired) this.Invoke(new delSetValue(DelSetFullTable), index);
            else
            {
                IsTableFull[int.Parse(index)] = true;
            }
        }
        public void SetStateConnected()
        {

            if (this.InvokeRequired) this.Invoke(new delNoValue(SetStateConnected));
            else
            {
                this.btnNewTable.Enabled = true;
                this.btnSendChat.Enabled = true;
            }
        }
        public void ChangeColor()
        {

            if (this.InvokeRequired) this.Invoke(new delNoValue(ChangeColor));
            else
            {

                this.toolTipBar.ForeColor = Color.Green;
            }
        }
        public void RemovePlayerFromList(string value)
        {

            if (this.InvokeRequired) this.Invoke(new delSetValue(RemovePlayerFromList), value);
            else
            {
                for (int i = 0; i < this.listBoxConnectedPlayers.Items.Count; i++)
                {
                    if (this.listBoxConnectedPlayers.Items[i].ToString().Contains(value))
                    {
                        this.listBoxConnectedPlayers.Items.RemoveAt(i);
                    }
                }

            }
        }
        public void UpDateTableList()
        {

            if (this.InvokeRequired) this.Invoke(new delNoValue(UpDateTableList));
            else
            {
                this.listBoxTables.Items.Clear();
                foreach (Table table in Table.tables)
                {
                    this.listBoxTables.Items.Add(table.Host.UserName + "'s table");
                }
                
            }
        }
        public void AppendTextBoxChat(string value)
        {

            if (this.InvokeRequired) this.Invoke(new delSetValue(AppendTextBoxChat), value);
            else
            {
                this.tbLobbyChat.AppendText(value + "\r\n"); 
               
            }
        }
        public void TableInfolblTableInfo(string value)
        {

            if (this.InvokeRequired) this.Invoke(new delSetValue(TableInfolblTableInfo), value);
            else
            {

                this.lblTableInfo.Text = value;
            }
        }
        public void ShowPlayers(string value)
        {

            if (this.InvokeRequired) this.Invoke(new delSetValue(ShowPlayers), value);
            else
            {
                
                this.listBoxConnectedPlayers.Items.Add(value);
            }
        }
        public void ClearShowPlayers()
        {

            if (this.InvokeRequired) this.Invoke(new delNoValue(ClearShowPlayers));
            else
            {
                this.listBoxConnectedPlayers.Items.Clear();
            }
        }


        public frmMain()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();

        }

        private void hostGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConnect fc = new frmConnect();
            fc.Show();
        }

        private void joinGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }


        private void listBoxLobbys_DoubleClick(object sender, EventArgs e)
        {
            int i = listBoxTables.SelectedIndex;
            if (i >= 0)
            {
                if (!IsTableFull[i])
                {
                    if (i >= 0)
                    {
                        DialogResult result = MessageBox.Show("Join this game?", "Join game", MessageBoxButtons.YesNo);
                        if (result.ToString() == "Yes")
                        {
                            bool alreadyAtThisTable = false;
                            foreach (Seat seat in Table.tables[listBoxTables.SelectedIndex].Seats)
                            {
                                if (seat.SeatedUser != null)
                                {
                                    if (seat.SeatedUser.UserName == User.Users[0].UserName)
                                    {
                                        alreadyAtThisTable = true;
                                    }
                                }
                            }
                            if ((listBoxTables.SelectedItem != null) && (!alreadyAtThisTable))
                            {
                                Table t = Table.tables[listBoxTables.SelectedIndex];
                                frmTable ft = new frmTable(t.TableID);
                                ft.Show();
                                frmTables[t.TableID] = ft;


                            }
                            else
                            {
                                MessageBox.Show("You're already sitting at this table.");
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Table is full.");
                }
            }


            
        }

        private void btnNewLobby_Click(object sender, EventArgs e)
        {
            var checkedButton = grpBoxNewTable.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);

            if (checkedButton == rbTwoSeats)
            {

                tbLobbyChat.Text = Table.tables.Count.ToString();
            }
            else if (checkedButton == rbSixSeats)
            {
                frmTable ft = new frmTable(Table.tables.Count);
                frmMain.frmTables[Table.tables.Count] = ft;
                
                Client.listClients[0].SendMessage("cmdNewTableSixSeats¤" + User.Users[0].UserName);

                ft.Show();

            }
            else if (checkedButton == rbNineSeats)
            {

            }
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            btnSendChat.Enabled = false;
            btnNewTable.Enabled = false;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void toolTipBar_TextChanged(object sender, EventArgs e)
        {
             
        }
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Client.listClients.Count == 1)
            Client.listClients[0].CloseConnection("");
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            if ((tbLobbyChatInput.Text != "") && (!tbLobbyChatInput.Text.Contains('¤')))
            {
                if (tbLobbyChatInput.Text[0] != '.')
                {

                    Client.listClients[0].SendMessage("cmdChatAll¤" + tbLobbyChatInput.Text.Trim());
                }
                else if (((tbLobbyChatInput.Text.StartsWith(".")) &&
                    tbLobbyChatInput.Text.Trim().Length > 1) && ((tbLobbyChatInput.Text[1] != ' ') && tbLobbyChatInput.Text[1] != '.'))
                {  //Chat commands here that starts with "." and is followed by word/command.

                    
                        string[] splitted = tbLobbyChatInput.Text.Trim().Split(' ');

                        switch (splitted[0].ToLower())
                        {
                            case ".w": //Whisper
                                int numberOfWords = splitted.Count();
                                string[] messageArray = new string[numberOfWords - 2];
                                Array.Copy(splitted, 2, messageArray, 0, numberOfWords - 2);
                                string message = "";
                                for (int i = 0; i < messageArray.Length; i++)
                                {
                                    message += messageArray[i] + " ";
                                }
                                Client.listClients[0].SendMessage("cmdChatWhisper¤" + splitted[1] + "¤" + message);
                                tbLobbyChat.AppendText("You whispered to " + splitted[1] + ": " + message + " \r\n");
                                break;

                            case ".help":
                                string[] commands = { ".help = Help", ".w = whisper", ".somecommand = something" };
                                foreach (string command in commands)
                                {
                                    tbLobbyChat.AppendText(command + " \r\n");
                                }
                                break;



                            default:
                                tbLobbyChat.AppendText("Unknown command type .help for help. \r\n");
                                break;
                        }
            


                    

                }
                else if (tbLobbyChatInput.Text.StartsWith("."))
                {
                    Client.listClients[0].SendMessage("cmdChatAll¤" + tbLobbyChatInput.Text.Trim());

                }
                else
                {
                    
                }
            }
            else
            {
                tbLobbyChat.AppendText("'¤' is an illegal character! \r\n");
            }
            tbLobbyChatInput.Clear();
        } //Chat


        private void listBoxTables_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (listBoxTables.SelectedIndex >= 0)
            {
                Client.listClients[0].SendMessage("cmdGetThisTableInfo¤" +
                    listBoxTables.SelectedIndex);
            }

        }
    }
}

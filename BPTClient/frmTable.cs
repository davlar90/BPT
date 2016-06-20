using BPTClient.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPTClient
{
    public partial class frmTable : Form
    {
        public delegate void delNoValue();
        public delegate void delSetValue(string value);

        public int TableID { get; set; }

        public frmTable(int id)
        {
            this.TableID = id;
            InitializeComponent();
        }

        private void frmTable_Load(object sender, EventArgs e)
        {

            btnStartGame.Visible = false;
            cbReady.Visible = false;

            GetTableDataNewTable();

        }

        public void AppendToStatusBox(string text)
        {
            
            string time = DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            tbStatus.AppendText(time + " " + text + "\r\n");
        }
        public void AppendToChat(string text)
        {

            tbChat.AppendText(text + "\r\n");
        }

        public void UserTakeSeat()
        {

            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button5.Enabled = false;
            this.button6.Enabled = false;
        }
        public void DelAppendToChat(string text)
        {

            if (this.InvokeRequired) this.Invoke(new delSetValue(DelAppendToChat), text);
            else
            {
                AppendToChat(text);
            }
        }
        public void DelAppendToTableLog(string text)
        {

            if (this.InvokeRequired) this.Invoke(new delSetValue(DelAppendToTableLog), text);
            else
            {
                AppendToStatusBox(text);
            }
        }
        public void DelStartingGame()
        {

            if (this.InvokeRequired) this.Invoke(new delNoValue(StartingGame));
            else
            {
                StartingGame();
            }
        }

        private void StartingGame()
        {
            btnStartGame.Visible = false;
            cbReady.Visible = false;

        }

        public void DelUpdateTables()
        {

            if (this.InvokeRequired) this.Invoke(new delNoValue(DelUpdateTables));
            else
            {
                GetTableDataNewTable();
            }
        }
        

        public void GetTableDataNewTable()
        {


            foreach (Seat seat in Table.tables[TableID].Seats)
            {
                if (seat.SeatedUser != null)
                {
                    if (User.Users[0].UserName == Table.tables[TableID].Host.UserName)
                    {
                        btnStartGame.Enabled = true;
                        btnStartGame.Visible = true;
                        cbReady.Visible = true;
                        UserTakeSeat();
                    }
                }

                if (seat.IsOccupied)
                {
                ((Button)this.Controls.Find(
                        "button" + (seat.SeatNumber + 1).ToString(), true)[0]).Enabled = false;
                ((Button)this.Controls.Find(
                         "button" + (seat.SeatNumber + 1).ToString(), true)[0]).Text = 
                         seat.SeatedUser.UserName;
                }
            }
        }

        private void PlayerSitDown(string seat)
        {
            Client.listClients[0].SendMessage("cmdSit¤" +
                    User.Users[0].UserName + "¤" + TableID + "¤" + seat);
            cbReady.Visible = true;
            UserTakeSeat();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            PlayerSitDown("1");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PlayerSitDown("2");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PlayerSitDown("3");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PlayerSitDown("4");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PlayerSitDown("5");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
        }

        private void cbShowLog_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowLog.Checked)
            {
                lblTableStatus.Visible = true;
                tbStatus.Visible = true;
            }
            else
            {
                lblTableStatus.Visible = false;
                tbStatus.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            Client.listClients[0].SendMessage("cmdTryStartGame¤" + TableID);
        }

        private void cbReady_CheckedChanged(object sender, EventArgs e)
        {
            int seatNr = -1;
            foreach (Seat seat in Table.tables[TableID].Seats)
            {
                if (seat.SeatedUser != null)
                {
                    if (seat.SeatedUser.UserName == User.Users[0].UserName)
                    {
                        seatNr = seat.SeatNumber;
                        break;
                    }
                }
            }
            if (cbReady.Checked)
            {
                Table.tables[TableID].Seats[seatNr].IsReadyToStart = true;

                Client.listClients[0].SendMessage("cmdIsUserReadyToStart¤yes¤" + TableID.ToString() +
                    "¤" + seatNr.ToString());
            }
            else
            {
                Table.tables[TableID].Seats[seatNr].IsReadyToStart = false;
                Client.listClients[0].SendMessage("cmdIsUserReadyToStart¤no¤" + TableID.ToString() +
                    "¤" + seatNr.ToString());
            }
        }
    }
}

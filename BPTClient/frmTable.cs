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
            lblUserName0.Visible = false;
            lblUserName1.Visible = false;
            lblUserName2.Visible = false;
            lblUserName3.Visible = false;
            lblUserName4.Visible = false;
            lblUserName5.Visible = false;

            countdownBar.Maximum = 100;
            countdownBar.Value = 100;
            countdownBar.Visible = false;

            btnStartGame.Visible = false;
            cbReady.Visible = false;

            GetTableDataNewTable();
        }

        public void PlayerTurn()
        {
            timerPlayerTurn.Enabled = true;
            timerPlayerTurn.Start();
            timerPlayerTurn.Interval = 100;
            timerPlayerTurn.Tick += new EventHandler(timerPlayerTurn_Tick);
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

        public void UserTakeSeat(int seatNr)
        {

            this.btnSeat0.Visible = false;
            this.btnSeat1.Visible = false;
            this.btnSeat2.Visible = false;
            this.btnSeat3.Visible = false;
            this.btnSeat4.Visible = false;
            this.btnSeat5.Visible = false;
            ((Label)this.Controls.Find(
                        "lblUserName" + (seatNr).ToString(), true)[0]).Visible = true;
            ((Label)this.Controls.Find(
                        "lblUserName" + (seatNr).ToString(), true)[0]).Text = User.Users[0].UserName;
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

        public void DelPlayerHand(string cards)
        {
            string[] splitted = cards.Split('¤');
            if (this.InvokeRequired) this.Invoke(new delSetValue(DelPlayerHand), cards);
            else
            {
                foreach (string item in splitted)
                {
                    AppendToChat("Cards: " + item);
                }
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
                    }
                }

                if (seat.IsOccupied)
                {
                ((Button)this.Controls.Find(
                       "btnSeat" + (seat.SeatNumber).ToString(), true)[0]).Enabled = false;
                    ((Label)this.Controls.Find(
                    "lblUserName" + (seat.SeatNumber).ToString(), true)[0]).Visible = true;
                    ((Label)this.Controls.Find(
                        "lblUserName" + (seat.SeatNumber).ToString(), true)[0]).Text = seat.SeatedUser.UserName;
                }
            }
        }

        private void PlayerSitDown(string seat)
        {
            Client.listClients[0].SendMessage("cmdSit¤" +
                    User.Users[0].UserName + "¤" + TableID + "¤" + seat);
            cbReady.Visible = true;
            UserTakeSeat(int.Parse(seat));
        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            
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

        private void btnSeat1_Click(object sender, EventArgs e)
        {
            PlayerSitDown("1");

        }

        private void btnSeat2_Click(object sender, EventArgs e)
        {
            PlayerSitDown("2");

        }

        private void btnSeat3_Click(object sender, EventArgs e)
        {
            PlayerSitDown("3");

        }

        private void btnSeat4_Click(object sender, EventArgs e)
        {
            PlayerSitDown("4");

        }

        internal void HostSitDown()
        {
            PlayerSitDown("0");
        }

        private void btnSeat5_Click(object sender, EventArgs e)
        {
            PlayerSitDown("5");

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void timerPlayerTurn_Tick(object sender, EventArgs e)
        {
            
            
            if (countdownBar.Value != 0)
            {

                if (countdownBar.Value == 99) countdownBar.Visible = true;
                countdownBar.Value--;
            }
            else
            {
                
                
                timerPlayerTurn.Stop();
                MessageBox.Show("Times up!");
                countdownBar.Value = 100;
                countdownBar.Visible = false;
                

            }
        }

        private void btnFold_Click(object sender, EventArgs e)
        {
            PlayerTurn();
        }
    }
}

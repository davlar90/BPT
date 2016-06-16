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
        

        public delegate void delSetValue(string value);
        public delegate void delNoValue();



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
            //Join lobby
        }

        private void btnNewLobby_Click(object sender, EventArgs e)
        {
            //CreateLobby
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void toolTipBar_TextChanged(object sender, EventArgs e)
        {
             
        }
        public void OnApplicationExit(object sender, EventArgs e)
        {

            Client.listClients[0].CloseConnection("");
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

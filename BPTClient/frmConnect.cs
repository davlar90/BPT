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
    public partial class frmConnect : Form
    {
        Client c = new Client();
        public frmConnect()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            c.Connect(tbIP.Text, int.Parse(tbPort.Text));
            Client.listClients.Add(c);
            
           this.Close();
            
        }
    }
}

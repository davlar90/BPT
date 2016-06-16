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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        public frmMain Fm { get; set; }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new User(tbUsername.Text, "");
            user.AddUser(user);
            

            this.Hide();

            frmMain fm = new frmMain();
            frmMain.listFrmMain.Add(fm);
            fm.Show();

        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {
            if (tbUsername.Text != "")
            {
                btnLogin.Enabled = true;
            }
            else
            btnLogin.Enabled = false;
        }
    }
}

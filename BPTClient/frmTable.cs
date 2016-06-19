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
    public partial class frmTable : Form
    {
        public delegate void delNoValue();

        public int TableID { get; set; }

        public frmTable(int id)
        {
            this.TableID = id;
            InitializeComponent();
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            GetTableDataNewTable();
        }
        public void UpdateTableSeats()
        {

            if (this.InvokeRequired) this.Invoke(new delNoValue(UpdateTableSeats));
            else
            {

                GetTableDataNewTable();
            }
        }
        private void GetTableDataNewTable()
        {
            foreach (Table table in Table.tables)
            {
                if (table.TableID == TableID)
                {
                    foreach (Seat seat in table.Seats)
                    {
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
            }

             


        }
    }
}

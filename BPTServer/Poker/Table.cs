using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer.Poker
{
    class Table
    {
        public static List<Table> tables = new List<Table>();

        public int TableID { get; set; }
        public User Host { get; set; }
        public int TableSize { get; set; }
        public Seat[] Seats { get; set; }



        public Table(User host, int tableSize)
        {
            this.TableID = tables.Count;
            this.Host = host;
            this.TableSize = tableSize;
            this.Seats = new Seat[tableSize];
        }
        public void AddTableToList(Table t)
        {
            for (int i = 0; i < t.TableSize; i++)
            {
                if (i == 0)
                {
                    Seat s = new Seat(i, t.Host, true, false);
                    t.Seats[i] = s;
                }
                else
                {
                    Seat s = new Seat(i, null, false, true);
                    t.Seats[i] = s;
                }
            }
            tables.Add(t);
        }
        public static Table GetTable(int tID)
        {
            Table t = null;
            foreach (Table table in Table.tables)
            {
                if (table.TableID == tID)
                {
                    t = table;
                }
            }
            return t;
        }
    }
}

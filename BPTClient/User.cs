using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTClient
{
    public class User
    {
        public static List<User> Users = new List<User>();

        private int UserID { get; set; }
        public string UserName { get; set; }
        private string Password { get; set; }

        public bool IsReadyToStart { get; set; }

        public int Chips { get; set; }
        public bool IsDealer { get; set; }
        public bool IsSmallBlind { get; set; }
        public bool IsBigBlind { get; set; }
        public int TableSeatNumber { get; set; }

        public User(string userName, string password)
        {
            this.UserID = Users.Count + 1;
            this.UserName = userName;
            this.Password = password;  //Unencrypted!!
        }

        public void AddUser(User u)
        {
            Users.Add(u);
        }
        public static User GetUser(string userName)
        {
            User u = null;
            foreach (User user in Users)
            {
                if (user.UserName == userName)
                {
                    u = user;
                }
            }
            return u;
        }
    }
}

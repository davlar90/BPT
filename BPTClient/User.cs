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
    }
}

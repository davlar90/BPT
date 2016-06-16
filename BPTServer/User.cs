using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTServer
{
    public class User
    {
        private static List<User> Users = new List<User>();

        private int UserID { get; set; }
        private string UserName { get; set; }
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

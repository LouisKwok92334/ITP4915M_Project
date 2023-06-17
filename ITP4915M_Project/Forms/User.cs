using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITP4915M_Project
{
    public class User
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string StaffName { get; set; }
        public string Role { get; set; } // Add this line

        public User(int id, string loginName, string password, string staffName, string role)
        {
            ID = id;
            LoginName = loginName;
            Password = password;
            StaffName = staffName;
            Role = role; // And this line
        }
    }


}

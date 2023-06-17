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
        // other properties...
        public User(int id, string loginName, string password, string staffName)
        {
            ID = id;
            LoginName = loginName;
            Password = password;
            StaffName = staffName;
        }

      
    }

}

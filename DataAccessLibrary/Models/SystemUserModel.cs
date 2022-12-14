using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class SystemUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Memword { get; set; }
        public string System_User_ID { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

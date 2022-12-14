using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class UserModel
    {
        //alerts
        public string AlertID { get; set; }
        public string DateCreated { get; set; }
        public string Alert_Type { get; set; }
        public string Ref_ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        //users
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        //withdrawal
        public string AccountID { get; set; }
        public string Amount { get; set; }
    }
}

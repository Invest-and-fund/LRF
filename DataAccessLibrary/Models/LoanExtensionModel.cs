using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class LoanExtensionModel
    {
        public string IsActive { get; set; }
        public string DateCreated { get; set; }
        public string DateActive { get; set; }
        public string OldRate { get; set; }
        public string LoanID { get; set; }
        public string OldBorrowerRate { get; set; }
    }
}

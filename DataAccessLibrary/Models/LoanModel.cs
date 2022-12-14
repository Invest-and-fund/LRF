using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class LoanModel
    {
        //Loan Parents
        public string Business_Name { get; set; }
        public int LoanID { get; set; }
        public int Loan_Parent_ID { get; set; }
        public int LoanType { get; set; }
        public int LoanStatus { get; set; }
        //Loan Parent
        public int HEParent { get; set; }
        public DateTime DD_Date { get; set; }

        public int MaxLoanAmount { get; set; }
        public int Facility_Amount { get; set; }
        public int Term { get; set; }
        public int BorrowerRate { get; set; }
        public int HE_loan { get; set; }
        public int SLC_LH_BALANCE { get; set; }
        public int JLC_LH_BALANCE { get; set; }
        public int SLC_TRANCHE_AMOUNT { get; set; }
        public int JLC_TRANCHE_AMOUNT { get; set; }
        public int SLC_INTEREST_PAID { get; set; }
        public int SLC_CAPITAL_REPAID { get; set; }
        public int JLC_INTEREST_PAID { get; set; }
        public int SLC_RATE { get; set; }
        public int JLC_RATE { get; set; }
        public int RLC_INTEREST_PAID { get; set; }
        public int Fixed_rate { get; set; }

        public int Totalnumunits { get; set; }

        public int PartialRepaymentAmount { get; set; }
        public int PercentageOfLoan { get; set; }

        public int OutstandingBalance { get; set; }
        public int AccruingInterest { get; set; }
        public DateTime MaturityDate { get; set; }
    }
}

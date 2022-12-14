using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class TrancheInformationCalculations
    {
        //Loan Parents
        public string Business_Name { get; set; }
        public string LoanID { get; set; }
        public string Loan_Parent_ID { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }
        //Loan Parent
        public string HEParent { get; set; }
        public string DD_Date { get; set; }
        public string MaxLoanAmount { get; set; }
        public string facility_Amount { get; set; }
        public string Term { get; set; }
        public string BorrowerRate { get; set; }
        public string he_loan { get; set; }
        public string SLC_LH_BALANCE { get; set; }
        public string JLC_LH_BALANCE { get; set; }
        public string SLC_TRANCHE_AMOUNT { get; set; }
        public string JLC_TRANCHE_AMOUNT { get; set; }
        public string SLC_INTEREST_PAID { get; set; }
        public string SLC_CAPITAL_REPAID { get; set; }
        public string JLC_INTEREST_PAID { get; set; }
        public string SLC_RATE { get; set; }
        public string JLC_RATE { get; set; }
        public string RLC_INTEREST_PAID { get; set; }
        public string fixed_rate { get; set; }

        public string totalnumunits { get; set; }
        public string Testicle { get; set; }
        public string trancheid { get; set; }
        public string tranchename { get; set; }
        public string tranchedddate { get; set; }
        public string trancheamount { get; set; }
        public string tranchelendername { get; set; }
        public string trancheaccountid { get; set; }
        public string tranchelhamount { get; set; }
        public string trancheaccountinterest { get; set; }
        public string TranchePlatformFee { get; set; }
        public string TrancheRepaymentAmount { get; set; }
        public string TrancheRepaymentInterest { get; set; }
        public string TrancheRepaymentPlatformFee { get; set; }
        public string trancheSLCamount { get; set; }
        public string SLCTotalInterestAccruing { get; set; }
        public string SLCCapitalRepaid { get; set; }
        public string SLCInterestRepaid { get; set; }
        public string trancheSLCbalance { get; set; }
        public string trancheSLCinterest { get; set; }
        public string trancheSLCrepayment { get; set; }
        public string trancheSLCrepaymentinterest { get; set; }
        public string trancheJLCamount { get; set; }
        public string JuniorTotalInterestAccruing { get; set; }
        public string JuniorCapitalRepaid { get; set; }
        public string JuniorInterestRepaid { get; set; }
        public string trancheJLCbalance { get; set; }
        public string trancheJLCinterest { get; set; }
        public string trancheJLCrepayment { get; set; }
        public string trancheJLCrepaymentinterest { get; set; }
        public string trancheresidualinterest { get; set; }
        public string trancheresidualinterestrepayment { get; set; }
    }
}

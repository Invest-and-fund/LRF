using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class LoanData : ILoanData
    {
        private readonly ISqlDataAccess _db;

        public LoanData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<LoanModel>> GetLiveLoanParents()
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select business_name, loanid  " +
                "from loans " +
                "where(loan_parent_id = loanid or loan_parent_id = 0) " +
                "and loantype > 3 " +
                "and loanstatus in (1, 2, 4, 7) " +
                "order by business_name, loanid ";

            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<LoanModel, dynamic>(sql, null);
        }

        public Task<List<LoanModel>> GetLiveLoanParent(int LoanParentID)
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select loanid, fixed_rate, BUSINESS_NAME, DD_date, maxloanamount, term, facility_amount, isnull(borrowerrate, fixed_rate) as borrowerrate," +
                "he_loan, iif(he_loan > 0, iif(he_loanid = 0, loan_parent_id, he_loanid), 0) as HE_PARENT, loantype, dd_lastdate as maturitydate " +
                "from loans " +
                "where loanid = @LoanParentID";

            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<LoanModel, dynamic>(sql, new { LoanParentID });
        }

        public Task<List<LoanModel>> GetRelatedLoans(int LoanParentID)
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select loanid, loantype, BUSINESS_NAME, fixed_rate, DD_date, maxloanamount, isnull(borrowerrate, fixed_rate) as borrowerrate,  dd_lastdate as maturitydate " +
                "from loans " +
                "where (loan_parent_id = @LoanParentID or(loan_parent_id = 0 and loanid = @LoanParentID)) " +
                "and loanstatus in (2, 4, 7) " +
                "and DD_date<GetDate() " + //this GetDate() needs to be a variable taken from a ddl 
                "order by loanid";

            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<LoanModel, dynamic>(sql, new { LoanParentID });
        }

        public Task<List<LendersModel>> GetAllLenders(int LoanParentID)
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select l.loanid, h.loan_holdings_id, b.num_units, a.accountid, concat(trim(u.lastname) , ', ' , trim(u.firstname)) as thename, b.lh_bals_id " +
                "from loan_holdings h, LH_POS_BALANCES b, accounts a, users u, loans l " +
                "where(loan_parent_id = @LoanParentID or(loan_parent_id = 0 and l.loanid = @LoanParentID)) " +
                "and loanstatus in (2, 4, 7) " +
                "and h.loanid = l.loanid " +
                "and h.loan_holdings_id = b.lh_id " +
                "and b.accountid = a.accountid " +
                "and a.accountid not in (3712) " +
                "and a.userid = u.userid " +
                "and u.usertype = 0 " +
                "union all " +
                "select l.loanid, h.loan_holdings_id, b.num_units, a.accountid, concat(trim(u.lastname), ', ', trim(u.firstname)) as thename, b.lh_bals_id "+
                "from loan_holdings h, LH_POS_BALANCES_SUSPENSE b, accounts a, users u,  loans l " +
                "where(loan_parent_id = @LoanParentID or(loan_parent_id = 0 and l.loanid = @LoanParentID)) " +
                "and loanstatus in (2, 4, 7) " +
                "and h.loanid = l.loanid " +
                "and h.loan_holdings_id = b.lh_id " +
                "and b.accountid = a.accountid " +
                "and a.accountid not in (3712) " +
                "and a.userid = u.userid " +
                "and u.usertype = 0 " +
                "order by l.loanid,  accountid ";


            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<LendersModel, dynamic>(sql, new { LoanParentID });
        }

        public Task<List<LoanModel>> GetTrancheSum(int LoanParentID)
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select loanid, sum(num_units) as totalnumunits from " +
                "(select l.loanid, h.loan_holdings_id, b.num_units, a.accountid, concat(trim(u.lastname), ', ', trim(u.firstname)) as thename " +
                "from loan_holdings h, LH_POS_BALANCES b, accounts a, users u, loans l " +
                "where(loan_parent_id = @LoanParentID or(loan_parent_id = 0 and l.loanid = @LoanParentID)) " +
                "and loanstatus in (2, 4, 7) " +
                "and h.loanid = l.loanid " +
                "and h.loan_holdings_id = b.lh_id " +
                "and b.accountid = a.accountid " +
                "and a.userid = u.userid " +
                "and a.accountid not in (3712) " +
                "and u.usertype = 0) x " +
                "group by loanid " +
                "order by loanid";

            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<LoanModel, dynamic>(sql, new { LoanParentID });
        }

        public Task<List<LoanModel>> GetHELoans(int LoanParentID)
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select l.loanid, l.SLC_LH_BALANCE,  l.JLC_LH_BALANCE, (l.SLC_LH_BALANCE + l.SLC_CAPITAL_REPAID) as SLC_TRANCHE_AMOUNT, " +
                "(l.JLC_LH_BALANCE + l.JLC_CAPITAL_REPAID) as JLC_TRANCHE_AMOUNT, l.SLC_CAPITAL_REPAID, l.JLC_CAPITAL_REPAID, l.SLC_INTEREST_PAID, l.JLC_INTEREST_PAID, " +
                "l.RLC_INTEREST_PAID, s.fixed_rate as SLC_RATE, j.fixed_rate as JLC_RATE " +
                "from LOANCO_LH_POS_BALANCES l, loans m, loans s, loans j " +
                "where m.LOAN_PARENT_ID = @LoanParentID " +
                "and l.loanid = m.loanid " +
                "and j.loanid = (l.loanid + 1) " +
                "and s.loanid = 753 " +
                "order by m.loanid ";

            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<LoanModel, dynamic>(sql, new { LoanParentID });
        }

        public Task<List<LoanExtensionModel>> GetExtensionDataSet(int LoanID, DateTime StartDate)
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select * from loan_extensions where isactive = 0 " +
                "and dateactive > @StartDate " +
                "and loanid = @LoanID " +
                "order by dateactive";

            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<LoanExtensionModel, dynamic>(sql, new { LoanID, StartDate });
        }

        public Task<int> AlterLHPosBalances(string DisplayDate)
        {
            string sql = "ALTER VIEW LH_POS_BALANCES(ACCOUNTID, LH_ID, LH_BALS_ID, NUM_UNITS) AS " +
                "SELECT t.ACCOUNTID, t.LH_ID, vt.maxlhbalsid AS LH_BALS_ID, t.NUM_UNITS " +
                "FROM " +
                "(SELECT MAX(LH_BALS_ID) AS maxlhbalsid, LHID_ACCID, LH_ID, ACCOUNTID " +
                "FROM dbo.LH_BALS AS l " +
                "WHERE(LH_ID >= 0) AND(DATECREATED < '" + DisplayDate + "') " +
                "GROUP BY LHID_ACCID, LH_ID, ACCOUNTID) AS vt INNER JOIN " +
                "dbo.LH_BALS AS t ON t.LH_BALS_ID = vt.maxlhbalsid " +
                "WHERE(t.NUM_UNITS > 0); ";

            return _db.SaveData(sql, new { DisplayDate });
        }

        public Task<int> AlterLHPosBalancesSuspense(string DisplayDate)
        {
            string sql = "ALTER VIEW LH_POS_BALANCES_SUSPENSE(ACCOUNTID, LH_ID, LH_BALS_ID, NUM_UNITS) AS " +
                  "SELECT  vt.ACCOUNTID, t.LH_ID, vt.max_lh_bals_sus_id AS LH_BALS_ID, t.NUM_UNITS " +
                   "FROM " +
                   "(SELECT  ACCOUNTID, MAX(LH_BALS_SUSPENSE_ID) AS max_lh_bals_sus_id " +
                   "FROM dbo.LH_BALS_SUSPENSE AS s " +
                   "WHERE(LH_ID > 0) AND(DATECREATED < '" + DisplayDate + "') " +
                   "GROUP BY ACCOUNTID, LH_ID) AS vt INNER JOIN " +
                   "dbo.LH_BALS_SUSPENSE AS t ON t.LH_BALS_SUSPENSE_ID = vt.max_lh_bals_sus_id " +
                    "WHERE(t.NUM_UNITS > 0); ";

            return _db.SaveData(sql, new { DisplayDate });
        }

        public Task<int> AlterLoanCoPosBalances(string DisplayDate)
        {
            string sql = "ALTER VIEW LOANCO_LH_POS_BALANCES(maxloancolhbalsid, DATECREATED, ACCOUNTID, LOANID, AMOUNT, BALANCE, SLC_LH_BALANCE, SLC_CAPITAL_REPAID, " +
                "SLC_INTEREST_PAID, JLC_LH_BALANCE, JLC_CAPITAL_REPAID, JLC_INTEREST_PAID, RLC_INTEREST_PAID) " +
                "AS " +
                "SELECT vt.maxloancolhbalsid, t.DATECREATED, t.ACCOUNTID, t.LOANID, t.AMOUNT, t.BALANCE, t.SLC_LH_BALANCE, t.SLC_CAPITAL_REPAID, " +
                "t.SLC_INTEREST_PAID, t.JLC_LH_BALANCE, t.JLC_CAPITAL_REPAID, " +
                "t.JLC_INTEREST_PAID, t.RLC_INTEREST_PAID " +
                "FROM " +
                "(SELECT LOANID, MAX(LOANCO_LH_BALS_ID) AS maxloancolhbalsid " +
                "From dbo.LOANCO_LH_BALS AS l " +
                "Where(LOANID >= 0) And(ISACTIVE = 0) AND(DATECREATED < '" + DisplayDate + "') " +
                "Group By LOANID) AS vt INNER Join " +
                "dbo.LOANCO_LH_BALS AS t ON t.LOANCO_LH_BALS_ID = vt.maxloancolhbalsid;";

            return _db.SaveData(sql, new { DisplayDate });
        }


        //CRUD SELECT
        //OUT: Type Object of class SystemUserModel
        public Task<List<SystemUserModel>> GetSystemUsers()
        {
            //SQL to pass in through our parameter when calling LoadData in the SqlDataAccess class
            string sql = "select * " +
                "from SYSTEM_USERS";

            //Call SqlDataAccess with our SystemUserModel object, sql and we define any number of parameters required to execute here using the new keyword
            return _db.LoadData<SystemUserModel, dynamic>(sql, null);
        }

        //CRUD INSERT
        //PARAMS: Takes an object of type SystemUserModel
        //OUT: Returns rows affected as int
        public Task InsertSystemUser(SystemUserModel systemUser)
        {
            string sql = "insert into SYSTEM_USERS (FIRSTNAME, USERTYPE) " +
                "values (@FirstName, @UserType, @LastName, @Username)";

            return _db.SaveData(sql, new { systemUser.FirstName, systemUser.UserType, systemUser.LastName, systemUser.UserName });
        }

        //CRUD UPDATE
        //PARMAS: Takes an object of type SystemUserModel
        //OUT: Returns rows affected as int
        public Task<int> UpdateSystemUser(SystemUserModel systemUser)
        {
            string sql = "update SYSTEM_USERS " +
                "set FIRSTNAME = @FirstName, LASTNAME =@LastName " +
                "where @System_User_ID = SYSTEM_USER_ID";

            return _db.SaveData(sql, new { systemUser.FirstName, systemUser.LastName, systemUser.System_User_ID });
        }

        //CRUD DELETE
        //PARMAS: Takes an object of type SystemUserModel
        //OUT: Returns rows affected as int
        public Task<int> DeleteSystemUser(SystemUserModel systemUser)
        {
            string sql = "delete from SYSTEM_USERS " +
                "where @System_user_id = SYSTEM_USER_ID";

            return _db.SaveData(sql, new { systemUser.System_User_ID });
        }
    }
}
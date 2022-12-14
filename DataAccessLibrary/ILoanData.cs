using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface ILoanData
    {
        Task<int> AlterLHPosBalances(string DisplayDate);
        Task<int> AlterLHPosBalancesSuspense(string DisplayDate);
        Task<int> AlterLoanCoPosBalances(string DisplayDate);
        Task<int> DeleteSystemUser(SystemUserModel systemUser);
        Task<List<LendersModel>> GetAllLenders(int LoanParentID);
        Task<List<LoanExtensionModel>> GetExtensionDataSet(int LoanID, DateTime StartDate);
        Task<List<LoanModel>> GetHELoans(int LoanParentID);
        Task<List<LoanModel>> GetLiveLoanParent(int LoanParentID);
        Task<List<LoanModel>> GetLiveLoanParents();
        Task<List<LoanModel>> GetRelatedLoans(int LoanParentID);
        Task<List<SystemUserModel>> GetSystemUsers();
        Task<List<LoanModel>> GetTrancheSum(int LoanParentID);
        Task InsertSystemUser(SystemUserModel systemUser);
        Task<int> UpdateSystemUser(SystemUserModel systemUser);
    }
}
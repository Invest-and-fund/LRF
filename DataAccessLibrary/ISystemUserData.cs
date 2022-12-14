using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface ISystemUserData
    {
        Task<int> DeleteSystemUser(SystemUserModel systemUser);
        Task<List<SystemUserModel>> GetSystemUsers();
        Task InsertSystemUser(SystemUserModel systemUser);
        Task<int> UpdateSystemUser(SystemUserModel systemUser);
    }
}
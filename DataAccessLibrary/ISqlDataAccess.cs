using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task ExecuteProcedure<T>(string sql, T parameters);
        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task<int> SaveData<T>(string sql, T parameters);
    }
}
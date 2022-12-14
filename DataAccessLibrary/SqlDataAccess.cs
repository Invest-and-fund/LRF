using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    //We define an interface of ISqlDataAccess for abstraction and polymorphism
    //If you change the return types here you must extract a new interface
    //Delete ": ISqlDataAccess" and the respective "I" file in solution explorer
    //Right click "SqlDataAccess" then go to "quick actions and refactoring" and extract the interface then press ok.
    //If it worked you should see ": ISqlDataAccess" has returned
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        //Set the name of the connectionstring to the connection string in the appsettings.json file
        public string ConnectionStringName { get; set; } = "Default";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            try
            {
                string connectionString = _config.GetConnectionString(ConnectionStringName);

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    var data = await connection.QueryAsync<T>(sql, parameters);

                    return data.ToList();
                }
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message);
                return null;
            }
        }

        public async Task<int> SaveData<T>(string sql, T parameters)
        {
            try
            {
                string connectionString = _config.GetConnectionString(ConnectionStringName);

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    int count = await connection.ExecuteAsync(sql, parameters);

                    return count;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message);
                return 0;
            }
        }

        public async Task ExecuteProcedure<T>(string sql, T parameters)
        {
            try
            {
                string connectionString = _config.GetConnectionString(ConnectionStringName);

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    var rows = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message);
            }
        }

    }
}

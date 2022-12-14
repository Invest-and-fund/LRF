using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SystemUserData : ISystemUserData
    {
        private readonly ISqlDataAccess _db;

        public SystemUserData(ISqlDataAccess db)
        {
            _db = db;
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
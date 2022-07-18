using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;


namespace ToDoList.Data.Repositories
{
    public class UserRepository : IUserRepository   
    {
        private DBConnection _connectionString;
        public UserRepository(DBConnection connectinString)
        {
            _connectionString = connectinString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> CreateUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO users (username, password, created)
                        VALUES (@username, @password, @created )
                        ";

            var result = await db.ExecuteAsync(sql, new { user.Username, user.Password, user.Created });

            return result > 0;
        }

        public Task<bool> LoguinUser(User user)
        {
            throw new NotImplementedException();
        }


    }
}

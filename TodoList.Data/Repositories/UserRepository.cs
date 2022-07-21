using Dapper;
using Npgsql;
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

        public async Task<bool> RegisterUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO users (username, password, created)
                        VALUES (@username, @password, @created )
                        ";

            var result = await db.ExecuteAsync(sql, new { user.Username, user.Password, user.Created });

            if (true)
                return true;
            { 
            }
        }

        public async Task<bool> LoginUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * FROM users
                        WHERE username = @Username
                          AND password = @Password
                        ";

            var result = await db.QueryFirstOrDefaultAsync(sql, new { user.Username, user.Password });

            if (result != null)
                return true; 
            else
                return false;   
        }
    }
}
